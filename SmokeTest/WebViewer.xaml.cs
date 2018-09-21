﻿using System;
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
    public partial class WebViewer : Window, INotifyPropertyChanged
    {
        public WebViewer()
        {
            InitializeComponent();
        }

        private string webPage;

        public string WebPage
        {
            get { return webPage; }
            set
            {
                if (webPage != value)
                {
                    webPage = value;
                    webViewer.Source = new Uri(webPage);
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
