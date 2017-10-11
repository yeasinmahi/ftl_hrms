using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.Models;
using System;
using System.Collections.Generic;
using FTL_HRMS.DAL;
using FTL_HRMS.Models.Hr;
using FTL_HRMS.Utility;
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
            string userName = User.Identity.Name;
            int userId = DbUtility.GetUserId(_db, userName);
            List<DepartmentTransfer> departmentTransferList = _db.DepartmentTransfer.Include(a => a.Employee).Where(i => i.EmployeeId != userId).ToList();
            return View(departmentTransferList);
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
            List<DepartmentGroup> departmentGroupList = new List<DepartmentGroup>();
            departmentGroupList = _db.DepartmentGroup.Where(i => i.Status == true).ToList();
            ViewBag.DepartmentGroupId = new SelectList(departmentGroupList, "Sl", "Name");
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
                int fromDesignationId = _db.Employee.Where(i => i.Sl == departmentTransfer.EmployeeId).Select(x => x.DesignationId).FirstOrDefault();
                int toDesignationId = Convert.ToInt32(Request["ddl_designation"]);

                departmentTransfer.FromDesignationId = fromDesignationId;
                departmentTransfer.ToDesignationId = toDesignationId;
                _db.DepartmentTransfer.Add(departmentTransfer);
                _db.SaveChanges();

                #region Role Transfer
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
                string existingRole = _db.Designation.Where(i => i.Sl == fromDesignationId).Select(i => i.RoleName).FirstOrDefault();
                string newRole = _db.Designation.Where(i => i.Sl == toDesignationId).Select(i => i.RoleName).FirstOrDefault();
                string userId = _db.Users.Where(i => i.CustomUserId == departmentTransfer.EmployeeId).Select(s => s.Id).FirstOrDefault();
                var result1 = userManager.RemoveFromRole(userId, existingRole);
                var result2 = userManager.AddToRole(userId, newRole);
                #endregion

                #region Edit Employee
                Employee employee = _db.Employee.Find(departmentTransfer.EmployeeId);
                employee.DesignationId = toDesignationId;
                _db.Entry(employee).State = EntityState.Modified;
                _db.SaveChanges();
                #endregion

                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddSuccess);
                return RedirectToAction("Create");
            }
            TempData["message"] =  DbUtility.GetStatusMessage(DbUtility.Status.AddFailed);

            List<DepartmentGroup> departmentGroupList = new List<DepartmentGroup>();
            departmentGroupList = _db.DepartmentGroup.Where(i => i.Status == true).ToList();
            ViewBag.DepartmentGroupId = new SelectList(departmentGroupList, "Sl", "Name");
            return View(departmentTransfer);
        }
        #endregion

        #region Get Information
        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetDesignation()
        {
            string[] employeeData = new string[3];
            if (Request["empId"].ToString() != "")
            {
                int empId = Convert.ToInt32(Request["empId"]);
                Employee employee = _db.Employee.Find(empId);

                int designationId = _db.Employee.Where( i=> i.Sl == empId).Select(x=> x.DesignationId).FirstOrDefault();
                employeeData[0] = _db.Designation.Where(i => i.Sl == designationId).Select(x => x.Name).FirstOrDefault();

                int departmentId = _db.Designation.Where(i => i.Sl == designationId).Select(x => x.DepartmentId).FirstOrDefault();
                employeeData[1] = _db.Department.Where(i => i.Sl == departmentId).Select(x => x.Name).FirstOrDefault();

                int departmentGroupId = _db.Department.Where(i => i.Sl == departmentId).Select(x => x.DepartmentGroupId).FirstOrDefault();
                employeeData[2] = _db.DepartmentGroup.Where(i => i.Sl == departmentGroupId).Select(x => x.Name).FirstOrDefault();
            }
            else
            {
                employeeData[0] = "";
            }
            return Json(employeeData.ToList(), JsonRequestBehavior.AllowGet);
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
            int departmentId = _db.Designation.Where(x => x.Sl == departmentTransfer.ToDesignationId).Select(t => t.DepartmentId).FirstOrDefault();
            ViewBag.Department = _db.Department.Where(x => x.Sl == departmentId).Select(t=> t.Name).FirstOrDefault();
            int departmentGroupId = _db.Department.Where(x => x.Sl == departmentId).Select(t => t.DepartmentGroupId).FirstOrDefault();
            ViewBag.DepartmentGroup = _db.DepartmentGroup.Where(x => x.Sl == departmentGroupId).Select(t => t.Name).FirstOrDefault();

            List<DepartmentGroup> departmentGroupList = new List<DepartmentGroup>();
            departmentGroupList = _db.DepartmentGroup.Where(i => i.Status == true).ToList();
            ViewBag.DepartmentGroupId = new SelectList(departmentGroupList, "Sl", "Name");
           
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
                int toDesignationId = Convert.ToInt32(Request["ddl_designation"]);
                departmentTransfer.ToDesignationId = toDesignationId;
                int fromDesignationId = _db.Employee.Where(i => i.Sl == departmentTransfer.EmployeeId).Select(x => x.DesignationId).FirstOrDefault();
                departmentTransfer.FromDesignationId = fromDesignationId;
                _db.Entry(departmentTransfer).State = EntityState.Modified;
                _db.SaveChanges();

                #region Edit Employee
                Employee employee = _db.Employee.Find(departmentTransfer.EmployeeId);
                employee.DesignationId = toDesignationId;
                _db.Entry(employee).State = EntityState.Modified;
                _db.SaveChanges();
                #endregion

                #region Role Transfer
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
                string existingRole = _db.Designation.Where(i => i.Sl == fromDesignationId).Select(i => i.RoleName).FirstOrDefault();
                string newRole = _db.Designation.Where(i => i.Sl == toDesignationId).Select(i => i.RoleName).FirstOrDefault();
                string userId = _db.Users.Where(i => i.CustomUserId == departmentTransfer.EmployeeId).Select(s => s.Id).FirstOrDefault();
                var result1 = userManager.RemoveFromRole(userId, existingRole);
                var result2 = userManager.AddToRole(userId, newRole);
                #endregion

                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateSuccess);
                return RedirectToAction("Index");
            }
            DepartmentTransfer department_Transfer = _db.DepartmentTransfer.Find(departmentTransfer.Sl);
            ViewBag.Designation = _db.Designation.Where(x => x.Sl == department_Transfer.ToDesignationId).Select(t => t.Name).FirstOrDefault();
            int departmentId = _db.Designation.Where(x => x.Sl == department_Transfer.ToDesignationId).Select(t => t.DepartmentId).FirstOrDefault();
            ViewBag.Department = _db.Department.Where(x => x.Sl == departmentId).Select(t => t.Name).FirstOrDefault();
            int departmentGroupId = _db.Department.Where(x => x.Sl == departmentId).Select(t => t.DepartmentGroupId).FirstOrDefault();
            ViewBag.DepartmentGroup = _db.DepartmentGroup.Where(x => x.Sl == departmentGroupId).Select(t => t.Name).FirstOrDefault();

            List<DepartmentGroup> departmentGroupList = new List<DepartmentGroup>();
            departmentGroupList = _db.DepartmentGroup.Where(i => i.Status == true).ToList();
            ViewBag.DepartmentGroupId = new SelectList(departmentGroupList, "Sl", "Name");

            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateFailed);
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
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.DeleteSuccess);
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
