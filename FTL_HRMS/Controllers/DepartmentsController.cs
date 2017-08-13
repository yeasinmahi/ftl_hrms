using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using FTL_HRMS.Models;

namespace FTL_HRMS.Controllers
{
    public class DepartmentsController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        #region List
        // GET: Departments
        public ActionResult Index()
        {
            return View(_db.Department.Include(a=>a.DepartmentGroup).Where(i=> i.Status == true).ToList());
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
            int departmentGroupId = Convert.ToInt32(Request["DepartmentGroupId"]);
            List<Department> departmentList = _db.Department.Where(t => t.DepartmentGroupId == departmentGroupId).ToList();
            return Json(departmentList, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Get Department Group
        public ActionResult GetDepartmentGroup()
        {
            string departmentGroupName = Request["DepartmentGroupName"];
            List<DepartmentGroup> groupList = _db.DepartmentGroup.Where(r => r.Name.Contains(departmentGroupName)).ToList();
            return Json(groupList, JsonRequestBehavior.AllowGet);
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
            if (department.Name != "")
            {
                string userName = User.Identity.Name;
                int userId = _db.Users.Where(i => i.UserName == userName).Select(s => s.CustomUserId).FirstOrDefault();
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
            ViewBag.DepartmentGroupId = new SelectList(groupList, "Sl", "Name",department.DepartmentGroupId);
            TempData["WarningMsg"] = "Something went wrong !!";
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
            if (ModelState.IsValid)
            {
                string userName = User.Identity.Name;
                int userId = _db.Users.Where(i => i.UserName == userName).Select(s => s.CustomUserId).FirstOrDefault();
                department.UpdatedBy = userId;
                department.UpdateDate = DateTime.Now;
                _db.Entry(department).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Updated Successfully!";
                ViewBag.DepartmentGroupId = new SelectList(groupList, "Sl", "Name", department.DepartmentGroupId);
                return View(department);
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            ViewBag.DepartmentGroupId = new SelectList(groupList, "Sl", "Name", department.DepartmentGroupId);
            return View(department);
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
            Department department = _db.Department.Find(id);
            department.Status = false;
            _db.Entry(department).State = EntityState.Modified;
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