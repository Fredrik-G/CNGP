namespace UDPLog
{
    partial class AnalyseLog
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.AnalyseInfoGridView = new System.Windows.Forms.DataGridView();
            this.SortButton = new System.Windows.Forms.Button();
            this.ShowDaysButton = new System.Windows.Forms.Button();
            this.ShowHoursButton = new System.Windows.Forms.Button();
            this.ButtonsPanel = new System.Windows.Forms.Panel();
            this.DisplayMethodsLabel = new System.Windows.Forms.Label();
            this.AnalyseMethodsLabel = new System.Windows.Forms.Label();
            this.CalculatePlaytimeButton = new System.Windows.Forms.Button();
            this.ShowPlaytimeButton = new System.Windows.Forms.Button();
            this.UserPlaytimeGridView = new System.Windows.Forms.DataGridView();
            this.AnalyseDataChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readLogFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createDebugLogFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InformationLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.AnalyseInfoGridView)).BeginInit();
            this.ButtonsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UserPlaytimeGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AnalyseDataChart)).BeginInit();
            this.MainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // AnalyseInfoGridView
            // 
            this.AnalyseInfoGridView.AllowUserToAddRows = false;
            this.AnalyseInfoGridView.AllowUserToDeleteRows = false;
            this.AnalyseInfoGridView.AllowUserToOrderColumns = true;
            this.AnalyseInfoGridView.AllowUserToResizeColumns = false;
            this.AnalyseInfoGridView.AllowUserToResizeRows = false;
            this.AnalyseInfoGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.AnalyseInfoGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AnalyseInfoGridView.Location = new System.Drawing.Point(7, 25);
            this.AnalyseInfoGridView.Name = "AnalyseInfoGridView";
            this.AnalyseInfoGridView.ReadOnly = true;
            this.AnalyseInfoGridView.Size = new System.Drawing.Size(315, 238);
            this.AnalyseInfoGridView.TabIndex = 0;
            this.AnalyseInfoGridView.SelectionChanged += new System.EventHandler(this.AnalyseInfoGridView_SelectionChanged);
            // 
            // SortButton
            // 
            this.SortButton.Location = new System.Drawing.Point(12, 35);
            this.SortButton.Name = "SortButton";
            this.SortButton.Size = new System.Drawing.Size(176, 23);
            this.SortButton.TabIndex = 2;
            this.SortButton.Text = "Sort Most Frequent Day/Hours";
            this.SortButton.UseVisualStyleBackColor = true;
            this.SortButton.Click += new System.EventHandler(this.SortButton_Click);
            // 
            // ShowDaysButton
            // 
            this.ShowDaysButton.Location = new System.Drawing.Point(220, 35);
            this.ShowDaysButton.Name = "ShowDaysButton";
            this.ShowDaysButton.Size = new System.Drawing.Size(150, 23);
            this.ShowDaysButton.TabIndex = 4;
            this.ShowDaysButton.Text = "Show Days";
            this.ShowDaysButton.UseVisualStyleBackColor = true;
            this.ShowDaysButton.Click += new System.EventHandler(this.ShowDaysButton_Click);
            // 
            // ShowHoursButton
            // 
            this.ShowHoursButton.Location = new System.Drawing.Point(220, 64);
            this.ShowHoursButton.Name = "ShowHoursButton";
            this.ShowHoursButton.Size = new System.Drawing.Size(150, 23);
            this.ShowHoursButton.TabIndex = 6;
            this.ShowHoursButton.Text = "Show Hours";
            this.ShowHoursButton.UseVisualStyleBackColor = true;
            this.ShowHoursButton.Click += new System.EventHandler(this.ShowHoursButton_Click);
            // 
            // ButtonsPanel
            // 
            this.ButtonsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ButtonsPanel.Controls.Add(this.DisplayMethodsLabel);
            this.ButtonsPanel.Controls.Add(this.AnalyseMethodsLabel);
            this.ButtonsPanel.Controls.Add(this.CalculatePlaytimeButton);
            this.ButtonsPanel.Controls.Add(this.ShowDaysButton);
            this.ButtonsPanel.Controls.Add(this.ShowHoursButton);
            this.ButtonsPanel.Controls.Add(this.SortButton);
            this.ButtonsPanel.Controls.Add(this.ShowPlaytimeButton);
            this.ButtonsPanel.Location = new System.Drawing.Point(7, 283);
            this.ButtonsPanel.Name = "ButtonsPanel";
            this.ButtonsPanel.Size = new System.Drawing.Size(418, 128);
            this.ButtonsPanel.TabIndex = 6;
            // 
            // DisplayMethodsLabel
            // 
            this.DisplayMethodsLabel.AutoSize = true;
            this.DisplayMethodsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisplayMethodsLabel.Location = new System.Drawing.Point(216, 12);
            this.DisplayMethodsLabel.Name = "DisplayMethodsLabel";
            this.DisplayMethodsLabel.Size = new System.Drawing.Size(130, 20);
            this.DisplayMethodsLabel.TabIndex = 9;
            this.DisplayMethodsLabel.Text = "Display Methods:";
            // 
            // AnalyseMethodsLabel
            // 
            this.AnalyseMethodsLabel.AutoSize = true;
            this.AnalyseMethodsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AnalyseMethodsLabel.Location = new System.Drawing.Point(8, 12);
            this.AnalyseMethodsLabel.Name = "AnalyseMethodsLabel";
            this.AnalyseMethodsLabel.Size = new System.Drawing.Size(135, 20);
            this.AnalyseMethodsLabel.TabIndex = 8;
            this.AnalyseMethodsLabel.Text = "Analyse Methods:";
            // 
            // CalculatePlaytimeButton
            // 
            this.CalculatePlaytimeButton.Location = new System.Drawing.Point(12, 64);
            this.CalculatePlaytimeButton.Name = "CalculatePlaytimeButton";
            this.CalculatePlaytimeButton.Size = new System.Drawing.Size(176, 23);
            this.CalculatePlaytimeButton.TabIndex = 3;
            this.CalculatePlaytimeButton.Text = "Calculate User Playtime";
            this.CalculatePlaytimeButton.UseVisualStyleBackColor = true;
            this.CalculatePlaytimeButton.Click += new System.EventHandler(this.CalculatePlaytimeButton_Click);
            // 
            // ShowPlaytimeButton
            // 
            this.ShowPlaytimeButton.Location = new System.Drawing.Point(220, 93);
            this.ShowPlaytimeButton.Name = "ShowPlaytimeButton";
            this.ShowPlaytimeButton.Size = new System.Drawing.Size(150, 23);
            this.ShowPlaytimeButton.TabIndex = 7;
            this.ShowPlaytimeButton.Text = "Show Users Playtime";
            this.ShowPlaytimeButton.UseVisualStyleBackColor = true;
            this.ShowPlaytimeButton.Click += new System.EventHandler(this.ShowPlaytimeButton_Click);
            // 
            // UserPlaytimeGridView
            // 
            this.UserPlaytimeGridView.AllowUserToAddRows = false;
            this.UserPlaytimeGridView.AllowUserToDeleteRows = false;
            this.UserPlaytimeGridView.AllowUserToOrderColumns = true;
            this.UserPlaytimeGridView.AllowUserToResizeColumns = false;
            this.UserPlaytimeGridView.AllowUserToResizeRows = false;
            this.UserPlaytimeGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.UserPlaytimeGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UserPlaytimeGridView.Location = new System.Drawing.Point(328, 25);
            this.UserPlaytimeGridView.Name = "UserPlaytimeGridView";
            this.UserPlaytimeGridView.ReadOnly = true;
            this.UserPlaytimeGridView.Size = new System.Drawing.Size(315, 238);
            this.UserPlaytimeGridView.TabIndex = 9;
            // 
            // AnalyseDataChart
            // 
            chartArea1.Name = "ChartArea1";
            this.AnalyseDataChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.AnalyseDataChart.Legends.Add(legend1);
            this.AnalyseDataChart.Location = new System.Drawing.Point(649, 27);
            this.AnalyseDataChart.Name = "AnalyseDataChart";
            series1.ChartArea = "ChartArea1";
            series1.IsVisibleInLegend = false;
            series1.LabelBackColor = System.Drawing.Color.White;
            series1.LabelBorderColor = System.Drawing.Color.White;
            series1.LabelForeColor = System.Drawing.Color.White;
            series1.Legend = "Legend1";
            series1.Name = " ";
            this.AnalyseDataChart.Series.Add(series1);
            this.AnalyseDataChart.Size = new System.Drawing.Size(300, 236);
            this.AnalyseDataChart.TabIndex = 0;
            this.AnalyseDataChart.Text = "Analyse Data Chart";
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(964, 24);
            this.MainMenuStrip.TabIndex = 12;
            this.MainMenuStrip.Text = "menuStrip2";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.readLogFileToolStripMenuItem,
            this.createDebugLogFileToolStripMenuItem,
            this.saveGraphToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // readLogFileToolStripMenuItem
            // 
            this.readLogFileToolStripMenuItem.Name = "readLogFileToolStripMenuItem";
            this.readLogFileToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.readLogFileToolStripMenuItem.Text = "&Read Log File";
            this.readLogFileToolStripMenuItem.Click += new System.EventHandler(this.readLogFileToolStripMenuItem_Click);
            // 
            // createDebugLogFileToolStripMenuItem
            // 
            this.createDebugLogFileToolStripMenuItem.Name = "createDebugLogFileToolStripMenuItem";
            this.createDebugLogFileToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.createDebugLogFileToolStripMenuItem.Text = "&Create Debug Log File";
            this.createDebugLogFileToolStripMenuItem.Click += new System.EventHandler(this.createDebugLogFileToolStripMenuItem_Click);
            // 
            // saveGraphToolStripMenuItem
            // 
            this.saveGraphToolStripMenuItem.Name = "saveGraphToolStripMenuItem";
            this.saveGraphToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.saveGraphToolStripMenuItem.Text = "&Save Graph";
            this.saveGraphToolStripMenuItem.Click += new System.EventHandler(this.saveGraphToolStripMenuItem_Click);
            // 
            // InformationLabel
            // 
            this.InformationLabel.AutoSize = true;
            this.InformationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InformationLabel.Location = new System.Drawing.Point(442, 391);
            this.InformationLabel.Name = "InformationLabel";
            this.InformationLabel.Size = new System.Drawing.Size(133, 20);
            this.InformationLabel.TabIndex = 13;
            this.InformationLabel.Text = "Information Label";
            this.InformationLabel.Visible = false;
            // 
            // AnalyseLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 423);
            this.Controls.Add(this.InformationLabel);
            this.Controls.Add(this.MainMenuStrip);
            this.Controls.Add(this.AnalyseDataChart);
            this.Controls.Add(this.UserPlaytimeGridView);
            this.Controls.Add(this.ButtonsPanel);
            this.Controls.Add(this.AnalyseInfoGridView);
            this.Name = "AnalyseLog";
            this.Text = "Analyse Log";
            ((System.ComponentModel.ISupportInitialize)(this.AnalyseInfoGridView)).EndInit();
            this.ButtonsPanel.ResumeLayout(false);
            this.ButtonsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UserPlaytimeGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AnalyseDataChart)).EndInit();
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView AnalyseInfoGridView;
        private System.Windows.Forms.Button SortButton;
        private System.Windows.Forms.Button ShowDaysButton;
        private System.Windows.Forms.Button ShowHoursButton;
        private System.Windows.Forms.Panel ButtonsPanel;
        private System.Windows.Forms.Button CalculatePlaytimeButton;
        private System.Windows.Forms.Label AnalyseMethodsLabel;
        private System.Windows.Forms.Button ShowPlaytimeButton;
        private System.Windows.Forms.DataGridView UserPlaytimeGridView;
        private System.Windows.Forms.DataVisualization.Charting.Chart AnalyseDataChart;
        private System.Windows.Forms.MenuStrip MainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readLogFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createDebugLogFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveGraphToolStripMenuItem;
        private System.Windows.Forms.Label DisplayMethodsLabel;
        private System.Windows.Forms.Label InformationLabel;
    }
}