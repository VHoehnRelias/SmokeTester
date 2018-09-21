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
    public static class AnalyticTestHelper
    {
        public static WebDriverWait LocalWait = new WebDriverWait(Driver.Instance, new TimeSpan(hours: 0, minutes: 0, seconds: 30));
        public static int GetProfileGridRowPatientCount()
        {
            Driver.PleaseWait();
            int count = 0;

            IWebElement profileDiv = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "divProfile")));
            IWebElement profileGrid = profileDiv.FindElement(By.ClassName(classNameToFind: "k-selectable"));

            //ReadOnlyCollection<IWebElement> rows = profileGrid.FindElements(By.TagName(tagNameToFind: "tr"));
            IWebElement selectedRow = profileDiv.FindElement(By.ClassName(classNameToFind: "k-state-selected"));

            try
            {
                ReadOnlyCollection<IWebElement> cells = selectedRow.FindElements(By.TagName(tagNameToFind: "td"));
                if (cells.Count > 0)
                {
                    count = Convert.ToInt32(cells[2].Text);
                }
            }
            catch (Exception) {}
            return count;
        }
        public static int GetProfileGridRowPatientCount(int profileRowNumber)
        {
            Driver.PleaseWait();
            int count = 0;

            string rowXPath = string.Format(format: "//*[@id=\"profileGrid\"]/div[2]/table/tbody/tr[{0}]", arg0: profileRowNumber);                       
            IWebElement selectedRow = LocalWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(rowXPath)));

            selectedRow.Click();
            Driver.PleaseWait();

            string columnXPath = string.Format(format: "//*[@id=\"profileGrid\"]/div[2]/table/tbody/tr[{0}]/td[3]", arg0: profileRowNumber);
            IWebElement numberColumn = LocalWait.Until(ExpectedConditions.ElementExists(By.XPath(columnXPath)));

            count = Convert.ToInt32(numberColumn.Text);
            
            return count;
        }

        public static int GetMeasuresGridRowCount()
        {
            int count = 0;
            Driver.PleaseWait();

            try
            {
                //IWebElement measureOuter = wait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "measureDiv")));
                IWebElement measureDiv = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "divMeasure")));
                char[] chr = "...".ToCharArray();
                string[] par = measureDiv.Text.Split(separator: chr ).Last().Split(separator: new char[] { ' ' });
                int len = par.Length - 2;
                count = Convert.ToInt32(par[len]);
            }
            catch (Exception ex)
            {
                if (ex == null) { return 0; };
            }
            //ReadOnlyCollection<IWebElement> ageGroup = Driver.Instance.FindElements(By.ClassName(classNameToFind: "k-pager-info"));
            //IWebElement measure = ageGroup.FirstOrDefault(p => p.Location.X > 1000);
            //int count = AnalyticsTabs.ParseFromLabel(measure.Text);
            return count;
        }

        private static ReadOnlyCollection<IWebElement> GetRows()
        {
            IWebElement profileGrid = LocalWait.Until(ExpectedConditions.ElementExists(By.ClassName(classNameToFind: "k-selectable")));
            ReadOnlyCollection<IWebElement> rows = profileGrid.FindElements(By.TagName(tagNameToFind: "tr"));
            return rows;
        }

        private static ReadOnlyCollection<IWebElement> GetRowCells(int rowIndex = 1)
        {
            ReadOnlyCollection<IWebElement> rows = GetRows();
            IWebElement theRow = rows[rowIndex-1];
            ReadOnlyCollection<IWebElement> theCells = theRow.FindElements(By.TagName("td"));
            return theCells;
        }

        public static int GetRowQualityIndicatorId(int rowIndex = 1)
        {
            int qId = 0;
            ReadOnlyCollection<IWebElement> theCells = GetRowCells(rowIndex);
            IWebElement theCell = theCells[0];
            string strQId = theCell.Text;
            qId = Convert.ToInt32(strQId);
            return qId;
        }

        public static string GetQualityIndicatorDescription(int rowIndex)
        {
            string description = "";
            ReadOnlyCollection<IWebElement> theCells = GetRowCells(rowIndex);
            IWebElement theCell = theCells[1];
            description = theCell.Text;
            return description;
        }
    }
}
