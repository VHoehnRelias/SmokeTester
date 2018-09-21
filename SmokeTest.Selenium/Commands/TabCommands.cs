//using Cmt.Online.Core.Enums;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmt.Online.Web.TestUi.Selenium.Commands
{
    
    public class TabCommands
    {
        private IWebElement currentTab;

        public string SelectedTabText { get; internal set; }
        public TabOrdinance TabOrdinance { get; internal set; }

        public TabCommands()
        {

        }

        public TabCommands MainTabGroup()
        {
            return this;
        }

        public TabCommands Tab(TabOrdinance tab)
        {
            currentTab = Driver.Instance.FindElement(By.ClassName(classNameToFind: "analysisTabs"));
            return this;
        }

        public void Goto()
        {
            currentTab.Click();
        }
        public bool IsActive()
        {
            ReadOnlyCollection<IWebElement> mainTabs = Driver.Instance.FindElements(By.ClassName(classNameToFind: "methodOption"));
            IWebElement selectedTab = Driver.Instance.FindElements(By.ClassName(classNameToFind: "active"))[0];
            //bool selected = (mainTabs[(int) this.TabOrdinance].Text == this.SelectedTabText);
            bool selected = (mainTabs[(int) this.TabOrdinance].Text == selectedTab.Text);
            return selected;
        }

    }

    public enum TabOrdinance
    {
        First = 0,
        Second = 1,
        Third = 2,
        Forth = 3
    }
}
