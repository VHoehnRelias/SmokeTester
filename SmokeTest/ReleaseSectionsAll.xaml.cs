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
    /// Interaction logic for ReleaseReportSections.xaml
    /// </summary>
    public partial class ReleaseSectionsAll : Window, INotifyPropertyChanged
    {
        private SmokeTestsEntities ste;

        private string rowCount;

        public string RowCount
        {
            get { return rowCount; }
            set
            {
                if (rowCount != value)
                {
                    rowCount = value;
                    OnPropertyChanged("RowCount");
                }
            }
        }
        
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
            set { theRelease = value; }
        }
        
        private Report_Evaluation theReport;

        public Report_Evaluation TheReport
        {
            get { return theReport; }
            set
            {
                if (theReport != value)
                {
                    theReport = value;
                    OnPropertyChanged("TheReport");
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
                    LoadSections();
                }
            }
        }

        private List<Status> theStatuses;

        public List<Status> TheStatuses
        {
            get { return theStatuses; }
            set
            {
                if (theStatuses != value)
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
                    LoadSections();
                }
            }
        }

        private List<Section_Evaluation> sections;

        public List<Section_Evaluation> Sections
        {
            get { return sections; }
            set
            {
                if (sections != value)
                {
                    sections = value;
                    OnPropertyChanged("Sections");
                    RowCount = string.Format("({0} rows)", Sections.Count());
                }
            }
        }
        
        public ReleaseSectionsAll(Release theRelease)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            DataContext = this;
            ste = new SmokeTestsEntities();
            TheRelease = theRelease;
            LoadStatuses();
            LoadEvaluators();
        }

        private void LoadStatuses()
        {
            TheStatuses = ste.Status.ToList();
        }

        private void LoadEvaluators()
        {
            TheEvaluators = ste.Evaluators.ToList();
        }

        private void LoadSections()
        {
            if (TheStatus != null)
            {
                if (TheEvaluator != null)
                {
                    Sections = ste.Section_Evaluations.Where(a => a.Release_ID == TheRelease.Id && a.Evaluator_ID == TheEvaluator.Id && a.Status_ID == TheStatus.Id).ToList();
                }
                else
                {
                    Sections = ste.Section_Evaluations.Where(s => s.Release_ID == TheRelease.Id && s.Status_ID == TheStatus.Id).ToList();
                }
            }
            else
            {
                if (TheEvaluator != null)
                {
                    Sections = ste.Section_Evaluations.Where(a => a.Release_ID == TheRelease.Id && a.Evaluator_ID == TheEvaluator.Id).ToList();
                }
                else
                {
                    Sections = ste.Section_Evaluations.Where(s => s.Release_ID == TheRelease.Id).ToList();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void BtnView_Click(object sender, RoutedEventArgs e)
        {
            var vwr = new WebViewer
            {
                //WebPage = "https://www.cmtanalytics.com/AnalyticsView.aspx"
                WebPage = "http://new.vhoehn.com/Tools/ValStuff.htm"
            };
            vwr.Show();
        }

        private void BtnDetail_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            ste.SaveChanges();
            StringLabel = "Saved Successfully";
            LoadSections();
        }

        private void BtnSave_LostFoucs(object sender, RoutedEventArgs e)
        {
            StringLabel = "Ready";
        }
    }
}
