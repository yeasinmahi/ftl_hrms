using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.DAL;
using FTL_HRMS.Models.Hr;
using FTL_HRMS.Utility;

namespace FTL_HRMS.Controllers
{
    public class DepartmentGroupsController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();
        private readonly GenericData _genericData = GenericData.GetInstance();
        #region List
        // GET: DepartmentGroups
        public ActionResult Index()
        {
            return View(_db.DepartmentGroup.Include(a => a.CreateEmployee).Include(a => a.UpdateEmployee).Where(i=> i.Status==true).ToList());
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
            DepartmentGroup departmentGroup = _db.DepartmentGroup.Find(id);
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
            if (_db.DepartmentGroup.Where(i=> i.Code == departmentGroup.Code).ToList().Count < 1)
            {
                string userName = User.Identity.Name;
                int userId = DbUtility.GetUserId(_db, userName);
                departmentGroup.CreatedBy = userId;
                departmentGroup.CreateDate = DateTime.Now;
                departmentGroup.Status = true;
                DbUtility.Status status = _genericData.Insert<DepartmentGroup>(departmentGroup);
                TempData["message"] = DbUtility.GetStatusMessage(status);
                return RedirectToAction("Create");
            }
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.BlankError);
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
            DepartmentGroup departmentGroup = _db.DepartmentGroup.Find(id);
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
            if (_db.DepartmentGroup.Where(i => i.Sl == departmentGroup.Sl).Select(i=> i.Code).FirstOrDefault() != departmentGroup.Code && departmentGroup.Code != null)
            {
                if(_db.DepartmentGroup.Where(i => i.Code == departmentGroup.Code).ToList().Count < 1)
                {
                    string userName = User.Identity.Name;
                    int userId = DbUtility.GetUserId(_db, userName);
                    departmentGroup.UpdatedBy = userId;
                    departmentGroup.UpdateDate = DateTime.Now;
                    _db.Entry(departmentGroup).State = EntityState.Modified;
                    _db.SaveChanges();
                    TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateSuccess);
                    return View(departmentGroup);
                }
                else
                {
                    TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateFailed);
                    return View(departmentGroup);
                }
            }
            else
            {
                string userName = User.Identity.Name;
                int userId = DbUtility.GetUserId(_db, userName);
                departmentGroup.UpdatedBy = userId;
                departmentGroup.UpdateDate = DateTime.Now;
                _db.Entry(departmentGroup).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateSuccess);
                return View(departmentGroup);
            }
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
            DepartmentGroup departmentGroup = _db.DepartmentGroup.Find(id);
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
            if (_db.Department.Where(i => i.DepartmentGroupId == id && i.Status == true).ToList().Count < 1)
            {
                DepartmentGroup departmentGroup = _db.DepartmentGroup.Find(id);
                departmentGroup.Status = false;
                _db.Entry(departmentGroup).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.DeleteSuccess);
            }
            else
            {
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.Exist);
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
