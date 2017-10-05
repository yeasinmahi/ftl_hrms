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

namespace FTL_HRMS.Controllers
{
    public class DepartmentsController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        #region List
        // GET: Departments
        public ActionResult Index()
        {
            return View(_db.Department.Include(a => a.DepartmentGroup).Include(a => a.CreateEmployee).Include(a => a.UpdateEmployee).Where(i => i.Status == true).ToList());
        }
        #endregion

        #region Details
        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _db.Department.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }
        #endregion

        #region Get Departments By Group
        public ActionResult GetDepartmentByGroupId()
        {
            int departmentGroupId;
            Int32.TryParse(Request["DepartmentGroupId"],out departmentGroupId);
            var s = from p in _db.Department.AsEnumerable()
                    where p.DepartmentGroupId == departmentGroupId
                    select new Department { Sl = p.Sl, Name = p.Name };
            return Json(s, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Get Department Group
        public ActionResult GetDepartmentGroup()
        {
            string departmentGroupName = Request["DepartmentGroupName"];
            var s = from p in _db.DepartmentGroup.AsEnumerable()
                    where p.Name.Contains(departmentGroupName) || p.Code.Contains(departmentGroupName)
                    select new DepartmentGroup { Sl = p.Sl, Name = p.Name, Code=p.Code };
            
            return Json(s, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Create
        // GET: Departments/Create
        public ActionResult Create()
        {
            List<DepartmentGroup> groupList = new List<DepartmentGroup>();
            groupList = _db.DepartmentGroup.Where(i => i.Status == true).ToList();
            ViewBag.DepartmentGroupId = new SelectList(groupList, "Sl", "Name");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,Code,Name,DepartmentGroupId,CreatedBy,CreateDate,UpdatedBy,UpdateDate,Status")] Department department)
        {
            List<DepartmentGroup> groupList = new List<DepartmentGroup>();
            groupList = _db.DepartmentGroup.Where(i => i.Status == true).ToList();
            if (_db.Department.Where(i => i.Code == department.Code).ToList().Count < 1)
            {
                string userName = User.Identity.Name;
                int userId = DbUtility.GetUserId(_db, userName);
                department.CreatedBy = userId;
                department.CreateDate = DateTime.Now;
                department.Status = true;
                _db.Department.Add(department);
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Added Successfully !!";
                ViewBag.DepartmentGroupId = new SelectList(groupList, "Sl", "Name");
                ViewBag.Departments = groupList;
                return RedirectToAction("Create");
            }
            else
            {
                TempData["WarningMsg"] = "Code already exists !!";
            }
            ViewBag.DepartmentGroupId = new SelectList(groupList, "Sl", "Name",department.DepartmentGroupId);
            return View(department);
        }
        #endregion

        #region Edit
        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _db.Department.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            List<DepartmentGroup> groupList = new List<DepartmentGroup>();
            groupList = _db.DepartmentGroup.Where(i => i.Status == true).ToList();
            ViewBag.DepartmentGroupId = new SelectList(groupList, "Sl", "Name", department.DepartmentGroupId);
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,Code,Name,DepartmentGroupId,CreatedBy,CreateDate,UpdatedBy,UpdateDate,Status")] Department department)
        {
            List<DepartmentGroup> groupList = new List<DepartmentGroup>();
            groupList = _db.DepartmentGroup.Where(i => i.Status == true).ToList();
            if (_db.Department.Where(i => i.Sl == department.Sl).Select(i => i.Code).ToString() != department.Code)
            {
                if (_db.Department.Where(i => i.Code == department.Code).ToList().Count < 1)
                {
                    string userName = User.Identity.Name;
                    int userId = DbUtility.GetUserId(_db, userName);
                    department.UpdatedBy = userId;
                    department.UpdateDate = DateTime.Now;
                    _db.Entry(department).State = EntityState.Modified;
                    _db.SaveChanges();
                    TempData["SuccessMsg"] = "Updated Successfully!";
                    ViewBag.DepartmentGroupId = new SelectList(groupList, "Sl", "Name", department.DepartmentGroupId);
                    return View(department);
                }
                else
                {
                    TempData["WarningMsg"] = "Code already exists !!";
                    ViewBag.DepartmentGroupId = new SelectList(groupList, "Sl", "Name", department.DepartmentGroupId);
                    return View(department);
                }
            }
            else
            {
                string userName = User.Identity.Name;
                int userId = DbUtility.GetUserId(_db, userName);
                department.UpdatedBy = userId;
                department.UpdateDate = DateTime.Now;
                _db.Entry(department).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Updated Successfully!";
                ViewBag.DepartmentGroupId = new SelectList(groupList, "Sl", "Name", department.DepartmentGroupId);
                return View(department);
            }
        }
        #endregion

        #region Delete
        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _db.Department.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (_db.Designation.Where(i => i.DepartmentId == id && i.Status == true).ToList().Count < 1)
            {
                Department department = _db.Department.Find(id);
                department.Status = false;
                _db.Entry(department).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Deleted Successfully !!";
            }
            else
            {
                TempData["WarningMsg"] = "Already exists some designations under this department !!";
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