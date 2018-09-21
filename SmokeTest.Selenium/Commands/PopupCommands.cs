using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmt.Online.Web.TestUi.Selenium.Commands
{
    public class PopupCommands
    {
        private IWebElement currentPopup;

        public PopupCommands()
        {

        }

        public static void DoubleClick2(IWebElement element)
        {
            try
            {
                var action = new Actions(Driver.Instance);
                action.DoubleClick(element);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void DoubleClick(IWebElement element)
        {
            try
            {
                Actions action = new Actions(Driver.Instance).DoubleClick(element);
                action.Build().Perform();
                Debug.WriteLine(message: "Double clicked the element");
            } catch (StaleElementReferenceException e) {
                Debug.WriteLine("Element is not attached to the page document "
                        + e.StackTrace);
            } catch (NoSuchElementException e) {
                Debug.WriteLine("Element " + element + " was not found in DOM "
                        + e.StackTrace);
            } catch (Exception e) {
                Debug.WriteLine("Element " + element + " was not clickable "
                        + e.StackTrace);
            }
        }

        public PopupCommands Popup()
        {
            currentPopup = Driver.Instance.FindElement(By.ClassName(classNameToFind: "panel-heading"));
            return this;
        }
    }
}
