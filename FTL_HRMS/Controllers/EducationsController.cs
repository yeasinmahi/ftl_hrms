using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.Models;

namespace FTL_HRMS.Controllers
{
    public class EducationsController : Controller
    {
        private HRMSDbContext db = new HRMSDbContext();

        #region List
        // GET: Educations
        public ActionResult Index(int employeeId)
        {
            var education = db.Education.Where(i => i.EmployeeId == employeeId).ToList();
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
            Education education = db.Education.Find(id);
            if (education == null)
            {
                return HttpNotFound();
            }
            return View(education);
        }
        #endregion

        #region Create (We don't use it)
        // GET: Educations/Create
        public ActionResult Create()
        {
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
                db.Education.Add(education);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
            Education education = db.Education.Find(id);
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
                db.Entry(education).State = EntityState.Modified;
                db.SaveChanges();
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
            Education education = db.Education.Find(id);
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
            Education education = db.Education.Find(id);
            db.Education.Remove(education);
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
