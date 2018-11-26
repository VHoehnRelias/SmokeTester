using SmokeTestDBClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmokeTestAsp.Controllers
{
    public class SSRSMenuController : Controller
    {
        private SmokeTestsEntities db = new SmokeTestsEntities();
        private readonly string path = "http://wcsqldev02/ReportServer/Pages/ReportViewer.aspx?%2fKalamata%2fKalamataDEV%2fReports%2f{0}&rs%3aCommand=Render";
        //private string path1 = "http://wcsqldev02/Reports/report/Kalamata/KalamataDEV/Reports/{0}";
        // GET: SSRSMenu
        public ActionResult Index()
        {
            ViewBag.Report = string.Format(path, "CaseloadDashboard");
            return View();
        }

        public ActionResult Menu()
        {
            var menuList = db.MainMenus;
            ViewBag.Report = string.Format(path, "CaseloadDashboard");
            return View(menuList);
        }

        public ActionResult GotoReport(string report)
        {
            var menuList = db.MainMenus;
            ViewBag.Report = string.Format(path, report);
            ViewBag.ReportMenu = report;
            return View("Menu",menuList);
        }

        public ActionResult FillMenu(int mainMenuId)
        {
            var menuSection = db.MainMenus.Find(mainMenuId);
            ViewBag.MenuHeader = menuSection.DisplayLabel;
            var itms = menuSection.MainMenuSubMenus;
            return PartialView("~/Views/Shared/_MenuItem.cshtml", itms);
        }
    }
}