﻿using System;
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
    public class LeaveCountsController : Controller
    {
        private HRMSDbContext db = new HRMSDbContext();

        // GET: LeaveCounts
        public ActionResult Index()
        {
            return View(db.LeaveCounts.ToList());
        }

        // GET: LeaveCounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveCount leaveCount = db.LeaveCounts.Find(id);
            if (leaveCount == null)
            {
                return HttpNotFound();
            }
            return View(leaveCount);
        }

        // GET: LeaveCounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveCounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,EmployeeId,LeaveTypeId,AvailableDay")] LeaveCount leaveCount)
        {
            if (ModelState.IsValid)
            {
                db.LeaveCounts.Add(leaveCount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(leaveCount);
        }

        // GET: LeaveCounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveCount leaveCount = db.LeaveCounts.Find(id);
            if (leaveCount == null)
            {
                return HttpNotFound();
            }
            return View(leaveCount);
        }

        // POST: LeaveCounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,EmployeeId,LeaveTypeId,AvailableDay")] LeaveCount leaveCount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leaveCount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(leaveCount);
        }

        // GET: LeaveCounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveCount leaveCount = db.LeaveCounts.Find(id);
            if (leaveCount == null)
            {
                return HttpNotFound();
            }
            return View(leaveCount);
        }

        // POST: LeaveCounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LeaveCount leaveCount = db.LeaveCounts.Find(id);
            db.LeaveCounts.Remove(leaveCount);
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
