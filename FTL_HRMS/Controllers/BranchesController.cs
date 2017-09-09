using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FTL_HRMS.Models;
using FTL_HRMS.Models.Hr;

namespace FTL_HRMS.Controllers
{
    public class BranchesController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        #region List
        // GET: Branches
        public ActionResult Index()
        {
            return View(_db.Branches.Where(i => i.Status == true).ToList());
        }
        #endregion

        #region Details
        // GET: Branches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = _db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }
        #endregion

        #region Create
        // GET: Branches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Branches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,Name,Address,OpeningTime,EndingTime,IsLateCalculated,LateConsiderationTime,LateConsiderationDay,LateDeductionPercentage,IsOvertimeCalculated,OvertimeConsiderationTime,OvertimePaymentPercentage,Status")] Branch branch)
        {
            if (branch.Name != "")
            {
                branch.Status = true;
                _db.Branches.Add(branch);
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Added Successfully !!";
                return RedirectToAction("Create");
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(branch);
        }
        #endregion

        #region Edit
        // GET: Branches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = _db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // POST: Branches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,Name,Address,OpeningTime,EndingTime,IsLateCalculated,LateConsiderationTime,LateConsiderationDay,LateDeductionPercentage,IsOvertimeCalculated,OvertimeConsiderationTime,OvertimePaymentPercentage,Status")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(branch).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Updated Successfully!";
                return View(branch);
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(branch);
        }
        #endregion

        #region Delete
        // GET: Branches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = _db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // POST: Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Branch branch = _db.Branches.Find(id);
            branch.Status = false;
            _db.Entry(branch).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
