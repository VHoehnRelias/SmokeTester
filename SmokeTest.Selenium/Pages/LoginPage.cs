using Cmt.Online.Web.TestUi.Selenium.Commands;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmt.Online.Web.TestUi.Selenium.Pages
{
    public class LoginPage
    {
        public static WebDriverWait LocalWait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(value: 5));
        public static void GoTo()
        {
            //
            //Driver.Instance.Navigate().GoToUrl(Driver.TheLoginData.WebSite + "/Login.aspx");
        }

        public static LoginCommand LoginAs(string clientName, string userName)
        {
            return new LoginCommand(clientName, userName);
        }
    }
}
