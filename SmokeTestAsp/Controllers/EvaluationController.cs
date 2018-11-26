using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmokeTestDBClassLibrary;

namespace SmokeTestAsp.Controllers
{
    public class EvaluationController : Controller
    {
        private SmokeTestsEntities db = new SmokeTestsEntities();

        // GET: Evaluation
        public ActionResult Index()
        {
            var report_Evaluations = db.Report_Evaluations.Include(r => r.Evaluator).Include(r => r.Release).Include(r => r.Report).Include(r => r.Status);
            return View(report_Evaluations.ToList());
        }

        // GET: Evaluation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report_Evaluation report_Evaluation = db.Report_Evaluations.Find(id);
            if (report_Evaluation == null)
            {
                return HttpNotFound();
            }
            return View(report_Evaluation);
        }

        // GET: Evaluation/Create
        public ActionResult Create()
        {
            ViewBag.Evaluator_ID = new SelectList(db.Evaluators, "Id", "Name");
            ViewBag.Release_ID = new SelectList(db.Releases, "Id", "Name");
            ViewBag.Report_ID = new SelectList(db.Reports, "Id", "Report_Name");
            ViewBag.Status_ID = new SelectList(db.Status, "Id", "Name");
            return View();
        }

        // POST: Evaluation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Release_ID,Evaluator_ID,Report_ID,Status_ID,Comment,DateAdded,AddedBy,DateModified,ModifiedBy")] Report_Evaluation report_Evaluation)
        {
            if (ModelState.IsValid)
            {
                db.Report_Evaluations.Add(report_Evaluation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Evaluator_ID = new SelectList(db.Evaluators, "Id", "Name", report_Evaluation.Evaluator_ID);
            ViewBag.Release_ID = new SelectList(db.Releases, "Id", "Name", report_Evaluation.Release_ID);
            ViewBag.Report_ID = new SelectList(db.Reports, "Id", "Report_Name", report_Evaluation.Report_ID);
            ViewBag.Status_ID = new SelectList(db.Status, "Id", "Name", report_Evaluation.Status_ID);
            return View(report_Evaluation);
        }

        // GET: Evaluation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report_Evaluation report_Evaluation = db.Report_Evaluations.Find(id);
            if (report_Evaluation == null)
            {
                return HttpNotFound();
            }
            ViewBag.Evaluator_ID = new SelectList(db.Evaluators, "Id", "Name", report_Evaluation.Evaluator_ID);
            ViewBag.Release_ID = new SelectList(db.Releases, "Id", "Name", report_Evaluation.Release_ID);
            ViewBag.Report_ID = new SelectList(db.Reports, "Id", "Report_Name", report_Evaluation.Report_ID);
            ViewBag.Status_ID = new SelectList(db.Status, "Id", "Name", report_Evaluation.Status_ID);
            return View(report_Evaluation);
        }

        // POST: Evaluation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Release_ID,Evaluator_ID,Report_ID,Status_ID,Comment,DateAdded,AddedBy,DateModified,ModifiedBy")] Report_Evaluation report_Evaluation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(report_Evaluation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Evaluator_ID = new SelectList(db.Evaluators, "Id", "Name", report_Evaluation.Evaluator_ID);
            ViewBag.Release_ID = new SelectList(db.Releases, "Id", "Name", report_Evaluation.Release_ID);
            ViewBag.Report_ID = new SelectList(db.Reports, "Id", "Report_Name", report_Evaluation.Report_ID);
            ViewBag.Status_ID = new SelectList(db.Status, "Id", "Name", report_Evaluation.Status_ID);
            return View(report_Evaluation);
        }

        // GET: Evaluation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report_Evaluation report_Evaluation = db.Report_Evaluations.Find(id);
            if (report_Evaluation == null)
            {
                return HttpNotFound();
            }
            return View(report_Evaluation);
        }

        // POST: Evaluation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Report_Evaluation report_Evaluation = db.Report_Evaluations.Find(id);
            db.Report_Evaluations.Remove(report_Evaluation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
