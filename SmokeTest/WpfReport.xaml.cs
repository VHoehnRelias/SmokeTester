using SmokeTestDBClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Section = SmokeTestDBClassLibrary.Section;

namespace SmokeTest
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class WpfReport : Window, INotifyPropertyChanged
    {
        private Report theReport = null;

        public Report TheReport
        {
            get { return theReport; }
            set
            {
                if (theReport != value)
                {
                    theReport = value;
                    OnPropertyChanged("TheReport");
                    UpdateGrid();
                }
            }
        }

        private List<Section> reportSections;

        public List<Section> ReportSections
        {
            get { return reportSections; }
            set { reportSections = value; }
        }


        public WpfReport()
        {
            InitializeComponent();
            DataContext = this;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void UpdateGrid()
        {
            var dbr = new SmokeTestsEntities();
            var dset = dbr.Sections.Where(a => a.Report_ID == TheReport.Id);
            ReportSections = dset.ToList();
            //DgReport.ItemsSource = ReportSections;
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
                WebPage = "https://www.cmtanalytics.com/AnalyticsView.aspx"
            };
            vwr.Show();
        }

        private void DgReport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var g = (DataGrid)sender;
            var itm = (SmokeTestDBClassLibrary.Section)g.SelectedItem;
            var report = new ReportSection
            {
                TheSection = itm
            };
            report.Show();
        }
    }
}
