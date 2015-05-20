using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace UDPLog
{
    public partial class AnalyseLog : Form
    {
        #region Data

        private LogAnalysis.LogAnalysis _logAnalysis = new LogAnalysis.LogAnalysis();
        private bool _showingPlaytime = false;

        #endregion

        #region Constructor

        public AnalyseLog()
        {
            InitializeComponent();

            //DEBUG!! REMOVE THIS
          //  _logAnalysis.ReadLogFile(
            //    "M:/Desktop/år2/SystemProgramvaruutveckling/UDPLog/UDPLog/UDPLog/bin/Debug/logs/gameplay LoginAndDay.log",
          //      true);
            //DEBUG!! REMOVE THIS TODO
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
                InformationLabel.Visible = true;
                InformationLabel.Text = "Read " + openFileDialog.SafeFileName + " in "
                                    + Math.Round(watch.Elapsed.TotalSeconds, 3) + " seconds.";
            }
        }

        #region Form events

        /// <summary>
        /// Occurs when the CalculatePlaytimeButton is clicked.
        /// Calculates users play time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculatePlaytimeButton_Click(object sender, EventArgs e)
        {
            var watch = Stopwatch.StartNew();
            var numberOfPlayTimesToList = (int)TopPlayTimesToShow.Value;

            _logAnalysis.CalculateUserPlayTime();
            _logAnalysis.SortUsersPlayTime(numberOfPlayTimesToList);

            watch.Stop();

            InformationLabel.Visible = true;
            InformationLabel.Text = "Calculation took " + Math.Round(watch.Elapsed.TotalSeconds, 3) + " seconds.";
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
            InformationLabel.Visible = true;
            InformationLabel.Text = "Sorted information in " + Math.Round(watch.Elapsed.TotalSeconds, 3) + " seconds.";
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

            AnalyseInfoDataGridView.DataSource = _logAnalysis.GetDaysInfo();
            AnalyseInfoDataGridView.Columns[0].HeaderText = "Day of week";

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

            AnalyseInfoDataGridView.DataSource = _logAnalysis.GetHoursInfos();
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

            AnalyseInfoDataGridView.DataSource = _logAnalysis.GetUserPlaytime();
            AnalyseInfoDataGridView.Columns[0].HeaderText = "Account Email";
            AnalyseInfoDataGridView.Columns[1].Visible = false;
            AnalyseInfoDataGridView.Columns[2].Visible = false;

            DisplayUserPlaytimeInChart();
            DisplayTopPlayTimes();
        }

        /// <summary>
        /// Occurs when a cell is clicked in AnalyseInfoGridView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnalyseInfoGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (AnalyseInfoDataGridView.SelectedCells.Count > 0 && _showingPlaytime)
            {
                DisplaySelectedUserInDataGridView();
            }
        }

        #endregion

        #region GUI Related Methods

        /// <summary>
        /// Displays the users play time in the UserPlaytimeGridView.
        /// </summary>
        private void DisplaySelectedUserInDataGridView()
        {
            var selectedrowindex = AnalyseInfoDataGridView.SelectedCells[0].RowIndex;
            var selectedRow = AnalyseInfoDataGridView.Rows[selectedrowindex];

            var accountEmail = Convert.ToString(selectedRow.Cells[0].Value);
            UserPlaytimeDataGridView.DataSource = _logAnalysis.GetPlaytimeForUser(accountEmail);
            UserPlaytimeDataGridView.Columns[0].HeaderText = "Login Time";
            UserPlaytimeDataGridView.Columns[1].HeaderText = "Logoff Time";
            UserPlaytimeDataGridView.Columns[2].HeaderText = "Time Played (hours)";
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
                AnalyseDataChart.Series[serieName].Points.AddXY(occasion.Key, occasion.Count());
            }
        }

        /// <summary>
        /// Displays the top play times in TopPlaytimesDataGridView.
        /// </summary>
        private void DisplayTopPlayTimes()
        {
            var playOccasions = _logAnalysis.GetUsersPlayOccasions();

            TopPlaytimesDataGridView.Visible = true;
            TopPlaytimesDataGridView.Columns.Add("Count", "Count");
            TopPlaytimesDataGridView.Columns.Add("Play Time", "Play Time");

            foreach (var playOccasion in playOccasions)
            {
                TopPlaytimesDataGridView.Rows.Add(playOccasion.Count(), playOccasion.Key);
            }
        }

        /// <summary>
        /// Resets the GUI elements.
        /// </summary>
        private void ResetGui()
        {
            AnalyseInfoDataGridView.DataSource =
                UserPlaytimeDataGridView.DataSource = TopPlaytimesDataGridView.DataSource = null;

            TopPlaytimesDataGridView.Rows.Clear();
            TopPlaytimesDataGridView.Columns.Clear();
            TopPlaytimesDataGridView.Visible = false;

            InformationLabel.Text = String.Empty;
            InformationLabel.Visible = false;

            ClearChartSeries();
        }

        #endregion

        #region Chart Methods

        /// <summary>
        /// Saves the current graph as an image.
        /// </summary>
        private void SaveGraphAsImage()
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif";
            saveFileDialog.Title = "Save an Image File";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var fileStream = (FileStream) saveFileDialog.OpenFile())
                {
                    switch (saveFileDialog.FilterIndex)
                    {
                        case 1:
                            AnalyseDataChart.SaveImage(fileStream, ChartImageFormat.Jpeg);
                            break;
                        case 2:
                            AnalyseDataChart.SaveImage(fileStream, ChartImageFormat.Bmp);
                            break;
                        case 3:
                            AnalyseDataChart.SaveImage(fileStream, ChartImageFormat.Gif);
                            break;
                    }
                }
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
            AnalyseDataChart.Legends[0].IsDockedInsideChartArea = false;

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

        #endregion

        #region File Menu Click Events

        /// <summary>
        /// Occurs when the Read Log File-item is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReadLogFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReadLog();
        }

        /// <summary>
        /// Occurs when the Save Graph-item is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveGraphAsImage();
        }

        /// <summary>
        /// Occurs when the Create Debug-item is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateDebugLogFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var createLogFile = new CreateDebugLogFile();
            createLogFile.ShowDialog();
        }

        #endregion

    }
}
