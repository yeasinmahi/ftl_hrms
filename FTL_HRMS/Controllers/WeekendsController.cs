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
    public class WeekendsController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        #region List
        // GET: Weekends
        public ActionResult Index()
        {
            var weekend = _db.Weekend.Include(w => w.Branch);
            return View(weekend.ToList());
        }
        #endregion

        #region Details
        // GET: Weekends/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weekend weekend = _db.Weekend.Find(id);
            if (weekend == null)
            {
                return HttpNotFound();
            }
            return View(weekend);
        }
        #endregion

        #region Create
        // GET: Weekends/Create
        public ActionResult Create()
        {
            List<Branch> branchList = new List<Branch>();
            branchList = _db.Branches.Where(i => i.Status == true).ToList();
            ViewBag.BranchId = new SelectList(branchList, "Sl", "Name");
            return View();
        }

        // POST: Weekends/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,BranchId,Day")] Weekend weekend)
        {
            if (weekend.BranchId != 0)
            {
                _db.Weekend.Add(weekend);
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Added Successfully !!";
                return RedirectToAction("Create");
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            List<Branch> branchList = new List<Branch>();
            branchList = _db.Branches.Where(i => i.Status == true).ToList();
            ViewBag.BranchId = new SelectList(branchList, "Sl", "Name", weekend.BranchId);
            return View(weekend);
        }
        #endregion

        #region Edit
        // GET: Weekends/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weekend weekend = _db.Weekend.Find(id);
            if (weekend == null)
            {
                return HttpNotFound();
            }
            List<Branch> branchList = new List<Branch>();
            branchList = _db.Branches.Where(i => i.Status == true).ToList();
            ViewBag.BranchId = new SelectList(branchList, "Sl", "Name", weekend.BranchId);
            return View(weekend);
        }

        // POST: Weekends/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,BranchId,Day")] Weekend weekend)
        {
            List<Branch> branchList = new List<Branch>();
            branchList = _db.Branches.Where(i => i.Status == true).ToList();
            ViewBag.BranchId = new SelectList(branchList, "Sl", "Name", weekend.BranchId);
            if (ModelState.IsValid)
            {
                _db.Entry(weekend).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Updated Successfully!";
                ViewBag.BranchId = new SelectList(branchList, "Sl", "Name", weekend.BranchId);
                return View(weekend);
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            ViewBag.BranchId = new SelectList(branchList, "Sl", "Name", weekend.BranchId);
            return View(weekend);
        }
        #endregion

        #region Delete
        // GET: Weekends/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weekend weekend = _db.Weekend.Find(id);
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
            Weekend weekend = _db.Weekend.Find(id);
            _db.Weekend.Remove(weekend);
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
