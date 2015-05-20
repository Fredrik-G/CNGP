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
            ((System.ComponentModel.ISupportInitialize)(this.LogMessagesDataGridView)).BeginInit();
            this.LogMessagePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // StopListenerButton
            // 
            this.StopListenerButton.Enabled = false;
            this.StopListenerButton.Location = new System.Drawing.Point(114, 262);
            this.StopListenerButton.Name = "StopListenerButton";
            this.StopListenerButton.Size = new System.Drawing.Size(110, 37);
            this.StopListenerButton.TabIndex = 10;
            this.StopListenerButton.Text = "Stop Listener";
            this.StopListenerButton.UseVisualStyleBackColor = true;
            this.StopListenerButton.Click += new System.EventHandler(this.StopListenerButton_Click);
            // 
            // StartListenerButton
            // 
            this.StartListenerButton.Location = new System.Drawing.Point(5, 262);
            this.StartListenerButton.Name = "StartListenerButton";
            this.StartListenerButton.Size = new System.Drawing.Size(103, 37);
            this.StartListenerButton.TabIndex = 9;
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
            this.IPAdressTextBox.Location = new System.Drawing.Point(5, 236);
            this.IPAdressTextBox.Name = "IPAdressTextBox";
            this.IPAdressTextBox.Size = new System.Drawing.Size(103, 20);
            this.IPAdressTextBox.TabIndex = 12;
            this.IPAdressTextBox.Text = "127.0.0.1";
            this.IPAdressTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.IPAdressTextBox_MouseDown);
            // 
            // PortTextBox
            // 
            this.PortTextBox.Location = new System.Drawing.Point(114, 236);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(110, 20);
            this.PortTextBox.TabIndex = 13;
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
            this.LogLevelComboBox.FormattingEnabled = true;
            this.LogLevelComboBox.Items.AddRange(new object[] {
            "--ALL--",
            "FATAL",
            "ERROR",
            "WARN",
            "INFO",
            "DEBUG"});
            this.LogLevelComboBox.Location = new System.Drawing.Point(230, 271);
            this.LogLevelComboBox.Name = "LogLevelComboBox";
            this.LogLevelComboBox.Size = new System.Drawing.Size(121, 21);
            this.LogLevelComboBox.TabIndex = 16;
            this.LogLevelComboBox.SelectedIndexChanged += new System.EventHandler(this.LogLevelComboBox_SelectedIndexChanged);
            // 
            // ListenerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
    }
}
