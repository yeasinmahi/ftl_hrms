using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.Models;
using System.Collections.Generic;

namespace FTL_HRMS.Controllers
{
    public class PerformanceRatingsController : Controller
    {
        private HRMSDbContext db = new HRMSDbContext();

        #region List
        // GET: PerformanceRatings
        public ActionResult Index()
        {
            return View(db.PerformanceRating.Include(i => i.PerformanceIssue).Include(i => i.Employee).ToList());
        }
        #endregion

        #region Details
        // GET: PerformanceRatings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerformanceRating performanceRating = db.PerformanceRating.Find(id);
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
            List<Employee> EmployeeList = new List<Employee>();
            EmployeeList = db.Employee.Where(i => i.Status == true).ToList();
            ViewBag.EmployeeId = new SelectList(EmployeeList, "Sl", "Name");
            ViewBag.PerformanceIssueId = new SelectList(db.PerformanceIssue, "Sl", "Name");
            return View();
        }

        // POST: PerformanceRatings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,Rating,Date,EmployeeId,PerformanceIssueId")] PerformanceRating performanceRating)
        {
            List<Employee> EmployeeList = new List<Employee>();
            EmployeeList = db.Employee.Where(i => i.Status == true).ToList();
            if (ModelState.IsValid)
            {
                db.PerformanceRating.Add(performanceRating);
                db.SaveChanges();
                TempData["SuccessMsg"] = "Added Successfully !!";
                ViewBag.EmployeeId = new SelectList(EmployeeList, "Sl", "Name");
                ViewBag.PerformanceIssueId = new SelectList(db.PerformanceIssue, "Sl", "Name");
                return RedirectToAction("Create");
            }
            ViewBag.EmployeeId = new SelectList(EmployeeList, "Sl", "Name", performanceRating.EmployeeId);
            ViewBag.PerformanceIssueId = new SelectList(db.PerformanceIssue, "Sl", "Name", performanceRating.PerformanceIssueId);
            TempData["WarningMsg"] = "Something went wrong !!";
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
            PerformanceRating performanceRating = db.PerformanceRating.Find(id);
            if (performanceRating == null)
            {
                return HttpNotFound();
            }
            List<Employee> EmployeeList = new List<Employee>();
            EmployeeList = db.Employee.Where(i => i.Status == true).ToList();
            ViewBag.EmployeeId = new SelectList(EmployeeList, "Sl", "Name", performanceRating.EmployeeId);
            ViewBag.PerformanceIssueId = new SelectList(db.PerformanceIssue, "Sl", "Name", performanceRating.PerformanceIssueId);
            return View(performanceRating);
        }

        // POST: PerformanceRatings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,Rating,Date,EmployeeId,PerformanceIssueId")] PerformanceRating performanceRating)
        {
            List<Employee> EmployeeList = new List<Employee>();
            EmployeeList = db.Employee.Where(i => i.Status == true).ToList();
            if (ModelState.IsValid)
            {
                db.Entry(performanceRating).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMsg"] = "Updated Successfully!";
                ViewBag.EmployeeId = new SelectList(EmployeeList, "Sl", "Name");
                ViewBag.PerformanceIssueId = new SelectList(db.PerformanceIssue, "Sl", "Name");
                return RedirectToAction("Create");
            }
            ViewBag.EmployeeId = new SelectList(EmployeeList, "Sl", "Name", performanceRating.EmployeeId);
            ViewBag.PerformanceIssueId = new SelectList(db.PerformanceIssue, "Sl", "Name", performanceRating.PerformanceIssueId);
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(performanceRating);
        }
        #endregion

        #region Delete
        // GET: PerformanceRatings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerformanceRating performanceRating = db.PerformanceRating.Find(id);
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
            PerformanceRating performanceRating = db.PerformanceRating.Find(id);
            db.PerformanceRating.Remove(performanceRating);
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
