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
    public class LeaveHistoriesController : Controller
    {
        private HRMSDbContext db = new HRMSDbContext();

        // GET: LeaveHistories
        public ActionResult Index()
        {
            return View(db.LeaveHistories.ToList());
        }

        // GET: LeaveHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveHistory leaveHistory = db.LeaveHistories.Find(id);
            if (leaveHistory == null)
            {
                return HttpNotFound();
            }
            return View(leaveHistory);
        }

        // GET: LeaveHistories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,EmployeeId,LeaveTypeId,FromDate,ToDate,Day,Cause,UpdatedBy,UpdateDate,Status")] LeaveHistory leaveHistory)
        {
            if (ModelState.IsValid)
            {
                db.LeaveHistories.Add(leaveHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(leaveHistory);
        }

        // GET: LeaveHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveHistory leaveHistory = db.LeaveHistories.Find(id);
            if (leaveHistory == null)
            {
                return HttpNotFound();
            }
            return View(leaveHistory);
        }

        // POST: LeaveHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,EmployeeId,LeaveTypeId,FromDate,ToDate,Day,Cause,UpdatedBy,UpdateDate,Status")] LeaveHistory leaveHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leaveHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(leaveHistory);
        }

        // GET: LeaveHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveHistory leaveHistory = db.LeaveHistories.Find(id);
            if (leaveHistory == null)
            {
                return HttpNotFound();
            }
            return View(leaveHistory);
        }

        // POST: LeaveHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LeaveHistory leaveHistory = db.LeaveHistories.Find(id);
            db.LeaveHistories.Remove(leaveHistory);
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
