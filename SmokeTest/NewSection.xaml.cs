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
    /// Interaction logic for NewSection.xaml
    /// </summary>
    public partial class NewSection : Window, INotifyPropertyChanged
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

        private Report theReport;

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

        private NewReport formParent;

        public NewReport FormParent
        {
            get { return formParent; }
            set { formParent = value; }
        }

        private SmokeTestDBClassLibrary.Section theSection;

        public SmokeTestDBClassLibrary.Section TheSection
        {
            get { return theSection; }
            set
            {
                if(theSection != value)
                {
                    theSection = value;
                    OnPropertyChanged("TheSection");
                }
            }
        }
        
        public NewSection(Release theRelease, SmokeTestsEntities oSte,Report theReport)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            DataContext = this;
            CurrentRelease = theRelease;
            TheReport = theReport;
            ste = oSte;
            var newOrderID = ste.Sections.Where(s => s.Report_ID == TheReport.Id).Max(s => s.OrderID) + 1;
            TheSection = new SmokeTestDBClassLibrary.Section
            {
                Report_ID = theReport.Id,
                OrderID = newOrderID
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            ste.Sections.Add(TheSection);
            var newSectionEval = new Section_Evaluation
            {
                Evaluator_ID = 1,
                Status_ID = 1,
                Release_ID = CurrentRelease.Id,
                Report_ID = TheReport.Id           
            };
            ste.Section_Evaluations.Add(newSectionEval);
            ste.SaveChanges();
            StringLabel = "Saved Successfully";
            formParent.UpdateSections();
            //formParent.DgReport.Items.Refresh();
        }

        private void BtnSave_LostFoucs(object sender, RoutedEventArgs e)
        {
            StringLabel = "Ready";
        }
    }
}
