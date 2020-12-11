using System.Windows.Forms;

namespace FDPSTestingTool
{
    public partial class TestToolMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.FileInputDir = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.BrowseDirectories = new System.Windows.Forms.Button();
            this.inputDirLabel = new System.Windows.Forms.Label();
            this.ProcessFiles = new System.Windows.Forms.Button();
            this.processFileProgressBar = new System.Windows.Forms.ProgressBar();
            this.processedFileCount = new System.Windows.Forms.Label();
            this.truncateDB = new System.Windows.Forms.Button();
            this.simulatorProcessingGroup = new System.Windows.Forms.GroupBox();
            this.observedValueCB = new System.Windows.Forms.CheckBox();
            this.operationalCB = new System.Windows.Forms.CheckBox();
            this.componentCB = new System.Windows.Forms.CheckBox();
            this.alertCB = new System.Windows.Forms.CheckBox();
            this.waveformCB = new System.Windows.Forms.CheckBox();
            this.metricsCB = new System.Windows.Forms.CheckBox();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.databaseProcessingGroupBox = new System.Windows.Forms.GroupBox();
            this.dropDB = new System.Windows.Forms.Button();
            this.createDB = new System.Windows.Forms.Button();
            this.DBStatus = new System.Windows.Forms.Label();
            this.dropSchema = new System.Windows.Forms.Button();
            this.createSchema = new System.Windows.Forms.Button();
            this.truncateDBTables = new System.Windows.Forms.ToolTip(this.components);
            this.createDBSchema = new System.Windows.Forms.ToolTip(this.components);
            this.dropDBSchema = new System.Windows.Forms.ToolTip(this.components);
            this.processSimFiles = new System.Windows.Forms.ToolTip(this.components);
            this.dataView = new System.Windows.Forms.GroupBox();
            this.patientIDLabel = new System.Windows.Forms.Label();
            this.locationLabel = new System.Windows.Forms.Label();
            this.patientIDCB = new System.Windows.Forms.ComboBox();
            this.locationCB = new System.Windows.Forms.ComboBox();
            this.useDatesCB = new System.Windows.Forms.CheckBox();
            this.endDate = new System.Windows.Forms.Label();
            this.startDate = new System.Windows.Forms.Label();
            this.endDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.startDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.displayListGridView = new System.Windows.Forms.DataGridView();
            this.displayDataList = new System.Windows.Forms.ComboBox();
            this.displayData = new System.Windows.Forms.Label();
            this.deviceIDList = new System.Windows.Forms.ComboBox();
            this.deviceID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.resetButton = new System.Windows.Forms.Button();
            this.simulatorProcessingGroup.SuspendLayout();
            this.databaseProcessingGroupBox.SuspendLayout();
            this.dataView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.displayListGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // FileInputDir
            // 
            this.FileInputDir.Location = new System.Drawing.Point(96, 24);
            this.FileInputDir.Name = "FileInputDir";
            this.FileInputDir.Size = new System.Drawing.Size(149, 20);
            this.FileInputDir.TabIndex = 5;
            this.FileInputDir.Text = "C:\\";
            // 
            // BrowseDirectories
            // 
            this.BrowseDirectories.Location = new System.Drawing.Point(255, 22);
            this.BrowseDirectories.Name = "BrowseDirectories";
            this.BrowseDirectories.Size = new System.Drawing.Size(21, 23);
            this.BrowseDirectories.TabIndex = 6;
            this.BrowseDirectories.Text = "...";
            this.BrowseDirectories.UseVisualStyleBackColor = true;
            this.BrowseDirectories.Click += new System.EventHandler(this.BrowseDirectories_Click);
            // 
            // inputDirLabel
            // 
            this.inputDirLabel.AutoSize = true;
            this.inputDirLabel.Location = new System.Drawing.Point(6, 28);
            this.inputDirLabel.Name = "inputDirLabel";
            this.inputDirLabel.Size = new System.Drawing.Size(76, 13);
            this.inputDirLabel.TabIndex = 15;
            this.inputDirLabel.Text = "Input Directory";
            // 
            // ProcessFiles
            // 
            this.ProcessFiles.Location = new System.Drawing.Point(109, 103);
            this.ProcessFiles.Name = "ProcessFiles";
            this.ProcessFiles.Size = new System.Drawing.Size(75, 23);
            this.ProcessFiles.TabIndex = 14;
            this.ProcessFiles.Text = "Process";
            this.processSimFiles.SetToolTip(this.ProcessFiles, "Process Device Simulator Files");
            this.ProcessFiles.UseVisualStyleBackColor = true;
            this.ProcessFiles.Click += new System.EventHandler(this.ProcessFiles_Click);
            // 
            // processFileProgressBar
            // 
            this.processFileProgressBar.Location = new System.Drawing.Point(16, 181);
            this.processFileProgressBar.Name = "processFileProgressBar";
            this.processFileProgressBar.Size = new System.Drawing.Size(282, 23);
            this.processFileProgressBar.TabIndex = 16;
            // 
            // processedFileCount
            // 
            this.processedFileCount.AutoSize = true;
            this.processedFileCount.Location = new System.Drawing.Point(11, 135);
            this.processedFileCount.Name = "processedFileCount";
            this.processedFileCount.Size = new System.Drawing.Size(0, 13);
            this.processedFileCount.TabIndex = 17;
            // 
            // truncateDB
            // 
            this.truncateDB.Location = new System.Drawing.Point(146, 94);
            this.truncateDB.Name = "truncateDB";
            this.truncateDB.Size = new System.Drawing.Size(106, 24);
            this.truncateDB.TabIndex = 4;
            this.truncateDB.Text = "Truncate Tables";
            this.truncateDBTables.SetToolTip(this.truncateDB, "Truncate Database Tables");
            this.truncateDB.UseVisualStyleBackColor = true;
            this.truncateDB.Click += new System.EventHandler(this.truncateDB_Click);
            // 
            // simulatorProcessingGroup
            // 
            this.simulatorProcessingGroup.Controls.Add(this.observedValueCB);
            this.simulatorProcessingGroup.Controls.Add(this.operationalCB);
            this.simulatorProcessingGroup.Controls.Add(this.componentCB);
            this.simulatorProcessingGroup.Controls.Add(this.alertCB);
            this.simulatorProcessingGroup.Controls.Add(this.waveformCB);
            this.simulatorProcessingGroup.Controls.Add(this.metricsCB);
            this.simulatorProcessingGroup.Controls.Add(this.BrowseDirectories);
            this.simulatorProcessingGroup.Controls.Add(this.FileInputDir);
            this.simulatorProcessingGroup.Controls.Add(this.processFileProgressBar);
            this.simulatorProcessingGroup.Controls.Add(this.processedFileCount);
            this.simulatorProcessingGroup.Controls.Add(this.inputDirLabel);
            this.simulatorProcessingGroup.Controls.Add(this.ProcessFiles);
            this.simulatorProcessingGroup.Location = new System.Drawing.Point(344, 29);
            this.simulatorProcessingGroup.Name = "simulatorProcessingGroup";
            this.simulatorProcessingGroup.Size = new System.Drawing.Size(310, 212);
            this.simulatorProcessingGroup.TabIndex = 16;
            this.simulatorProcessingGroup.TabStop = false;
            this.simulatorProcessingGroup.Text = "Simulator Processing";
            // 
            // observedValueCB
            // 
            this.observedValueCB.AutoSize = true;
            this.observedValueCB.Checked = true;
            this.observedValueCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.observedValueCB.Location = new System.Drawing.Point(204, 78);
            this.observedValueCB.Name = "observedValueCB";
            this.observedValueCB.Size = new System.Drawing.Size(102, 17);
            this.observedValueCB.TabIndex = 12;
            this.observedValueCB.Text = "Observed Value";
            this.observedValueCB.UseVisualStyleBackColor = true;
            // 
            // operationalCB
            // 
            this.operationalCB.AutoSize = true;
            this.operationalCB.Checked = true;
            this.operationalCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.operationalCB.Location = new System.Drawing.Point(204, 54);
            this.operationalCB.Name = "operationalCB";
            this.operationalCB.Size = new System.Drawing.Size(80, 17);
            this.operationalCB.TabIndex = 9;
            this.operationalCB.Text = "Operational";
            this.operationalCB.UseVisualStyleBackColor = true;
            // 
            // componentCB
            // 
            this.componentCB.AutoSize = true;
            this.componentCB.Checked = true;
            this.componentCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.componentCB.Location = new System.Drawing.Point(115, 78);
            this.componentCB.Name = "componentCB";
            this.componentCB.Size = new System.Drawing.Size(80, 17);
            this.componentCB.TabIndex = 11;
            this.componentCB.Text = "Component";
            this.componentCB.UseVisualStyleBackColor = true;
            // 
            // alertCB
            // 
            this.alertCB.AutoSize = true;
            this.alertCB.Checked = true;
            this.alertCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.alertCB.Location = new System.Drawing.Point(115, 54);
            this.alertCB.Name = "alertCB";
            this.alertCB.Size = new System.Drawing.Size(52, 17);
            this.alertCB.TabIndex = 8;
            this.alertCB.Text = "Alerts";
            this.alertCB.UseVisualStyleBackColor = true;
            // 
            // waveformCB
            // 
            this.waveformCB.AutoSize = true;
            this.waveformCB.Checked = true;
            this.waveformCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.waveformCB.Location = new System.Drawing.Point(14, 78);
            this.waveformCB.Name = "waveformCB";
            this.waveformCB.Size = new System.Drawing.Size(80, 17);
            this.waveformCB.TabIndex = 10;
            this.waveformCB.Text = "Waveforms";
            this.waveformCB.UseVisualStyleBackColor = true;
            // 
            // metricsCB
            // 
            this.metricsCB.AutoSize = true;
            this.metricsCB.Checked = true;
            this.metricsCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.metricsCB.Location = new System.Drawing.Point(14, 54);
            this.metricsCB.Name = "metricsCB";
            this.metricsCB.Size = new System.Drawing.Size(60, 17);
            this.metricsCB.TabIndex = 7;
            this.metricsCB.Text = "Metrics";
            this.metricsCB.UseVisualStyleBackColor = true;
            // 
            // databaseProcessingGroupBox
            // 
            this.databaseProcessingGroupBox.Controls.Add(this.dropDB);
            this.databaseProcessingGroupBox.Controls.Add(this.createDB);
            this.databaseProcessingGroupBox.Controls.Add(this.DBStatus);
            this.databaseProcessingGroupBox.Controls.Add(this.dropSchema);
            this.databaseProcessingGroupBox.Controls.Add(this.createSchema);
            this.databaseProcessingGroupBox.Controls.Add(this.truncateDB);
            this.databaseProcessingGroupBox.Location = new System.Drawing.Point(16, 29);
            this.databaseProcessingGroupBox.Name = "databaseProcessingGroupBox";
            this.databaseProcessingGroupBox.Size = new System.Drawing.Size(322, 212);
            this.databaseProcessingGroupBox.TabIndex = 17;
            this.databaseProcessingGroupBox.TabStop = false;
            this.databaseProcessingGroupBox.Text = "Database Processing";
            // 
            // dropDB
            // 
            this.dropDB.Location = new System.Drawing.Point(16, 28);
            this.dropDB.Name = "dropDB";
            this.dropDB.Size = new System.Drawing.Size(106, 24);
            this.dropDB.TabIndex = 0;
            this.dropDB.Text = "Drop Database";
            this.dropDB.UseVisualStyleBackColor = true;
            this.dropDB.Click += new System.EventHandler(this.dropDB_Click);
            // 
            // createDB
            // 
            this.createDB.Location = new System.Drawing.Point(16, 61);
            this.createDB.Name = "createDB";
            this.createDB.Size = new System.Drawing.Size(106, 24);
            this.createDB.TabIndex = 1;
            this.createDB.Text = "Create Database";
            this.createDB.UseVisualStyleBackColor = true;
            this.createDB.Click += new System.EventHandler(this.createDB_Click);
            // 
            // DBStatus
            // 
            this.DBStatus.AutoSize = true;
            this.DBStatus.Location = new System.Drawing.Point(10, 147);
            this.DBStatus.Name = "DBStatus";
            this.DBStatus.Size = new System.Drawing.Size(0, 13);
            this.DBStatus.TabIndex = 18;
            // 
            // dropSchema
            // 
            this.dropSchema.Location = new System.Drawing.Point(146, 28);
            this.dropSchema.Name = "dropSchema";
            this.dropSchema.Size = new System.Drawing.Size(106, 24);
            this.dropSchema.TabIndex = 2;
            this.dropSchema.Text = "Drop Schema";
            this.dropDBSchema.SetToolTip(this.dropSchema, "Drop Database Schema");
            this.dropSchema.UseVisualStyleBackColor = true;
            this.dropSchema.Click += new System.EventHandler(this.dropSchema_Click);
            // 
            // createSchema
            // 
            this.createSchema.Location = new System.Drawing.Point(146, 61);
            this.createSchema.Name = "createSchema";
            this.createSchema.Size = new System.Drawing.Size(106, 24);
            this.createSchema.TabIndex = 3;
            this.createSchema.Text = "Create Schema";
            this.createDBSchema.SetToolTip(this.createSchema, "Create Database Schema");
            this.createSchema.UseVisualStyleBackColor = true;
            this.createSchema.Click += new System.EventHandler(this.createSchema_Click);
            // 
            // dataView
            // 
            this.dataView.Controls.Add(this.resetButton);
            this.dataView.Controls.Add(this.patientIDLabel);
            this.dataView.Controls.Add(this.locationLabel);
            this.dataView.Controls.Add(this.patientIDCB);
            this.dataView.Controls.Add(this.locationCB);
            this.dataView.Controls.Add(this.useDatesCB);
            this.dataView.Controls.Add(this.endDate);
            this.dataView.Controls.Add(this.startDate);
            this.dataView.Controls.Add(this.endDateTimePicker);
            this.dataView.Controls.Add(this.startDateTimePicker);
            this.dataView.Controls.Add(this.displayListGridView);
            this.dataView.Controls.Add(this.displayDataList);
            this.dataView.Controls.Add(this.displayData);
            this.dataView.Controls.Add(this.deviceIDList);
            this.dataView.Controls.Add(this.deviceID);
            this.dataView.Location = new System.Drawing.Point(16, 248);
            this.dataView.Name = "dataView";
            this.dataView.Size = new System.Drawing.Size(640, 391);
            this.dataView.TabIndex = 18;
            this.dataView.TabStop = false;
            this.dataView.Text = "Data View";
            // 
            // patientIDLabel
            // 
            this.patientIDLabel.AutoSize = true;
            this.patientIDLabel.Location = new System.Drawing.Point(350, 53);
            this.patientIDLabel.Name = "patientIDLabel";
            this.patientIDLabel.Size = new System.Drawing.Size(54, 13);
            this.patientIDLabel.TabIndex = 13;
            this.patientIDLabel.Text = "Patient ID";
            // 
            // locationLabel
            // 
            this.locationLabel.AutoSize = true;
            this.locationLabel.Location = new System.Drawing.Point(12, 52);
            this.locationLabel.Name = "locationLabel";
            this.locationLabel.Size = new System.Drawing.Size(48, 13);
            this.locationLabel.TabIndex = 12;
            this.locationLabel.Text = "Location";
            // 
            // patientIDCB
            // 
            this.patientIDCB.FormattingEnabled = true;
            this.patientIDCB.Location = new System.Drawing.Point(421, 48);
            this.patientIDCB.Name = "patientIDCB";
            this.patientIDCB.Size = new System.Drawing.Size(212, 21);
            this.patientIDCB.TabIndex = 11;
            this.patientIDCB.SelectedIndexChanged += new System.EventHandler(this.patientIDCB_SelectedIndexChanged);
            // 
            // locationCB
            // 
            this.locationCB.FormattingEnabled = true;
            this.locationCB.Location = new System.Drawing.Point(68, 48);
            this.locationCB.Name = "locationCB";
            this.locationCB.Size = new System.Drawing.Size(263, 21);
            this.locationCB.TabIndex = 10;
            this.locationCB.SelectedIndexChanged += new System.EventHandler(this.locationCB_SelectedIndexChanged);
            // 
            // useDatesCB
            // 
            this.useDatesCB.AutoSize = true;
            this.useDatesCB.Location = new System.Drawing.Point(555, 79);
            this.useDatesCB.Name = "useDatesCB";
            this.useDatesCB.Size = new System.Drawing.Size(76, 17);
            this.useDatesCB.TabIndex = 9;
            this.useDatesCB.Text = "Use Dates";
            this.useDatesCB.UseVisualStyleBackColor = true;
            // 
            // endDate
            // 
            this.endDate.AutoSize = true;
            this.endDate.Location = new System.Drawing.Point(292, 80);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(52, 13);
            this.endDate.TabIndex = 8;
            this.endDate.Text = "End Date";
            // 
            // startDate
            // 
            this.startDate.AutoSize = true;
            this.startDate.Location = new System.Drawing.Point(12, 80);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(55, 13);
            this.startDate.TabIndex = 7;
            this.startDate.Text = "Start Date";
            // 
            // endDateTimePicker
            // 
            this.endDateTimePicker.CustomFormat = "MM/dd/yyyy hh:mm:ss";
            this.endDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDateTimePicker.Location = new System.Drawing.Point(348, 77);
            this.endDateTimePicker.Name = "endDateTimePicker";
            this.endDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.endDateTimePicker.TabIndex = 6;
            // 
            // startDateTimePicker
            // 
            this.startDateTimePicker.CustomFormat = "MM/dd/yyyy hh:mm:ss";
            this.startDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDateTimePicker.Location = new System.Drawing.Point(68, 76);
            this.startDateTimePicker.Name = "startDateTimePicker";
            this.startDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.startDateTimePicker.TabIndex = 5;
            // 
            // displayListGridView
            // 
            this.displayListGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.displayListGridView.Location = new System.Drawing.Point(16, 104);
            this.displayListGridView.Name = "displayListGridView";
            this.displayListGridView.Size = new System.Drawing.Size(608, 245);
            this.displayListGridView.TabIndex = 4;
            // 
            // displayDataList
            // 
            this.displayDataList.FormattingEnabled = true;
            this.displayDataList.Location = new System.Drawing.Point(421, 17);
            this.displayDataList.Name = "displayDataList";
            this.displayDataList.Size = new System.Drawing.Size(213, 21);
            this.displayDataList.TabIndex = 3;
            this.displayDataList.SelectedIndexChanged += new System.EventHandler(this.dataTables_SelectedIndexChanged);
            // 
            // displayData
            // 
            this.displayData.AutoSize = true;
            this.displayData.Location = new System.Drawing.Point(347, 20);
            this.displayData.Name = "displayData";
            this.displayData.Size = new System.Drawing.Size(67, 13);
            this.displayData.TabIndex = 2;
            this.displayData.Text = "Display Data";
            // 
            // deviceIDList
            // 
            this.deviceIDList.FormattingEnabled = true;
            this.deviceIDList.Location = new System.Drawing.Point(68, 17);
            this.deviceIDList.Name = "deviceIDList";
            this.deviceIDList.Size = new System.Drawing.Size(264, 21);
            this.deviceIDList.TabIndex = 1;
            this.deviceIDList.SelectedIndexChanged += new System.EventHandler(this.deviceIDList_SelectedIndexChanged);
            // 
            // deviceID
            // 
            this.deviceID.AutoSize = true;
            this.deviceID.Location = new System.Drawing.Point(9, 20);
            this.deviceID.Name = "deviceID";
            this.deviceID.Size = new System.Drawing.Size(55, 13);
            this.deviceID.TabIndex = 0;
            this.deviceID.Text = "Device ID";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(17, 356);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 14;
            this.resetButton.Text = "Reset Values";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // TestToolMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 647);
            this.Controls.Add(this.dataView);
            this.Controls.Add(this.databaseProcessingGroupBox);
            this.Controls.Add(this.simulatorProcessingGroup);
            this.Name = "TestToolMain";
            this.Text = "FDPSTestingTool";
            this.Load += new System.EventHandler(this.TestToolMain_Load);
            this.simulatorProcessingGroup.ResumeLayout(false);
            this.simulatorProcessingGroup.PerformLayout();
            this.databaseProcessingGroupBox.ResumeLayout(false);
            this.databaseProcessingGroupBox.PerformLayout();
            this.dataView.ResumeLayout(false);
            this.dataView.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.displayListGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox FileInputDir;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button BrowseDirectories;
        private System.Windows.Forms.Label inputDirLabel;
        private System.Windows.Forms.Button ProcessFiles;
        private System.Windows.Forms.ProgressBar processFileProgressBar;
        private System.Windows.Forms.Label processedFileCount;
        private System.Windows.Forms.Button truncateDB;
        private System.Windows.Forms.GroupBox simulatorProcessingGroup;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.GroupBox databaseProcessingGroupBox;
        private System.Windows.Forms.Button createSchema;
        private System.Windows.Forms.Button dropSchema;
        private System.Windows.Forms.Label DBStatus;
        private System.Windows.Forms.ToolTip truncateDBTables;
        private ToolTip createDBSchema;
        private ToolTip dropDBSchema;
        private ToolTip processSimFiles;
        public CheckBox componentCB;
        public CheckBox alertCB;
        public CheckBox waveformCB;
        public CheckBox metricsCB;
        public CheckBox observedValueCB;
        public CheckBox operationalCB;
        private Button dropDB;
        private Button createDB;
        private GroupBox dataView;
        private ComboBox deviceIDList;
        private Label deviceID;
        private Label label1;
        private ComboBox displayDataList;
        private Label displayData;
        private DataGridView displayListGridView;
        private Label endDate;
        private Label startDate;
        private DateTimePicker endDateTimePicker;
        private DateTimePicker startDateTimePicker;
        private CheckBox useDatesCB;
        private Label patientIDLabel;
        private Label locationLabel;
        private ComboBox patientIDCB;
        private ComboBox locationCB;
        private Button resetButton;
    }
}

