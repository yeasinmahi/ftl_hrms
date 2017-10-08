using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.DAL;
using FTL_HRMS.Models;
using FTL_HRMS.Models.Hr;
using FTL_HRMS.Utility;
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
            return View(_db.Designation.Include(i => i.Department).Include(a => a.CreateEmployee).Include(a => a.UpdateEmployee).Where(i => i.Status == true).ToList());
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
            int departmentId;
            Int32.TryParse(Request["DepartmentId"],out departmentId);
            var s = from p in _db.Designation.AsEnumerable()
                    where p.DepartmentId == departmentId
                    select new Designation { Sl = p.Sl, Name = p.Name };
            return Json(s, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Create
        // GET: Designations/Create
        public ActionResult Create()
        {
            List<DepartmentGroup> departmentGroupList = new List<DepartmentGroup>();
            departmentGroupList = _db.DepartmentGroup.Where(i => i.Status == true).ToList();
            ViewBag.DepartmentGroupId = new SelectList(departmentGroupList, "Sl", "Name");

            List<IdentityRole> roleList = new List<IdentityRole>();
            roleList = _db.Roles.Where(i => i.Name != "System Admin" && i.Name != "Super Admin").ToList();
            ViewBag.RoleName = new SelectList(roleList, "Name", "Name");

            return View();
        }

        // POST: Designations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,Code,Name,DepartmentId,RoleName,CreatedBy,CreateDate,UpdatedBy,UpdateDate,Status")] Designation designation)
        {
            if (_db.Designation.Where(i => i.Code == designation.Code).ToList().Count < 1)
            {
                string userName = User.Identity.Name;
                int userId = DbUtility.GetUserId(_db, userName);
                designation.CreatedBy = userId;
                designation.CreateDate = DateTime.Now;
                designation.DepartmentId = Convert.ToInt32(Request["ddl_dept"]);
                designation.Status = true;
                _db.Designation.Add(designation);
                _db.SaveChanges();
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddSuccess);
                return RedirectToAction("Create");
            }
            else
            {
                TempData["message"] =DbUtility.GetStatusMessage(DbUtility.Status.Exist);
            }
            List<DepartmentGroup> departmentGroupList = new List<DepartmentGroup>();
            departmentGroupList = _db.DepartmentGroup.Where(i => i.Status == true).ToList();
            ViewBag.DepartmentGroupId = new SelectList(departmentGroupList, "Sl", "Name");
            List<IdentityRole> roleList = new List<IdentityRole>();
            roleList = _db.Roles.Where(i => i.Name != "System Admin" && i.Name != "Super Admin").ToList();
            ViewBag.RoleName = new SelectList(roleList, "Name", "Name");
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

            int departmentId = _db.Designation.Where(x => x.Sl == designation.Sl).Select(t => t.DepartmentId).FirstOrDefault();
            ViewBag.DepartmentId = _db.Department.Where(x => x.Sl == departmentId).Select(t => t.Sl).FirstOrDefault();
            ViewBag.DepartmentName = _db.Department.Where(x => x.Sl == departmentId).Select(t => t.Name).FirstOrDefault();
            int departmentGroupId = _db.Department.Where(x => x.Sl == departmentId).Select(t => t.DepartmentGroupId).FirstOrDefault();

            List<DepartmentGroup> departmentGroupList = new List<DepartmentGroup>();
            departmentGroupList = _db.DepartmentGroup.Where(i => i.Status == true).ToList();
            ViewBag.DepartmentGroupId = new SelectList(departmentGroupList, "Sl", "Name", departmentGroupId);

            List<IdentityRole> roleList = new List<IdentityRole>();
            roleList = _db.Roles.Where(i => i.Name != "System Admin" && i.Name != "Super Admin").ToList();
            ViewBag.RoleName = new SelectList(roleList, "Name", "Name", designation.RoleName);
            return View(designation);
        }

        // POST: Designations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,Code,Name,DepartmentId,RoleName,CreatedBy,CreateDate,UpdatedBy,UpdateDate,Status")] Designation designation)
        {
            List<DepartmentGroup> departmentGroupList = new List<DepartmentGroup>();
            departmentGroupList = _db.DepartmentGroup.Where(i => i.Status == true).ToList();
            List<IdentityRole> roleList = new List<IdentityRole>();
            roleList = _db.Roles.Where(i => i.Name != "System Admin" && i.Name != "Super Admin").ToList();
            if (_db.Designation.Where(i => i.Sl == designation.Sl).Select(i => i.Code).ToString() != designation.Code)
            {
                if (_db.Designation.Where(i => i.Code == designation.Code).ToList().Count < 1)
                {
                    string userName = User.Identity.Name;
                    int userId = DbUtility.GetUserId(_db, userName);
                    designation.UpdatedBy = userId;
                    designation.UpdateDate = DateTime.Now;
                    designation.DepartmentId = Convert.ToInt32(Request["ddl_dept"]);
                    _db.Entry(designation).State = EntityState.Modified;
                    _db.SaveChanges();
                    TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateSuccess);
                }
                else
                {
                    TempData["message"] =  DbUtility.GetStatusMessage(DbUtility.Status.Exist);
                }
            }
            else
            {
                string userName = User.Identity.Name;
                int userId = DbUtility.GetUserId(_db, userName);
                designation.UpdatedBy = userId;
                designation.UpdateDate = DateTime.Now;
                designation.DepartmentId = Convert.ToInt32(Request["ddl_dept"]);
                _db.Entry(designation).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateSuccess);
            }
            int departmentId = designation.DepartmentId;
            ViewBag.DepartmentId = _db.Department.Where(x => x.Sl == departmentId).Select(t => t.Sl).FirstOrDefault();
            ViewBag.DepartmentName = _db.Department.Where(x => x.Sl == departmentId).Select(t => t.Name).FirstOrDefault();
            int departmentGroupId = _db.Department.Where(x => x.Sl == departmentId).Select(t => t.DepartmentGroupId).FirstOrDefault();
            ViewBag.DepartmentGroupId = new SelectList(departmentGroupList, "Sl", "Name", departmentGroupId);
            ViewBag.RoleName = new SelectList(roleList, "Name", "Name", designation.RoleName);
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
            if (_db.Employee.Where(i => i.DesignationId == id && i.Status == true).ToList().Count < 1)
            {
                Designation designation = _db.Designation.Find(id);
                designation.Status = false;
                _db.Entry(designation).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.DeleteSuccess);
            }
            else
            {
                TempData["message"] =DbUtility.GetStatusMessage(DbUtility.Status.Exist);
            }
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
