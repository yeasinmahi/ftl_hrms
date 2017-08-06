using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FTL_HRMS.Models;

namespace FTL_HRMS.Controllers
{
    public class PerformanceRatingsController : Controller
    {
        private HRMSDbContext db = new HRMSDbContext();

        // GET: PerformanceRatings
        public ActionResult Index()
        {
            return View(db.PerformanceRating.ToList());
        }

        // GET: PerformanceRatings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerformanceRating performanceRating = db.PerformanceRating.Find(id);
            if (performanceRating == null)
            {
                return HttpNotFound();
            }
            return View(performanceRating);
        }

        // GET: PerformanceRatings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PerformanceRatings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,Rating,Date,EmployeeId,PerformanceIssueId")] PerformanceRating performanceRating)
        {
            if (ModelState.IsValid)
            {
                db.PerformanceRating.Add(performanceRating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(performanceRating);
        }

        // GET: PerformanceRatings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerformanceRating performanceRating = db.PerformanceRating.Find(id);
            if (performanceRating == null)
            {
                return HttpNotFound();
            }
            return View(performanceRating);
        }

        // POST: PerformanceRatings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,Rating,Date,EmployeeId,PerformanceIssueId")] PerformanceRating performanceRating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(performanceRating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(performanceRating);
        }

        // GET: PerformanceRatings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerformanceRating performanceRating = db.PerformanceRating.Find(id);
            if (performanceRating == null)
            {
                return HttpNotFound();
            }
            return View(performanceRating);
        }

        // POST: PerformanceRatings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PerformanceRating performanceRating = db.PerformanceRating.Find(id);
            db.PerformanceRating.Remove(performanceRating);
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
