﻿using SmokeTestDBClassLibrary;
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
    public partial class ReportEditor: Window, INotifyPropertyChanged
    {
        private SmokeTestsEntities ste;

        private string stringLabel = "Ready";

        public string StringLabel
        {
            get { return stringLabel; }
            set
            {
                if (stringLabel != value)
                {
                    stringLabel = value;
                    OnPropertyChanged("StringLabel");
                }
            }
        }

        private bool useCurrentRelease = true;

        public bool UseCurrentRelease
        {
            get { return useCurrentRelease; }
            set
            {
                if (useCurrentRelease != value)
                {
                    useCurrentRelease = value;
                    OnPropertyChanged("UseCurrentRelease");
                }
            }
        }

        private Release currentRelease;

        public Release CurrentRelease
        {
            get { return currentRelease; }
            set
            {
                if (currentRelease != value)
                {
                    currentRelease = value;
                    OnPropertyChanged("CurrentRelease");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<Report> theReports;

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

        public ReportEditor(Release theRelease)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            DataContext = this;
            ste = new SmokeTestsEntities();
            CurrentRelease = theRelease;
            PopulateTheReports();
        }

        private void PopulateTheReports()
        {
            TheReports = ste.Reports.ToList();
        }

        private void BtnNewRelease_Click(object sender, RoutedEventArgs e)
        {
            //Add data for new release
            var newRelease = new WpfRelease();
            newRelease.Show();
        }

        private void BtnReports_Click(object sender, RoutedEventArgs e)
        {
            Release latestRelease = GetLatestRelease();
            var reports = new ReleaseReports(latestRelease);
            reports.Show();
        }

        private Release GetLatestRelease()
        {
            Release theRelease = ste.Releases.ToList().Last();
            return theRelease;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            ste.SaveChanges();
            StringLabel = "Saved Successfully";
        }
        private void BtnSave_LostFoucs(object sender, RoutedEventArgs e)
        {
            StringLabel = "Ready";
        }

        private void BtnSections_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var g = (Button)sender;
                //var itm = (SmokeTestDBClassLibrary.Report)g.SelectedItem;
                var id = (int)g.Tag;
                var itm = TheReports.Single(a => a.Id == id);
                var report = new NewReport(ste, itm, CurrentRelease);
                report.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var newReport = new Report();
            var newRpt = new NewReport(ste, newReport, CurrentRelease);
            newRpt.Show();
        }

        private void CbxRelease_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
