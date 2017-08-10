﻿using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.Models;

namespace FTL_HRMS.Controllers
{
    public class BranchTransfersController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        // GET: BranchTransfers
        public ActionResult Index()
        {
            return View(_db.BranchTransfer.ToList());
        }

        // GET: BranchTransfers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BranchTransfer branchTransfer = _db.BranchTransfer.Find(id);
            if (branchTransfer == null)
            {
                return HttpNotFound();
            }
            return View(branchTransfer);
        }

        // GET: BranchTransfers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BranchTransfers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,EmployeeId,FromBranchId,ToBranchId,TransferDate")] BranchTransfer branchTransfer)
        {
            if (ModelState.IsValid)
            {
                _db.BranchTransfer.Add(branchTransfer);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(branchTransfer);
        }

        // GET: BranchTransfers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BranchTransfer branchTransfer = _db.BranchTransfer.Find(id);
            if (branchTransfer == null)
            {
                return HttpNotFound();
            }
            return View(branchTransfer);
        }

        // POST: BranchTransfers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,EmployeeId,FromBranchId,ToBranchId,TransferDate")] BranchTransfer branchTransfer)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(branchTransfer).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(branchTransfer);
        }

        // GET: BranchTransfers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BranchTransfer branchTransfer = _db.BranchTransfer.Find(id);
            if (branchTransfer == null)
            {
                return HttpNotFound();
            }
            return View(branchTransfer);
        }

        // POST: BranchTransfers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BranchTransfer branchTransfer = _db.BranchTransfer.Find(id);
            _db.BranchTransfer.Remove(branchTransfer);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
