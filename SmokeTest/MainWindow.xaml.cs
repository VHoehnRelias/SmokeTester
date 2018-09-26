using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Deployment.Application;
using System.Reflection;
using System.Diagnostics;
using System.ComponentModel;
using SmokeTestDBClassLibrary;

namespace SmokeTest
{
    /// <summary>
    /// Interaction logic for WindowMain.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private SmokeTestsEntitiesNew ste;

        public string CurrentVersion
        {
            get
            {
                return ApplicationDeployment.IsNetworkDeployed
                       ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
                       : Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        private List<Release> releases;

        public List<Release> Releases
        {
            get { return releases; }
            set { releases = value; }
        }

        private List<ReportSummmary> reportSummmaries;

        public List<ReportSummmary> ReportSummmaries
        {
            get { return reportSummmaries; }
            set
            {
                if(reportSummmaries != value)
                {
                    reportSummmaries = value;
                    OnPropertyChanged("ReportSummmaries");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            //Assembly ass = Assembly.GetExecutingAssembly();
            //FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(ass.Location);
            //var ver = Assembly.GetExecutingAssembly().GetName().Version;
            //var strVer = fvi.ProductVersion;
            //var strVersion = string.Format("{0}.{1}.{2}.{3}",ver.Major,ver.Minor,ver.Build,ver.Revision);
            ste = new SmokeTestsEntitiesNew();
            Title = string.Format("Smoking ({0})", CurrentVersion);
            Releases = GetReleases();
            PopulateReportSummaries();
        }
        private List<Release> GetReleases()
        {
            var lst = ste.Releases.ToList();
            return lst;
            //var pRelease = lst.Last();
            //if (pRelease is null)
            //{
            //    pRelease = new Release
            //    {
            //        Date = DateTime.Now
            //    };
            //}
            //return pRelease;
        }

        private void PopulateReportSummaries()
        {
            ReportSummmaries = new List<ReportSummmary>
            {
                new ReportSummmary() { ReportType = "Good", TypeCount = 20 },
                new ReportSummmary() { ReportType = "Bad", TypeCount = 15 },
                new ReportSummmary() { ReportType = "Ugly", TypeCount = 5 }
            };
        }

        private void BtnEditReports_Click(object sender, RoutedEventArgs e)
        {
            var frm = new ReportEditor();
            frm.Show();
        }

        private void BtnReleaseReports_Click(object sender, RoutedEventArgs e)
        {
            var frm = new ReleaseReports();
            frm.Show();
        }
    }
}
