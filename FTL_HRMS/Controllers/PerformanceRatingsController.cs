using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.DAL;
using FTL_HRMS.Models;
using FTL_HRMS.Models.Hr;
using FTL_HRMS.Utility;

namespace FTL_HRMS.Controllers
{
    public class PerformanceRatingsController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        #region List
        // GET: PerformanceRatings
        public ActionResult Index()
        {
            string userName = User.Identity.Name;
            int userId = DbUtility.GetUserId(_db, userName);
            List<PerformanceRating> performanceRatingList = _db.PerformanceRating.Include(i => i.PerformanceIssue).Include(i => i.Employee).Where(i => i.EmployeeId != userId).ToList();
            return View(performanceRatingList);
        }
        #endregion

        #region Details (We don't use it)
        // GET: PerformanceRatings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerformanceRating performanceRating = _db.PerformanceRating.Find(id);
            if (performanceRating == null)
            {
                return HttpNotFound();
            }
            return View(performanceRating);
        }
        #endregion

        #region Create
        // GET: PerformanceRatings/Create
        public ActionResult Create()
        {
            ViewBag.PerformanceIssueId = new SelectList(_db.PerformanceIssue, "Sl", "Name");
            return View();
        }

        // POST: PerformanceRatings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,Rating,Date,EmployeeId,PerformanceIssueId")] PerformanceRating performanceRating)
        {
            if (ModelState.IsValid)
            {
                _db.PerformanceRating.Add(performanceRating);
                _db.SaveChanges();
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddSuccess);
                ViewBag.PerformanceIssueId = new SelectList(_db.PerformanceIssue, "Sl", "Name");
                return RedirectToAction("Create");
            }
            ViewBag.PerformanceIssueId = new SelectList(_db.PerformanceIssue, "Sl", "Name", performanceRating.PerformanceIssueId);
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddFailed);
            return View(performanceRating);
        }
        #endregion

        #region Edit
        // GET: PerformanceRatings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerformanceRating performanceRating = _db.PerformanceRating.Find(id);
            if (performanceRating == null)
            {
                return HttpNotFound();
            }
            ViewBag.PerformanceIssueId = new SelectList(_db.PerformanceIssue, "Sl", "Name", performanceRating.PerformanceIssueId);
            return View(performanceRating);
        }

        // POST: PerformanceRatings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,Rating,Date,EmployeeId,PerformanceIssueId")] PerformanceRating performanceRating)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(performanceRating).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateSuccess);
                ViewBag.PerformanceIssueId = new SelectList(_db.PerformanceIssue, "Sl", "Name");
                return RedirectToAction("Create");
            }
            ViewBag.PerformanceIssueId = new SelectList(_db.PerformanceIssue, "Sl", "Name", performanceRating.PerformanceIssueId);
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateFailed);
            return View(performanceRating);
        }
        #endregion

        #region Delete (We don't use it)
        // GET: PerformanceRatings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerformanceRating performanceRating = _db.PerformanceRating.Find(id);
            if (performanceRating == null)
            {
                return HttpNotFound();
            }
            return View(performanceRating);
        }

        // POST: PerformanceRatings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PerformanceRating performanceRating = _db.PerformanceRating.Find(id);
            _db.PerformanceRating.Remove(performanceRating);
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
