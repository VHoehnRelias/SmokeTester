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
using System.IO;
using System.Xml;

namespace SmokeTest
{
    /// <summary>
    /// Interaction logic for WindowMain.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private SmokeTestsEntities ste;

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
                if (reportSummmaries != value)
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
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            //Assembly ass = Assembly.GetExecutingAssembly();
            //FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(ass.Location);
            //var ver = Assembly.GetExecutingAssembly().GetName().Version;
            //var strVer = fvi.ProductVersion;
            //var strVersion = string.Format("{0}.{1}.{2}.{3}",ver.Major,ver.Minor,ver.Build,ver.Revision);
            ste = new SmokeTestsEntities();
            Title = string.Format("Smoking ({0})", CurrentVersion);
            Releases = GetReleases();
            TheRelease = GetCurrentRelease();
            //PopulateReportSummaries();
        }

        private Release GetCurrentRelease()
        {
            Release rel = Releases.First();
            return rel;
        }

        private List<Release> GetReleases()
        {
            var lst = ste.Releases.ToList();
            return lst;
        }

        private void PopulateReportSummaries()
        {
            var dateString = "2018, 9, 26";
            var goodReport = "";
            var badReport = "";
            var uglyReport = "";
            var untestedReport = "";
            var goodSection = "";
            var badSection = "";
            var uglySection = "";
            var untestedSection = "";

            List<Status> lstStatuses = ste.Status.ToList();
            if (TheRelease != null)
            {
                if (TheRelease.Date != null)
                {
                    dateString = TheRelease.Date.Value.Year.ToString() + ", " + TheRelease.Date.Value.Month.ToString() + ", " + TheRelease.Date.Value.Day.ToString();
                }

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
                    catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                    rs.Add(new ReportSummmary() { Status = s.Name, Count = count });

                    if (s.Id == 1)
                    {
                        untestedReport = count.ToString();
                    }
                    else if (s.Id == 2)
                    {
                        goodReport = count.ToString();
                    }
                    else if (s.Id == 3)
                    {
                        badReport = count.ToString();
                    }
                    else if (s.Id == 4)
                    {
                        uglyReport = count.ToString();
                    }
                };
                var ss = new List<ReportSummmary>();
                foreach (Status s in lstStatuses)
                {
                    var count = 0;
                    //new ReportSummmary() { Status = "Good", Count = 20 },
                    var someStuff = ste.Section_Evaluations.Where(a => a.Release_ID == TheRelease.Id & a.Status_ID == s.Id).ToList();
                    count = someStuff.Count();
                    ss.Add(new ReportSummmary() { Status = s.Name, Count = count });

                    if (s.Id == 1)
                    {
                        untestedSection = count.ToString();
                    }
                    else if (s.Id == 2)
                    {
                        goodSection = count.ToString();
                    }
                    else if (s.Id == 3)
                    {
                        badSection = count.ToString();
                    }
                    else if (s.Id == 4)
                    {
                        uglySection = count.ToString();
                    }
                };
                ReportSummmaries = rs;
                SectionSummmaries = ss;
            }

            string appDir = Environment.CurrentDirectory;
            int post = appDir.LastIndexOf("bin\\Debug");
            string strDir = appDir.Remove(post);
            string templatePageLink = strDir + "StatusTemplate.html";
            string reportStatusPage = strDir + "ReportStatus.html";
            string sectionStatusPage = strDir + "SectionStatus.html";
            string pageSource = System.IO.File.ReadAllText(templatePageLink);
            string reportSource = "";
            string sectionSource = "";

            // Report Status
            reportSource = pageSource.Replace("@Date", dateString);
            reportSource = reportSource.Replace("@Good", goodReport);
            reportSource = reportSource.Replace("@Bad", badReport);
            reportSource = reportSource.Replace("@Ugly", uglyReport);
            reportSource = reportSource.Replace("@Untested", untestedReport);

            // Section Status
            sectionSource = pageSource.Replace("@Date", dateString);
            sectionSource = sectionSource.Replace("@Good", goodSection);
            sectionSource = sectionSource.Replace("@Bad", badSection);
            sectionSource = sectionSource.Replace("@Ugly", uglySection);
            sectionSource = sectionSource.Replace("@Untested", untestedSection);

            System.IO.File.WriteAllText(reportStatusPage, reportSource);
            System.IO.File.WriteAllText(sectionStatusPage, sectionSource);


            Uri pageUriR = new Uri(reportStatusPage);
            Uri pageUriS = new Uri(sectionStatusPage);
            wbReports.Source = pageUriR;
            wbSections.Source = pageUriS;
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

        private void DgReports_Selected(object sender, RoutedEventArgs e)
        {
            var frm = new ReleaseReports(TheRelease);
            frm.Show();
        }

        private void DgSections_Selected(object sender, RoutedEventArgs e)
        {
            var frm = new ReleaseSectionsAll(TheRelease);
            frm.Show();
        }


        private void BtnView_Click_Report(object sender, RoutedEventArgs e)
        {
            var vwr = new WebViewer
            {
                WebPage = "ReportStatus.html"
            };
            vwr.Show();
        }

        private void BtnView_Click_Section(object sender, RoutedEventArgs e)
        {
            var vwr = new WebViewer
            {
                WebPage = "SectionStatus.html"
            };
            vwr.Show();
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var frm = new ReleaseReports(TheRelease);
            // frm.TheStatus = ste.Status.Where(a => a.Release_ID == TheRelease.Id && a.Status_ID == TheStatus.Id)
            frm.Show();
        }
    }
}
