namespace UDPListener
{
    partial class UDPListener
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
            this.ListenerTabControl = new System.Windows.Forms.TabControl();
            this.ListenersMenuStrip = new System.Windows.Forms.MenuStrip();
            this.openNewListenerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopAllListenersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ListenersMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListenerTabControl
            // 
            this.ListenerTabControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ListenerTabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.ListenerTabControl.Location = new System.Drawing.Point(0, 27);
            this.ListenerTabControl.Name = "ListenerTabControl";
            this.ListenerTabControl.SelectedIndex = 0;
            this.ListenerTabControl.Size = new System.Drawing.Size(917, 346);
            this.ListenerTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.ListenerTabControl.TabIndex = 6;
            this.ListenerTabControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ListenerTabControl_DrawItem);
            this.ListenerTabControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListenerTabControl_MouseDown);
            // 
            // ListenersMenuStrip
            // 
            this.ListenersMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openNewListenerToolStripMenuItem,
            this.stopAllListenersToolStripMenuItem});
            this.ListenersMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.ListenersMenuStrip.Name = "ListenersMenuStrip";
            this.ListenersMenuStrip.Size = new System.Drawing.Size(917, 24);
            this.ListenersMenuStrip.TabIndex = 8;
            this.ListenersMenuStrip.Text = "menuStrip1";
            // 
            // openNewListenerToolStripMenuItem
            // 
            this.openNewListenerToolStripMenuItem.Name = "openNewListenerToolStripMenuItem";
            this.openNewListenerToolStripMenuItem.Size = new System.Drawing.Size(119, 20);
            this.openNewListenerToolStripMenuItem.Text = "Open New Listener";
            this.openNewListenerToolStripMenuItem.Click += new System.EventHandler(this.openNewListenerToolStripMenuItem_Click_1);
            // 
            // stopAllListenersToolStripMenuItem
            // 
            this.stopAllListenersToolStripMenuItem.Name = "stopAllListenersToolStripMenuItem";
            this.stopAllListenersToolStripMenuItem.Size = new System.Drawing.Size(109, 20);
            this.stopAllListenersToolStripMenuItem.Text = "Stop All Listeners";
            this.stopAllListenersToolStripMenuItem.Click += new System.EventHandler(this.stopAllListenersToolStripMenuItem_Click);
            // 
            // UDPListener
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 373);
            this.Controls.Add(this.ListenersMenuStrip);
            this.Controls.Add(this.ListenerTabControl);
            this.Name = "UDPListener";
            this.Text = " ";
            this.Load += new System.EventHandler(this.UDPListener_Load);
            this.ListenersMenuStrip.ResumeLayout(false);
            this.ListenersMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl ListenerTabControl;
        private System.Windows.Forms.MenuStrip ListenersMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem openNewListenerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopAllListenersToolStripMenuItem;
    }
}

