﻿namespace UDPLog
{
    partial class CreateDebugLogFile
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
            this.CreateButton = new System.Windows.Forms.Button();
            this.LinesToWriteNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.LoginLogoffRadioButton = new System.Windows.Forms.RadioButton();
            this.DaysAndHoursRadioButton = new System.Windows.Forms.RadioButton();
            this.BothOptionsRadioButton = new System.Windows.Forms.RadioButton();
            this.LinesToWriteLabel = new System.Windows.Forms.Label();
            this.TypeOfLogLabel = new System.Windows.Forms.Label();
            this.InformationLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.LinesToWriteNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // CreateButton
            // 
            this.CreateButton.Location = new System.Drawing.Point(56, 107);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(116, 33);
            this.CreateButton.TabIndex = 0;
            this.CreateButton.Text = "Create Log";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateLogButton_Click);
            // 
            // LinesToWriteNumericUpDown
            // 
            this.LinesToWriteNumericUpDown.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.LinesToWriteNumericUpDown.Location = new System.Drawing.Point(89, 12);
            this.LinesToWriteNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.LinesToWriteNumericUpDown.Name = "LinesToWriteNumericUpDown";
            this.LinesToWriteNumericUpDown.Size = new System.Drawing.Size(83, 20);
            this.LinesToWriteNumericUpDown.TabIndex = 1;
            this.LinesToWriteNumericUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // LoginLogoffRadioButton
            // 
            this.LoginLogoffRadioButton.AutoSize = true;
            this.LoginLogoffRadioButton.Checked = true;
            this.LoginLogoffRadioButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.LoginLogoffRadioButton.Location = new System.Drawing.Point(89, 38);
            this.LoginLogoffRadioButton.Name = "LoginLogoffRadioButton";
            this.LoginLogoffRadioButton.Size = new System.Drawing.Size(107, 17);
            this.LoginLogoffRadioButton.TabIndex = 2;
            this.LoginLogoffRadioButton.TabStop = true;
            this.LoginLogoffRadioButton.Text = "Login/Logoff Log";
            this.LoginLogoffRadioButton.UseVisualStyleBackColor = true;
            // 
            // DaysAndHoursRadioButton
            // 
            this.DaysAndHoursRadioButton.AutoSize = true;
            this.DaysAndHoursRadioButton.Location = new System.Drawing.Point(89, 61);
            this.DaysAndHoursRadioButton.Name = "DaysAndHoursRadioButton";
            this.DaysAndHoursRadioButton.Size = new System.Drawing.Size(122, 17);
            this.DaysAndHoursRadioButton.TabIndex = 3;
            this.DaysAndHoursRadioButton.Text = "Days and Hours Log";
            this.DaysAndHoursRadioButton.UseVisualStyleBackColor = true;
            // 
            // BothOptionsRadioButton
            // 
            this.BothOptionsRadioButton.AutoSize = true;
            this.BothOptionsRadioButton.Location = new System.Drawing.Point(89, 84);
            this.BothOptionsRadioButton.Name = "BothOptionsRadioButton";
            this.BothOptionsRadioButton.Size = new System.Drawing.Size(50, 17);
            this.BothOptionsRadioButton.TabIndex = 4;
            this.BothOptionsRadioButton.Text = "Both ";
            this.BothOptionsRadioButton.UseVisualStyleBackColor = true;
            // 
            // LinesToWriteLabel
            // 
            this.LinesToWriteLabel.AutoSize = true;
            this.LinesToWriteLabel.Location = new System.Drawing.Point(7, 12);
            this.LinesToWriteLabel.Name = "LinesToWriteLabel";
            this.LinesToWriteLabel.Size = new System.Drawing.Size(76, 13);
            this.LinesToWriteLabel.TabIndex = 6;
            this.LinesToWriteLabel.Text = "Lines To Write";
            // 
            // TypeOfLogLabel
            // 
            this.TypeOfLogLabel.AutoSize = true;
            this.TypeOfLogLabel.Location = new System.Drawing.Point(7, 43);
            this.TypeOfLogLabel.Name = "TypeOfLogLabel";
            this.TypeOfLogLabel.Size = new System.Drawing.Size(64, 13);
            this.TypeOfLogLabel.TabIndex = 7;
            this.TypeOfLogLabel.Text = "Type of Log";
            // 
            // InformationLabel
            // 
            this.InformationLabel.AutoSize = true;
            this.InformationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InformationLabel.Location = new System.Drawing.Point(7, 143);
            this.InformationLabel.Name = "InformationLabel";
            this.InformationLabel.Size = new System.Drawing.Size(117, 17);
            this.InformationLabel.TabIndex = 8;
            this.InformationLabel.Text = "Information Label";
            this.InformationLabel.Visible = false;
            // 
            // CreateDebugLogFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(247, 165);
            this.Controls.Add(this.InformationLabel);
            this.Controls.Add(this.LoginLogoffRadioButton);
            this.Controls.Add(this.BothOptionsRadioButton);
            this.Controls.Add(this.TypeOfLogLabel);
            this.Controls.Add(this.DaysAndHoursRadioButton);
            this.Controls.Add(this.LinesToWriteLabel);
            this.Controls.Add(this.LinesToWriteNumericUpDown);
            this.Controls.Add(this.CreateButton);
            this.Name = "CreateDebugLogFile";
            this.Text = "Create Debug Log File";
            ((System.ComponentModel.ISupportInitialize)(this.LinesToWriteNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.NumericUpDown LinesToWriteNumericUpDown;
        private System.Windows.Forms.RadioButton LoginLogoffRadioButton;
        private System.Windows.Forms.RadioButton DaysAndHoursRadioButton;
        private System.Windows.Forms.RadioButton BothOptionsRadioButton;
        private System.Windows.Forms.Label LinesToWriteLabel;
        private System.Windows.Forms.Label TypeOfLogLabel;
        private System.Windows.Forms.Label InformationLabel;
    }
}