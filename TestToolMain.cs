using Database;
using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace FDPSTestingTool
{
    public partial class TestToolMain : Form
    {
        public TestToolMain()
        {
            InitializeComponent();
            InitializedeviceIdComboBox();
            InitializedatatableComboBox();
            InitializePatientIdComboBox();
            InitializeLocationComboBox();
        }

        private void BrowseDirectories_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            if (folderBrowserDialog1.SelectedPath != "")
            {
                FileInputDir.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void TestToolMain_Load(object sender, EventArgs e)
        {

        }

        private void ProcessFiles_Click(object sender, EventArgs e)
        {
            ProcessSimulatorFiles();
            ClearComboBoxes();
            InitializedeviceIdComboBox();
            InitializeLocationComboBox();
            InitializePatientIdComboBox();
            InitializedatatableComboBox();
        }


        private void truncateDB_Click(object sender, EventArgs e)
        {
            DatabaseFunctions.TruncateFdpsTables();
            DBStatus.Text = @"Database tables successfully truncated";
            DBStatus.Update();
            ClearComboBoxes();
            InitializedatatableComboBox();
        }

        private void createSchema_Click(object sender, EventArgs e)
        {
            DatabaseFunctions.CreateFdpsSchema();
            DBStatus.Text = @"Schema successfully created";
            DBStatus.Update();
            InitializedatatableComboBox();
        }

        private void dropSchema_Click(object sender, EventArgs e)
        {
            DatabaseFunctions.DropFdpsSchema();
            DBStatus.Text = @"Schema successfully dropped";
            DBStatus.Update();
            ClearComboBoxes();
        }


        private void createDB_Click(object sender, EventArgs e)
        {
            var dbExists = DatabaseFunctions.CreateFdpsDb();
            if (dbExists == 1)
            {
                DBStatus.Text = @"FDPS Database already exists";
                DatabaseFunctions.DropFdpsSchema();
            }
            else
            {
                DBStatus.Text = @"FDPS Database successfully created";
            }

            DatabaseFunctions.CreateFdpsSchema();
            DBStatus.Update();
        }

        private void dropDB_Click(object sender, EventArgs e)
        {
            DatabaseFunctions.DropFdpsDb();
            DBStatus.Text = @"FDPS database successfully dropped";
            DBStatus.Update();
        }

        private void ClearComboBoxes()
        {
            ClearDisplayDataCb();
            ClearDeviceIdcb();
            ClearLocationCb();
            ClearPatientIdcb();
        }

        private void ClearDisplayDataCb()
        {
            if (displayDataList.Items.Count > 0)
            {
                displayDataList.Items.Clear();
            }

            displayDataList.ResetText();
        }

        private void ClearDeviceIdcb()
        {
            if (deviceIDList.Items.Count > 0)
            {
                deviceIDList.Items.Clear();
            }

            deviceIDList.ResetText();
        }

        private void ClearLocationCb()
        {
            if (locationCB.Items.Count > 0)
            {
                locationCB.Items.Clear();
            }

            locationCB.ResetText();
        }

        private void ClearPatientIdcb()
        {
            if (patientIDCB.Items.Count > 0)
            {
                patientIDCB.Items.Clear();
            }

            patientIDCB.ResetText();
        }

        void InitializedeviceIdComboBox()
        {
            var connection = DatabaseCommands.ConnectToDb();
            var sqlCmd = "SELECT device_id from device";
            var pcmd = new NpgsqlCommand(sqlCmd, connection);
            var dataAdapter = new NpgsqlDataAdapter
            {
                SelectCommand = pcmd
            };
            var dt = new DataTable();
            dataAdapter.Fill(dt);
            deviceIDList.Items.Add(String.Empty);

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                deviceIDList.Items.Add(dt.Rows[i].ItemArray[0].ToString());
            }

            DatabaseCommands.DisconnectFromDb(connection);
            deviceIDList.SelectedIndex = 0;
        }

        void InitializedatatableComboBox()
        {
            var connection = DatabaseCommands.ConnectToDb();
            var sqlCmd = "SELECT table_name FROM information_schema.tables" +
                         " WHERE table_schema = 'public' AND table_type = 'BASE TABLE'" +
                         " order by table_name; ";
            var pcmd = new NpgsqlCommand(sqlCmd, connection);
            var dataAdapter = new NpgsqlDataAdapter
            {
                SelectCommand = pcmd
            };
            var dt = new DataTable();
            dataAdapter.Fill(dt);
            displayDataList.Items.Add(String.Empty);

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                displayDataList.Items.Add(dt.Rows[i].ItemArray[0].ToString());
            }

            DatabaseCommands.DisconnectFromDb(connection);
            displayDataList.SelectedIndex = 0;
        }

        void InitializeLocationComboBox()
        {
            var connection = DatabaseCommands.ConnectToDb();
            var sqlCmd = "SELECT distinct PoC, room, bed from location";
            var pcmd = new NpgsqlCommand(sqlCmd, connection);
            var dataAdapter = new NpgsqlDataAdapter
            {
                SelectCommand = pcmd
            };
            var dt = new DataTable();
            dataAdapter.Fill(dt);
            locationCB.Items.Add(String.Empty);

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var loc = dt.Rows[i].ItemArray[0] + "_" +
                          dt.Rows[i].ItemArray[1] + "_" +
                          dt.Rows[i].ItemArray[2];
                locationCB.Items.Add(loc);
            }

            DatabaseCommands.DisconnectFromDb(connection);
            locationCB.SelectedIndex = 0;

        }

        void InitializePatientIdComboBox()
        {
            var connection = DatabaseCommands.ConnectToDb();
            var sqlCmd = "SELECT distinct patient_id from patient";
            var pcmd = new NpgsqlCommand(sqlCmd, connection);
            var dataAdapter = new NpgsqlDataAdapter
            {
                SelectCommand = pcmd
            };
            var dt = new DataTable();
            dataAdapter.Fill(dt);
            patientIDCB.Items.Add(String.Empty);

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                patientIDCB.Items.Add(dt.Rows[i].ItemArray[0].ToString());
            }

            DatabaseCommands.DisconnectFromDb(connection);
            patientIDCB.SelectedIndex = 0;
        }

        private void locationCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (locationCB.SelectedIndex != 0)
            {
                ClearDeviceIdcb();
                ClearPatientIdcb();

                string joinTable = "location";
                string whereClause = " where" + ParseLocationString(locationCB.SelectedItem.ToString());

                PopulateDeviceComboBox(joinTable, whereClause);
                PopulatePatientComboBox(joinTable, whereClause);

                if (patientIDCB.Items.Count == 0)
                {
                    patientIDCB.Items.Add(String.Empty);
                }

                if (deviceIDList.Items.Count == 0)
                {
                    deviceIDList.Items.Add(String.Empty);
                }

                patientIDCB.SelectedIndex = 0;
                deviceIDList.SelectedIndex = 0;
            }
        }

        private void patientIDCB_SelectedIndexChanged(object sender, EventArgs e)
         {
            if (patientIDCB.SelectedIndex != 0)
            {
                var whereClause = " where patient_id = '" + patientIDCB.SelectedItem + "'";
                var joinTable = "patient";
                ClearDeviceIdcb();
                ClearLocationCb();

                PopulateLocationComboBox(joinTable, whereClause);
                PopulateDeviceComboBox(joinTable, whereClause);

                if (locationCB.Items.Count == 0)
                {
                    locationCB.Items.Add(String.Empty);
                }

                if (deviceIDList.Items.Count == 0)
                {
                    deviceIDList.Items.Add(String.Empty);
                }

                locationCB.SelectedIndex = 0;
                deviceIDList.SelectedIndex = 0;
            }
        }

        private void deviceIDList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (deviceIDList.Focused)
            {
                if (displayDataList.Items.Count > 0)
                {
                    displayDataList.SelectedIndex = 0;
                    displayListGridView.DataSource = null;
                }

                if (patientIDCB.SelectedIndex != -1 && locationCB.SelectedIndex != -1)
                {
                    var whereClause = "";
                    if (deviceIDList.SelectedItem != null && (string) deviceIDList.SelectedItem != String.Empty)
                    {
                        whereClause = " where device.device_id = '" + deviceIDList.SelectedItem + "'";
                    }
                    var joinTable = "device";

                    ClearPatientIdcb();
                    ClearLocationCb();

                    PopulateLocationComboBox(joinTable, whereClause);
                    PopulatePatientComboBox(joinTable, whereClause);

                    if (locationCB.Items.Count == 0)
                    {
                        locationCB.Items.Add(String.Empty);
                    }

                    if (patientIDCB.Items.Count == 0)
                    {
                        patientIDCB.Items.Add(String.Empty);
                    }


                    locationCB.SelectedIndex = 0;
                    patientIDCB.SelectedIndex = 0;
                }
            }
        }


        private void PopulateLocationComboBox(string joinTable, string whereClause)
        {
            var connection = DatabaseCommands.ConnectToDb();

            var sqlCmd = "SELECT PoC, room, bed from location join " + joinTable + " on " + joinTable +
                         ".device_id = location.device_id" + whereClause;
                
            var pcmd = new NpgsqlCommand(sqlCmd, connection);
            var dataAdapter = new NpgsqlDataAdapter
            {
                SelectCommand = pcmd
            };

            var dt = new DataTable();
            dataAdapter.Fill(dt);

            for (var i = 0; i<dt.Rows.Count; i++)
            {
                var loc = dt.Rows[i].ItemArray[0] + "_" +
                          dt.Rows[i].ItemArray[1] + "_" +
                          dt.Rows[i].ItemArray[2];
                locationCB.Items.Add(loc);
            }

            DatabaseCommands.DisconnectFromDb(connection);
        }

        private void PopulatePatientComboBox(string joinTable, string whereClause)
        {

            var connection = DatabaseCommands.ConnectToDb();


            var sqlCmd = "SELECT patient_id from patient join " + joinTable + " on " + joinTable + ".device_id = patient.device_id" +
                         whereClause;

            var pcmd = new NpgsqlCommand(sqlCmd, connection);
            var dataAdapter = new NpgsqlDataAdapter
            {
                SelectCommand = pcmd
            };

            var dt = new DataTable();
            dataAdapter.Fill(dt);

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                patientIDCB.Items.Add(dt.Rows[i].ItemArray[0].ToString());
            }

            DatabaseCommands.DisconnectFromDb(connection);
        }

        private void PopulateDeviceComboBox(string joinTable, string whereClause)
        {

            var connection = DatabaseCommands.ConnectToDb();
            
            var sqlCmd = "SELECT device.device_id from device join " + joinTable + " on " + joinTable + ".device_id = device.device_id" +
                         whereClause;

            var pcmd = new NpgsqlCommand(sqlCmd, connection);
            var dataAdapter = new NpgsqlDataAdapter
            {
                SelectCommand = pcmd
            };

            var dt = new DataTable();
            dataAdapter.Fill(dt);


            for (var i = 0; i < dt.Rows.Count; i++)
            {
                deviceIDList.Items.Add(dt.Rows[i].ItemArray[0].ToString());
            }
            
            DatabaseCommands.DisconnectFromDb(connection);
        }


        private void dataTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            var whereClause = "";
            var joinClause = "";
            if (deviceIDList.SelectedItem != null && (string)deviceIDList.SelectedItem != "")
            {
                if (displayDataList.SelectedIndex != 0)
                {
                    if (patientIDCB.SelectedIndex != 0)
                    {
                        joinClause = " JOIN patient on patient.device_id = " + displayDataList.SelectedItem +
                                     ".device_id ";
                        whereClause += " AND patient.patient_id = '" + patientIDCB.SelectedItem + "'";
                    }

                    if (locationCB.SelectedIndex != 0)
                    {
                        joinClause = " JOIN location on location.device_id = " + displayDataList.SelectedItem +
                                     ".device_id ";
                        whereClause += " AND " + ParseLocationString(locationCB.SelectedItem.ToString());
                    }


                    if (useDatesCB.Checked)
                    {
                        var utcStartDateTime = CalculateUtcTimeFromDate(startDateTimePicker);
                        var utcEndtDateTime = CalculateUtcTimeFromDate(endDateTimePicker);

                        whereClause = " AND determination_time >= " + utcStartDateTime +
                                      " AND determination_time <=" + utcEndtDateTime;
                    }


                    var connection = DatabaseCommands.ConnectToDb();
                    var sqlCmd = "SELECT * from " + displayDataList.SelectedItem + joinClause + " where " + displayDataList.SelectedItem + ".device_id = '" +
                                 deviceIDList.SelectedItem + "'" + whereClause;
                    var pcmd = new NpgsqlCommand(sqlCmd, connection);
                    var dataAdapter = new NpgsqlDataAdapter { SelectCommand = pcmd };
                    var dt = new DataTable();
                    dataAdapter.Fill(dt);

                    displayListGridView.DataSource = dt;

                    DatabaseCommands.DisconnectFromDb(connection);
                }
            }
        }
    
        double CalculateUtcTimeFromDate(DateTimePicker dateToConvert)
        {
            DateTime calculatedUtcTime = new DateTime(dateToConvert.Value.Year,
                dateToConvert.Value.Month, dateToConvert.Value.Day,
                dateToConvert.Value.Hour, dateToConvert.Value.Minute,
                dateToConvert.Value.Second, dateToConvert.Value.Millisecond);
            double utcTime =
                ((calculatedUtcTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds) * 1000;

            return utcTime;
        }

        private string ParseLocationString(string location)
        {
            string[] loc = location.Split('_');
            var locationwhere = " location.Poc = '" + loc[0] + "' AND location.room = '" + loc[1] + "' AND location.bed = '" + loc[2] + "'";
            return locationwhere;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            ClearComboBoxes();
            InitializedeviceIdComboBox();
            InitializedatatableComboBox();
            InitializePatientIdComboBox();
            InitializeLocationComboBox();

        }
    }   
}