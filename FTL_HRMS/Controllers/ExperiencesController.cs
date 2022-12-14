using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System;
using FTL_HRMS.DAL;
using FTL_HRMS.Models.Hr;
using FTL_HRMS.Utility;

namespace FTL_HRMS.Controllers
{
    public class ExperiencesController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        #region List
        // GET: Experiences
        public ActionResult Index(int employeeId)
        {
            var experience = _db.Experience.Where(i => i.EmployeeId == employeeId).ToList();
            ViewBag.EmployeeId = employeeId;
            return View(experience);
        }
        #endregion

        #region Details
        // GET: Experiences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experience experience = _db.Experience.Find(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            return View(experience);
        }
        #endregion

        #region Create
        // GET: Experiences/Create
        public ActionResult Create(int employeeId)
        {
            ViewBag.EmployeeId = employeeId;
            return View();
        }

        // POST: Experiences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,InstituteName,InstituteAddress,Website,Phone,Designation,FromDate,ToDate,EmployeeId")] Experience experience)
        {
            if (ModelState.IsValid)
            {
                int empId = Convert.ToInt32(Request["EmployeeId"]);
                experience.EmployeeId = empId;
                _db.Experience.Add(experience);
                _db.SaveChanges();
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddSuccess);
                return RedirectToAction("Create", "Experiences", new { employeeId = empId });
            }
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddFailed);
            return View(experience);
        }
        #endregion

        #region Edit
        // GET: Experiences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experience experience = _db.Experience.Find(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            return View(experience);
        }

        // POST: Experiences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,InstituteName,InstituteAddress,Website,Phone,Designation,FromDate,ToDate,EmployeeId")] Experience experience)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(experience).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateSuccess);
                return View(experience);
            }
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateFailed);
            return View(experience);
        }
        #endregion

        #region Delete
        // GET: Experiences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experience experience = _db.Experience.Find(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            return View(experience);
        }

        // POST: Experiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Experience experience = _db.Experience.Find(id);
            int employeeId = experience.EmployeeId;
            _db.Experience.Remove(experience);
            _db.SaveChanges();
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.DeleteSuccess);
            return RedirectToAction("Index", "Experiences", new { employeeId = employeeId });
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