using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.DAL;
using FTL_HRMS.Models;
using FTL_HRMS.Models.Hr;

namespace FTL_HRMS.Controllers
{
    public class PerformanceIssuesController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        #region List
        // GET: PerformanceIssues
        public ActionResult Index()
        {
            return View(_db.PerformanceIssue.ToList());
        }
        #endregion

        #region Details
        // GET: PerformanceIssues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerformanceIssue performanceIssue = _db.PerformanceIssue.Find(id);
            if (performanceIssue == null)
            {
                return HttpNotFound();
            }
            return View(performanceIssue);
        }
        #endregion

        #region Create
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
                _db.PerformanceIssue.Add(performanceIssue);
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Added Successfully !!";
                return RedirectToAction("Create");
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(performanceIssue);
        }
        #endregion

        #region Edit
        // GET: PerformanceIssues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerformanceIssue performanceIssue = _db.PerformanceIssue.Find(id);
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
                _db.Entry(performanceIssue).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Updated Successfully!";
                return View(performanceIssue);
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(performanceIssue);
        }
        #endregion

        #region Delete
        // GET: PerformanceIssues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerformanceIssue performanceIssue = _db.PerformanceIssue.Find(id);
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
            if (_db.PerformanceRating.Where(i => i.PerformanceIssueId == id).ToList().Count < 1)
            {
                PerformanceIssue performanceIssue = _db.PerformanceIssue.Find(id);
                _db.PerformanceIssue.Remove(performanceIssue);
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Deleted Successfully !!";
            }
            else
            {
                TempData["WarningMsg"] = "Already exists some ratings using this issue !!";
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