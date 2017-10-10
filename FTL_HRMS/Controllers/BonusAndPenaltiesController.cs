using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.DAL;
using FTL_HRMS.Models;
using FTL_HRMS.Models.Payroll;
using FTL_HRMS.Utility;
using FTL_HRMS.Models.Hr;
using System.Collections.Generic;
using FTL_HRMS.Models.ViewModels;

namespace FTL_HRMS.Controllers
{
    public class BonusAndPenaltiesController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        // GET: BonusAndPenalties
        public ActionResult Index()
        {
            var bonusAndPenalty = _db.BonusAndPenalty.Include(b => b.CreateEmployee).Include(b => b.Employee).Include(b => b.UpdateEmployee);
            return View(bonusAndPenalty.ToList());
        }

        // GET: BonusAndPenalties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BonusAndPenalty bonusAndPenalty = _db.BonusAndPenalty.Find(id);
            if (bonusAndPenalty == null)
            {
                return HttpNotFound();
            }
            return View(bonusAndPenalty);
        }

        // GET: BonusAndPenalties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BonusAndPenalties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,EmployeeId,Date,Type,Amount,Remarks,CreatedBy,CreateDate,UpdatedBy,UpdateDate")] BonusAndPenalty bonusAndPenalty)
        {
            string userName = User.Identity.Name;
            int userId = DbUtility.GetUserId(_db, userName);
            if (bonusAndPenalty.Type != "")
            {
                bonusAndPenalty.EmployeeId = bonusAndPenalty.EmployeeId;
                bonusAndPenalty.CreatedBy = userId;
                bonusAndPenalty.CreateDate = DateTime.Now;
                _db.BonusAndPenalty.Add(bonusAndPenalty);
                _db.SaveChanges();
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddSuccess);
                return RedirectToAction("Create");
            }
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddFailed);
            return View(bonusAndPenalty);
        }

        // GET: BonusAndPenalties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BonusAndPenalty bonusAndPenalty = _db.BonusAndPenalty.Find(id);
            if (bonusAndPenalty == null)
            {
                return HttpNotFound();
            }            
            return View(bonusAndPenalty);
        }

        // POST: BonusAndPenalties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,EmployeeId,Date,Type,Amount,Remarks,CreatedBy,CreateDate,UpdatedBy,UpdateDate")] BonusAndPenalty bonusAndPenalty)
        {
            if (ModelState.IsValid)
            {
                string userName = User.Identity.Name;
                int UserId = DbUtility.GetUserId(_db, userName);
                bonusAndPenalty.UpdatedBy = UserId;
                bonusAndPenalty.UpdateDate = DateTime.Now;
                _db.Entry(bonusAndPenalty).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateSuccess);
                return RedirectToAction("Edit");
            }
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateFailed);
            return View(bonusAndPenalty);
        }

        // GET: BonusAndPenalties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BonusAndPenalty bonusAndPenalty = _db.BonusAndPenalty.Find(id);
            if (bonusAndPenalty == null)
            {
                return HttpNotFound();
            }
            return View(bonusAndPenalty);
        }

        // POST: BonusAndPenalties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BonusAndPenalty bonusAndPenalty = _db.BonusAndPenalty.Find(id);
            _db.BonusAndPenalty.Remove(bonusAndPenalty);
            _db.SaveChanges();
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.DeleteSuccess);
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
