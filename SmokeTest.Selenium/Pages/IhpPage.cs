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
    public static class IhpPage
    {
        public static WebDriverWait LocalWait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(value: 5));
        public static void GotoFirstIhp()
        {
            IWebElement infoBtn = LocalWait.Until( d =>
            {
                IWebElement measureDiv = Driver.Instance.FindElement(By.Id(idToFind: "divMeasure"));
                IWebElement selectableTable = measureDiv.FindElement(By.ClassName(classNameToFind: "k-selectable"));
                ReadOnlyCollection<IWebElement> infoBtns = selectableTable.FindElements(By.ClassName(classNameToFind: "infoButton"));
                if (infoBtns.Count > 0)
                {
                    return infoBtns[1];
                }
                return null;
            });
            infoBtn.Click();
        }

        public static void ClickOnIhp()
        {
            IWebElement ihpBtn = LocalWait.Until(d =>
               {
                   IWebElement analyticsPopup = Driver.Instance.FindElement(By.ClassName(classNameToFind: "analyticsPopup"));
                   ReadOnlyCollection<IWebElement> ihpBtns = analyticsPopup.FindElements(By.ClassName(classNameToFind: "btn-warning"));
                   if (ihpBtns.Count > 0)
                   {
                       return ihpBtns[0];
                   }
                   return null;
               });
            ihpBtn.Click();
            //Driver.ExecuteJavascript(string.Format(format: "vrhPopupHelper.ShowIhp({0},'{1}','#analPopup')", arg0: 274961, arg1: "JULIE E WILLIAMS"));
            //vrhPopupHelper.ShowIhp(274961,"JULIE E WILLIAMS","#analPopup")
            // onclick='vrhPopupHelper.ShowMeasureLetter(274961,"JULIE E WILLIAMS","#analPopup")'
            Driver.PleaseWait();
        }

        public static void ClickOnIhpPrint()
        {
            try
            {
                IWebElement printBtn = LocalWait.Until(d =>
                   {
                       IWebElement ihpRww = Driver.Instance.FindElement(By.Id(idToFind: "RadWindowWrapper_ctl00_cph_main_rw_PageRadWindow"));
                       IWebElement ihpIFrame = ihpRww.FindElement(By.Name(nameToFind: "rw_PageRadWindow"));
                       IWebElement btnPrint = Driver.Instance.FindElement(By.CssSelector(cssSelectorToFind: "div#radwindow input[type='submit']"));
                       IWebElement ihpForm = ihpIFrame.FindElement(By.Id(idToFind: "aspnetForm"));
                       IWebElement ihpDiv = ihpIFrame.FindElement(By.Id(idToFind: "ihp_wrapper"));
                       IWebElement printBtn1 = ihpIFrame.FindElement(By.Id(idToFind: "ctl00_cph_Main_btn_PrintIhp"));
                       //if (printBtn1 != null)
                       return printBtn1;
                   });
                if (printBtn != null)
                {
                    printBtn.Click();
                }
                else
                {
                    Driver.ExecuteJavascript(string.Format(format: "alert('Button is Null.')"));//return OpenPrintIhpWindow()
                }
            }
            catch (Exception e)
            {
                Driver.ExecuteJavascript(string.Format(format: "alert('{0}.')", arg0: e.Message));//return OpenPrintIhpWindow()
            }
            Driver.PleaseWait();
        }

        public static void IncludeDefaults()
        {
            bool p = false;
            IWebElement printDialog = LocalWait.Until(d =>
               {
                   if (p)
                   {
                       IWebElement prtDialog = Driver.Instance.FindElement(By.ClassName(classNameToFind: "rwContentRow"));
                       return prtDialog;
                   }
                   return null;
               });
            
            ReadOnlyCollection<IWebElement> printOpts = printDialog.FindElements(By.ClassName(classNameToFind: "default"));
            foreach(var po in printOpts)
            {
                po.Click();
            }
            Driver.PleaseWait();
        }

        public static void ClickOnIhpPdf()
        {
            IWebElement printBtn = Driver.Instance.FindElement(By.Id(idToFind: "ctl00_cph_Main_btn_Submit"));
            printBtn.Click();
            Driver.PleaseWait();
        }
    }
}
