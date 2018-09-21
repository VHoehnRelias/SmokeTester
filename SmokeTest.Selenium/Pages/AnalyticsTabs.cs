using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using Cmt.Online.Web.TestUi.Selenium.Commands;
//using Cmt.Online.Core.Enums;
using OpenQA.Selenium.Support.UI;
using System.IO;
using Cmt.Online.Web.TestUi.Selenium.Enums;

namespace Cmt.Online.Web.TestUi.Selenium.Pages
{
    public static class AnalyticsTabs
    {
        public static string LogPath = @"C:\Temp\LogStatus.txt"; 

        public static WebDriverWait LocalWait = new WebDriverWait(Driver.Instance, new TimeSpan(hours: 0, minutes: 0, seconds: 30));
        public static Dictionary<MeasureType, string> GetMeasureTypeDictionary()
        {
            var md = new Dictionary<MeasureType, string>();
            md.Add(MeasureType.Adh, value: "Adherence");
            md.Add(MeasureType.Adherence, value: "Adherence");
            md.Add(MeasureType.Behavioral, value: "Behavioral Rx");
            md.Add(MeasureType.Bipolar, value: "Bipolar");
            md.Add(MeasureType.Bpm, value: "Behavioral Rx");
            md.Add(MeasureType.BpmCmhc, value: "Behavioral Rx");
            md.Add(MeasureType.Community, value: "Community Health");
            md.Add(MeasureType.Comorbid, value: "Comorbid");
            md.Add(MeasureType.Compliance, value: "Compliance");
            md.Add(MeasureType.Diabetes, value: "Diabetes");
            md.Add(MeasureType.Dmr3700, value: "DMR3700");
            md.Add(MeasureType.ErVisits, value: "ErVisits");
            md.Add(MeasureType.Hospital, value: "Hospital");
            md.Add(MeasureType.Idd, value: "Idd");
            md.Add(MeasureType.Opi, value: "Opioid");
            md.Add(MeasureType.Readmit, value: "Readmit");
            return md;
        }

