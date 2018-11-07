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
    public partial class ReleaseReportSections : Window, INotifyPropertyChanged
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
                    UpdateList();
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
                if (theStatuses != value)
                {
                    theStatuses = value;
                    OnPropertyChanged("TheStatues");
                }
            }
        }

        private List<Section_Evaluation> sections;

        public List<Section_Evaluation> Sections
        {
            get { return sections; }
            set { sections = value; }
        }
        
        public ReleaseReportSections()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            DataContext = this;
            ste = new SmokeTestsEntities();
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

        private void UpdateList()
        {
            Sections = ste.Section_Evaluations.Where(s => s.Release_ID == TheReport.Release_ID & s.Report_ID == TheReport.Report_ID).ToList();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void BtnView_Click(object sender, RoutedEventArgs e)
        {
            var vwr = new ReportViewer(TheReport.Report.File_Name.Replace(".rdl",""));
            vwr.Show();
        }

        private void BtnDetail_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
