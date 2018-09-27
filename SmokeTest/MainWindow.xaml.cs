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

        private Release theRelease;

        public Release TheRelease
        {
            get { return theRelease; }
            set
            {
                if (theRelease != value)
                {
                    theRelease = value;
                    OnPropertyChanged("TheRelease");
                    PopulateReportSummaries();
                }
            }
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

        private List<ReportSummmary> sectionSummmaries;

        public List<ReportSummmary> SectionSummmaries
        {
            get { return sectionSummmaries; }
            set
            {
                if (sectionSummmaries != value)
                {
                    sectionSummmaries = value;
                    OnPropertyChanged("SectionSummmaries");
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
        }

        private void PopulateReportSummaries()
        {
            List<Status> lstStatuses = ste.Status.ToList();
            if (TheRelease != null)
            {
                var rs = new List<ReportSummmary>();                
                foreach (Status s in lstStatuses)
                {
                    //new ReportSummmary() { Status = "Good", Count = 20 },
                    var count = 0;
                    try
                    {
                        var someStuff = ste.Report_Evaluations.Where(a => a.Release_ID == TheRelease.Id & a.Status_ID == s.Id).ToList();
                        count = someStuff.Count();
                    }
                    catch (Exception ex){ MessageBox.Show(ex.ToString()); }
                    rs.Add(new ReportSummmary() { Status = s.Name, Count = count });
                };
                var ss = new List<ReportSummmary>();
                foreach (Status s in lstStatuses)
                {
                    var count = 0;
                    //new ReportSummmary() { Status = "Good", Count = 20 },
                    var someStuff = ste.Section_Evaluations.Where(a => a.Release_ID == TheRelease.Id & a.Status_ID == s.Id).ToList();
                    count = someStuff.Count();
                    ss.Add(new ReportSummmary() { Status = s.Name, Count = count });
                };
                ReportSummmaries = rs;
                SectionSummmaries = ss;
            }
        }

        private void BtnEditReports_Click(object sender, RoutedEventArgs e)
        {
            var frm = new ReportEditor();
            frm.Show();
        }

        private void BtnReleaseReports_Click(object sender, RoutedEventArgs e)
        {
            var frm = new ReleaseReports(TheRelease);
            frm.Show();
        }
    }
}
