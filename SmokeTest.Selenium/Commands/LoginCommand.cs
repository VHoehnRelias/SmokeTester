using Cmt.Online.Web.TestUi.Selenium.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Cmt.Online.Web.TestUi.Selenium.Commands
{
    public class LoginCommand
    {
        private readonly string clientName;
        private readonly string userName;
        private string password;
        private WebDriverWait LocalWait = new WebDriverWait(Driver.Instance, new TimeSpan(hours: 0, minutes: 0, seconds: 30));


        public LoginCommand(string clientName, string userName)
        {
            this.clientName = clientName;
            this.userName = userName;
        }

        public LoginCommand WithPassword(string password)
        {
            this.password = password;
            return this;
        }

        public void Login()
        {
            if (Driver.Instance.GetType().Name == "InternetExplorerDriver")
            {
                if (!(Driver.TheEnvironment.Type == EnvironmentType.UatCmt) && !(Driver.TheEnvironment.Type == EnvironmentType.Production) && !(Driver.TheEnvironment.Type == EnvironmentType.Local))
                {
                    try
                    {
                        //IWebElement overrideLink = Driver.Instance.FindElement(By.Id(idToFind: "overridelink"));
                        IWebElement overrideLink = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "overridelink")));
                        overrideLink.Click();
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            Driver.PleaseWait();
            IWebElement clientInput = Driver.Instance.FindElement(By.Id(idToFind: "code"));
            //IWebElement clientInput = LocalWait.Until(ExpectedConditions.ElementExists(By.XPath(xpathToFind: "/html/body/form/div[3]/div/div[1]/div[3]/table/tbody/tr[1]/td[2]/input")));
            clientInput.Clear();
            clientInput.SendKeys(clientName);
            IWebElement userInput = Driver.Instance.FindElement(By.Id(idToFind: "email"));
            //IWebElement userInput = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "email")));
            userInput.Clear();
            userInput.SendKeys(userName);
            IWebElement passwordInput = Driver.Instance.FindElement(By.Id(idToFind: "password"));
            //IWebElement passwordInput = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "password")));
            passwordInput.Clear();
            passwordInput.SendKeys(password);

            IWebElement loginButton = Driver.Instance.FindElement(By.Id(idToFind: "btn_Login"));
            //IWebElement loginButton = LocalWait.Until(ExpectedConditions.ElementExists(By.XPath(xpathToFind: "/html/body/form/div[3]/div/div[1]/div[3]/div[2]/input[1]")));
            Driver.PleaseWait();
            loginButton.Click();

            Driver.PleaseWait();

            IWebElement confirmButton = Driver.Instance.FindElement(By.Id(idToFind: "rwnd_Confirm_C_btn_ConfirmAgreement"));
            //IWebElement confirmButton = LocalWait.Until(ExpectedConditions.ElementExists(By.Id(idToFind: "rwnd_Confirm_C_btn_ConfirmAgreement")));
            confirmButton.Click();
        }
    }
}