using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.Models;

namespace FTL_HRMS.Controllers
{
    public class ExperiencesController : Controller
    {
        private HRMSDbContext db = new HRMSDbContext();

        #region List
        // GET: Experiences
        public ActionResult Index(int employeeId)
        {
            var experience = db.Experience.Where(i => i.EmployeeId == employeeId).ToList();
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
            Experience experience = db.Experience.Find(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            return View(experience);
        }
        #endregion

        #region Create (We don't use it)
        // GET: Experiences/Create
        public ActionResult Create()
        {
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
                db.Experience.Add(experience);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
            Experience experience = db.Experience.Find(id);
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
                db.Entry(experience).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMsg"] = "Updated Successfully!";
                return View(experience);
            }
            TempData["WarningMsg"] = "Something went wrong !!";
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
            Experience experience = db.Experience.Find(id);
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
            Experience experience = db.Experience.Find(id);
            db.Experience.Remove(experience);
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