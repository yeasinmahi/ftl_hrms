using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.Models;

namespace FTL_HRMS.Controllers
{
    public class DesignationsController : Controller
    {
        private HRMSDbContext db = new HRMSDbContext();

        #region List
        // GET: Designations
        public ActionResult Index()
        {
            return View(db.Designation.Include(i => i.Department).Where(i => i.Status == true).ToList());
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
            Designation designation = db.Designation.Find(id);
            if (designation == null)
            {
                return HttpNotFound();
            }
            return View(designation);
        }
        #endregion

        #region Create
        // GET: Designations/Create
        public ActionResult Create()
        {
            List<Department> DepartmentList = new List<Department>();
            DepartmentList = db.Department.Where(i => i.Status == true).ToList();
            ViewBag.DepartmentId = new SelectList(DepartmentList, "Sl", "Name");
            return View();
        }

        // POST: Designations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,Code,Name,DepartmentId,CreatedBy,CreateDate,UpdatedBy,UpdateDate,Status")] Designation designation)
        {
            List<Department> DepartmentList = new List<Department>();
            DepartmentList = db.Department.Where(i => i.Status == true).ToList();
            if (ModelState.IsValid)
            {
                string UserName = User.Identity.Name;
                int userId = db.Users.Where(i => i.UserName == UserName).Select(s => s.CustomUserId).FirstOrDefault();
                designation.CreatedBy = userId;
                designation.CreateDate = DateTime.Now;
                designation.Status = true;
                db.Designation.Add(designation);
                db.SaveChanges();
                TempData["SuccessMsg"] = "Added Successfully !!";
                ViewBag.DepartmentId = new SelectList(DepartmentList, "Sl", "Name");
                return RedirectToAction("Create");
            }
            ViewBag.DepartmentId = new SelectList(DepartmentList, "Sl", "Name", designation.DepartmentId);
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
            Designation designation = db.Designation.Find(id);
            if (designation == null)
            {
                return HttpNotFound();
            }
            List<Department> DepartmentList = new List<Department>();
            DepartmentList = db.Department.Where(i => i.Status == true).ToList();
            ViewBag.DepartmentId = new SelectList(DepartmentList, "Sl", "Name", designation.DepartmentId);
            return View(designation);
        }

        // POST: Designations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,Code,Name,DepartmentId,CreatedBy,CreateDate,UpdatedBy,UpdateDate,Status")] Designation designation)
        {
            List<Department> DepartmentList = new List<Department>();
            DepartmentList = db.Department.Where(i => i.Status == true).ToList();
            if (ModelState.IsValid)
            {
                string UserName = User.Identity.Name;
                int userId = db.Users.Where(i => i.UserName == UserName).Select(s => s.CustomUserId).FirstOrDefault();
                designation.UpdatedBy = userId;
                designation.UpdateDate = DateTime.Now;
                db.Entry(designation).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMsg"] = "Updated Successfully!";
                ViewBag.DepartmentId = new SelectList(DepartmentList, "Sl", "Name", designation.DepartmentId);
                return View(designation);
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            ViewBag.DepartmentId = new SelectList(DepartmentList, "Sl", "Name", designation.DepartmentId);
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
            Designation designation = db.Designation.Find(id);
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
            Designation designation = db.Designation.Find(id);
            designation.Status = false;
            db.Entry(designation).State = EntityState.Modified;
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
