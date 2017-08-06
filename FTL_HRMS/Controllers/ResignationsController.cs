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
    public class ResignationsController : Controller
    {
        private HRMSDbContext db = new HRMSDbContext();

        // GET: Resignations
        public ActionResult Index()
        {
            return View(db.Resignation.ToList());
        }

        // GET: Resignations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resignation resignation = db.Resignation.Find(id);
            if (resignation == null)
            {
                return HttpNotFound();
            }
            return View(resignation);
        }

        // GET: Resignations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Resignations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,ResignDate,Reason,Suggestion,Status,CreateDate,UpdatedBy,UpdateDate,Remarks,EmployeeId")] Resignation resignation)
        {
            if (ModelState.IsValid)
            {
                db.Resignation.Add(resignation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(resignation);
        }

        // GET: Resignations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resignation resignation = db.Resignation.Find(id);
            if (resignation == null)
            {
                return HttpNotFound();
            }
            return View(resignation);
        }

        // POST: Resignations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,ResignDate,Reason,Suggestion,Status,CreateDate,UpdatedBy,UpdateDate,Remarks,EmployeeId")] Resignation resignation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resignation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resignation);
        }

        // GET: Resignations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resignation resignation = db.Resignation.Find(id);
            if (resignation == null)
            {
                return HttpNotFound();
            }
            return View(resignation);
        }

        // POST: Resignations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Resignation resignation = db.Resignation.Find(id);
            db.Resignation.Remove(resignation);
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
