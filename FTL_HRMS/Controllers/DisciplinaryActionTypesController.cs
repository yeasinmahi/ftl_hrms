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

        // GET: DisciplinaryActionTypes
        public ActionResult Index()
        {
            return View(db.DisciplinaryActionType.ToList());
        }

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
                return RedirectToAction("Index");
            }

            return View(disciplinaryActionType);
        }

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
                return RedirectToAction("Index");
            }
            return View(disciplinaryActionType);
        }

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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
