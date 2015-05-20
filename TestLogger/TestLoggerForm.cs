using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestLogger
{
    public partial class TestLoggerForm : Form
    {
        private Logger _logger = new Logger(System.Reflection.MethodBase.GetCurrentMethod());

        public TestLoggerForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _logger.Debug("Debug");
            _logger.Info("Info");
            _logger.Warn("Warn");
            _logger.Error("Error");
            _logger.Fatal("Fatal");
        }
    }
}
