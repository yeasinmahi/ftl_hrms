﻿using System;
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
        private HRMSDbContext _db = new HRMSDbContext();

        #region List (We don't use it)
        // GET: SalaryDistributions
        public ActionResult Index()
        {
            return View(_db.SalaryDistribution.ToList());
        }
        #endregion

        #region Details (We don't use it)
        // GET: SalaryDistributions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryDistribution salaryDistribution = _db.SalaryDistribution.Find(id);
            if (salaryDistribution == null)
            {
                return HttpNotFound();
            }
            return View(salaryDistribution);
        }
        #endregion

        #region Create (We don't use it)
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
        public ActionResult Create([Bind(Include = "Sl,BasicSalary,HouseRent,MedicalAllowance,LifeInsurance,FoodAllowance,Entertainment")] SalaryDistribution salaryDistribution)
        {
            if (salaryDistribution.BasicSalary != 0)
            {
                _db.SalaryDistribution.Add(salaryDistribution);
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Added Successfully !!";
                return RedirectToAction("Create");
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(salaryDistribution);
        }
        #endregion

        #region Edit
        // GET: SalaryDistributions/Edit/5
        public ActionResult Edit()
        {
            if (_db.SalaryDistribution.Select(i => i.Sl).Count() > 0)
            {
                int id = _db.SalaryDistribution.Select(i => i.Sl).FirstOrDefault();
                SalaryDistribution salaryDistribution = _db.SalaryDistribution.Find(id);
                if (salaryDistribution == null)
                {
                    return HttpNotFound();
                }
                return View(salaryDistribution);
            }
            else
            {
                return View();
            }            
        }

        // POST: SalaryDistributions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,BasicSalary,HouseRent,MedicalAllowance,LifeInsurance,FoodAllowance,Entertainment")] SalaryDistribution salaryDistribution)
        {
            double gross = salaryDistribution.BasicSalary + salaryDistribution.HouseRent + salaryDistribution.MedicalAllowance +
                salaryDistribution.LifeInsurance + salaryDistribution.FoodAllowance + salaryDistribution.Entertainment;

            if (gross == 100)
            {
                if (salaryDistribution.Sl != 0)
                {
                    _db.Entry(salaryDistribution).State = EntityState.Modified;
                    _db.SaveChanges();
                    TempData["SuccessMsg"] = "Updated Successfully!";
                    return View(salaryDistribution);
                }
                else
                {
                    _db.SalaryDistribution.Add(salaryDistribution);
                    _db.SaveChanges();
                    TempData["SuccessMsg"] = "Added Successfully !!";
                    return View(salaryDistribution);
                }
            }
            else
            {
                TempData["WarningMsg"] = "Total must be 100% !!";
                return View(salaryDistribution);
            }
        }
        #endregion

        #region Delete (We don't use it)
        // GET: SalaryDistributions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryDistribution salaryDistribution = _db.SalaryDistribution.Find(id);
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
            SalaryDistribution salaryDistribution = _db.SalaryDistribution.Find(id);
            _db.SalaryDistribution.Remove(salaryDistribution);
            _db.SaveChanges();
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