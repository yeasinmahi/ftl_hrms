using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.Models;

namespace FTL_HRMS.Controllers
{
    public class DisciplinaryActionTypesController : Controller
    {
        private HRMSDbContext db = new HRMSDbContext();

        #region List
        // GET: DisciplinaryActionTypes
        public ActionResult Index()
        {
            return View(db.DisciplinaryActionType.ToList());
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
            DisciplinaryActionType disciplinaryActionType = db.DisciplinaryActionType.Find(id);
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
                db.DisciplinaryActionType.Add(disciplinaryActionType);
                db.SaveChanges();
                TempData["SuccessMsg"] = "Added Successfully !!";
                return RedirectToAction("Create");
            }
            TempData["WarningMsg"] = "Something went wrong !!";
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
            DisciplinaryActionType disciplinaryActionType = db.DisciplinaryActionType.Find(id);
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
                db.Entry(disciplinaryActionType).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMsg"] = "Updated Successfully!";
                return View(disciplinaryActionType);
            }
            TempData["WarningMsg"] = "Something went wrong !!";
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
            DisciplinaryActionType disciplinaryActionType = db.DisciplinaryActionType.Find(id);
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
            DisciplinaryActionType disciplinaryActionType = db.DisciplinaryActionType.Find(id);
            db.DisciplinaryActionType.Remove(disciplinaryActionType);
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