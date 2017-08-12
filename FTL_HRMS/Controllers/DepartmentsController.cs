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
        private HRMSDbContext db = new HRMSDbContext();

        #region List
        // GET: Departments
        public ActionResult Index()
        {
            return View(db.Department.Include(a=>a.DepartmentGroup).Where(i=> i.Status == true).ToList());
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
            Department department = db.Department.Find(id);
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
            int DepartmentGroupId = Convert.ToInt32(Request["DepartmentGroupId"]);
            List<Department> DepartmentList = db.Department.Where(t => t.DepartmentGroupId == DepartmentGroupId).ToList();
            return Json(DepartmentList, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Get Department Group
        public ActionResult GetDepartmentGroup()
        {
            string DepartmentGroupName = Request["DepartmentGroupName"];
            List<DepartmentGroup> GroupList = db.DepartmentGroup.Where(r => r.Name.Contains(DepartmentGroupName)).ToList();
            return Json(GroupList, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Create
        // GET: Departments/Create
        public ActionResult Create()
        {
            List<DepartmentGroup> GroupList = new List<DepartmentGroup>();
            GroupList = db.DepartmentGroup.Where(i => i.Status == true).ToList();
            ViewBag.DepartmentGroupId = new SelectList(GroupList, "Sl", "Name");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,Code,Name,DepartmentGroupId,CreatedBy,CreateDate,UpdatedBy,UpdateDate,Status")] Department department)
        {
            List<DepartmentGroup> GroupList = new List<DepartmentGroup>();
            GroupList = db.DepartmentGroup.Where(i => i.Status == true).ToList();
            if (department.Name != "")
            {
                string UserName = User.Identity.Name;
                int userId = db.Users.Where(i => i.UserName == UserName).Select(s => s.CustomUserId).FirstOrDefault();
                department.CreatedBy = userId;
                department.CreateDate = DateTime.Now;
                department.Status = true;
                db.Department.Add(department);
                db.SaveChanges();
                TempData["SuccessMsg"] = "Added Successfully !!";
                ViewBag.DepartmentGroupId = new SelectList(GroupList, "Sl", "Name");
                ViewBag.Departments = GroupList;
                return RedirectToAction("Create");
            }
            ViewBag.DepartmentGroupId = new SelectList(GroupList, "Sl", "Name",department.DepartmentGroupId);
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
            Department department = db.Department.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            List<DepartmentGroup> GroupList = new List<DepartmentGroup>();
            GroupList = db.DepartmentGroup.Where(i => i.Status == true).ToList();
            ViewBag.DepartmentGroupId = new SelectList(GroupList, "Sl", "Name", department.DepartmentGroupId);
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,Code,Name,DepartmentGroupId,CreatedBy,CreateDate,UpdatedBy,UpdateDate,Status")] Department department)
        {
            List<DepartmentGroup> GroupList = new List<DepartmentGroup>();
            GroupList = db.DepartmentGroup.Where(i => i.Status == true).ToList();
            if (ModelState.IsValid)
            {
                string UserName = User.Identity.Name;
                int userId = db.Users.Where(i => i.UserName == UserName).Select(s => s.CustomUserId).FirstOrDefault();
                department.UpdatedBy = userId;
                department.UpdateDate = DateTime.Now;
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMsg"] = "Updated Successfully!";
                ViewBag.DepartmentGroupId = new SelectList(GroupList, "Sl", "Name", department.DepartmentGroupId);
                return View(department);
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            ViewBag.DepartmentGroupId = new SelectList(GroupList, "Sl", "Name", department.DepartmentGroupId);
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
            Department department = db.Department.Find(id);
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
            Department department = db.Department.Find(id);
            department.Status = false;
            db.Entry(department).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}