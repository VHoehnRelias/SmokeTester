using SmokeTestDBClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace SmokeTest
{
    /// <summary>
    /// Interaction logic for ReleaseReports.xaml
    /// </summary>
    public partial class ReleaseReports : Window, INotifyPropertyChanged
    {
        private SmokeTestsEntitiesNew ste;

        private Release theRelease;

        public Release TheRelease
        {
            get { return theRelease; }
            set { theRelease = value; }
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

        public ReleaseReports()
        {
            InitializeComponent();
            DataContext = this;
            ste = new SmokeTestsEntitiesNew();
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
            TheReports = ste.Report_Evaluations.Where(a => a.Release_ID == TheRelease.Id).ToList();
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
                WebPage = "https://www.cmtanalytics.com/AnalyticsView.aspx"
            };
            vwr.Show();
        }
    }
}
