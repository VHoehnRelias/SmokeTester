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
    /// Interaction logic for WebViewer.xaml
    /// </summary>
    public partial class ReportViewer : Window, INotifyPropertyChanged
    {
        public ReportViewer(string theReport)
        {
            InitializeComponent();
            TheReport = theReport;
        }

        private string theReport;

        public string TheReport
        {
            get { return theReport; }
            set
            {
                if (theReport != value)
                {
                    theReport = value;
                    //"http://wcsqldev02/ReportServer/Pages/ReportViewer.aspx?%2fKalamata%2fKalamataDEV%2fReports%2f{0}&rs:Command=Render"
                    var pathUri = SmokeTest.Properties.Settings.Default.ReportPath;
                    var strUri = string.Format(pathUri, theReport);// "ExecutiveDashboard");
                    Uri pageUri = new Uri(strUri);
                    reportViewer.Source = pageUri;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
