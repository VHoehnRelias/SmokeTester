using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmt.Online.Web.TestUi.Selenium.Pages
{
    public class AnalyticsPage
    {
        public static WebDriverWait LocalWait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(value: 5));
    }
}