        public static bool IsAt
        {
            get
            {
                IWebElement profileDiv = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "divReportPeriod")));
                ReadOnlyCollection<IWebElement> divReportPeriod = Driver.Instance.FindElements(By.Id(idToFind: "divReportPeriod"));
                return divReportPeriod.Count > 0;
            }
        }

        public static void PleaseWait()
        {
            Driver.PleaseWait();
        }

        public static bool IsNextTabActive(TabOrdinance tabOrdinance)
        {
            ReadOnlyCollection<IWebElement> mainTabs = Driver.Instance.FindElements(By.ClassName(classNameToFind: "methodOption"));
            IWebElement selectedTab = Driver.Instance.FindElements(By.ClassName(classNameToFind: "active"))[0];
            IWebElement checkTab = mainTabs[(int) tabOrdinance];
            bool selected = (checkTab.Text == selectedTab.Text);
            return selected;
        }

        public static TabCommands GotoTab(TabOrdinance tabOrdinance)
        {
            ReadOnlyCollection<IWebElement> mainTabs = Driver.Instance.FindElements(By.ClassName(classNameToFind: "methodOption"));
            IWebElement selectedTab = mainTabs[(int) tabOrdinance];
            var navCommand = new TabCommands
            {
                TabOrdinance = tabOrdinance,
                SelectedTabText = selectedTab.Text
            };
            selectedTab.Click();
            //Driver.Wait(TimeSpan.FromSeconds(value: 3));
            Driver.PleaseWait();
            return navCommand;
        }

        public static void LogStatus()
        {
            var sw = new StreamWriter(LogPath);
        }


        public static void LogStatus(string msg)
        {
            var sw = new StreamWriter(LogPath);
        }

        public static void LogStatus(string msg, int expectedCount, int actualCount)
        {

        }

        public static int GetExpectedCount(string dictionaryKey)
        {
            return  Driver.TheProfileDictionary[dictionaryKey];
        }

        public static int GetExpectedCount(GridType gt, string dictionaryKey)
        {
            Dictionary<string, int> theDictionary = Driver.TheProfileDictionary;
            switch (gt)
            {
                case GridType.Measure:
                    theDictionary = Driver.TheMeasureDictionary;
                    break;
                case GridType.Registry:
                    theDictionary = Driver.TheRegistryDictionary;
                    break;
            }
            return  theDictionary[dictionaryKey];
        }

        public static int GetExpectedCount(Dictionary<string, int> theDictionary, string dictionaryKey)
        {
            //switch (gt)
            //{
            //    case GridType.Measure:
            //        theDictionary = Driver.TheMeasureDictionary;
            //        break;
            //    case GridType.Registry:
            //        theDictionary = Driver.TheRegistryDictionary;
            //        break;
            //}
            return theDictionary[dictionaryKey];
        }

        public static bool IsNameTabAvailable(MeasureType tab)
        {
            ReadOnlyCollection<IWebElement> mainTabs = Driver.Instance.FindElements(By.ClassName(classNameToFind: "methodOption"));
            IWebElement selectedTab = mainTabs.FirstOrDefault(t => t.Text.ToUpper().Contains(tab.ToString().ToUpper()));
            return selectedTab != null;
        }

        public static bool IsTabSelected(MeasureType tab)
        {
            bool isActive = false;
            ReadOnlyCollection <IWebElement> mainTabs = Driver.Instance.FindElements(By.ClassName(classNameToFind: "methodOption"));
            IWebElement selectedTab = mainTabs.FirstOrDefault(t => t.Text.ToUpper().Contains(tab.ToString().ToUpper()));
            if (selectedTab != null)
            {
                isActive = selectedTab.GetAttribute(attributeName: "class").Contains(value: "active");
            }
            return isActive;
        }

        public static TabCommands GotoNameTab(MeasureType tab)
        {
            Driver.PleaseWait();
            var navCommand = new TabCommands();
            try
            {
                //IWebElement measureDiv = wait.Until(ExpectedConditions.ElementExists(By.ClassName(classNameToFind: "methodOption")));
                ReadOnlyCollection<IWebElement> mainTabs = LocalWait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.ClassName(classNameToFind: "methodOption")));
                //ReadOnlyCollection <IWebElement> mainTabs = Driver.Instance.FindElements(By.ClassName(classNameToFind: "methodOption"));
                string tabName = GetMeasureTypeDictionary()[tab];
                IWebElement selectedTab = mainTabs.FirstOrDefault(t => t.Text.ToUpper() == tabName.ToUpper());
                if (selectedTab != null)
                {
                    navCommand.SelectedTabText = selectedTab.Text;
                    if (!selectedTab.GetAttribute(attributeName: "class").Contains(value: "active"))
                    {
                        selectedTab.Click();
                        Driver.PleaseWait();
                    }
                }
            }
            catch
            {

            }
            return navCommand;
        }

        public static TabCommands GotoLastTab()
        {
            ReadOnlyCollection<IWebElement> mainTabs = Driver.Instance.FindElements(By.ClassName(classNameToFind: "methodOption"));
            IWebElement selectedTab = mainTabs[mainTabs.Count -1];
            var navCommand = new TabCommands
            {
                SelectedTabText = selectedTab.Text
            };
            selectedTab.Click();
            Driver.PleaseWait();
            return navCommand;
        }

        public static bool IsLastTabActive()
        {
            ReadOnlyCollection<IWebElement> mainTabs = Driver.Instance.FindElements(By.ClassName(classNameToFind: "methodOption"));
            IWebElement selectedTab = mainTabs[mainTabs.Count -1];
            return selectedTab.GetAttribute(attributeName: "class").Contains(value: "active");
        }

        public static bool GotoAllTabs()
        {
            ReadOnlyCollection<IWebElement> mainTabs = Driver.Instance.FindElements(By.ClassName(classNameToFind: "methodOption"));
            int operations = 0;
            List<string> tabNames = GetTabNames(mainTabs);

            for (var t = 0; t < mainTabs.Count; t++)
            {
                IWebElement selectedTab = mainTabs[t];
                selectedTab.Click();
                Driver.Wait(TimeSpan.FromSeconds(value: 90));
                IWebElement activeTab = Driver.Instance.FindElements(By.ClassName(classNameToFind: "active"))[0];
                if(tabNames[t] == selectedTab.Text)
                {
                    IWebElement checkTab = mainTabs[t];
                    if(checkTab.Text == activeTab.Text)
                    {
                        operations++;
                    }
                }
            }
            return operations == mainTabs.Count;
        }

        public static bool GetListOfProfiles(MeasureType theMeasureType, AgeType age)
        {
            GotoNameTab(theMeasureType);
            SelectAgeGroup(age);
            return true;
        }

        public static int GetMeasureCount()
        {
            ReadOnlyCollection<IWebElement> ageGroup = Driver.Instance.FindElements(By.ClassName(classNameToFind: "k-pager-info"));
            IWebElement measure = ageGroup.FirstOrDefault(p => p.Location.X > 1000);
            int count = ParseFromLabel(measure.Text);
            return count;
        }

        private static List<string> GetTabNames(ReadOnlyCollection<IWebElement> mainTabs)
        {
            var tabNames = new List<string>();

            for (var t = 0; t < mainTabs.Count; t++)
            {
                tabNames.Add(mainTabs[t].Text);
            }

            return tabNames;
        }

        public static string GetSelectedMeasureType()
        {
            string strType = "";
            ReadOnlyCollection<IWebElement> mainTypes = Driver.Instance.FindElements(By.ClassName(classNameToFind: "active"));
            strType = mainTypes[0].Text;
            return strType; 
        }

        public static string GetSelectedPageType()
        {
            string strType = "";
            ReadOnlyCollection<IWebElement> mainTypes = Driver.Instance.FindElements(By.ClassName(classNameToFind: "active"));
            strType = mainTypes[1].Text;
            return strType; 
        }

        public static bool DoesLogoExists()
        {
            bool exists = false;
            IWebElement selectedTD = Driver.Instance.FindElement(By.Id("td_logo"));
            IWebElement selectedImg = selectedTD.FindElement(By.ClassName("img_Logo"));
            return exists;
        }

        public static string GetSelectedFlaggedType()
        {
            string strType = "";
            ReadOnlyCollection<IWebElement> mainTypes = Driver.Instance.FindElements(By.ClassName(classNameToFind: "active"));
            strType = mainTypes[2].Text;
            return strType; 
        }
        
        public static string GetSelectedChartType()
        {
            string strType = "";
            ReadOnlyCollection<IWebElement> mainTypes = Driver.Instance.FindElements(By.ClassName(classNameToFind: "active"));
            strType = mainTypes[3].Text;
            return strType; 
        }
                
        public static string GetSelectedDisplayType()
        {
            string strType = "";
            ReadOnlyCollection<IWebElement> mainTypes = Driver.Instance.FindElements(By.ClassName(classNameToFind: "active"));
            strType = mainTypes[4].Text;
            return strType; 
        }

        public static string GetSelectedAgeGroup()
        {
            ReadOnlyCollection<IWebElement> ageGroup = Driver.Instance.FindElements(By.ClassName(classNameToFind: "k-input"));
            return ageGroup[0].Text;
        }

        public static string GetSelectedIfTargeted()
        {
            IWebElement cbxTargeted = Driver.Instance.FindElement(By.Id(idToFind: "analTargeted"));
            return cbxTargeted.Selected.ToString();
        }

        public static void SelectPageType(PageType pt)
        {
            IWebElement profileDiv = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "divProfile")));
            switch (pt)
            {
                case PageType.Claims:
                    IWebElement btnC = profileDiv.FindElement(By.Id(idToFind: "btnClaimsData"));
                    btnC.Click();
                    break;
                case PageType.Patients:
                    IWebElement btnPat = profileDiv.FindElement(By.Id(idToFind: "btnPatientData"));
                    btnPat.Click();
                    break;
                case PageType.Prescribers:
                    IWebElement btnP = profileDiv.FindElement(By.Id(idToFind: "btnProviderData"));
                    btnP.Click();
                    break;
            }
        }

        public static bool IsPageTypeSelected(PageType pt)
        {
            IWebElement profileDiv = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "profileDataTypeSelector")));
            IWebElement activeBtn = profileDiv.FindElement(By.ClassName(classNameToFind: "active"));           
            bool page = pt.ToString() == activeBtn.Text;
            return page;
        }
        
        public static bool IsProfileTypeSelected(ProfileType pt)
        {
            bool page = false;
            IWebElement btn = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "btnFlaggedData")));
            switch (pt)
            {
                case ProfileType.Ok:
                    btn = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "btnOkData")));
                    break;
                case ProfileType.Flagged:
                    break;
                case ProfileType.NoData:
                    btn = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "btnNoData")));
                    break;
            }
            if (btn.GetAttribute(attributeName: "class").Contains(value: "active"))
            {
                page = true;
            }
            return page;
        }

        public static void SelectProfileType(ProfileType pt)
        {
            IWebElement btn = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "btnFlaggedData")));
            switch (pt)
            {
                case ProfileType.Ok:
                    btn = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "btnOkData")));
                    break;
                case ProfileType.Flagged:
                    break;
                case ProfileType.NoData:
                    btn = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "btnNoData")));
                    break;
            }
            if (!btn.GetAttribute(attributeName: "class").Contains(value: "active"))
            {
                btn.Click();
            }
        }
          
        public static void SelectTargeted(bool selected)
        {
            IWebElement btn = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "analTargeted")));
            btn.Click();
            Driver.PleaseWait();
        }

        public static void KendoSelectByValue(IWebElement select, string value)
        {
            var selectElement = new SelectElement(select);
            for (int i = 0; i < selectElement.Options.Count; i++)
            {
                if (selectElement.Options[i].GetAttribute("value") == value || selectElement.Options[i].GetAttribute("text") == value)
                {
                    string id = select.GetAttribute("id");
                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript(String.Format("$('#{0}').data('kendoDropDownList').select({1});", id, i));
                    break;
                }
            }
        }
        public static void SelectAgeGroupDoesNotWorkWithKendo(AgeType age)
        {
            IWebElement cbo = LocalWait.Until(ExpectedConditions.ElementExists(By.ClassName(classNameToFind: "ageGroup")));
            IWebElement cboAge = LocalWait.Until(ExpectedConditions.ElementExists(By.ClassName(classNameToFind: "k-dropdown")));
            var ageGroup = new SelectElement(cboAge);
            ageGroup.SelectByValue(age.ToString());
        }
        public static void SelectAgeGroup(AgeType age)
        {
            IWebElement divAge = LocalWait.Until(ExpectedConditions.ElementExists(By.ClassName(classNameToFind: "ageGroup")));
            IWebElement cboAge = LocalWait.Until(ExpectedConditions.ElementExists(By.ClassName(classNameToFind: "k-dropdown")));

            cboAge.Click();

            IWebElement select = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "analAgeGroup-list")));
            ReadOnlyCollection<IWebElement> options = select.FindElements(By.TagName(tagNameToFind: "li"));

            for (int i = 0; i < options.Count; i++)
            {
                if (options[i].Text == age.ToString() && !IsAgeSelected(age))
                {
                    string jscript = string.Format(format: "$('#analAgeGroup').data('kendoDropDownList').select({0});", arg0: i);
                    try
                    {
                        ((IJavaScriptExecutor) Driver.Instance).ExecuteScript(jscript);
                        cboAge.Click();
                        ((IJavaScriptExecutor) Driver.Instance).ExecuteScript(script: "vrhProfileGrid.ChangedAgeGroup();");
                    }
                    catch (Exception ex)
                    {
                        jscript = string.Format(format: "alert('{0}');", arg0: ex.Message);
                        ((IJavaScriptExecutor) Driver.Instance).ExecuteScript(jscript);
                    }
                    break;
                }
            }
        }

        public static void SelectAgency(int agencyId)
        {
            Driver.PleaseWait();
            IWebElement ddAgency = LocalWait.Until(ExpectedConditions.ElementExists(By.ClassName(classNameToFind: "ddAgency")));
            if (ddAgency.GetAttribute("value") != agencyId.ToString())
            {
                var realSelect = new SelectElement(ddAgency);
                if (agencyId > 0)
                {
                    var rnd = new Random();
                    int cnt = realSelect.Options.Count;
                    int num = rnd.Next(maxValue: cnt);
                    realSelect.SelectByIndex(index: num);
                    //realSelect.SelectByValue(agencyId.ToString());
                }
                else
                {
                    realSelect.SelectByIndex(index: 0);
                }                
            }
        }

        public static bool IsAgeSelected(AgeType age)
        {
            Driver.PleaseWait();
            IWebElement divAge = LocalWait.Until(ExpectedConditions.ElementExists(By.ClassName(classNameToFind: "ageGroup")));
            IWebElement cboAge = LocalWait.Until(ExpectedConditions.ElementExists(By.ClassName(classNameToFind: "k-dropdown")));
            IWebElement ageSelected = LocalWait.Until(ExpectedConditions.ElementExists(By.ClassName(classNameToFind: "k-input")));
            return ageSelected.Text == age.ToString();
        }

        //public static bool IsProfileRowSelected()
        //{
        //    IWebElement profileDiv = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "divProfile")));
        //    IWebElement row = profileDiv.FindElement(By.ClassName(classNameToFind: "k-state-selected"));           
        //    bool bln = false;
        //    try
        //    {
        //        if (!(row.TagName == "span"))
        //        {
        //            bln = row.Enabled;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex == null;
        //    }
        //    return bln;
        //}
        
        public static int NumberOfProfilePages()
        {
            int pageCount = 0;

            try
            {
                IWebElement profileDiv = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "divProfile")));
                IWebElement profilePageCount = profileDiv.FindElement(By.XPath(xpathToFind: "//div[@id='profileGrid']/div[3]/span[1]"));
                string strPageCount = profilePageCount.Text.Remove(startIndex: 0, count: 7);
                pageCount = Convert.ToInt16(strPageCount);
            }
            catch (Exception)
            {

            }

            return pageCount;
        }
        
        public static void SelectProfilePage(int pageIndex)
        {
            ////div[@id="profileGrid"]/div[3]/ul/li[3]/a
            int visiblePages = GetNumberOfVisiblePages();
            if (pageIndex > visiblePages)
            {
                pageIndex = 5;
            }
            pageIndex += 1;//Todo: fix
            
            string pageXPath = string.Format(format: "//div[@id='profileGrid']/div[3]/ul/li[{0}]/a", arg0: pageIndex);
            IWebElement profileDiv = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "divProfile")));
            IWebElement profilePage = profileDiv.FindElement(By.XPath(xpathToFind: pageXPath));
            profilePage.Click();
        }

        private static int GetNumberOfVisiblePages()
        {
            string pageXPath = string.Format(format: "//div[@id='profileGrid']/div[3]/ul/li");
            IWebElement profileDiv = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "divProfile")));
            ReadOnlyCollection<IWebElement> profilePages = profileDiv.FindElements(By.XPath(xpathToFind: pageXPath));
            return profilePages.Count;
        }

        public static int NumberOfProfileVisibleRows()
        {
            string pageRowsXPath = string.Format(format: "//div[@id='profileGrid']/div[2]/table/tbody/tr");
            IWebElement profileDiv = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "divProfile")));
            ReadOnlyCollection<IWebElement> profileRows = profileDiv.FindElements(By.XPath(xpathToFind: pageRowsXPath));
            return profileRows.Count;
        }

        public static void GotoFirstProfilePage()
        {
            IWebElement profileGrid = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "profileGrid")));
            IWebElement firstBtn = profileGrid.FindElement(By.ClassName(classNameToFind: "k-pager-first"));
            firstBtn.Click();
        }

        public static void GotoNextProfilePage()
        {
            IWebElement profileGrid = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "profileGrid")));
            string nextBtnXPath = "//*[@id='profileGrid']/div[3]/a[3]";
            IWebElement nextBtn = profileGrid.FindElement(By.XPath(nextBtnXPath));
            nextBtn.Click();
        }

        public static bool IsProfileRowSelected(int rowIndex = 1)
        {
            if (rowIndex == 0) { rowIndex = 1; }
            IWebElement profileDiv = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "divProfile")));
            IWebElement selectedRow = profileDiv.FindElement(By.ClassName(classNameToFind: "k-state-selected"));
            string rowPath = string.Format(format: "//div[@id='profileGrid']/div[2]/table/tbody/tr[{0}]", arg0: rowIndex);
            IWebElement row = profileDiv.FindElement(By.XPath(rowPath));
            bool bln = false;
            try
            {
                if (row == selectedRow)
                {
                    bln = true;
                }
            }
            catch (Exception ex)
            {
                return ex == null;
            }
            return bln;
        }

        public static void SelectProfileRowByIndex(int rowIndex = 0)
        {
            if (rowIndex != 0) { rowIndex = rowIndex - 1; }
            IWebElement profileGrid = LocalWait.Until(ExpectedConditions.ElementExists(By.ClassName(classNameToFind: "k-selectable")));
            ReadOnlyCollection<IWebElement> rows = profileGrid.FindElements(By.TagName(tagNameToFind: "tr"));
            if (rows.Count > rowIndex)
            {
                IWebElement theRow = rows[rowIndex];
                theRow.Click();
            }

        }

        public static void SelectDisplayType(DisplayType dt)
        {
            IWebElement btn = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "btnNumber")));
            if(dt == DisplayType.Percent)
            {
                btn = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "btnPercent")));
            }
            if (!btn.GetAttribute(attributeName: "class").Contains(value: "active"))
            {
                btn.Click();
            }
        }

        public static int GetProfileCount()
        {
            Driver.PleaseWait();
            IWebElement profileDiv = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "divProfile")));
            IWebElement ageGroup = LocalWait.Until(ExpectedConditions.ElementExists(By.ClassName(classNameToFind: "k-pager-info")));
            int count = ParseFromLabel(ageGroup.Text);
            return count;
        }

        public static int ParseFromLabel(string text)
        {
            int count = 0;
            if (text.ToLower() != "no items to display")
            {
                int idxOf = text.IndexOf(value: "of")+ 3;
                string fromOf = text.Substring(idxOf);
                string strWithOutItems = fromOf.TrimEnd("items".ToCharArray());
                string strCount = strWithOutItems.Trim();
                count = Convert.ToInt32(strCount);
            }
            return count;
        }

        public static int GetProfilePageCount()
        {
            Driver.PleaseWait();
            ReadOnlyCollection<IWebElement> ageGroup = Driver.Instance.FindElements(By.ClassName(classNameToFind: "k-pager-input"));
            IWebElement pgr = LocalWait.Until(ExpectedConditions.ElementExists(By.ClassName(classNameToFind: "k-pager-input")));
            int indxOf = pgr.Text.IndexOf(value: "of") + 3;
            string numberOfPages = pgr.Text.Substring(indxOf);
            return Convert.ToInt32(numberOfPages);
        }

        public static int GetProfileMeasureCount(int measureId, int profileCount)
        {
            int count = 0;
            IWebElement profileDiv = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "divProfile")));
            IWebElement profileGrid = LocalWait.Until(ExpectedConditions.ElementExists(By.ClassName(classNameToFind: "k-selectable")));
            
            ReadOnlyCollection<IWebElement> rows = profileGrid.FindElements(By.TagName(tagNameToFind: "tr"));

            foreach (var row in rows)
            {
                ReadOnlyCollection<IWebElement> cells = row.FindElements(By.TagName(tagNameToFind: "td"));

                if (cells.Count > 0 && cells[0].Text == measureId.ToString())
                {
                    count = Convert.ToInt32(cells[2].Text);
                    break;
                }         
            }

            return count;
        }

        public static void SelectProfileRowByMeasureId(int measureId)
        {
            bool measureFound = false;
            int pages = GetProfilePageCount();
            for (var p = 1; p <= pages; p++)
            {
                SelectProfilePage(p);
                Driver.PleaseWait();
                measureFound = SelectTheProfileRow(measureId);
                if (measureFound){ break; }
            }
            Driver.PleaseWait();
        }

        public static void SelectProfileRowByRowNumber(int profileRowNumber)
        {

            int rowNumber = profileRowNumber + 1;
            string strXPath = string.Format(format: "//*[@id=\"profileGrid\"]/div[2]/table/tbody/tr[{0}]", arg0: rowNumber);

            IWebElement profileRow = LocalWait.Until(ExpectedConditions.ElementExists(By.XPath(strXPath)));

            profileRow.Click();
        }

        private static void SelectProfilePageWithoutXPath(int page)
        {
            IWebElement profileDiv = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "divProfile")));
            IWebElement txtPager = LocalWait.Until(ExpectedConditions.ElementExists(By.ClassName(classNameToFind: "k-textbox")));
            txtPager.Clear();
            txtPager.SendKeys(page.ToString());
            txtPager.SendKeys(Keys.Return);
        }

        private static bool SelectTheProfileRow(int measureId)
        {
            bool measureFound = false;

            IWebElement profileDiv = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "divProfile")));
            IWebElement profileGrid = LocalWait.Until(ExpectedConditions.ElementExists(By.ClassName(classNameToFind: "k-selectable")));
            
            ReadOnlyCollection<IWebElement> rows = profileGrid.FindElements(By.TagName(tagNameToFind: "tr"));

            foreach (var row in rows)
            {
                ReadOnlyCollection<IWebElement> cells = row.FindElements(By.TagName(tagNameToFind: "td"));

                if (cells[0].Text == measureId.ToString())
                {
                    row.Click();
                    measureFound = true;
                    break;
                }         
            }
            return measureFound;
        }

        public static bool SelectProfilePdf(int profileRow)
        {
            bool measureFound = false;

            int row = profileRow + 1;

            string strXPath = string.Format(format: "//*[@id=\"profileGrid\"]/div[2]/table/tbody/tr[{0}]/td[5]/div", arg0: row);

            IWebElement pdfLink = LocalWait.Until(ExpectedConditions.ElementExists(By.XPath(strXPath)));

            pdfLink.Click();

            measureFound = true;

            return measureFound;
        }

        public static bool DoesDriverInstanceExist()
        {
            bool exist = false;
            try
            {
                string tile = Driver.Instance.Title;
                exist = true;
            }
            catch (Exception ex)
            {
                return ex == null;
            }
            return exist;
        }

        public static string SelectReportPeriod()
        {
            IWebElement lblReportPeriod = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "lblReportPeriod")));
            LocalWait.Until(ExpectedConditions.TextToBePresentInElement(lblReportPeriod, text: "May 2015"));

            string period = lblReportPeriod.Text;

            return period;
        }

        public static bool OpenPdf(int rowIndex)
        {
            string pdfWindow = "div.k-widget.k-window";
            string pdfTitle = "div.k-window-titlebar.k-header";
            string pdfClose = "a.k-window-action.k-link";
            string pdfDivId = "analPopup"; //object id='pdf' data='https://uat.cmtanalytics.com/pdf/184.pdf';
            bool opened = false;
            string currentHandle = Driver.Instance.CurrentWindowHandle;
            var finder = new PopupWindowFinder(Driver.Instance);
            try
            {
                // Get the current window handle so you can switch back later.

                // Find the element that triggers the popup when clicked on.
                string pdfClick = string.Format(format: "//*[@id='profileGrid']/div[2]/table/tbody/tr[{0}]/td[5]/div", arg0: rowIndex);
                IWebElement element = Driver.Instance.FindElement(By.XPath(xpathToFind: pdfClick));
                element.Click();// Makes the popup window visible

                IWebElement window = Driver.Instance.FindElement(By.CssSelector(pdfWindow));
                IWebElement title = window.FindElement(By.CssSelector(pdfTitle));
                IWebElement close = title.FindElement(By.CssSelector(pdfClose));
                IWebElement pdf = Driver.Instance.FindElement(By.Id(pdfDivId));

                close.Click();

                //string popupWindowHandle = finder.Click(element);
                //Driver.Instance.SwitchTo().Window(popupWindowHandle);
                //Driver.Instance.SwitchTo().Window(Driver.Instance.WindowHandles.ToList().Last());
                //string closeButton = "/html/body/div[4]/div[1]/div/a";
                //IWebElement closeBtn = Driver.Instance.FindElement(By.XPath(xpathToFind: closeButton));
                //closeBtn.Click();
                opened = true;
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
            } 
            finally
            {

                // The Click method of the PopupWindowFinder class will click
                // the desired element, wait for the popup to appear, and return
                // the window handle to the popped-up browser window. Note that
                // you still need to switch to the window to manipulate the page
                // displayed by the popup window.
                //var finder = new PopupWindowFinder(Driver.Instance);
                //string popupWindowHandle = finder.Click(element);

                //Driver.Instance.SwitchTo().Window(popupWindowHandle);
                //string closeButton = "/html/body/div[4]/div[1]/div/a";
                //IWebElement closeBtn = Driver.Instance.FindElement(By.XPath(xpathToFind: closeButton));
                //closeBtn.Click();

                //// Do whatever you need to on the popup browser, then...
                //Driver.Instance.Close();
                Driver.Instance.SwitchTo().Window(currentHandle);
                finder = null;
            }
            return opened;
        }
    }


}
