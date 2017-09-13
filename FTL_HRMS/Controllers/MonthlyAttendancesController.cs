﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FTL_HRMS.Models;
using FTL_HRMS.Models.Payroll;
using FTL_HRMS.Models.Hr;

namespace FTL_HRMS.Controllers
{
    public class MonthlyAttendancesController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        // GET: MonthlyAttendances
        public ActionResult Index()
        {
            var monthlyAttendance = _db.MonthlyAttendance.Include(m => m.Employee);
            return View(monthlyAttendance.ToList());
        }
        #region Department Transfer Report
        public ActionResult EmployeeAttendenceReport()
        {
            ViewBag.DepartmentGroupId = new SelectList(_db.DepartmentGroup, "Sl", "Name");
            List<MonthlyAttendance> attendenceList = GetEmployeeList(0, 0, Utility.Utility.GetDefaultDate());
            return View(attendenceList);
        }

        [HttpPost]
        public ActionResult EmployeeAttendenceReport(string departmentGroupId, string ddl_dept)
        {
            int dgid, did;
            Int32.TryParse(departmentGroupId, out dgid);
            Int32.TryParse(ddl_dept, out did);
            DateTime Date = Utility.Utility.GetDefaultDate();
            DateTime.TryParse(Request["Date"], out Date);
            
            ViewBag.DepartmentGroupId = new SelectList(_db.DepartmentGroup, "Sl", "Name");
            ViewBag.Status = "SelectType";
            TempData["dgid"] = dgid;
            TempData["did"] = did;
            TempData["Date"] = Date;
            List<MonthlyAttendance> attendenceList = GetEmployeeList(dgid, did, Date);
            return View(attendenceList);
        }
        public List<MonthlyAttendance> GetEmployeeList(int departmentGroupId, int departmentId, DateTime Date)
        {
            List<MonthlyAttendance> attendenceList = new List<MonthlyAttendance>();
            if (Date.Equals(Utility.Utility.GetDefaultDate()))
            {
                Date = DateTime.Now.AddDays(-1);
            }
            if (departmentGroupId > 0)
            {
                if (departmentId > 0)
                {
                    attendenceList = _db.MonthlyAttendance.Where(x => x.Employee.Designation.Department.DepartmentGroup.Sl.Equals(departmentGroupId)).Where(x => x.Employee.Designation.Department.Sl.Equals(departmentId)).Where(x=> x.Date==Date).ToList();
                }
                else
                {
                    attendenceList = _db.MonthlyAttendance.Where(x => x.Employee.Designation.Department.DepartmentGroup.Sl.Equals(departmentGroupId)).Where(x => x.Date == Date).ToList();
                    
                }

            }
            else
            {
                attendenceList = _db.MonthlyAttendance.Include(x=> x.Employee).ToList();
            }
            return attendenceList;
        }
        #endregion
   
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyAttendance monthlyAttendance = _db.MonthlyAttendance.Find(id);
            if (monthlyAttendance == null)
            {
                return HttpNotFound();
            }
            return View(monthlyAttendance);
        }

        // GET: MonthlyAttendances/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(_db.Employee, "Sl", "Code");
            return View();
        }

        // POST: MonthlyAttendances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,EmployeeId,Date,Status,IsCalculated")] MonthlyAttendance monthlyAttendance)
        {
            if (ModelState.IsValid)
            {
                _db.MonthlyAttendance.Add(monthlyAttendance);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(_db.Employee, "Sl", "Code", monthlyAttendance.EmployeeId);
            return View(monthlyAttendance);
        }

        // GET: MonthlyAttendances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyAttendance monthlyAttendance = _db.MonthlyAttendance.Find(id);
            if (monthlyAttendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(_db.Employee, "Sl", "Code", monthlyAttendance.EmployeeId);
            return View(monthlyAttendance);
        }

        // POST: MonthlyAttendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,EmployeeId,Date,Status,IsCalculated")] MonthlyAttendance monthlyAttendance)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(monthlyAttendance).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(_db.Employee, "Sl", "Code", monthlyAttendance.EmployeeId);
            return View(monthlyAttendance);
        }

        // GET: MonthlyAttendances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyAttendance monthlyAttendance = _db.MonthlyAttendance.Find(id);
            if (monthlyAttendance == null)
            {
                return HttpNotFound();
            }
            return View(monthlyAttendance);
        }

        // POST: MonthlyAttendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MonthlyAttendance monthlyAttendance = _db.MonthlyAttendance.Find(id);
            _db.MonthlyAttendance.Remove(monthlyAttendance);
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