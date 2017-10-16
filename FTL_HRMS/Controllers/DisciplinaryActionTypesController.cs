using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.DAL;
using FTL_HRMS.Models.Hr;
using FTL_HRMS.Utility;

namespace FTL_HRMS.Controllers
{
    public class DisciplinaryActionTypesController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        #region List
        // GET: DisciplinaryActionTypes
        public ActionResult Index()
        {
            return View(_db.DisciplinaryActionType.ToList());
        }
        #endregion

        #region Details
        // GET: DisciplinaryActionTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisciplinaryActionType disciplinaryActionType = _db.DisciplinaryActionType.Find(id);
            if (disciplinaryActionType == null)
            {
                return HttpNotFound();
            }
            return View(disciplinaryActionType);
        }
        #endregion

        #region Create
        // GET: DisciplinaryActionTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DisciplinaryActionTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,Name")] DisciplinaryActionType disciplinaryActionType)
        {
            if (ModelState.IsValid)
            {
                _db.DisciplinaryActionType.Add(disciplinaryActionType);
                _db.SaveChanges();
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddSuccess);
                return RedirectToAction("Create");
            }
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddFailed);
            return View(disciplinaryActionType);
        }
        #endregion

        #region Edit
        // GET: DisciplinaryActionTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisciplinaryActionType disciplinaryActionType = _db.DisciplinaryActionType.Find(id);
            if (disciplinaryActionType == null)
            {
                return HttpNotFound();
            }
            return View(disciplinaryActionType);
        }

        // POST: DisciplinaryActionTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,Name")] DisciplinaryActionType disciplinaryActionType)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(disciplinaryActionType).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateSuccess);
                return View(disciplinaryActionType);
            }
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateFailed);
            return View(disciplinaryActionType);
        }
        #endregion

        #region Delete
        // GET: DisciplinaryActionTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisciplinaryActionType disciplinaryActionType = _db.DisciplinaryActionType.Find(id);
            if (disciplinaryActionType == null)
            {
                return HttpNotFound();
            }
            return View(disciplinaryActionType);
        }

        // POST: DisciplinaryActionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (_db.DisciplinaryAction.Where(i => i.DisciplinaryActionTypeId == id).ToList().Count < 1)
            {
                DisciplinaryActionType disciplinaryActionType = _db.DisciplinaryActionType.Find(id);
                _db.DisciplinaryActionType.Remove(disciplinaryActionType);
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