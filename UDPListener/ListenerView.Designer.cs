namespace UDPListener
{
    partial class ListenerView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.StopListenerButton = new System.Windows.Forms.Button();
            this.StartListenerButton = new System.Windows.Forms.Button();
            this.LogMessagesDataGridView = new System.Windows.Forms.DataGridView();
            this.Date2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Thread = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Logger = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IPAdressTextBox = new System.Windows.Forms.TextBox();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.LogMessagePanel = new System.Windows.Forms.Panel();
            this.LogLevelComboBox = new System.Windows.Forms.ComboBox();
            this.TimeActiveLabel = new System.Windows.Forms.Label();
            this.TimeActiveTimer = new System.Windows.Forms.Timer(this.components);
            this.InformationLabel = new System.Windows.Forms.Label();
            this.ClearMessagesButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.LogMessagesDataGridView)).BeginInit();
            this.LogMessagePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // StopListenerButton
            // 
            this.StopListenerButton.Enabled = false;
            this.StopListenerButton.Location = new System.Drawing.Point(120, 258);
            this.StopListenerButton.Name = "StopListenerButton";
            this.StopListenerButton.Size = new System.Drawing.Size(110, 37);
            this.StopListenerButton.TabIndex = 5;
            this.StopListenerButton.Text = "Stop Listener";
            this.StopListenerButton.UseVisualStyleBackColor = true;
            this.StopListenerButton.Click += new System.EventHandler(this.StopListenerButton_Click);
            // 
            // StartListenerButton
            // 
            this.StartListenerButton.Location = new System.Drawing.Point(8, 258);
            this.StartListenerButton.Name = "StartListenerButton";
            this.StartListenerButton.Size = new System.Drawing.Size(110, 37);
            this.StartListenerButton.TabIndex = 4;
            this.StartListenerButton.Text = "Start Listener";
            this.StartListenerButton.UseVisualStyleBackColor = true;
            this.StartListenerButton.Click += new System.EventHandler(this.StartListenerButton_Click);
            // 
            // LogMessagesDataGridView
            // 
            this.LogMessagesDataGridView.AllowUserToAddRows = false;
            this.LogMessagesDataGridView.AllowUserToDeleteRows = false;
            this.LogMessagesDataGridView.AllowUserToOrderColumns = true;
            this.LogMessagesDataGridView.AllowUserToResizeColumns = false;
            this.LogMessagesDataGridView.AllowUserToResizeRows = false;
            this.LogMessagesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogMessagesDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.LogMessagesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LogMessagesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date2,
            this.Level,
            this.Thread,
            this.Logger,
            this.Message});
            this.LogMessagesDataGridView.Location = new System.Drawing.Point(3, 3);
            this.LogMessagesDataGridView.Name = "LogMessagesDataGridView";
            this.LogMessagesDataGridView.RowHeadersWidth = 28;
            this.LogMessagesDataGridView.Size = new System.Drawing.Size(521, 207);
            this.LogMessagesDataGridView.TabIndex = 8;
            // 
            // Date2
            // 
            this.Date2.HeaderText = "Date";
            this.Date2.Name = "Date2";
            // 
            // Level
            // 
            this.Level.HeaderText = "Level";
            this.Level.Name = "Level";
            // 
            // Thread
            // 
            this.Thread.HeaderText = "Thread";
            this.Thread.Name = "Thread";
            // 
            // Logger
            // 
            this.Logger.HeaderText = "Logger";
            this.Logger.Name = "Logger";
            // 
            // Message
            // 
            this.Message.HeaderText = "Message";
            this.Message.Name = "Message";
            // 
            // IPAdressTextBox
            // 
            this.IPAdressTextBox.Location = new System.Drawing.Point(8, 232);
            this.IPAdressTextBox.Name = "IPAdressTextBox";
            this.IPAdressTextBox.Size = new System.Drawing.Size(110, 20);
            this.IPAdressTextBox.TabIndex = 1;
            this.IPAdressTextBox.Text = "127.0.0.1";
            this.IPAdressTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.IPAdressTextBox_MouseDown);
            // 
            // PortTextBox
            // 
            this.PortTextBox.Location = new System.Drawing.Point(120, 232);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(110, 20);
            this.PortTextBox.TabIndex = 2;
            this.PortTextBox.Text = "9059";
            this.PortTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PortTextBox_MouseDown);
            // 
            // LogMessagePanel
            // 
            this.LogMessagePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogMessagePanel.Controls.Add(this.LogMessagesDataGridView);
            this.LogMessagePanel.Location = new System.Drawing.Point(5, 3);
            this.LogMessagePanel.Name = "LogMessagePanel";
            this.LogMessagePanel.Size = new System.Drawing.Size(527, 213);
            this.LogMessagePanel.TabIndex = 14;
            // 
            // LogLevelComboBox
            // 
            this.LogLevelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LogLevelComboBox.FormattingEnabled = true;
            this.LogLevelComboBox.Items.AddRange(new object[] {
            "--ALL--",
            "FATAL",
            "ERROR",
            "WARN",
            "INFO",
            "DEBUG"});
            this.LogLevelComboBox.Location = new System.Drawing.Point(233, 232);
            this.LogLevelComboBox.Name = "LogLevelComboBox";
            this.LogLevelComboBox.Size = new System.Drawing.Size(110, 21);
            this.LogLevelComboBox.TabIndex = 3;
            this.LogLevelComboBox.SelectedIndexChanged += new System.EventHandler(this.LogLevelComboBox_SelectedIndexChanged);
            // 
            // TimeActiveLabel
            // 
            this.TimeActiveLabel.AutoSize = true;
            this.TimeActiveLabel.Location = new System.Drawing.Point(348, 278);
            this.TimeActiveLabel.Name = "TimeActiveLabel";
            this.TimeActiveLabel.Size = new System.Drawing.Size(86, 13);
            this.TimeActiveLabel.TabIndex = 17;
            this.TimeActiveLabel.Text = "TimeActiveLabel";
            this.TimeActiveLabel.Visible = false;
            // 
            // TimeActiveTimer
            // 
            this.TimeActiveTimer.Interval = 1000;
            this.TimeActiveTimer.Tick += new System.EventHandler(this.TimeActiveTimer_Tick);
            // 
            // InformationLabel
            // 
            this.InformationLabel.AutoSize = true;
            this.InformationLabel.Location = new System.Drawing.Point(353, 267);
            this.InformationLabel.Name = "InformationLabel";
            this.InformationLabel.Size = new System.Drawing.Size(0, 13);
            this.InformationLabel.TabIndex = 18;
            // 
            // ClearMessagesButton
            // 
            this.ClearMessagesButton.Location = new System.Drawing.Point(233, 259);
            this.ClearMessagesButton.Name = "ClearMessagesButton";
            this.ClearMessagesButton.Size = new System.Drawing.Size(110, 37);
            this.ClearMessagesButton.TabIndex = 6;
            this.ClearMessagesButton.Text = "Clear Messages";
            this.ClearMessagesButton.UseVisualStyleBackColor = true;
            this.ClearMessagesButton.Click += new System.EventHandler(this.ClearMessagesButton_Click);
            // 
            // ListenerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ClearMessagesButton);
            this.Controls.Add(this.InformationLabel);
            this.Controls.Add(this.TimeActiveLabel);
            this.Controls.Add(this.LogLevelComboBox);
            this.Controls.Add(this.LogMessagePanel);
            this.Controls.Add(this.PortTextBox);
            this.Controls.Add(this.IPAdressTextBox);
            this.Controls.Add(this.StopListenerButton);
            this.Controls.Add(this.StartListenerButton);
            this.Name = "ListenerView";
            this.Size = new System.Drawing.Size(535, 308);
            this.Load += new System.EventHandler(this.ListenerView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LogMessagesDataGridView)).EndInit();
            this.LogMessagePanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StopListenerButton;
        private System.Windows.Forms.Button StartListenerButton;
        private System.Windows.Forms.DataGridView LogMessagesDataGridView;
        private System.Windows.Forms.TextBox IPAdressTextBox;
        private System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level;
        private System.Windows.Forms.DataGridViewTextBoxColumn Thread;
        private System.Windows.Forms.DataGridViewTextBoxColumn Logger;
        private System.Windows.Forms.DataGridViewTextBoxColumn Message;
        private System.Windows.Forms.Panel LogMessagePanel;
        private System.Windows.Forms.ComboBox LogLevelComboBox;
        private System.Windows.Forms.Label TimeActiveLabel;
        private System.Windows.Forms.Timer TimeActiveTimer;
        private System.Windows.Forms.Label InformationLabel;
        private System.Windows.Forms.Button ClearMessagesButton;
    }
}
