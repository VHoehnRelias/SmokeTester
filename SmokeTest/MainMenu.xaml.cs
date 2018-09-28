using SmokeTestDBClassLibrary;
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

namespace SmokeTest
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        SmokeTestsEntities ste;

        private List<Release> theReleases;

        public List<Release> TheReleases
        {
            get { return theReleases; }
            set { theReleases = value; }
        }

        public MainMenu()
        {
            InitializeComponent();
            DataContext = this;
            ste = new SmokeTestsEntities();
            PopulateReleases();
        }

        private void PopulateReleases()
        {
            TheReleases = ste.Releases.ToList();
        }

        private void BtnReport_Click(object sender, RoutedEventArgs e)
        {
            int num = 4;
            var random = new Random(num);
            //for(int x = 1; x < 10; x++)//
            foreach (SmokeTestDBClassLibrary.Report_Evaluation re in ste.Report_Evaluations.Where(a => a.Release_ID == 1).ToList())
            {
                num = random.Next(2, 4);
                re.Evaluator_ID = num;
            }
            ste.SaveChanges();
            MessageBox.Show("finished");
        }

        private void BtnSections_Click(object sender, RoutedEventArgs e)
        {
            int num = 4;
            var random = new Random(num);
            //for(int x = 1; x < 10; x++)//
            foreach( SmokeTestDBClassLibrary.Section_Evaluation se in ste.Section_Evaluations.Where(a => a.Report_ID == 1).ToList())
            {
                num = random.Next(2, 4);
                se.Status_ID = num;
            }
            ste.SaveChanges();
            MessageBox.Show("finished");
        }
    }
}
