using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FTL_HRMS.Models;

namespace FTL_HRMS.Controllers
{
    public class SalaryDistributionsController : Controller
    {
        private HRMSDbContext db = new HRMSDbContext();

        // GET: SalaryDistributions
        public ActionResult Index()
        {
            return View(db.SalaryDistribution.ToList());
        }

        // GET: SalaryDistributions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryDistribution salaryDistribution = db.SalaryDistribution.Find(id);
            if (salaryDistribution == null)
            {
                return HttpNotFound();
            }
            return View(salaryDistribution);
        }

        // GET: SalaryDistributions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalaryDistributions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,GrossSalary,BasicSalary,HouseRent,MedicalAllowance,LifeInsurance,FoodAllowance,Entertainment")] SalaryDistribution salaryDistribution)
        {
            if (ModelState.IsValid)
            {
                db.SalaryDistribution.Add(salaryDistribution);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(salaryDistribution);
        }

        // GET: SalaryDistributions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryDistribution salaryDistribution = db.SalaryDistribution.Find(id);
            if (salaryDistribution == null)
            {
                return HttpNotFound();
            }
            return View(salaryDistribution);
        }

        // POST: SalaryDistributions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,GrossSalary,BasicSalary,HouseRent,MedicalAllowance,LifeInsurance,FoodAllowance,Entertainment")] SalaryDistribution salaryDistribution)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salaryDistribution).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(salaryDistribution);
        }

        // GET: SalaryDistributions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryDistribution salaryDistribution = db.SalaryDistribution.Find(id);
            if (salaryDistribution == null)
            {
                return HttpNotFound();
            }
            return View(salaryDistribution);
        }

        // POST: SalaryDistributions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalaryDistribution salaryDistribution = db.SalaryDistribution.Find(id);
            db.SalaryDistribution.Remove(salaryDistribution);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
