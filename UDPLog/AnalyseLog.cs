using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace UDPLog
{
    public partial class AnalyseLog : Form
    {
        LogAnalysis.LogAnalysis _logAnalysis = new LogAnalysis.LogAnalysis();

        public AnalyseLog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Occurs when the ReadLogButton is clicked.
        /// Creates an OpenFileDialog and attempts to read the log file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReadLogButton_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Gameplay Log File|gameplay*.log|General Log File|general*.log";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ResetGui();

                var watch = Stopwatch.StartNew();
                if (openFileDialog.FilterIndex == 1)
                {
                    _logAnalysis.ReadLogFile(openFileDialog.FileName, true);
                }
                if (openFileDialog.FilterIndex == 2)
                {
                    _logAnalysis.ReadLogFile(openFileDialog.FileName, false);
                }

                watch.Stop();
                ReadLogLabel.Visible = true;
                ReadLogLabel.Text = "Read " + openFileDialog.SafeFileName + " in "
                    + Math.Round(watch.Elapsed.TotalSeconds, 3) + " seconds.";
            }
        }

        /// <summary>
        /// Occurs when the SortButton is clicked.
        /// Sorts the day and hour information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortButton_Click(object sender, EventArgs e)
        {
            var watch = Stopwatch.StartNew();
            _logAnalysis.SortMostFrequentDayAndHour();

            watch.Stop();
            SortLabel.Visible = true;
            SortLabel.Text = "Sorted information in " + Math.Round(watch.Elapsed.TotalSeconds, 3) + " seconds.";
        }

        /// <summary>
        /// Occurs when the ShowDaysButton is clicked.
        /// Changes the data source for the DataGridView to display day information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowDaysButton_Click(object sender, EventArgs e)
        {
            AnalyseInfoGridView.DataSource = _logAnalysis.GetDaysInfo();
        }

        /// <summary>
        /// Occurs when the ShowHoursButton is clicked.
        /// Changes the data source for the DataGridView to display hour information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowHoursButton_Click(object sender, EventArgs e)
        {
            AnalyseInfoGridView.DataSource = _logAnalysis.GetHoursInfos();
        }

        /// <summary>
        /// Resets the GUI elements.
        /// </summary>
        private void ResetGui()
        {
            AnalyseInfoGridView.Rows.Clear();
            ReadLogLabel.Text = SortLabel.Text = CalculatePlaytimeLabel.Text = String.Empty;
            ReadLogLabel.Visible = SortLabel.Visible = CalculatePlaytimeLabel.Visible = false;
        }

        private void CalculatePlaytimeButton_Click(object sender, EventArgs e)
        {
            var inputMail = Interaction.InputBox("User Account to look up", "Calculate Playtime", "user@mail.com");

            var watch = Stopwatch.StartNew();
            var asd = _logAnalysis.CalculateUserPlayTime();
            watch.Stop();

            AnalyseInfoGridView.DataSource = asd;

            CalculatePlaytimeLabel.Visible = true;
            CalculatePlaytimeLabel.Text = "Calculation took " + Math.Round(watch.Elapsed.TotalSeconds, 3) + " seconds.";
        }

        private void ShowPlaytime_Click(object sender, EventArgs e)
        {

        }
    }
}
