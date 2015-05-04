using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UDPLog
{
    public partial class CreateDebugLogFile : Form
    {
        public CreateDebugLogFile()
        {
            InitializeComponent();
        }

        public void CreateDebugLog(string fileLocation)
        {
            var linesToWrite = LinesToWriteNumericUpDown.Value;
            if (LoginLogoffRadioButton.Checked)
            {
                linesToWrite = linesToWrite / 3;
            }
            var random = new Random();


            using (var file = new StreamWriter(fileLocation))
            {
                for (var i = 0; i < linesToWrite; i++)
                {
                    if (LoginLogoffRadioButton.Checked)
                    {
                        WriteLoginLogoffLines(file, random, i);
                    }
                    else if (DaysAndHoursRadioButton.Checked)
                    {
                        WriteDaysAndHoursLines(file, i, random);
                    }
                    else if (BothOptionsRadioButton.Checked)
                    {
                        WriteLoginLogoffLines(file, random, i);
                        WriteDaysAndHoursLines(file, i, random);
                    }
                }
            }
        }

        private void WriteLoginLogoffLines(StreamWriter file, Random random, int startMillisecond)
        {
            var startSecond = 10;
            if (startMillisecond >= 999)
            {
                startSecond++;
                startMillisecond = 0;
            }

            var hour = random.Next(0, 23);
            var minute = random.Next(0, 59);
            var userId = random.Next(0, 10);

            var loginMessage = "2015-01-01 0:0:" +
                               startSecond + "," + startMillisecond
                               + ";[10];DEBUG ;NameHere;(null);Account user" + userId + "@mail.com has logged on.";
            var logoffMessage = "2015-01-01 " + hour + ":" + minute + ":30,500;[10];DEBUG ;NameHere;(null);Account user" + userId + "@mail.com has logged off.";
            const string stuff = "2015-01-01 1:2:3,456;[10]30,500;[10];DEBUG ;NameHere;(null);stuff";

            file.WriteLine(loginMessage);
            file.WriteLine(stuff);
            file.WriteLine(logoffMessage);
        }

        private void WriteDaysAndHoursLines(TextWriter file, int i, Random random)
        {
            var day = random.Next(1, 28);
            var hour = random.Next(0, 23);

            var line = "2015-01-" + day + " " + hour + ":25:30,500;[10];DEBUG ;NameHere;(null);texttexttext";
            file.WriteLine(line);
        }
        private void CreateLogButton_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save Log File As";
            saveFileDialog.Filter = "Log file|gameplay*.log";

            if (LoginLogoffRadioButton.Checked)
            {
                saveFileDialog.FileName = "gameplay LoginLogoff";
            }
            else if (DaysAndHoursRadioButton.Checked)
            {
                saveFileDialog.FileName = "gameplay DaysAndHours";
            }
            else if (BothOptionsRadioButton.Checked)
            {
                saveFileDialog.FileName = "gameplay LoginAndDay";
            }

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var watch = Stopwatch.StartNew();
                CreateDebugLog(saveFileDialog.FileName);

                watch.Stop();
                InformationLabel.Visible = true;
                InformationLabel.Text = "Created log file in " + Math.Round(watch.Elapsed.TotalSeconds, 3) + " seconds.";
            }
        }
    }
}