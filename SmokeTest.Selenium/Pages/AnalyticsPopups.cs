using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using Cmt.Online.Web.TestUi.Selenium.Commands;
using OpenQA.Selenium.Interactions;
using System.Diagnostics;

namespace Cmt.Online.Web.TestUi.Selenium.Pages
{
    public class AnalyticsPopups
    {

        public static void GotoPopup(Popups popUp)
        {
            ReadOnlyCollection<IWebElement> popups = Driver.Instance.FindElements(By.ClassName(classNameToFind: "panel-heading"));
            IWebElement selectedPopup = popups[(int) popUp];
            PopupCommands.DoubleClick(selectedPopup);
            Driver.PleaseWait();
        }

        public static void MaximizePopup(Popups profile)
        {
            GotoPopup(profile);
            IWebElement thePopup = Driver.Instance.FindElement(By.ClassName("k-window"));
            if (IsPopupOpen(thePopup))
            {
                IWebElement btnMax = thePopup.FindElement(By.ClassName("k-i-maximize"));
                btnMax.Click();
                IWebElement btnClose = thePopup.FindElement(By.ClassName("k-i-close"));
                btnClose.Click();
                Driver.PleaseWait();
                GotoPopup(profile);
                IWebElement thePopup2 = Driver.Instance.FindElement(By.ClassName("k-window"));
                IWebElement btnMax2 = thePopup2.FindElement(By.ClassName("k-i-maximize"));
                try
                {
                    btnMax2.Click();
                    Driver.Wait(TimeSpan.FromSeconds(value: 3));
                }
                catch (Exception ex)
                {
                    TraceListenerCollection lsts = Debug.Listeners;
                    Debug.WriteLine(ex.Message);
                    IWebElement btnRestore = thePopup2.FindElement(By.ClassName("k-i-restore"));
                    try
                    {
                        btnRestore.Click();
                        Driver.PleaseWait();
                    }
                    catch (Exception sex)
                    {
                        Debug.WriteLine(sex.Message);
                    }
                }
            }
        }

        private static bool IsPopupOpen(IWebElement thePopup)
        {
            IWebElement title = thePopup.FindElement(By.Id(idToFind: "profilePopup_wnd_title"));
            return (title.Text == "Profile");
        }
    }

    public enum Popups
    {
        Profile,
        Chart,
        Measures,
        Registry
    }
}
