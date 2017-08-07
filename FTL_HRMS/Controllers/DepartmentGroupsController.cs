using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.Models;

namespace FTL_HRMS.Controllers
{
    public class DepartmentGroupsController : Controller
    {
        private HRMSDbContext db = new HRMSDbContext();

        #region List
        // GET: DepartmentGroups
        public ActionResult Index()
        {
            return View(db.DepartmentGroup.Where(i=> i.Status==true).ToList());
        }
        #endregion

        #region Details
        // GET: DepartmentGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentGroup departmentGroup = db.DepartmentGroup.Find(id);
            if (departmentGroup == null)
            {
                return HttpNotFound();
            }
            return View(departmentGroup);
        }
        #endregion

        #region Create
        // GET: DepartmentGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DepartmentGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,Code,Name,CreatedBy,CreateDate,UpdatedBy,UpdateDate,Status")] DepartmentGroup departmentGroup)
        {
            if (ModelState.IsValid)
            {
                string UserName = User.Identity.Name;
                int userId = db.Users.Where(i => i.UserName == UserName).Select(s => s.CustomUserId).FirstOrDefault();
                departmentGroup.CreatedBy = userId;
                departmentGroup.CreateDate = DateTime.Now;
                departmentGroup.Status = true;
                db.DepartmentGroup.Add(departmentGroup);
                db.SaveChanges();
                TempData["SuccessMsg"] = "Added Successfully !!";
                return RedirectToAction("Create");
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(departmentGroup);
        }
        #endregion

        #region Edit
        // GET: DepartmentGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentGroup departmentGroup = db.DepartmentGroup.Find(id);
            if (departmentGroup == null)
            {
                return HttpNotFound();
            }
            return View(departmentGroup);
        }

        // POST: DepartmentGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,Code,Name,CreatedBy,CreateDate,UpdatedBy,UpdateDate,Status")] DepartmentGroup departmentGroup)
        {
            if (ModelState.IsValid)
            {
                string UserName = User.Identity.Name;
                int userId = db.Users.Where(i => i.UserName == UserName).Select(s => s.CustomUserId).FirstOrDefault();
                departmentGroup.UpdatedBy = userId;
                departmentGroup.UpdateDate = DateTime.Now;
                db.Entry(departmentGroup).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMsg"] = "Updated Successfully!";
                return View(departmentGroup);
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(departmentGroup);
        }
        #endregion

        #region Delete
        // GET: DepartmentGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentGroup departmentGroup = db.DepartmentGroup.Find(id);
            if (departmentGroup == null)
            {
                return HttpNotFound();
            }
            return View(departmentGroup);
        }

        // POST: DepartmentGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DepartmentGroup departmentGroup = db.DepartmentGroup.Find(id);
            departmentGroup.Status = false;
            db.Entry(departmentGroup).State = EntityState.Modified;
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
