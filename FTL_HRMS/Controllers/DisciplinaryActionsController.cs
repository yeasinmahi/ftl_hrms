using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.DAL;
using FTL_HRMS.Models.Hr;
using FTL_HRMS.Utility;

namespace FTL_HRMS.Controllers
{
    public class DisciplinaryActionsController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        #region List
        // GET: DisciplinaryActions
        public ActionResult Index()
        {
            string userName = User.Identity.Name;
            int userId = DbUtility.GetUserId(_db, userName);
            List<DisciplinaryAction> disciplinaryActionList = _db.DisciplinaryAction.Include(i => i.DisciplinaryActionType).Include(i => i.Employee).Where(i => i.EmployeeId != userId).ToList();
            return View(disciplinaryActionList);
        }
        #endregion

        #region Details (We don't use it)
        // GET: DisciplinaryActions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisciplinaryAction disciplinaryAction = _db.DisciplinaryAction.Find(id);
            if (disciplinaryAction == null)
            {
                return HttpNotFound();
            }
            return View(disciplinaryAction);
        }
        #endregion

        #region Create
        // GET: DisciplinaryActions/Create
        public ActionResult Create()
        {
            ViewBag.DisciplinaryActionTypeId = new SelectList(_db.DisciplinaryActionType, "Sl", "Name");
            return View();
        }

        // POST: DisciplinaryActions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,EmployeeId,DisciplinaryActionTypeId,Date,Remarks")] DisciplinaryAction disciplinaryAction)
        {
            if (ModelState.IsValid)
            {
                _db.DisciplinaryAction.Add(disciplinaryAction);
                _db.SaveChanges();
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddSuccess);
                ViewBag.DisciplinaryActionTypeId = new SelectList(_db.DisciplinaryActionType, "Sl", "Name");
                return RedirectToAction("Create");
            }
            ViewBag.DisciplinaryActionTypeId = new SelectList(_db.DisciplinaryActionType, "Sl", "Name", disciplinaryAction.DisciplinaryActionTypeId);
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddFailed);
            return View(disciplinaryAction);
        }
        #endregion

        #region Edit
        // GET: DisciplinaryActions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisciplinaryAction disciplinaryAction = _db.DisciplinaryAction.Find(id);
            if (disciplinaryAction == null)
            {
                return HttpNotFound();
            }
            ViewBag.DisciplinaryActionTypeId = new SelectList(_db.DisciplinaryActionType, "Sl", "Name", disciplinaryAction.DisciplinaryActionTypeId);
            return View(disciplinaryAction);
        }

        // POST: DisciplinaryActions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,EmployeeId,DisciplinaryActionTypeId,Date,Remarks")] DisciplinaryAction disciplinaryAction)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(disciplinaryAction).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateSuccess);
                ViewBag.DisciplinaryActionTypeId = new SelectList(_db.DisciplinaryActionType, "Sl", "Name");
                return RedirectToAction("Create");
            }
            ViewBag.DisciplinaryActionTypeId = new SelectList(_db.DisciplinaryActionType, "Sl", "Name", disciplinaryAction.DisciplinaryActionTypeId);
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateFailed);
            return View(disciplinaryAction);
        }
        #endregion

        #region Delete
        // GET: DisciplinaryActions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisciplinaryAction disciplinaryAction = _db.DisciplinaryAction.Find(id);
            if (disciplinaryAction == null)
            {
                return HttpNotFound();
            }
            return View(disciplinaryAction);
        }

        // POST: DisciplinaryActions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DisciplinaryAction disciplinaryAction = _db.DisciplinaryAction.Find(id);
            _db.DisciplinaryAction.Remove(disciplinaryAction);
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
