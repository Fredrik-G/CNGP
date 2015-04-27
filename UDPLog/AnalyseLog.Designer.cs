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
            this.AnalyseInfoGridView = new System.Windows.Forms.DataGridView();
            this.ReadLogButton = new System.Windows.Forms.Button();
            this.SortButton = new System.Windows.Forms.Button();
            this.ShowDaysButton = new System.Windows.Forms.Button();
            this.ShowHoursButton = new System.Windows.Forms.Button();
            this.ReadLogLabel = new System.Windows.Forms.Label();
            this.SortLabel = new System.Windows.Forms.Label();
            this.ButtonsPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.CalculatePlaytimeLabel = new System.Windows.Forms.Label();
            this.CalculatePlaytimeButton = new System.Windows.Forms.Button();
            this.ShowPlaytime = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.AnalyseInfoGridView)).BeginInit();
            this.ButtonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // AnalyseInfoGridView
            // 
            this.AnalyseInfoGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AnalyseInfoGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.AnalyseInfoGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AnalyseInfoGridView.Location = new System.Drawing.Point(0, 0);
            this.AnalyseInfoGridView.Name = "AnalyseInfoGridView";
            this.AnalyseInfoGridView.Size = new System.Drawing.Size(572, 198);
            this.AnalyseInfoGridView.TabIndex = 0;
            // 
            // ReadLogButton
            // 
            this.ReadLogButton.Location = new System.Drawing.Point(12, 204);
            this.ReadLogButton.Name = "ReadLogButton";
            this.ReadLogButton.Size = new System.Drawing.Size(176, 23);
            this.ReadLogButton.TabIndex = 1;
            this.ReadLogButton.Text = "Read Log";
            this.ReadLogButton.UseVisualStyleBackColor = true;
            this.ReadLogButton.Click += new System.EventHandler(this.ReadLogButton_Click);
            // 
            // SortButton
            // 
            this.SortButton.Location = new System.Drawing.Point(7, 36);
            this.SortButton.Name = "SortButton";
            this.SortButton.Size = new System.Drawing.Size(176, 23);
            this.SortButton.TabIndex = 2;
            this.SortButton.Text = "Sort Most Frequent Day/Hours";
            this.SortButton.UseVisualStyleBackColor = true;
            this.SortButton.Click += new System.EventHandler(this.SortButton_Click);
            // 
            // ShowDaysButton
            // 
            this.ShowDaysButton.Location = new System.Drawing.Point(12, 233);
            this.ShowDaysButton.Name = "ShowDaysButton";
            this.ShowDaysButton.Size = new System.Drawing.Size(176, 23);
            this.ShowDaysButton.TabIndex = 3;
            this.ShowDaysButton.Text = "Show Days";
            this.ShowDaysButton.UseVisualStyleBackColor = true;
            this.ShowDaysButton.Click += new System.EventHandler(this.ShowDaysButton_Click);
            // 
            // ShowHoursButton
            // 
            this.ShowHoursButton.Location = new System.Drawing.Point(12, 262);
            this.ShowHoursButton.Name = "ShowHoursButton";
            this.ShowHoursButton.Size = new System.Drawing.Size(176, 23);
            this.ShowHoursButton.TabIndex = 4;
            this.ShowHoursButton.Text = "Show Hours";
            this.ShowHoursButton.UseVisualStyleBackColor = true;
            this.ShowHoursButton.Click += new System.EventHandler(this.ShowHoursButton_Click);
            // 
            // ReadLogLabel
            // 
            this.ReadLogLabel.AutoSize = true;
            this.ReadLogLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ReadLogLabel.Location = new System.Drawing.Point(194, 209);
            this.ReadLogLabel.Name = "ReadLogLabel";
            this.ReadLogLabel.Size = new System.Drawing.Size(77, 13);
            this.ReadLogLabel.TabIndex = 0;
            this.ReadLogLabel.Text = "ReadLogLabel";
            this.ReadLogLabel.Visible = false;
            // 
            // SortLabel
            // 
            this.SortLabel.AutoSize = true;
            this.SortLabel.Location = new System.Drawing.Point(189, 41);
            this.SortLabel.Name = "SortLabel";
            this.SortLabel.Size = new System.Drawing.Size(52, 13);
            this.SortLabel.TabIndex = 5;
            this.SortLabel.Text = "SortLabel";
            this.SortLabel.Visible = false;
            // 
            // ButtonsPanel
            // 
            this.ButtonsPanel.Controls.Add(this.label1);
            this.ButtonsPanel.Controls.Add(this.CalculatePlaytimeLabel);
            this.ButtonsPanel.Controls.Add(this.CalculatePlaytimeButton);
            this.ButtonsPanel.Controls.Add(this.SortLabel);
            this.ButtonsPanel.Controls.Add(this.SortButton);
            this.ButtonsPanel.Location = new System.Drawing.Point(0, 316);
            this.ButtonsPanel.Name = "ButtonsPanel";
            this.ButtonsPanel.Size = new System.Drawing.Size(561, 98);
            this.ButtonsPanel.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Analyse Methods:";
            // 
            // CalculatePlaytimeLabel
            // 
            this.CalculatePlaytimeLabel.AutoSize = true;
            this.CalculatePlaytimeLabel.Location = new System.Drawing.Point(189, 70);
            this.CalculatePlaytimeLabel.Name = "CalculatePlaytimeLabel";
            this.CalculatePlaytimeLabel.Size = new System.Drawing.Size(116, 13);
            this.CalculatePlaytimeLabel.TabIndex = 7;
            this.CalculatePlaytimeLabel.Text = "CalculatePlaytimeLabel";
            this.CalculatePlaytimeLabel.Visible = false;
            // 
            // CalculatePlaytimeButton
            // 
            this.CalculatePlaytimeButton.Location = new System.Drawing.Point(7, 65);
            this.CalculatePlaytimeButton.Name = "CalculatePlaytimeButton";
            this.CalculatePlaytimeButton.Size = new System.Drawing.Size(176, 23);
            this.CalculatePlaytimeButton.TabIndex = 6;
            this.CalculatePlaytimeButton.Text = "Calculate User Playtime";
            this.CalculatePlaytimeButton.UseVisualStyleBackColor = true;
            this.CalculatePlaytimeButton.Click += new System.EventHandler(this.CalculatePlaytimeButton_Click);
            // 
            // ShowPlaytime
            // 
            this.ShowPlaytime.Location = new System.Drawing.Point(12, 287);
            this.ShowPlaytime.Name = "ShowPlaytime";
            this.ShowPlaytime.Size = new System.Drawing.Size(176, 23);
            this.ShowPlaytime.TabIndex = 7;
            this.ShowPlaytime.Text = "Show Users Playtime";
            this.ShowPlaytime.UseVisualStyleBackColor = true;
            this.ShowPlaytime.Click += new System.EventHandler(this.ShowPlaytime_Click);
            // 
            // AnalyseLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 439);
            this.Controls.Add(this.ShowPlaytime);
            this.Controls.Add(this.ButtonsPanel);
            this.Controls.Add(this.AnalyseInfoGridView);
            this.Controls.Add(this.ReadLogButton);
            this.Controls.Add(this.ShowDaysButton);
            this.Controls.Add(this.ReadLogLabel);
            this.Controls.Add(this.ShowHoursButton);
            this.Name = "AnalyseLog";
            this.Text = "Analyse Log";
            ((System.ComponentModel.ISupportInitialize)(this.AnalyseInfoGridView)).EndInit();
            this.ButtonsPanel.ResumeLayout(false);
            this.ButtonsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView AnalyseInfoGridView;
        private System.Windows.Forms.Button ReadLogButton;
        private System.Windows.Forms.Button SortButton;
        private System.Windows.Forms.Button ShowDaysButton;
        private System.Windows.Forms.Button ShowHoursButton;
        private System.Windows.Forms.Label ReadLogLabel;
        private System.Windows.Forms.Label SortLabel;
        private System.Windows.Forms.Panel ButtonsPanel;
        private System.Windows.Forms.Label CalculatePlaytimeLabel;
        private System.Windows.Forms.Button CalculatePlaytimeButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ShowPlaytime;
    }
}