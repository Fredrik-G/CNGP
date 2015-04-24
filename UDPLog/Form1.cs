using System;
using System.Reflection;
using System.Windows.Forms;

namespace UDPLog
{
    public partial class Form1 : Form
    {
        private readonly Logger _logger = new Logger(MethodBase.GetCurrentMethod());

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _logger.Info("klick");
        }
    }
}
