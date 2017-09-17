using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FTL_HRMS.Models;
using FTL_HRMS.Models.Payroll;

namespace FTL_HRMS.Controllers
{
    public class FestibleBonusController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        // GET: FestibleBonus
        public ActionResult Index()
        {
            return View(_db.FestibleBonus.ToList());
        }

        // GET: FestibleBonus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FestibleBonus festibleBonus = _db.FestibleBonus.Find(id);
            if (festibleBonus == null)
            {
                return HttpNotFound();
            }
            return View(festibleBonus);
        }

        // GET: FestibleBonus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FestibleBonus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,BasedOn,BonusPersentage,Date,Remarks")] FestibleBonus festibleBonus)
        {
            if (ModelState.IsValid)
            {
                _db.FestibleBonus.Add(festibleBonus);
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Added Successfully !!";
                return RedirectToAction("Create");
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(festibleBonus);
        }

        // GET: FestibleBonus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FestibleBonus festibleBonus = _db.FestibleBonus.Find(id);
            if (festibleBonus == null)
            {
                return HttpNotFound();
            }
            return View(festibleBonus);
        }

        // POST: FestibleBonus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,BasedOn,BonusPersentage,Date,Remarks")] FestibleBonus festibleBonus)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(festibleBonus).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Added Successfully !!";
                return RedirectToAction("Edit");
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(festibleBonus);
        }

        // GET: FestibleBonus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FestibleBonus festibleBonus = _db.FestibleBonus.Find(id);
            if (festibleBonus == null)
            {
                return HttpNotFound();
            }
            return View(festibleBonus);
        }

        // POST: FestibleBonus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FestibleBonus festibleBonus = _db.FestibleBonus.Find(id);
            _db.FestibleBonus.Remove(festibleBonus);
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
