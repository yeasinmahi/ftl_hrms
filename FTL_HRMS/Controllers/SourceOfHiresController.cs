using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.DAL;
using FTL_HRMS.Models;
using FTL_HRMS.Models.Hr;

namespace FTL_HRMS.Controllers
{
    public class SourceOfHiresController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        #region List
        // GET: SourceOfHires
        public ActionResult Index()
        {
            return View(_db.SourceOfHire.Where(i => i.Status == true).ToList());
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
            SourceOfHire sourceOfHire = _db.SourceOfHire.Find(id);
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
            if (sourceOfHire.Name != "")
            {
                sourceOfHire.Status = true;
                _db.SourceOfHire.Add(sourceOfHire);
                _db.SaveChanges();
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
            SourceOfHire sourceOfHire = _db.SourceOfHire.Find(id);
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
                _db.Entry(sourceOfHire).State = EntityState.Modified;
                _db.SaveChanges();
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
            SourceOfHire sourceOfHire = _db.SourceOfHire.Find(id);
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
            if (_db.Employee.Where(i => i.SourceOfHireId == id && i.Status == true).ToList().Count < 1)
            {
                SourceOfHire sourceOfHire = _db.SourceOfHire.Find(id);
                sourceOfHire.Status = false;
                _db.Entry(sourceOfHire).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Deleted Successfully !!";
            }
            else
            {
                TempData["WarningMsg"] = "Already exists some employees under this source !!";
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
