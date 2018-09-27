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
    /// Interaction logic for NewReport.xaml
    /// </summary>
    public partial class NewReport : Window, INotifyPropertyChanged
    {
        private SmokeTestsEntitiesNew ste;

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

        private Report theReport = new Report();

        public Report TheReport
        {
            get { return theReport; }
            set
            {
                if(theReport != value)
                {
                    theReport = value;
                    OnPropertyChanged("TheReport");
                }

            }
        }
        
        private List<SmokeTestDBClassLibrary.Section> reportSections;

        public List<SmokeTestDBClassLibrary.Section> ReportSections
        {
            get { return reportSections; }
            set
            {
                if(reportSections != value)
                {
                    reportSections = value;
                    OnPropertyChanged("ReportSections");
                }
            }
        }

        private SmokeTestDBClassLibrary.Section selectedSection;

        public SmokeTestDBClassLibrary.Section SelectedSection
        {
            get { return selectedSection; }
            set
            {
                if(selectedSection != value)
                {
                    selectedSection = value;
                    OnPropertyChanged("SelectedSection");
                }
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public NewReport(Report theReport)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            DataContext = this;
            ste = new SmokeTestsEntitiesNew();
            TheReport = theReport;
            UpdateSections();
        }

        public void UpdateSections()
        {
            ReportSections = ste.Sections.Where(a => a.Report_ID == TheReport.Id).ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (TheReport != null)
            {
                ste.Reports.Add(TheReport);
                if(TheReport.File_Name != null && TheReport.Left_Navigation_Menu_location != null && TheReport.Report_Name != null)
                {
                    ste.SaveChanges();
                    StringLabel = "Saved Successfully";
                } else
                {
                    StringLabel = "Can't be blank";
                }
                
            }
        }

        private void BtnSave_LostFoucs(object sender, RoutedEventArgs e)
        {
            StringLabel = "Ready";
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (TheReport.Id != 0)
            {
                ste.Reports.Remove(TheReport);
                ste.SaveChanges();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Add Section open NewSection Form
            var frm = new NewSection(TheReport)
            {
                FormParent = this
            };
            frm.Show();
        }

        private void BtnDeleteSection_Click(object sender, RoutedEventArgs e)
        {
            ste.Sections.Remove(SelectedSection);
        }
    }
}
