using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using OpenQA.Selenium.Remote;
using Cmt.Online.Web.TestUi.Selenium.Enums;

namespace Cmt.Online.Web.TestUi.Selenium
{
    public class Driver
    {
        //string webSite = "https://morvmdev01:9292/";
        //string webSite = "https://morvmwebqa01:445/";

        public static IWebDriver Instance { get; set; }

        public static void Initialize()
        {
            TheEnvironment = GetEnvironment(EnvironmentType.Qa01);
            TheLoginData = TheEnvironment;

            InternetExplorerHelper.SetZoom100();
            string appPath = AppDomain.CurrentDomain.BaseDirectory.Trim();
            if (appPath == "")
            {
                appPath = "https://uat.cmtanalytics.com";
            }
            //Cmt.Online
            //string iePath = appPath.Substring(startIndex: 0, length: appPath.IndexOf(value: "TestResults"))+ @"packages\Selenium.WebDriver.IEDriver.2.53.1.1\driver";
            string iePath = "C:\\CMT\\Main\\" + @"packages\Selenium.WebDriver.IEDriver.2.53.1.1\driver";
            var opts = new InternetExplorerOptions
            {
                PageLoadStrategy = InternetExplorerPageLoadStrategy.Eager,
                IgnoreZoomLevel = true,
                EnableNativeEvents = false,
                EnsureCleanSession = true,
                RequireWindowFocus = false,
                InitialBrowserUrl = TheLoginData.WebSite + "/Login.aspx"
            };

            Instance = new InternetExplorerDriver(internetExplorerDriverServerDirectory: iePath,options: opts);
            Instance.Manage().Timeouts().ImplicitlyWait(new TimeSpan(hours: 0, minutes: 0, seconds: 15));
            Instance.FindElement(By.TagName("html")).SendKeys(Keys.Control + "0");

            //Instance = new FirefoxDriver();
        }

        public static void Initialize(EnvironmentType et)
        {
            TheEnvironment = GetEnvironment(et);
            TheLoginData = TheEnvironment;
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string iePath = appPath.Substring(startIndex: 0, length: appPath.IndexOf(value: "Cmt.Online")) + @"packages\Selenium.WebDriver.IEDriver.2.53.1.1\driver";
            Instance = new InternetExplorerDriver(iePath)
            {
                Url = TheLoginData.WebSite + "/Login.aspx"
            };
            //Instance = new FirefoxDriver();
        }

        public static IWebDriver Initialize(string url)
        {
            InternetExplorerHelper.SetZoom100();

            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string iePath = appPath.Substring(startIndex: 0, length: appPath.IndexOf(value: "Cmt.Online")) + @"packages\Selenium.WebDriver.IEDriver.2.53.1.1\driver";

            var opts = new InternetExplorerOptions
            {
                PageLoadStrategy = InternetExplorerPageLoadStrategy.Eager,
                IgnoreZoomLevel = true,
                EnableNativeEvents = false,
                EnsureCleanSession = true,
                RequireWindowFocus = false,
                InitialBrowserUrl = url
            };

            Instance = new InternetExplorerDriver(internetExplorerDriverServerDirectory: iePath, options: opts);
            Instance.Manage().Timeouts().ImplicitlyWait(new TimeSpan(hours: 0, minutes: 0, seconds: 15));
            return Instance;
        }

        internal static void ExecuteJavascript(string strJavaScript)
        {
            ((IJavaScriptExecutor) Instance).ExecuteScript(script: strJavaScript);

        }

        private static Enums.Environment GetEnvironment(EnvironmentType environmentType)
        {
            Enums.Environment ld = Enums.Environments.GetEnvironment(environmentType);
            return ld;
        }

        private static Enums.Environment theEnvironment;

        public static Enums.Environment TheEnvironment
        {
            get { return theEnvironment; }
            set { theEnvironment = value; }
        }

