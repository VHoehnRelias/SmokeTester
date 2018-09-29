using SmokeTestDBClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SmokeTest
{
    /// <summary>
    /// Interaction logic for ReleaseReports.xaml
    /// </summary>
    public partial class ReleaseReports : Window, INotifyPropertyChanged
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

        private Release theRelease;

        public Release TheRelease
        {
            get { return theRelease; }
            set
            {
                if(theRelease != value)
                {
                    theRelease = value;
                    OnPropertyChanged("TheRelease");
                }
            }
        }

        private List<Report_Evaluation> theReports;

        public List<Report_Evaluation> TheReports
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

        private List<Evaluator> theEvaluators;

        public List<Evaluator> TheEvaluators
        {
            get { return theEvaluators; }
            set
            {
                if (theEvaluators != value)
                {
                    theEvaluators = value;
                    OnPropertyChanged("TheEvaluators");
                }
            }
        }

        private List<Status> theStatuses;

        public List<Status> TheStatuses
        {
            get { return theStatuses; }
            set
            {
                if(theStatuses != value)
                {
                    theStatuses = value;
                    OnPropertyChanged("TheStatues");
                }
            }
        }

        private Status theStatus;

        public Status TheStatus
        {
            get { return theStatus; }
            set
            {
                if (theStatus != value)
                {
                    theStatus = value;
                    OnPropertyChanged("TheStatus");
                    LoadReportForStatus();
                }
            }
        }

        private Evaluator theEvaluator;

        public Evaluator TheEvaluator
        {
            get { return theEvaluator; }
            set
            {
                if (theEvaluator != value)
                {
                    theEvaluator = value;
                    OnPropertyChanged("TheEvaluator");
                    LoadReportForEvaluators();
                }
            }
        }
        
        public ReleaseReports(Release theRelease)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            DataContext = this;
            ste = new SmokeTestsEntities();
            TheRelease = theRelease;
            LoadData();
        }

        private void LoadData()
        {
            GetLatestRelease();
            LoadEvaluators();
            LoadStatuses();
            LoadReports();
        }

        private void LoadStatuses()
        {
            TheStatuses = ste.Status.ToList();
        }

        private void LoadEvaluators()
        {
            TheEvaluators = ste.Evaluators.ToList();
        }

        private void GetLatestRelease()
        {
            if(TheRelease is null)
            {
                TheRelease = ste.Releases.ToList().Last();
            }
        }

        private void LoadReports()
        {
            if (TheEvaluator != null)
            {
                if (TheStatus != null)
                {
                    TheReports = ste.Report_Evaluations.Where(a => a.Release_ID == TheRelease.Id && a.Evaluator_ID == TheEvaluator.Id && a.Status_ID == TheStatus.Id).ToList();
                }
                else
                { 
                    TheReports = ste.Report_Evaluations.Where(a => a.Release_ID == TheRelease.Id && a.Evaluator_ID == TheEvaluator.Id).ToList();
                }
            }
            else
            {
                if (TheStatus != null)
                {
                    TheReports = ste.Report_Evaluations.Where(a => a.Release_ID == TheRelease.Id && a.Status_ID == TheStatus.Id).ToList();
                }
                else
                {
                    TheReports = ste.Report_Evaluations.Where(a => a.Release_ID == TheRelease.Id).ToList();
                }
            }
        }

        private void LoadReportForStatus()
        {  
            if (TheStatus != null)
            {
                TheReports = ste.Report_Evaluations.Where(a => a.Release_ID == TheRelease.Id && a.Status_ID == TheStatus.Id).ToList();
            }
        }

        private void LoadReportForEvaluators()
        {
            if (TheEvaluator != null)
            {
                TheReports = ste.Report_Evaluations.Where(a => a.Release_ID == TheRelease.Id && a.Evaluator_ID == TheEvaluator.Id).ToList();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void BtnSections_Click(object sender, RoutedEventArgs e)
        {         
            var itm = (Report_Evaluation)DgReports.SelectedItem;
            var report = new ReleaseReportSections
            {
                TheRelease = theRelease,
                TheReport = itm
            };
            report.Show();
        }

        private void BtnSaveRecord_Click(object sender, RoutedEventArgs e)
        {
            var gitm = (Report_Evaluation)DgReports.SelectedItem;

            //var sitm = ste.Report_Evaluations.Single(a => a.Id == itm.Id);
            //ste.Entry(sitm).CurrentValues.SetValues(itm);
            //var btn = (Button)sender;
            //var itm = TheReports[((int)btn.Tag)-1];

            ste.SaveChanges();
        }

        private void BtnView_Click(object sender, RoutedEventArgs e)
        {
            var vwr = new WebViewer
            {
                //WebPage = "https://relias.atlassian.net/browse/KALAMATA-248"
                WebPage = "ReportsView.html"
            };
            vwr.Show();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            
            ste.SaveChanges();
            StringLabel = "Saved Successfully";
            LoadReports();
        }

        private void BtnSave_LostFoucs(object sender, RoutedEventArgs e)
        {
            StringLabel = "Ready";
        }
    }
}
