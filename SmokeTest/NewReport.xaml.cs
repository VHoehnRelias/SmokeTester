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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public NewReport(Report theReport)
        {
            InitializeComponent();
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
                ste.SaveChanges();
            }
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

        }
    }
}
