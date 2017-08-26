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
    public class WeekendsController : Controller
    {
        private HRMSDbContext db = new HRMSDbContext();

        // GET: Weekends
        public ActionResult Index()
        {
            var weekend = db.Weekend.Include(w => w.Branch);
            return View(weekend.ToList());
        }

        // GET: Weekends/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weekend weekend = db.Weekend.Find(id);
            if (weekend == null)
            {
                return HttpNotFound();
            }
            return View(weekend);
        }

        // GET: Weekends/Create
        public ActionResult Create()
        {
            ViewBag.BranchId = new SelectList(db.Branches, "Sl", "Name");
            return View();
        }

        // POST: Weekends/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,BranchId,Day")] Weekend weekend)
        {
            if (ModelState.IsValid)
            {
                db.Weekend.Add(weekend);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BranchId = new SelectList(db.Branches, "Sl", "Name", weekend.BranchId);
            return View(weekend);
        }

        // GET: Weekends/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weekend weekend = db.Weekend.Find(id);
            if (weekend == null)
            {
                return HttpNotFound();
            }
            ViewBag.BranchId = new SelectList(db.Branches, "Sl", "Name", weekend.BranchId);
            return View(weekend);
        }

        // POST: Weekends/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,BranchId,Day")] Weekend weekend)
        {
            if (ModelState.IsValid)
            {
                db.Entry(weekend).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BranchId = new SelectList(db.Branches, "Sl", "Name", weekend.BranchId);
            return View(weekend);
        }

        // GET: Weekends/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weekend weekend = db.Weekend.Find(id);
            if (weekend == null)
            {
                return HttpNotFound();
            }
            return View(weekend);
        }

        // POST: Weekends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Weekend weekend = db.Weekend.Find(id);
            db.Weekend.Remove(weekend);
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
