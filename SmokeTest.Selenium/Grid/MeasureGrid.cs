using System;
using System.Text;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace Cmt.Online.Web.TestUi.Selenium.Grid
{
    public class MeasureGrid
    {
        public static WebDriverWait LocalWait = new WebDriverWait(Driver.Instance, new TimeSpan(hours: 0, minutes: 0, seconds: 30));

        private IWebElement GetGrid(string strGridId = "registryGrid")
        {
            Driver.PleaseWait(); 
            IWebElement grid = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(strGridId)));
            return grid;
        }

        private IWebElement GetGridHeader(int colIndex = 3)//FirstName in Measure Grid
        {
            IWebElement gridDiv = GetGrid();
            IWebElement gridHeaderTable = gridDiv.FindElement(By.TagName(tagNameToFind: "thead"));
            IWebElement gridHeaderRow = gridHeaderTable.FindElement(By.TagName(tagNameToFind: "tr"));
            ReadOnlyCollection<IWebElement> gridHeaderCol = gridHeaderRow.FindElements(By.ClassName(classNameToFind: "vrhTopLineHeader"));
            return gridHeaderCol[colIndex];
        }

        private IWebElement GetGridRow(int rowIndex = 1)
        {
            IWebElement gridDiv = GetGrid();
            IWebElement gridBodyTable = gridDiv.FindElement(By.TagName(tagNameToFind: "tbody"));
            ReadOnlyCollection<IWebElement> gridRows = gridBodyTable.FindElements(By.TagName(tagNameToFind: "tr"));
            IWebElement gridRow = gridRows[rowIndex];
            return gridRow;
        }

        private IWebElement GetGridRowColumn(int colIndex = 3, int rowIndex = 1)
        {
            IWebElement gridRow = GetGridRow(rowIndex);
            ReadOnlyCollection<IWebElement> gridColumns = gridRow.FindElements(By.TagName(tagNameToFind: "td"));
            IWebElement gridCell = gridColumns[colIndex]; 
            return gridCell;
        }

        public void SortColumnnAscendingByClickingOnce(int colIndex = 3)
        {
            IWebElement gridHeaderDiv = GetGridHeader(colIndex);
            gridHeaderDiv.Click();
            Driver.PleaseWait();
        }
        //Descending
        public void SortColumnnDescendingByClickingTwice(int colIndex = 3)
        {
            IWebElement gridHeaderDiv = GetGridHeader(colIndex);
            gridHeaderDiv.Click();
            Driver.PleaseWait();
            gridHeaderDiv.Click();
            Driver.PleaseWait();
        }

        public void FilterFirstName(string firstName)
        {
            string xPathFirstName = "/html/body/form/div[4]/div[2]/div[4]/div/div/div[2]/div[2]/div/div/div[1]/div/div/div/div/div/div[2]/div/table/thead/tr/th[5]/a[1]/span";
            IWebElement firstNameHeader = LocalWait.Until(ExpectedConditions.ElementExists(By.XPath(xPathFirstName)));
            firstNameHeader.Click();
            //string xPathFilter = "/html/body/div[3]/div/ul/li[6]/span";
            string xPathFilter = "/html/body/div[3]/div/ul/li[6]";
            IWebElement firstNameFilter = LocalWait.Until(ExpectedConditions.ElementExists(By.XPath(xPathFilter)));
            firstNameFilter.Click();
            string xPathOperator = "/html/body/div[3]/div/ul/li[6]/div/ul/li/div/form/div/span[1]/span/span[1]";
            IWebElement firstNameOperator = LocalWait.Until(ExpectedConditions.ElementExists(By.XPath(xPathOperator)));
            firstNameOperator.Click();
            string xPathEqual = "/html/body/div[3]/div/ul/li[6]/div/ul/div[1]/div/div[2]/ul/li[1]";
            IWebElement firstNameEqual = LocalWait.Until(ExpectedConditions.ElementExists(By.XPath(xPathEqual)));
            firstNameEqual.Click();
            string xPathFirstNameText = "/html/body/div[3]/div/ul/li[6]/div/ul/li/div/form/div/input[1]";
            IWebElement firstNameText = LocalWait.Until(ExpectedConditions.ElementExists(By.XPath(xPathFirstNameText)));
            firstNameText.SendKeys(text: firstName);
            string xPathSelect = "/html/body/div[3]/div/ul/li[6]/div/ul/li/div/form/div/div[2]/button[1]";
            IWebElement firstNameSelect = LocalWait.Until(ExpectedConditions.ElementExists(By.XPath(xPathSelect)));
            firstNameSelect.Click();
        }

        public string GetRowColumnValue(int colIndex, int rowIndex)
        {
            IWebElement gridCell = GetGridRowColumn(colIndex, rowIndex);
            string value = gridCell.Text;
            return value;
        }

        public void FindFirstName(string firstName)
        {
            IWebElement gridHeaderDiv = GetGridHeader(colIndex: 3);
            gridHeaderDiv.Click();
            //gridHeaderCol[3].Click();
            //firstNameHeader.Click();
            ////string xPathFilter = "/html/body/div[3]/div/ul/li[6]/span";
            //string xPathFilter = "/html/body/div[3]/div/ul/li[6]";
            //IWebElement firstNameFilter = LocalWait.Until(ExpectedConditions.ElementExists(By.XPath(xPathFilter)));
            //firstNameFilter.Click();
            //string xPathOperator = "/html/body/div[3]/div/ul/li[6]/div/ul/li/div/form/div/span[1]/span/span[1]";
            //IWebElement firstNameOperator = LocalWait.Until(ExpectedConditions.ElementExists(By.XPath(xPathOperator)));
            //firstNameOperator.Click();
            //string xPathEqual = "/html/body/div[3]/div/ul/li[6]/div/ul/div[1]/div/div[2]/ul/li[1]";
            //IWebElement firstNameEqual = LocalWait.Until(ExpectedConditions.ElementExists(By.XPath(xPathEqual)));
            //firstNameEqual.Click();
            //string xPathFirstNameText = "/html/body/div[3]/div/ul/li[6]/div/ul/li/div/form/div/input[1]";
            //IWebElement firstNameText = LocalWait.Until(ExpectedConditions.ElementExists(By.XPath(xPathFirstNameText)));
            //firstNameText.SendKeys(text: firstName);
            //string xPathSelect = "/html/body/div[3]/div/ul/li[6]/div/ul/li/div/form/div/div[2]/button[1]";
            //IWebElement firstNameSelect = LocalWait.Until(ExpectedConditions.ElementExists(By.XPath(xPathSelect)));
            //firstNameSelect.Click();
        }

        public string GetFirstName(int row)
        {
            string firstName = "";
            string xPathFirstName = string.Format(format: "/html/body/form/div[4]/div[2]/div[4]/div/div/div[2]/div[2]/div/div/div[1]/div/div/div/div/div/div[3]/table/tbody/tr[{0}]/td[5]", arg0: row);
            IWebElement firstNameText = LocalWait.Until(ExpectedConditions.ElementExists(By.XPath(xPathFirstName)));
            firstName = firstNameText.Text;
            return firstName;
        }
    }
}
