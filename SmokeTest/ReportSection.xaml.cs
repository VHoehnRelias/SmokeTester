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
using SmokeTestDBClassLibrary;

namespace SmokeTest
{
    /// <summary>
    /// Interaction logic for ReportSection.xaml
    /// </summary>
    public partial class ReportSection : Window, INotifyPropertyChanged
    {
        private SmokeTestDBClassLibrary.Section theSection;

        public SmokeTestDBClassLibrary.Section TheSection   
        {
            get { return theSection; }
            set { theSection = value; }
        }


        public ReportSection()
        {
            InitializeComponent();
            DataContext = this;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var sec = new Section_Evaluation
            {
                Evaluator_ID = 1,
                Comment = "Testing 123"
            };

            theSection.Section_Evaluation.Add(sec);

        }
    }
}
