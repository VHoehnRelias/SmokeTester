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

        public NewReport(SmokeTestsEntities theSte, Report theReport, Release TheRelease)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            DataContext = this;
            ste = theSte;
            CurrentRelease = TheRelease;
            TheReport = theReport;
            UpdateSections();
        }

        public void UpdateSections()
        {
            ReportSections = ste.Sections.Where(a => a.Report_ID == TheReport.Id).OrderBy(a => a.OrderID).ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (TheReport != null)
            { 
                //If report already exists
                if (TheReport.Id != 0)
                {
                    ste.SaveChanges();
                }
                else
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
            var frm = new NewSection(CurrentRelease, ste, TheReport)
            {
                FormParent = this
            };
            frm.Show();
        }

        private void BtnDeleteSection_Click(object sender, RoutedEventArgs e)
        {
            ReportSections.Remove(SelectedSection);
            ste.Sections.Remove(SelectedSection);
            OnPropertyChanged("ReportSections");
            ste.SaveChanges();
        }
    }
}
