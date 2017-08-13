using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.Models;

namespace FTL_HRMS.Controllers
{
    public class ResignationsController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        // GET: Resignations
        public ActionResult Index()
        {
            return View(_db.Resignation.ToList());
        }

        // GET: Resignations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resignation resignation = _db.Resignation.Find(id);
            if (resignation == null)
            {
                return HttpNotFound();
            }
            return View(resignation);
        }

        // GET: Resignations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Resignations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,ResignDate,Reason,Suggestion,Status,CreateDate,UpdatedBy,UpdateDate,Remarks,EmployeeId")] Resignation resignation)
        {
            if (ModelState.IsValid)
            {
                _db.Resignation.Add(resignation);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(resignation);
        }

        // GET: Resignations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resignation resignation = _db.Resignation.Find(id);
            if (resignation == null)
            {
                return HttpNotFound();
            }
            return View(resignation);
        }

        // POST: Resignations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,ResignDate,Reason,Suggestion,Status,CreateDate,UpdatedBy,UpdateDate,Remarks,EmployeeId")] Resignation resignation)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(resignation).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resignation);
        }

        // GET: Resignations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resignation resignation = _db.Resignation.Find(id);
            if (resignation == null)
            {
                return HttpNotFound();
            }
            return View(resignation);
        }

        // POST: Resignations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Resignation resignation = _db.Resignation.Find(id);
            _db.Resignation.Remove(resignation);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
