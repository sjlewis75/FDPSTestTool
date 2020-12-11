using Database;
using Npgsql;
using System;
using System.IO;
using System.Xml;

namespace FDPSTestingTool
{
    partial class TestToolMain
    {

        private void ProcessSimulatorFiles()
        {
            string tempXmlString = "";
            int observedValue = 0;
            int operational = 0;
            int metrics = 0;
            int waveforms = 0;
            int alert = 0;
            int component = 0;


            var connection = DatabaseCommands.ConnectToDb();
			NpgsqlCommand cmd = new NpgsqlCommand
			{
				Connection = connection,
				CommandType = System.Data.CommandType.Text
			};


			if (Directory.Exists(FileInputDir.Text))
			{
				string[] fileList = Directory.GetFiles(FileInputDir.Text, "*.xml");
				if (fileList.Length == 0)
				{
					processedFileCount.Text = @"No files to process";
					processedFileCount.Update();
				}
				else
				{
					processFileProgressBar.Value = 0;
					processFileProgressBar.Maximum = 100;
				    int numFilesProcessed = 0;
				    foreach (string fileName in fileList)
				    {
				        var readText = File.ReadAllText(fileName);

				        XmlDocument xmlDoc = new XmlDocument();
				        xmlDoc.LoadXml(readText);

				        XmlNodeList getBodyList;
				        if (readText.Contains("SOAP-ENV:Body"))
				        {
				            getBodyList = xmlDoc.GetElementsByTagName("SOAP-ENV:Body");
				            tempXmlString = getBodyList.Item(0)?.InnerXml;
				        }
				        else if (readText.Contains("s:Body"))
				        {
				            getBodyList = xmlDoc.GetElementsByTagName("s:Body");
				            tempXmlString = getBodyList.Item(0)?.InnerXml;
				        }

				        if (!String.IsNullOrEmpty(tempXmlString))
				        {
				            xmlDoc.LoadXml(tempXmlString);
				        }

				        
				        var msgType = ProcessInputFile(xmlDoc, readText, cmd);
				        switch (msgType)
				        {
				            case "metric":
				                metrics++;
				                break;
				            case "waveform":
				                waveforms++;
				                break;
				            case "operational":
				                operational++;
				                break;
				            case "component":
				                component++;
				                break;
				            case "alert":
				                alert++;
				                break;
				            case "observedValue":
				                observedValue++;
				                break;
				        }
				        numFilesProcessed++;
				        processFileProgressBar.Value = (numFilesProcessed / fileList.Length) * 100;
                    }
                    processedFileCount.Text = String.Format("Files Processed {0} {1}Metrics {2}   Alerts {3}    Operational {4}{5}" +
														"Waveform {6}   Component {7}   Observed Value {8}{9}",
                                                            numFilesProcessed, Environment.NewLine, metrics,alert, 
														    operational, Environment.NewLine,  waveforms, component, observedValue, Environment.NewLine);
				    processedFileCount.Update();
				}
			}
			else
			{
				processedFileCount.Text = @"Input directory not found";
				processedFileCount.Update();
			}
            DatabaseCommands.DisconnectFromDb(connection);
        }



