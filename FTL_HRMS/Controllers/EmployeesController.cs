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
    public class EmployeesController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        // GET: Employees
        public ActionResult Index()
        {
            return View(_db.Employee.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            List<SourceOfHire> sourceOfHireList = new List<SourceOfHire>();
            sourceOfHireList = _db.SourceOfHire.Where(i => i.Status == true).ToList();
            ViewBag.SourceOfHireId = new SelectList(sourceOfHireList, "Sl", "Name");

            List<Branch> branchList = new List<Branch>();
            branchList = _db.Branches.Where(i => i.Status == true).ToList();
            ViewBag.BranchId = new SelectList(branchList, "Sl", "Name");

            List<EmployeeType> employeeTypeList = new List<EmployeeType>();
            employeeTypeList = _db.EmployeeType.Where(i => i.Status == true).ToList();
            ViewBag.EmployeeTypeId = new SelectList(employeeTypeList, "Sl", "Name");

            List<DepartmentGroup> departmentGroupList = new List<DepartmentGroup>();
            departmentGroupList = _db.DepartmentGroup.Where(i => i.Status == true).ToList();
            ViewBag.DepartmentGroupId = new SelectList(departmentGroupList, "Sl", "Name");

            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,Code,Name,FathersName,MothersName,PresentAddress,PermanentAddress,Mobile,Email,NIDorBirthCirtificate,DrivingLicence,PassportNumber,DateOfBirth,DateOfJoining,SourceOfHireId,DesignationId,EmployeeTypeId,BranchId,GrossSalary,CreatedBy,CreateDate,UpdatedBy,UpdateDate,Status")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _db.Employee.Add(employee);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,Code,Name,FathersName,MothersName,PresentAddress,PermanentAddress,Mobile,Email,NIDorBirthCirtificate,DrivingLicence,PassportNumber,DateOfBirth,DateOfJoining,SourceOfHireId,DesignationId,EmployeeTypeId,BranchId,GrossSalary,CreatedBy,CreateDate,UpdatedBy,UpdateDate,Status")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(employee).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = _db.Employee.Find(id);
            _db.Employee.Remove(employee);
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
