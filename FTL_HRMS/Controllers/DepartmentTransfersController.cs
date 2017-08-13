using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.Models;
using System;
using System.Collections.Generic;

namespace FTL_HRMS.Controllers
{
    public class DepartmentTransfersController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        // GET: DepartmentTransfers
        public ActionResult Index()
        {
            return View(_db.DepartmentTransfer.ToList());
        }

        // GET: DepartmentTransfers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentTransfer departmentTransfer = _db.DepartmentTransfer.Find(id);
            if (departmentTransfer == null)
            {
                return HttpNotFound();
            }
            return View(departmentTransfer);
        }

        // GET: DepartmentTransfers/Create
        public ActionResult Create()
        {
            List<Employee> EmployeeList = new List<Employee>();
            EmployeeList = db.Employee.Where(i => i.Status == true).ToList();
            ViewBag.EmployeeId = new SelectList(EmployeeList, "Sl", "Name");

            List<DepartmentGroup> DepartmentGroupList = new List<DepartmentGroup>();
            DepartmentGroupList = db.DepartmentGroup.Where(i => i.Status == true).ToList();
            ViewBag.DepartmentGroupId = new SelectList(DepartmentGroupList, "Sl", "Name");
            return View();
        }

        // POST: DepartmentTransfers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,EmployeeId,FromDesignationId,ToDesignationId,TransferDate")] DepartmentTransfer departmentTransfer)
        {
            if (ModelState.IsValid)
            {
                int FromDesignationId = db.Employee.Where(i => i.Sl == departmentTransfer.EmployeeId).Select(x => x.DesignationId).FirstOrDefault();
                int ToDesignationId = Convert.ToInt32(Request["ddl_designation"]);

                departmentTransfer.FromDesignationId = FromDesignationId;
                departmentTransfer.ToDesignationId = ToDesignationId;
                db.DepartmentTransfer.Add(departmentTransfer);
                db.SaveChanges();
                db.DepartmentTransfer.Add(departmentTransfer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

                #region Edit Employee

                Employee employee = db.Employee.Find(departmentTransfer.EmployeeId);
                employee.DesignationId = ToDesignationId;
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();

                #endregion
                TempData["SuccessMsg"] = "Added Successfully !!";
                return RedirectToAction("Create");
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(departmentTransfer);
        }
        #region Get Information
        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetDesignation()
        {
            string[] EmployeeData = new string[3];
            if (Request["empId"].ToString() != "")
            {
                int empId = Convert.ToInt32(Request["empId"]);
                Employee employee = db.Employee.Find(empId);

                int DesignationId = db.Employee.Where( i=> i.Sl == empId).Select(x=> x.DesignationId).FirstOrDefault();
                EmployeeData[0] = db.Designation.Where(i => i.Sl == DesignationId).Select(x => x.Name).FirstOrDefault();

                int DepartmentId = db.Designation.Where(i => i.Sl == DesignationId).Select(x => x.DepartmentId).FirstOrDefault();
                EmployeeData[1] = db.Department.Where(i => i.Sl == DepartmentId).Select(x => x.Name).FirstOrDefault();

                int DepartmentGroupId = db.Department.Where(i => i.Sl == DepartmentId).Select(x => x.DepartmentGroupId).FirstOrDefault();
                EmployeeData[2] = db.DepartmentGroup.Where(i => i.Sl == DepartmentGroupId).Select(x => x.Name).FirstOrDefault();
            }
            else
            {
                EmployeeData[0] = "";
            }
            return Json(EmployeeData.ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion
        
        // GET: DepartmentTransfers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentTransfer departmentTransfer = db.DepartmentTransfer.Find(id);
            ViewBag.Designation = db.Designation.Where(x => x.Sl == departmentTransfer.ToDesignationId).Select(t => t.Name).FirstOrDefault();
            ViewBag.Department = db.Department.Where(x => x.Sl == departmentTransfer.ToDesignationId).Select(t=> t.Name).FirstOrDefault();
            int Department = db.Department.Where(x => x.Sl == departmentTransfer.ToDesignationId).Select(t => t.Sl).FirstOrDefault();
            ViewBag.DepartmentGroup = db.DepartmentGroup.Where(x => x.Sl == Department).Select(t => t.Name).FirstOrDefault();
            List<DepartmentGroup> DepartmentGroupList = new List<DepartmentGroup>();
            DepartmentGroupList = db.DepartmentGroup.Where(i => i.Status == true).ToList();
            ViewBag.DepartmentGroupId = new SelectList(DepartmentGroupList, "Sl", "Name");
            DepartmentTransfer departmentTransfer = db.DepartmentTransfer.Find(id);
            if (departmentTransfer == null)
            {
                return HttpNotFound();
            }
            return View(departmentTransfer);
        }

        // POST: DepartmentTransfers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,EmployeeId,FromDesignationId,ToDesignationId,TransferDate")] DepartmentTransfer departmentTransfer)
        {
            if (ModelState.IsValid)
            {
                int ToDesignationId = Convert.ToInt32(Request["ddl_designation"]);
                departmentTransfer.ToDesignationId = ToDesignationId;
                int FromDesignationId = db.Employee.Where(i => i.Sl == departmentTransfer.EmployeeId).Select(x => x.DesignationId).FirstOrDefault();
                departmentTransfer.FromDesignationId = FromDesignationId;
                db.Entry(departmentTransfer).State = EntityState.Modified;
                db.SaveChanges();

                #region Edit Employee
                Employee employee = db.Employee.Find(departmentTransfer.EmployeeId);
                employee.DesignationId = ToDesignationId;
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                #endregion
                
                TempData["SuccessMsg"] = "Added Successfully !!";
                return RedirectToAction("Edit");
                db.Entry(departmentTransfer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(departmentTransfer);
        }

        // GET: DepartmentTransfers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentTransfer departmentTransfer = db.DepartmentTransfer.Find(id);
            if (departmentTransfer == null)
            {
                return HttpNotFound();
            }
            return View(departmentTransfer);
        }

        // POST: DepartmentTransfers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DepartmentTransfer departmentTransfer = _db.DepartmentTransfer.Find(id);
            _db.DepartmentTransfer.Remove(departmentTransfer);
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
