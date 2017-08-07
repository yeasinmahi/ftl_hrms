using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.Models;

namespace FTL_HRMS.Controllers
{
    public class PerformanceIssuesController : Controller
    {
        private HRMSDbContext db = new HRMSDbContext();

        // GET: PerformanceIssues
        public ActionResult Index()
        {
            return View(db.PerformanceIssue.ToList());
        }

        // GET: PerformanceIssues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerformanceIssue performanceIssue = db.PerformanceIssue.Find(id);
            if (performanceIssue == null)
            {
                return HttpNotFound();
            }
            return View(performanceIssue);
        }

        // GET: PerformanceIssues/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PerformanceIssues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,Name")] PerformanceIssue performanceIssue)
        {
            if (ModelState.IsValid)
            {
                db.PerformanceIssue.Add(performanceIssue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(performanceIssue);
        }

        // GET: PerformanceIssues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerformanceIssue performanceIssue = db.PerformanceIssue.Find(id);
            if (performanceIssue == null)
            {
                return HttpNotFound();
            }
            return View(performanceIssue);
        }

        // POST: PerformanceIssues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,Name")] PerformanceIssue performanceIssue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(performanceIssue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(performanceIssue);
        }

        // GET: PerformanceIssues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerformanceIssue performanceIssue = db.PerformanceIssue.Find(id);
            if (performanceIssue == null)
            {
                return HttpNotFound();
            }
            return View(performanceIssue);
        }

        // POST: PerformanceIssues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PerformanceIssue performanceIssue = db.PerformanceIssue.Find(id);
            db.PerformanceIssue.Remove(performanceIssue);
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
