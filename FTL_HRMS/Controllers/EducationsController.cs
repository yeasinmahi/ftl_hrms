using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.Models;
using System;

namespace FTL_HRMS.Controllers
{
    public class EducationsController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        #region List
        // GET: Educations
        public ActionResult Index(int employeeId)
        {
            var education = _db.Education.Where(i => i.EmployeeId == employeeId).ToList();
            ViewBag.EmployeeId = employeeId;
            return View(education);
        }
        #endregion

        #region Details
        // GET: Educations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Education education = _db.Education.Find(id);
            if (education == null)
            {
                return HttpNotFound();
            }
            return View(education);
        }
        #endregion

        #region Create
        // GET: Educations/Create
        public ActionResult Create(int employeeId)
        {
            ViewBag.EmployeeId = employeeId;
            return View();
        }

        // POST: Educations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,InstituteName,Program,Board,Result,FromDate,ToDate,EmployeeId")] Education education)
        {
            if (ModelState.IsValid)
            {
                int empId = Convert.ToInt32(Request["EmployeeId"]);
                education.EmployeeId = empId;
                _db.Education.Add(education);
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Added Successfully !!";
                return RedirectToAction("Create", "Educations", new { employeeId = empId});
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(education);
        }
        #endregion

        #region Edit
        // GET: Educations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Education education = _db.Education.Find(id);
            if (education == null)
            {
                return HttpNotFound();
            }
            return View(education);
        }

        // POST: Educations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,InstituteName,Program,Board,Result,FromDate,ToDate,EmployeeId")] Education education)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(education).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Updated Successfully!";
                return View(education);
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(education);
        }
        #endregion

        #region Delete
        // GET: Educations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Education education = _db.Education.Find(id);
            if (education == null)
            {
                return HttpNotFound();
            }
            return View(education);
        }

        // POST: Educations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Education education = _db.Education.Find(id);
            int employeeId = education.EmployeeId;
            if (education != null) _db.Education.Remove(education);
            _db.SaveChanges();
            return RedirectToAction("Index", "Educations", new { employeeId = employeeId });
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
