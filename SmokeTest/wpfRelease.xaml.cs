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
    /// Interaction logic for wpfRelease.xaml
    /// </summary>
    public partial class wpfRelease : Window, INotifyPropertyChanged
    {
        private SmokeTestsEntitiesNew stm;

        private Release theRelease;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Release TheRelease
        {
            get { return theRelease; }
            set
            {
                if (theRelease != value)
                {
                    theRelease = value;
                    OnPropertyChanged("TheRelease");
                }
            }
        }

        public wpfRelease()
        {
            InitializeComponent();
            DataContext = this;
            stm = new SmokeTestsEntitiesNew();
            TheRelease = GetRelease();

        }

        private Release GetRelease()
        {
            var lst = stm.Releases.ToList();
            var pRelease = lst.Last();
            if (pRelease is null)
            {
                pRelease = new Release
                {
                    Date = DateTime.Now
                };
            }
            return pRelease;
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (TheRelease.Id != 0)
            {
                MessageBox.Show(string.Format("{0} already exists.", TheRelease.Name));
                //AddToReportEvaluations();
                AddToSectionEvaluations();
            }
            else
            {
                stm.Releases.Add(TheRelease);
                AddToReportEvaluations();
                AddToSectionEvaluations();
            }
        }

        private void AddToReportEvaluations()
        {
            var lstReports = stm.Reports.ToList();
            foreach (Report rpt in lstReports)
            {
                var re = new Report_Evaluation
                {
                    Release_ID = TheRelease.Id,
                    Report_ID = rpt.Id,
                    Evaluator_ID = 1
                };
                stm.Report_Evaluations.Add(re);
            }
            stm.SaveChanges();
        }
        private void AddToSectionEvaluations()
        {
            var lstSections = stm.Sections.ToList();
            foreach (SmokeTestDBClassLibrary.Section sct in lstSections)
            {
                var re = new Section_Evaluation
                {
                    Release_ID = TheRelease.Id,
                    Section_ID = sct.Id,
                    Report_ID = sct.Report_ID,
                    Evaluator_ID = 1
                };
                stm.Section_Evaluations.Add(re);
            }
            stm.SaveChanges();
        }
    }
}
