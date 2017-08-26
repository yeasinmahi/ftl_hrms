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
    public class CompaniesController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        #region List
        // GET: Companies
        public ActionResult Index()
        {
            return View(_db.Company.ToList());
        }
        #endregion

        #region Details
        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = _db.Company.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }
        #endregion

        #region Create
        // GET: Companies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,Name,Address,Email,Website,Phone,Mobile,AlternativeMobile,RegistrationNo,RegistrationDate,TINNumber,StartingDate")] Company company)
        {
            if (company.Name != "")
            {
                _db.Company.Add(company);
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Added Successfully !!";
                return RedirectToAction("Create");
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(company);
        }
        #endregion

        #region Edit
        // GET: Companies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = _db.Company.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,Name,Address,Email,Website,Phone,Mobile,AlternativeMobile,RegistrationNo,RegistrationDate,TINNumber,StartingDate")] Company company)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(company).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Updated Successfully!";
                return View(company);
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(company);
        }
        #endregion

        #region Delete
        // GET: Companies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = _db.Company.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = _db.Company.Find(id);
            _db.Company.Remove(company);
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