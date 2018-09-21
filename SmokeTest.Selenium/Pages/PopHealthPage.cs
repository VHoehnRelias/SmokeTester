using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmt.Online.Web.TestUi.Selenium.Pages
{
    public static class PopHealthPage
    {
        public static WebDriverWait LocalWait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(value: 5));
        public static void OpenPage()
        {
            ReadOnlyCollection<IWebElement> mainTabs = Driver.Instance.FindElements(By.ClassName(classNameToFind: "methodOption"));

            IWebElement aPopHealth = Driver.Instance.FindElement(By.ClassName(classNameToFind: "paIcon"));
            aPopHealth.Click();
        }
    }
}