        private string ProcessInputFile(XmlDocument xmlString, string readText, NpgsqlCommand cmd)
        {
			
            string sequenceId = GetSequenceId(readText);
            string mdibVersion = GetMdibVersion(readText);
            string deviceId = GetSequenceId(readText);
            Int64 detTime = GetDeterminationTime(readText);
            string recType = "none";

            if (deviceId != null || sequenceId != null)
            {
                cmd.CommandText = "Select device_id from device where device_id = '" + deviceId + "';";

                int deviceExists = DatabaseCommands.ExecuteRowCountCommand(cmd);
                if (deviceExists == 1)
                {
                    cmd.CommandText =
                        "UPDATE device set current_mdib = " + Int32.Parse(mdibVersion) + " WHERE device_id = '" +
                        sequenceId + "'";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd.CommandText =
                        "INSERT INTO device (starting_mdib, device_id, current_mdib)  SELECT " +
                        Int32.Parse(mdibVersion) + ",'" + sequenceId + "'," + Int32.Parse(mdibVersion);
                    cmd.ExecuteNonQuery();
                }             
            }
            else
            {
                return recType;
            }

            if (readText.Contains(":GetMdibResponse"))
            {
                ProcessMdibLocationInfo(xmlString, deviceId, cmd);
                ProcessMdibPatientInfo(deviceId, cmd, xmlString.DocumentElement?.ChildNodes.Item(0)?.ChildNodes.Item(1)?.ChildNodes);
                ProcessMdibDeviceInfo(xmlString, deviceId, cmd);
                recType = "mdib";
                cmd.CommandText =
                    "INSERT INTO mdib (mdib_version, sequence_id,device_id, mdib_data_xml, determination_time)  SELECT " +
                    Int32.Parse(mdibVersion) + ",'" + sequenceId + "','" + deviceId + "','" + xmlString.InnerXml + "', " + detTime;
               // cmd.ExecuteNonQuery();
            }

            //EpisodicContextReport
            else if (readText.Contains(":EpisodicContextReport") || readText.Contains(":PeriodicContextReport"))
            {
                if (readText.Contains(":PatientContextState"))
                {
                    ProcessMdibPatientInfo(deviceId, cmd, xmlString.DocumentElement?.ChildNodes.Item(0)?.ChildNodes);
                }
            }

            else if ((readText.Contains(":EpisodicAlertReport") ||readText.Contains(":PeriodicAlertReport")) && alertCB.Checked)
            {
                recType = "alert";
                cmd.CommandText =
                    "INSERT INTO alert (mdib_version, sequence_id,device_id, alert_data_xml, determination_time)  SELECT " +
                    Int32.Parse(mdibVersion) + ",'" + sequenceId + "','" + deviceId + "','" + xmlString.InnerXml + "', " + detTime;
                cmd.ExecuteNonQuery();
			}

            else if ((readText.Contains(":EpisodicComponentReport") || readText.Contains(":PeriodicComponentReport")) && componentCB.Checked)
            {
                recType = "component";
                cmd.CommandText =
                    "INSERT INTO component (mdib_version,sequence_id, device_id, component_data_xml, determination_time)  SELECT " +
                    Int32.Parse(mdibVersion) + ",'" + sequenceId + "','" + deviceId + "','" + xmlString.InnerXml + "', " + detTime;
                cmd.ExecuteNonQuery();
            }

            else if ((readText.Contains(":EpisodicMetricReport") || readText.Contains(":PeriodicMetricReport")) && metricsCB.Checked)
            {
                recType = "metric";
                cmd.CommandText =
                    "INSERT INTO metric (mdib_version,sequence_id, device_id, metric_data_xml, determination_time)  SELECT " +
                    Int32.Parse(mdibVersion) + ",'" + sequenceId + "','" + deviceId + "','" + xmlString.InnerXml + "', " + detTime;
                cmd.ExecuteNonQuery();
            }

            else if (readText.Contains(":EpisodicOperationalStateReport") && operationalCB.Checked)
            {
                recType = "operational";
                cmd.CommandText =
                    "INSERT INTO operational (mdib_version,sequence_id, device_id, operational_data_xml, determination_time)  SELECT " +
                    Int32.Parse(mdibVersion) + ",'" + sequenceId + "','" + deviceId + "','" + xmlString.InnerXml + "', " + detTime;
                cmd.ExecuteNonQuery();
            }

            else if (readText.Contains(":WaveformStream") && waveformCB.Checked)
            {
                recType = "waveform";
                cmd.CommandText =
                    "INSERT INTO waveform (mdib_version, sequence_id,device_id, waveform_data_xml, determination_time)  SELECT " +
                    Int32.Parse(mdibVersion) + ",'" + sequenceId + "','" + deviceId + "','" + xmlString.InnerXml + "', " + detTime;
                cmd.ExecuteNonQuery();
            }

            else if (readText.Contains(":ObservedValueStream") && observedValueCB.Checked)
            {
                recType = "observedValue";
                cmd.CommandText =
                    "INSERT INTO observedValue (mdib_version, sequence_id,device_id, observedValue_data_xml,determination_time)  SELECT " +
                    Int32.Parse(mdibVersion) + ",'" + sequenceId + "','" + deviceId + "','" + xmlString.InnerXml + "', " + detTime;
                cmd.ExecuteNonQuery();
            }

            return recType;
        }

