using SmokeTestDBClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SmokeTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private SmokeTestsEntitiesNew ste;

        private List<Report> theReports;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<Report> TheReports
        {
            get { return theReports; }
            set
            {
                if (theReports != value)
                {
                    theReports = value;
                    OnPropertyChanged("TheReports");
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            ste = new SmokeTestsEntitiesNew();
            PopulateTheReports();
        }

        private void PopulateTheReports()
        {

            TheReports = ste.Reports.ToList();
        }

        private void DgSummary_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var g = (DataGrid)sender;
            var itm = (SmokeTestDBClassLibrary.Report)g.SelectedItem;
            var report = new WpfReport
            {
                TheReport = itm
            };
            report.Show();
        }

        private void BtnNewRelease_Click(object sender, RoutedEventArgs e)
        {
            //Add data for new release
            var newRelease = new wpfRelease();
            newRelease.Show();
        }

        private void BtnReports_Click(object sender, RoutedEventArgs e)
        {
            var reports = new ReleaseReports();
            reports.Show();
        }
    }
}
