using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.DAL;
using FTL_HRMS.Models;
using FTL_HRMS.Models.Payroll;

namespace FTL_HRMS.Controllers
{
    public class FestivalBonusController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        // GET: FestivalBonus
        public ActionResult Index()
        {
            return View(_db.FestivalBonus.ToList());
        }

        // GET: FestivalBonus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FestivalBonus festivalBonus = _db.FestivalBonus.Find(id);
            if (festivalBonus == null)
            {
                return HttpNotFound();
            }
            return View(festivalBonus);
        }

        // GET: FestivalBonus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FestivalBonus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,BasedOn,BonusPersentage,Date,Remarks")] FestivalBonus festivalBonus)
        {
            if (ModelState.IsValid)
            {
                _db.FestivalBonus.Add(festivalBonus);
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Added Successfully !!";
                return RedirectToAction("Create");
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(festivalBonus);
        }

        // GET: FestivalBonus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FestivalBonus festivalBonus = _db.FestivalBonus.Find(id);
            if (festivalBonus == null)
            {
                return HttpNotFound();
            }
            return View(festivalBonus);
        }

        // POST: FestivalBonus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,BasedOn,BonusPersentage,Date,Remarks")] FestivalBonus festivalBonus)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(festivalBonus).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Added Successfully !!";
                return RedirectToAction("Edit");
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(festivalBonus);
        }

        // GET: FestivalBonus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FestivalBonus festivalBonus = _db.FestivalBonus.Find(id);
            if (festivalBonus == null)
            {
                return HttpNotFound();
            }
            return View(festivalBonus);
        }

        // POST: FestivalBonus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FestivalBonus festivalBonus = _db.FestivalBonus.Find(id);
            _db.FestivalBonus.Remove(festivalBonus);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