        private static string GetSequenceId(string readText)
        {
            string seqId = null;
            if (readText.Contains("SequenceId"))
            {
                seqId = readText.Substring(readText.IndexOf("SequenceId", StringComparison.Ordinal) + 21, 36);
            }

            return seqId;
        }

        private static  string GetMdibVersion(string readText)
        {
            string mdib = null;
            if (readText.Contains("MdibVersion"))
            {
                mdib = readText.Substring(readText.IndexOf("MdibVersion=", StringComparison.Ordinal) + 13);
                int mdibEnd = mdib.IndexOf("\"", StringComparison.Ordinal);
                mdib = mdib.Substring(0, mdibEnd);
            }

            return mdib;
        }

        private static Int64 GetDeterminationTime(string readText)
        {
            string detTime = "0";
            if (readText.Contains("DeterminationTime"))
            {
                if (readText.Length > readText.IndexOf("DeterminationTime", StringComparison.Ordinal) + 19)
                    detTime = readText.Substring(readText.IndexOf("DeterminationTime", StringComparison.Ordinal) + 19, 13);
            }

            return Int64.Parse(detTime);
        }

        private static void ProcessMdibLocationInfo(XmlDocument xmlDoc, string deviceId, NpgsqlCommand cmd)
        {
            XmlNodeList list = xmlDoc.DocumentElement?.ChildNodes.Item(0)?.ChildNodes.Item(1)?.ChildNodes;
            
            if (list != null)
            {
                foreach (XmlNode currentNode in list)
                {
                    if (currentNode.HasChildNodes && currentNode.LastChild.Name == "pm:LocationDetail" && currentNode.LastChild.Attributes.Count > 0)
                    {
                        XmlAttributeCollection location = currentNode.LastChild.Attributes;

                        string whereClause = "";
                        var fieldList = "device_id ";
                        var selectList = " ) Select '" + deviceId + "'";
                                       

                        if (location.GetNamedItem("PoC") != null)
                        {
                            whereClause += " and poc = '" + location.GetNamedItem("PoC").Value + "'";
                            fieldList += ", poc ";
                            selectList += ",'" + location.GetNamedItem("PoC").Value + "'";
                        }
                        
                        if (location.GetNamedItem("Room") != null)
                        {
                            whereClause += " and room = '" + location.GetNamedItem("Room").Value + "'";
                            fieldList += ", room ";
                            selectList += ",'" + location.GetNamedItem("Room").Value + "'";
                        }

                        if (location.GetNamedItem("Bed") != null)
                        {
                            whereClause += " and bed = '" + location.GetNamedItem("Bed").Value + "'";
                            fieldList += ", bed ";
                            selectList += ",'" + location.GetNamedItem("Bed").Value + "'";
                        }
                        
                        if (location.GetNamedItem("Facility") != null)
                        {
                            whereClause += " and facility = '" + location.GetNamedItem("Facility").Value + "'";
                            fieldList += ", facility ";
                            selectList += ",'" + location.GetNamedItem("Facility").Value + "'";
                        }
                        
                        if (location.GetNamedItem("Building") != null)
                        {
                            whereClause += " and building = '" + location.GetNamedItem("Building").Value + "'";
                            fieldList += ", building ";
                            selectList += ",'" + location.GetNamedItem("Building").Value + "'";
                        }
                        
                        if (location.GetNamedItem("Floor") != null)
                        {
                            whereClause += " and floor = '" + location.GetNamedItem("Floor").Value + "'";
                            fieldList += ", floor ";
                            selectList += ",'" + location.GetNamedItem("Floor").Value + "'";
                        }

                        cmd.CommandText = "Select device_id from location where device_id = '" + deviceId + "'" + whereClause;
                                        
                        if (DatabaseCommands.ExecuteRowCountCommand(cmd) != 1)
                        {
                            cmd.CommandText = "INSERT INTO location (" +  fieldList + selectList;                            
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private static void ProcessMdibPatientInfo(string deviceId, NpgsqlCommand cmd, XmlNodeList list)
        {
            var fieldList = "";
            var selectList = "";
            var updateList = "set ";
            var whereClause = "";

            if (list != null) //List of state nodes
            {
                foreach (XmlNode currentNode in list) //State node
                {
                    if (currentNode.HasChildNodes)
                    {
                        XmlNodeList children = currentNode.ChildNodes;
                        
                        if (((currentNode.Attributes != null) &&
                            (currentNode.Attributes[0].Value == "pm:PatientContextState")) ||
                        (currentNode.Name == "msg:ContextState" && currentNode.Attributes != null))
                        {
                            string attributeUpdateList = "set ";
                            string attibuteFieldList = "";
                            string attibuteSelectList = "";
                            string attributewhereClause = "";
                           
                            for (int i = 0; i < currentNode.Attributes.Count; i++)
                            {
                                if (!currentNode.Attributes[i].Name.Contains("xsi") &&
                                    currentNode.Attributes[i].Name != "Handle")
                                {
                                    if (attibuteFieldList.Length != 0)
                                    {
                                        attibuteFieldList += ",";
                                    }
                                    if (attibuteSelectList.Length != 0)
                                    {
                                        attibuteSelectList += ",";
                                    }
                                    if (attributeUpdateList.Length != 4)
                                    {
                                        attributeUpdateList += ",";
                                    }

                                    if (attributewhereClause.Length != 0)
                                    {
                                        attributewhereClause += " AND ";
                                    }

                                    attibuteFieldList += currentNode.Attributes[i].Name;
                                    attibuteSelectList += "'" + currentNode.Attributes[i].Value + "'";
                                    attributeUpdateList +=
                                        currentNode.Attributes[i].Name + "='" + currentNode.Attributes[i].Value + "'";
                                    attributewhereClause += currentNode.Attributes[i].Name + "='" +
                                                   currentNode.Attributes[i].Value + "'";
                                }

                            }

                            cmd.CommandText = "Select device_id from patient_context where " + attributewhereClause;
                            if (DatabaseCommands.ExecuteRowCountCommand(cmd) != 1)
                            {
                                cmd.CommandText = "INSERT INTO patient_context ( device_id, " + attibuteFieldList + ") select '" + deviceId + "', " + attibuteSelectList;
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                cmd.CommandText = "Update patient_context " + attributeUpdateList +" where device_id = '" + deviceId + "'";
                                cmd.ExecuteNonQuery();
                            }

                            foreach (XmlNode childNode in children) //Identification & CoreData nodes
                            {
                                //Identification Node - set or update patientId table
                                if (childNode.Name == "pm:Identification")
                                {
                                    fieldList = "device_id ";
                                    selectList = "'" +  deviceId + "' ";
                                    updateList = "set ";
                                    whereClause = "where device_id = '" + deviceId + "'";

                                    for (int x = 0; x < childNode.Attributes.Count; x++)
                                    {
                                        fieldList += "," + childNode.Attributes[x].Name;
                                        selectList += ",'" + childNode.Attributes[x].Value + "'";
                                        if (updateList.Length != 4)
                                        {
                                            updateList += ",";
                                        }

                                        updateList += childNode.Attributes[x].Name + "='" +
                                                      childNode.Attributes[x].Value + "'";
                                        whereClause += " AND " + childNode.Attributes[x].Name + "='" +
                                                       childNode.Attributes[x].Value + "'";
                                    }

                                    cmd.CommandText = "Select device_id from patient " + whereClause;
                                    if (DatabaseCommands.ExecuteRowCountCommand(cmd) != 1)
                                    {
                                      cmd.CommandText = "INSERT INTO patient (" + fieldList + ") select " + selectList;
                                      cmd.ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        cmd.CommandText = "Update patient " + updateList + " where device_id = '" + deviceId + "'";
                                        cmd.ExecuteNonQuery();
                                    }
                                }


                                //no attributes, only child nodes
                                //Core Data Node - set fields to insert or update patientDemog table
                                //Identification Node - set or update patientId table
                                if (childNode.Name == "pm:CoreData")
                                {
                                    fieldList = "device_id ";
                                    selectList = "'" + deviceId +"'";
                                    updateList = "set ";
                                    whereClause = "where device_id = '" + deviceId + "'";

                                    for (int x = 0; x < childNode.ChildNodes.Count; x++)
                                    {
                                        fieldList += "," + childNode.ChildNodes[x].Name.Substring(3);
                                        if (updateList.Length != 4)
                                        {
                                            updateList += ",";
                                        }

                                        if (childNode.ChildNodes[x].Attributes.Count > 0)
                                        {
                                            selectList += ",'" + childNode.ChildNodes[x].Attributes[0].Value + "'";
                                            updateList +=
                                                childNode.ChildNodes[x].Name.Substring(3) + "='" +
                                                childNode.ChildNodes[x].Attributes[0].Value + "'";
                                            whereClause +=
                                                " AND " + childNode.ChildNodes[x].Name.Substring(3) + "='" +
                                                childNode.ChildNodes[x].Attributes[0].Value + "'";
                                        }
                                        else
                                        {
                                            selectList += ",'" + childNode.ChildNodes[x].ChildNodes[0].Value + "'";
                                            updateList +=
                                                childNode.ChildNodes[x].Name.Substring(3) + "='" +
                                                childNode.ChildNodes[x].ChildNodes[0].Value + "'";
                                            whereClause +=
                                                " AND " + childNode.ChildNodes[x].Name.Substring(3) + "='" +
                                                childNode.ChildNodes[x].ChildNodes[0].Value + "'";
                                        }
                                    }

                                    cmd.CommandText = "Select device_id from patient_demog " + whereClause;

                                    if (DatabaseCommands.ExecuteRowCountCommand(cmd) != 1)
                                    {
                                        cmd.CommandText = "INSERT INTO patient_demog (" + fieldList + ") select " + selectList;
                                        cmd.ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        cmd.CommandText = "Update patient_demog " + updateList + " where device_id = '" + deviceId + "'";
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void ProcessMdibDeviceInfo(XmlDocument xmlDoc, string deviceId, NpgsqlCommand cmd)
        {          
            XmlNodeList list = xmlDoc.DocumentElement?.ChildNodes.Item(0)?.ChildNodes.Item(0)?.ChildNodes.Item(0)?.ChildNodes;

            if (list != null)
            {
                foreach (XmlNode currentNode in list)
                {
                    if (currentNode.HasChildNodes)
                    {
                        XmlNodeList children = currentNode.ChildNodes;
                        if (currentNode.Name == "pm:MetaData" && currentNode.Attributes != null)
                        {
                            cmd.CommandText = "Select device_id from device where device_id = '" + deviceId + "';";
                            int deviceExists = DatabaseCommands.ExecuteRowCountCommand(cmd);
                            
                            cmd.CommandText = "INSERT INTO device (";
                            var fieldList = "device_id, starting_mdib, current_mdib";
                            var selectList = " ) Select '" + deviceId + "', 1,1";
                            var setList = "Update device set ";

                            foreach (XmlNode childNode in children)
                            {
                                if (childNode.Name == "pm:SerialNumber")
                                {
                                    
                                    fieldList += ", serial_number ";
                                    selectList += ",'" + childNode.ChildNodes.Item(0)?.Value + "'";
                                    if (setList.Length != 18)
                                    {
                                        setList += ",";
                                    }
                                    setList += " serial_number = '" + childNode.ChildNodes.Item(0)?.Value + "'";
                                }
                                if (childNode.Name == "pm:ModelName")
                                {
                                    fieldList += ", model ";
                                    selectList += ",'" + childNode.ChildNodes.Item(0)?.Value + "'";
                                    if (setList.Length != 18)
                                    {
                                        setList += ",";
                                    }
                                    setList += " model = '" + childNode.ChildNodes.Item(0)?.Value + "'";
                                }
                                if (childNode.Name == "pm:Manufacturer")
                                {
                                    fieldList += ", manufacturer ";
                                    selectList += ",'" + childNode.ChildNodes.Item(0)?.Value + "'";
                                    if (setList.Length != 18)
                                    {
                                        setList += ",";
                                    }
                                    setList += " manufacturer = '" + childNode.ChildNodes.Item(0)?.Value + "'";
                                }                     
                            }

                          
                            if (deviceExists !=1)
                            {
                                cmd.CommandText += fieldList + selectList;
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                cmd.CommandText = setList + "where device_id = '" + deviceId + "'";
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }
    }
}