        /// <summary>
        /// PleaseWait.Until(driver.Instance => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        /// </summary>
        /// <returns></returns>
        //public static void PleaseWait(int sec = 15)
        //{
        //    Wait(new TimeSpan(hours: 0, minutes: 0, seconds: sec));
        //    //IWait<IWebDriver> wait = new OpenQA.Selenium.Support.UI.WebDriverWait(Instance, TimeSpan.FromSeconds(30.00));
        //    //wait.Until(driver1 => ((IJavaScriptExecutor)Instance).ExecuteScript("return document.readyState").Equals("complete"));
        //    //return wait;
        //}

        public static WebDriverWait TheWait()
        {
            var wait = new WebDriverWait(Driver.Instance, new TimeSpan(hours: 0, minutes: 0, seconds: 10));           
            return wait;
        }

        public static void PleaseWait(int secs = 15)
        {
            Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(value: secs));
            //IWait<IWebDriver> wait = new OpenQA.Selenium.Support.UI.WebDriverWait(Instance, TimeSpan.FromSeconds(30.00));
            //return wait;
            //wait.Until(driver1 => ((IJavaScriptExecutor)Instance).ExecuteScript("return document.readyState").Equals("complete"));
        }

        private static LoginData FillDataFromConfigFile()
        {
            var ld = new LoginData();
            string strLine;
            using (var sr = new StreamReader(path: @"C:\CMT\App.CmtOnline\Branches\Environment\QA01\Cmt.Online.Core.Test\Resources\AnalyticsTest.config"))
            {
                while ((strLine = sr.ReadLine()) != null)
                {
                    string[] spltLine = strLine.Split(separator: new char[] { ';' });
                    switch (spltLine[0])
                    {
                        case "WebSite":
                            ld.WebSite = spltLine[1];
                            break;
                        case "ClientCode":
                            ld.ClientCode = spltLine[1];
                            break;
                        case "UserId":
                            ld.UserId = spltLine[1];
                            break;
                        case "Database":
                            ld.Database = spltLine[1];
                            break;
                        case "DictionaryFile":
                            ld.DictionaryFile = spltLine[1];
                            break;
                        case "Password":
                            ld.Password = spltLine[1];
                            break;
                        case "Server":
                            ld.Server = spltLine[1];
                            break;
                        case "ConfigurationFile":
                            ld.ConfigurationFile = spltLine[1];
                            break;
                    }
                }
            }
            return ld;
        }

        public static void Close()
        {
            if (Instance != null)
            {
                Instance.Close();
                Instance.Quit();
                //Instance.Close();            
                Instance.Dispose();
            }
        }

        public static void TurnOnWait(int seconds = 5)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
            Instance.Manage().Timeouts().ImplicitlyWait(timeSpan);
            //Thread.Sleep(timeSpan);
        }

        public static void TurnOffWait()
        {
            //Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            Thread.Sleep(millisecondsTimeout: 0);
        }

        public static void Wait(TimeSpan timeSpan)
        {
            //Instance.Manage().Timeouts().ImplicitlyWait(timeSpan);
            Thread.Sleep(timeSpan);
        }

        private static Enums.Environment theLoginData;

        //public static EnvironmentType TheEnvironment { get; private set; }
        //public static object TheLoginData { get; private set; }

        public static Enums.Environment TheLoginData
        {
            get { return theLoginData; }
            set { theLoginData = value; }
        }

        private static Dictionary<string, int> theDictionary;

        public static Dictionary<string, int> TheProfileDictionary
        {
            get { return theDictionary; }
            set { theDictionary = value; }
        }
       
        private static Dictionary<string, int> theMeasureDictionary;
        public static Dictionary<string, int> TheMeasureDictionary
        {
            get { return theMeasureDictionary; }
            set { theMeasureDictionary = value; }
        }

        private static Dictionary<string, int> theRegistryDictionary;
        public static Dictionary<string, int> TheRegistryDictionary
        {
            get { return theRegistryDictionary; }
            set { theRegistryDictionary = value; }
        }

    }

    public class LoginData
    {
        public string WebSite { get; set; }

        public string ClientCode { get; set; }

        public string UserId { get; set; }

        public string Password { get; set; }

        public string Server { get; set; }

        public string Database { get; set; }

        public string DictionaryFile { get; set; }

        public string ConfigurationFile { get; set; }

    }
}