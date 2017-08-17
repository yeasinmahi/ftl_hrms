using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace FTL_HRMS.Controllers
{
    public class DepartmentTransfersController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        #region List
        // GET: DepartmentTransfers
        public ActionResult Index()
        {
            return View(_db.DepartmentTransfer.ToList());
        }
        #endregion

        #region Details (We don't use it)
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
        #endregion

        #region Department Transfer
        // GET: DepartmentTransfers/Create
        public ActionResult Create()
        {
            List<Employee> EmployeeList = new List<Employee>();
            EmployeeList = _db.Employee.Where(i => i.Status == true).ToList();
            ViewBag.EmployeeId = new SelectList(EmployeeList, "Sl", "Code");

            List<DepartmentGroup> DepartmentGroupList = new List<DepartmentGroup>();
            DepartmentGroupList = _db.DepartmentGroup.Where(i => i.Status == true).ToList();
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
                int FromDesignationId = _db.Employee.Where(i => i.Sl == departmentTransfer.EmployeeId).Select(x => x.DesignationId).FirstOrDefault();
                int ToDesignationId = Convert.ToInt32(Request["ddl_designation"]);

                departmentTransfer.FromDesignationId = FromDesignationId;
                departmentTransfer.ToDesignationId = ToDesignationId;
                _db.DepartmentTransfer.Add(departmentTransfer);
                _db.SaveChanges();
               
                #region Edit Employee
                Employee employee = _db.Employee.Find(departmentTransfer.EmployeeId);
                employee.DesignationId = ToDesignationId;
                _db.Entry(employee).State = EntityState.Modified;
                _db.SaveChanges();
                #endregion

                #region Role Transfer
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
                string ExistingRole = _db.Designation.Where(i => i.Sl == FromDesignationId).Select(i => i.RoleName).FirstOrDefault();
                string NewRole = _db.Designation.Where(i => i.Sl == ToDesignationId).Select(i => i.RoleName).FirstOrDefault();
                string userId = _db.Users.Where(i => i.CustomUserId == departmentTransfer.EmployeeId).Select(s => s.Id).FirstOrDefault();
                var result1 = UserManager.RemoveFromRole(userId, ExistingRole);
                var result2 = UserManager.AddToRole(userId, NewRole);
                #endregion

                TempData["SuccessMsg"] = "Transfered Successfully !!";
                return RedirectToAction("Create");
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(departmentTransfer);
        }
        #endregion

        #region Get Information
        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetDesignation()
        {
            string[] EmployeeData = new string[3];
            if (Request["empId"].ToString() != "")
            {
                int empId = Convert.ToInt32(Request["empId"]);
                Employee employee = _db.Employee.Find(empId);

                int DesignationId = _db.Employee.Where( i=> i.Sl == empId).Select(x=> x.DesignationId).FirstOrDefault();
                EmployeeData[0] = _db.Designation.Where(i => i.Sl == DesignationId).Select(x => x.Name).FirstOrDefault();

                int DepartmentId = _db.Designation.Where(i => i.Sl == DesignationId).Select(x => x.DepartmentId).FirstOrDefault();
                EmployeeData[1] = _db.Department.Where(i => i.Sl == DepartmentId).Select(x => x.Name).FirstOrDefault();

                int DepartmentGroupId = _db.Department.Where(i => i.Sl == DepartmentId).Select(x => x.DepartmentGroupId).FirstOrDefault();
                EmployeeData[2] = _db.DepartmentGroup.Where(i => i.Sl == DepartmentGroupId).Select(x => x.Name).FirstOrDefault();
            }
            else
            {
                EmployeeData[0] = "";
            }
            return Json(EmployeeData.ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Edit
        // GET: DepartmentTransfers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DepartmentTransfer departmentTransfer = _db.DepartmentTransfer.Find(id);
            ViewBag.Designation = _db.Designation.Where(x => x.Sl == departmentTransfer.ToDesignationId).Select(t => t.Name).FirstOrDefault();
            int DepartmentId = _db.Designation.Where(x => x.Sl == departmentTransfer.ToDesignationId).Select(t => t.DepartmentId).FirstOrDefault();
            ViewBag.Department = _db.Department.Where(x => x.Sl == DepartmentId).Select(t=> t.Name).FirstOrDefault();
            int DepartmentGroupId = _db.Department.Where(x => x.Sl == DepartmentId).Select(t => t.DepartmentGroupId).FirstOrDefault();
            ViewBag.DepartmentGroup = _db.DepartmentGroup.Where(x => x.Sl == DepartmentGroupId).Select(t => t.Name).FirstOrDefault();

            List<DepartmentGroup> DepartmentGroupList = new List<DepartmentGroup>();
            DepartmentGroupList = _db.DepartmentGroup.Where(i => i.Status == true).ToList();
            ViewBag.DepartmentGroupId = new SelectList(DepartmentGroupList, "Sl", "Name");
           
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
                int FromDesignationId = _db.Employee.Where(i => i.Sl == departmentTransfer.EmployeeId).Select(x => x.DesignationId).FirstOrDefault();
                departmentTransfer.FromDesignationId = FromDesignationId;
                _db.Entry(departmentTransfer).State = EntityState.Modified;
                _db.SaveChanges();

                #region Edit Employee
                Employee employee = _db.Employee.Find(departmentTransfer.EmployeeId);
                employee.DesignationId = ToDesignationId;
                _db.Entry(employee).State = EntityState.Modified;
                _db.SaveChanges();
                #endregion

                #region Role Transfer
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
                string ExistingRole = _db.Designation.Where(i => i.Sl == FromDesignationId).Select(i => i.RoleName).FirstOrDefault();
                string NewRole = _db.Designation.Where(i => i.Sl == ToDesignationId).Select(i => i.RoleName).FirstOrDefault();
                string userId = _db.Users.Where(i => i.CustomUserId == departmentTransfer.EmployeeId).Select(s => s.Id).FirstOrDefault();
                var result1 = UserManager.RemoveFromRole(userId, ExistingRole);
                var result2 = UserManager.AddToRole(userId, NewRole);
                #endregion

                TempData["SuccessMsg"] = "Updated Successfully !!";
                return RedirectToAction("Index");
            }
            DepartmentTransfer department_Transfer = _db.DepartmentTransfer.Find(departmentTransfer.Sl);
            ViewBag.Designation = _db.Designation.Where(x => x.Sl == department_Transfer.ToDesignationId).Select(t => t.Name).FirstOrDefault();
            int DepartmentId = _db.Designation.Where(x => x.Sl == department_Transfer.ToDesignationId).Select(t => t.DepartmentId).FirstOrDefault();
            ViewBag.Department = _db.Department.Where(x => x.Sl == DepartmentId).Select(t => t.Name).FirstOrDefault();
            int DepartmentGroupId = _db.Department.Where(x => x.Sl == DepartmentId).Select(t => t.DepartmentGroupId).FirstOrDefault();
            ViewBag.DepartmentGroup = _db.DepartmentGroup.Where(x => x.Sl == DepartmentGroupId).Select(t => t.Name).FirstOrDefault();

            List<DepartmentGroup> DepartmentGroupList = new List<DepartmentGroup>();
            DepartmentGroupList = _db.DepartmentGroup.Where(i => i.Status == true).ToList();
            ViewBag.DepartmentGroupId = new SelectList(DepartmentGroupList, "Sl", "Name");

            TempData["WarningMsg"] = "Something went wrong !!";
            return View(departmentTransfer);
        }
        #endregion

        #region Delete (We don't use it)
        // GET: DepartmentTransfers/Delete/5
        public ActionResult Delete(int? id)
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
