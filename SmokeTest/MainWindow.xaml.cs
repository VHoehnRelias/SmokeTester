using System;
using System.Collections.Generic;
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
using System.Deployment.Internal;
using System.Reflection;

namespace SmokeTest
{
    /// <summary>
    /// Interaction logic for WindowMain.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var strVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            Title = string.Format("Smoking ({0})", strVersion);
        }

        private void BtnEditReports_Click(object sender, RoutedEventArgs e)
        {
            var frm = new ReportEditor();
            frm.Show();
        }

        private void BtnReleaseReports_Click(object sender, RoutedEventArgs e)
        {
            var frm = new ReleaseReports();
            frm.Show();
        }
    }
}
