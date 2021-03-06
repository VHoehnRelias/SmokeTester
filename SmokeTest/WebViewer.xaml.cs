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
                    Uri pageUri = new Uri(webPage);
                    webViewer.Source = pageUri;
                    ////webViewer.Source = new Uri(webPage);
                    //string appDir = Environment.CurrentDirectory;
                    //int post = appDir.LastIndexOf("bin\\Debug");
                    //if (post > 0)
                    //{
                    //    string strDir = appDir.Remove(post);
                    //    Uri pageUri = new Uri(strDir + webPage);
                    //    //Uri pageUri = new Uri(@"C:\Users\VHoehn\Desktop\Smoken.html");
                    //    webViewer.Source = pageUri;
                    //}
                    //else
                    //{
                    //    //@"E:/vhoehn/Backup/SmokeTest/SmokeTest/" + 
                    //    Uri pageUri = new Uri(webPage);
                    //    webViewer.Source = pageUri;
                    //}
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
