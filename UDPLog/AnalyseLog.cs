using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.VisualBasic;

namespace UDPLog
{
    public partial class AnalyseLog : Form
    {
        private LogAnalysis.LogAnalysis _logAnalysis = new LogAnalysis.LogAnalysis();
        private bool _showingPlaytime = false;

        public AnalyseLog()
        {
            InitializeComponent();

            //DEBUG!! REMOVE THIS
            _logAnalysis.ReadLogFile("M:/Desktop/år2/SystemProgramvaruutveckling/UDPLog/UDPLog/UDPLog/bin/Debug/logs/gameplay-login-logoff.log",
                true);
            //DEBUG!! REMOVE THIS
        }

        #region Form events

        /// <summary>
        /// Occurs when the ReadLogButton is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReadLogButton_Click(object sender, EventArgs e)
        {
            ReadLog();
        }

        /// <summary>
        /// Occurs when the CalculatePlaytimeButton is clicked.
        /// Calculates users play time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculatePlaytimeButton_Click(object sender, EventArgs e)
        {
            var watch = Stopwatch.StartNew();

            _logAnalysis.CalculateUserPlayTime();
            _logAnalysis.SortUsersPlayTime();

            watch.Stop();

            CalculatePlaytimeLabel.Visible = true;
            CalculatePlaytimeLabel.Text = "Calculation took " + Math.Round(watch.Elapsed.TotalSeconds, 3) + " seconds.";
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
            ResetGui();
            _showingPlaytime = false;

            AnalyseInfoGridView.DataSource = _logAnalysis.GetDaysInfo();
            DisplayDaysInChart();
        }

        /// <summary>
        /// Occurs when the ShowHoursButton is clicked.
        /// Changes the data source for the DataGridView to display hour information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowHoursButton_Click(object sender, EventArgs e)
        {
            ResetGui();
            _showingPlaytime = false;

            AnalyseInfoGridView.DataSource = _logAnalysis.GetHoursInfos();
            DisplayHoursInChart();
        }

        /// <summary>
        /// Occurs when the ShowPlaytimeButton is clicked.
        /// Changes the data source for the DataGridView to display user play time information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPlaytimeButton_Click(object sender, EventArgs e)
        {
            ResetGui();
            _showingPlaytime = true;

            AnalyseInfoGridView.DataSource = _logAnalysis.GetUserPlaytime();
            AnalyseInfoGridView.Columns[0].Name = "Account Email";
            AnalyseInfoGridView.Columns[1].Visible = false;
            AnalyseInfoGridView.Columns[2].Visible = false;

            DisplayUserPlaytimeInChart();

            
        }

        /// <summary>
        /// Occurs when a cell is clicked in AnalyseInfoGridView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnalyseInfoGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (AnalyseInfoGridView.SelectedCells.Count > 0 && _showingPlaytime)
            {
                DisplaySelectedUserInDataGridView();
            }
        }

        #endregion

        /// <summary>
        /// Creates an OpenFileDialog and attempts to read the log file.
        /// </summary>
        private void ReadLog()
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
        /// Displays the users play time in the UserPlaytimeGridView.
        /// </summary>
        private void DisplaySelectedUserInDataGridView()
        {
            var selectedrowindex = AnalyseInfoGridView.SelectedCells[0].RowIndex;
            var selectedRow = AnalyseInfoGridView.Rows[selectedrowindex];

            var accountEmail = Convert.ToString(selectedRow.Cells[0].Value);
            UserPlaytimeGridView.DataSource = _logAnalysis.GetPlaytimeForUser(accountEmail);
        }

        /// <summary>
        /// Displays the DayOfWeek information in the chart.
        /// </summary>
        private void DisplayDaysInChart()
        {
            var daysInfo = _logAnalysis.GetDaysInfo();
            if (daysInfo.Count == 0)
            {
                return;
            }          

            const string serieName = "Day of week";

            FormatChart(serieName);

            foreach (var day in daysInfo)
            {
                AnalyseDataChart.Series[serieName].Points.AddXY(day.DayOfWeek.ToString(), day.Count);
            }
        }

        /// <summary>
        /// Displays the hour information in the chart.
        /// </summary>
        private void DisplayHoursInChart()
        {
            var hoursInfos = _logAnalysis.GetHoursInfos();
            if (hoursInfos.Count == 0)
            {
                return;
            }

            const string serieName = "Hour";

            FormatChart(serieName);

            foreach (var hour in hoursInfos)
            {
                AnalyseDataChart.Series[serieName].Points.AddXY(hour.Hour, hour.Count);
            }
        }

        /// <summary>
        /// Displays the hour information in the chart.
        /// </summary>
        private void DisplayUserPlaytimeInChart()
        {
            var playOccasions = _logAnalysis.GetUsersPlayOccasions();
            if (playOccasions.Count == 0)
            {
                return;
            }

            const string serieName = "Time played";

            FormatChart(serieName);

            foreach (var occasion in playOccasions)
            {
                AnalyseDataChart.Series[serieName].Points.AddXY(occasion.Key.TotalHours, occasion.Count());
            }
        }
        /// <summary>
        /// Formats the chart, creating a new serie based on given values.
        /// </summary>
        /// <param name="serieName">The name of the serie to be created.</param>
        private void FormatChart(string serieName)
        {
            AnalyseDataChart.Series.Add(serieName);    
            AnalyseDataChart.Series[serieName].YValueMembers = "Count";

            AnalyseDataChart.ChartAreas[0].AxisY.Title = "Count";
            AnalyseDataChart.ChartAreas[0].AxisY.TitleAlignment = StringAlignment.Center;
            AnalyseDataChart.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Auto;
        }

        /// <summary>
        /// Clears all existing series from the chart
        /// </summary>
        private void ClearChartSeries()
        {
            foreach (var serie in AnalyseDataChart.Series)
            {
                serie.Points.Clear();
            }
            AnalyseDataChart.Series.Clear();
        }

        /// <summary>
        /// Resets the GUI elements.
        /// </summary>
        private void ResetGui()
        {
            //AnalyseInfoGridView.Rows.Clear();
            //UserPlaytimeGridView.Rows.Clear();
            //TODO ^tar bort allt från bindinglist -> borde cleara datagrid utan att rensa listan.
            AnalyseInfoGridView.DataSource = UserPlaytimeGridView.DataSource = null;


            ReadLogLabel.Text = SortLabel.Text = CalculatePlaytimeLabel.Text = String.Empty;
            ReadLogLabel.Visible = SortLabel.Visible = CalculatePlaytimeLabel.Visible = false;

            ClearChartSeries();
        }
    }
}
