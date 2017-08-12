﻿using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.Models;
using System.Collections.Generic;
using System;

namespace FTL_HRMS.Controllers
{
    public class BranchTransfersController : Controller
    {
        private HRMSDbContext db = new HRMSDbContext();

        // GET: BranchTransfers
        public ActionResult Index()
        {
            return View(db.BranchTransfer.ToList());
        }

        // GET: BranchTransfers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BranchTransfer branchTransfer = db.BranchTransfer.Find(id);
            if (branchTransfer == null)
            {
                return HttpNotFound();
            }
            return View(branchTransfer);
        }

        // GET: BranchTransfers/Create
        public ActionResult Create()
        {
            List<Employee> EmployeeList = new List<Employee>();
            EmployeeList = db.Employee.Where(i => i.Status == true).ToList();
            ViewBag.EmployeeId = new SelectList(EmployeeList, "Sl", "Name");

            List<Branch> BranchList = new List<Branch>();
            BranchList = db.Branches.Where(i => i.Status == true).ToList();
            ViewBag.BranchId = new SelectList(BranchList, "Sl", "Name");
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
                int FromBranchId = db.Employee.Where(i => i.Sl == branchTransfer.EmployeeId).Select(x => x.BranchId).FirstOrDefault();
                int ToBranchId = Convert.ToInt32(Request["BranchId"]);
                branchTransfer.FromBranchId = FromBranchId;
                branchTransfer.ToBranchId = ToBranchId;
                db.BranchTransfer.Add(branchTransfer);
                db.SaveChanges();

                #region Edit Employee

                Employee employee = db.Employee.Find(branchTransfer.EmployeeId);
                employee.BranchId = ToBranchId;
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();

                #endregion
                TempData["SuccessMsg"] = "Added Successfully !!";
                return RedirectToAction("Create");
            }
            TempData["WarningMsg"] = "Something went wrong !!";

            return View(branchTransfer);
        }

        #region Get Information
        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetBranch()
        {
            string[] EmployeeData = new string[1];
            if (Request["empId"].ToString() != "")
            {
                int empId = Convert.ToInt32(Request["empId"]);
                Employee employee = db.Employee.Find(empId);

                int BranchId = db.Employee.Where(i => i.Sl == empId).Select(x => x.BranchId).FirstOrDefault();
                EmployeeData[0] = db.Branches.Where(i => i.Sl == BranchId).Select(x => x.Name).FirstOrDefault();
            }
            else
            {
                EmployeeData[0] = "";
            }
            return Json(EmployeeData.ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        // GET: BranchTransfers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BranchTransfer branchTransfer = db.BranchTransfer.Find(id);
            ViewBag.Branch = db.Branches.Where(x => x.Sl == branchTransfer.ToBranchId).Select(t => t.Name).FirstOrDefault();
            List<Branch> BranchList = new List<Branch>();
            BranchList = db.Branches.Where(i => i.Status == true).ToList();
            ViewBag.BranchId = new SelectList(BranchList, "Sl", "Name");
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
                int ToBranchId = Convert.ToInt32(Request["BranchId"]);
                branchTransfer.ToBranchId = ToBranchId;
                int FromBranchId = db.Employee.Where(i => i.Sl == branchTransfer.EmployeeId).Select(x => x.BranchId).FirstOrDefault();
                branchTransfer.FromBranchId = FromBranchId;
                db.Entry(branchTransfer).State = EntityState.Modified;
                db.SaveChanges();
                #region Edit Employee
                Employee employee = db.Employee.Find(branchTransfer.EmployeeId);
                employee.BranchId = ToBranchId;
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                #endregion
                TempData["SuccessMsg"] = "Added Successfully !!";
                return RedirectToAction("Edit");
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(branchTransfer);
        }

        // GET: BranchTransfers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BranchTransfer branchTransfer = db.BranchTransfer.Find(id);
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
            BranchTransfer branchTransfer = db.BranchTransfer.Find(id);
            db.BranchTransfer.Remove(branchTransfer);
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
