using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.Models;

namespace FTL_HRMS.Controllers
{
    public class SourceOfHiresController : Controller
    {
        private HRMSDbContext db = new HRMSDbContext();

        #region List
        // GET: SourceOfHires
        public ActionResult Index()
        {
            return View(db.SourceOfHire.Where(i => i.Status == true).ToList());
        }
        #endregion

        #region Details
        // GET: SourceOfHires/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SourceOfHire sourceOfHire = db.SourceOfHire.Find(id);
            if (sourceOfHire == null)
            {
                return HttpNotFound();
            }
            return View(sourceOfHire);
        }
        #endregion

        #region Create
        // GET: SourceOfHires/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SourceOfHires/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,Name,Status")] SourceOfHire sourceOfHire)
        {
            if (ModelState.IsValid)
            {
                sourceOfHire.Status = true;
                db.SourceOfHire.Add(sourceOfHire);
                db.SaveChanges();
                TempData["SuccessMsg"] = "Added Successfully !!";
                return RedirectToAction("Create");
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(sourceOfHire);
        }
        #endregion

        #region Edit
        // GET: SourceOfHires/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SourceOfHire sourceOfHire = db.SourceOfHire.Find(id);
            if (sourceOfHire == null)
            {
                return HttpNotFound();
            }
            return View(sourceOfHire);
        }

        // POST: SourceOfHires/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,Name,Status")] SourceOfHire sourceOfHire)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sourceOfHire).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMsg"] = "Updated Successfully!";
                return View(sourceOfHire);
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(sourceOfHire);
        }
        #endregion

        #region Delete
        // GET: SourceOfHires/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SourceOfHire sourceOfHire = db.SourceOfHire.Find(id);
            if (sourceOfHire == null)
            {
                return HttpNotFound();
            }
            return View(sourceOfHire);
        }

        // POST: SourceOfHires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SourceOfHire sourceOfHire = db.SourceOfHire.Find(id);
            sourceOfHire.Status = false;
            db.Entry(sourceOfHire).State = EntityState.Modified;
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
