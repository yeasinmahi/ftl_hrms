using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FTL_HRMS.Controllers
{
    public class DesignationsController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        #region List
        // GET: Designations
        public ActionResult Index()
        {
            var designatinList = _db.Designation.Include(i => i.Department).Where(i => i.Status == true).ToList();
            return View(_db.Designation.Include(i => i.Department).Where(i => i.Status == true).ToList());
        }
        #endregion

        #region Details
        // GET: Designations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Designation designation = _db.Designation.Find(id);
            if (designation == null)
            {
                return HttpNotFound();
            }
            return View(designation);
        }
        #endregion

        #region Get Designations By Department
        public ActionResult GetDesignationByDeptId()
        {
            int departmentId = Convert.ToInt32(Request["DepartmentId"]);
            List<Designation> designationList = _db.Designation.Where(t => t.DepartmentId == departmentId).ToList();
            return Json(designationList, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Create
        // GET: Designations/Create
        public ActionResult Create()
        {
            List<DepartmentGroup> departmentGroupList = new List<DepartmentGroup>();
            departmentGroupList = _db.DepartmentGroup.Where(i => i.Status == true).ToList();
            ViewBag.DepartmentGroupId = new SelectList(departmentGroupList, "Sl", "Name");

            List<IdentityRole> RoleList = new List<IdentityRole>();
            RoleList = _db.Roles.Where(i => i.Name != "System Admin" && i.Name != "Super Admin").ToList();
            ViewBag.RoleName = new SelectList(RoleList, "Name", "Name");

            return View();
        }

        // POST: Designations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,Code,Name,DepartmentId,RoleName,CreatedBy,CreateDate,UpdatedBy,UpdateDate,Status")] Designation designation)
        {
            if (designation.Name != "")
            {
                string userName = User.Identity.Name;
                int userId = _db.Users.Where(i => i.UserName == userName).Select(s => s.CustomUserId).FirstOrDefault();
                designation.CreatedBy = userId;
                designation.CreateDate = DateTime.Now;
                designation.DepartmentId = Convert.ToInt32(Request["ddl_dept"]);
                designation.Status = true;
                _db.Designation.Add(designation);
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Added Successfully !!";
                return RedirectToAction("Create");
            }
            List<DepartmentGroup> departmentGroupList = new List<DepartmentGroup>();
            departmentGroupList = _db.DepartmentGroup.Where(i => i.Status == true).ToList();
            ViewBag.DepartmentGroupId = new SelectList(departmentGroupList, "Sl", "Name");
            List<IdentityRole> RoleList = new List<IdentityRole>();
            RoleList = _db.Roles.Where(i => i.Name != "System Admin" && i.Name != "Super Admin").ToList();
            ViewBag.RoleName = new SelectList(RoleList, "Name", "Name");
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(designation);
        }
        #endregion

        #region Edit
        // GET: Designations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Designation designation = _db.Designation.Find(id);
            if (designation == null)
            {
                return HttpNotFound();
            }
            List<Department> departmentList = new List<Department>();
            departmentList = _db.Department.Where(i => i.Status == true).ToList();
            ViewBag.DepartmentId = new SelectList(departmentList, "Sl", "Name", designation.DepartmentId);
            List<IdentityRole> RoleList = new List<IdentityRole>();
            RoleList = _db.Roles.Where(i => i.Name != "System Admin" && i.Name != "Super Admin").ToList();
            ViewBag.RoleName = new SelectList(RoleList, "Name", "Name", designation.RoleName);
            return View(designation);
        }

        // POST: Designations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,Code,Name,DepartmentId,RoleName,CreatedBy,CreateDate,UpdatedBy,UpdateDate,Status")] Designation designation)
        {
            List<Department> departmentList = new List<Department>();
            departmentList = _db.Department.Where(i => i.Status == true).ToList();
            List<IdentityRole> RoleList = new List<IdentityRole>();
            RoleList = _db.Roles.Where(i => i.Name != "System Admin" && i.Name != "Super Admin").ToList();
            if (ModelState.IsValid)
            {
                string userName = User.Identity.Name;
                int userId = _db.Users.Where(i => i.UserName == userName).Select(s => s.CustomUserId).FirstOrDefault();
                designation.UpdatedBy = userId;
                designation.UpdateDate = DateTime.Now;
                _db.Entry(designation).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Updated Successfully!";
                ViewBag.DepartmentId = new SelectList(departmentList, "Sl", "Name", designation.DepartmentId);
                ViewBag.RoleName = new SelectList(RoleList, "Name", "Name", designation.RoleName);
                return View(designation);
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            ViewBag.DepartmentId = new SelectList(departmentList, "Sl", "Name", designation.DepartmentId);
            ViewBag.RoleName = new SelectList(RoleList, "Name", "Name", designation.RoleName);
            return View(designation);
        }
        #endregion

        #region Delete
        // GET: Designations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Designation designation = _db.Designation.Find(id);
            if (designation == null)
            {
                return HttpNotFound();
            }
            return View(designation);
        }

        // POST: Designations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Designation designation = _db.Designation.Find(id);
            designation.Status = false;
            _db.Entry(designation).State = EntityState.Modified;
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
