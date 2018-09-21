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
        private SmokeTestsEntitiesNew stm;

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

        private List<Section_Evaluation> sections;

        public List<Section_Evaluation> Sections
        {
            get { return sections; }
            set { sections = value; }
        }
        
        public ReleaseReportSections()
        {
            InitializeComponent();
            DataContext = this;
            stm = new SmokeTestsEntitiesNew();
        }

        private void UpdateList()
        {
            Sections = stm.Section_Evaluations.Where(s => s.Release_ID == TheReport.Release_ID & s.Report_ID == TheReport.Report_ID).ToList();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
