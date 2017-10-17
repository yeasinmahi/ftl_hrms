using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.DAL;
using FTL_HRMS.Models.Hr;
using FTL_HRMS.Utility;
using Microsoft.AspNet.Identity;
using FTL_HRMS.Models.Payroll;

namespace FTL_HRMS.Controllers
{
    public class LoansController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        // GET: Loans
        public ActionResult Index()
        {
            string userName = User.Identity.Name;
            int userId = DbUtility.GetUserId(_db, userName);
            List<Loan> LoanHistoryList = new List<Loan>();
            LoanHistoryList = _db.Loan.Include(a => a.UpdateEmployee).Where(i => i.EmployeeId == userId).ToList();
            return View(LoanHistoryList);
        }

        // GET: Loans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = _db.Loan.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // GET: Loans/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(_db.Employee, "Sl", "Code");
            ViewBag.UpdatedBy = new SelectList(_db.Employee, "Sl", "Code");
            return View();
        }

        // POST: Loans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,EmployeeId,LoanAmount,CreateDate,LoanReason,LoanDuration,Status,UpdateDate,UpdatedBy,Remarks")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                _db.Loan.Add(loan);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(_db.Employee, "Sl", "Code", loan.EmployeeId);
            ViewBag.UpdatedBy = new SelectList(_db.Employee, "Sl", "Code", loan.UpdatedBy);
            return View(loan);
        }

        // GET: Loans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = _db.Loan.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(_db.Employee, "Sl", "Code", loan.EmployeeId);
            ViewBag.UpdatedBy = new SelectList(_db.Employee, "Sl", "Code", loan.UpdatedBy);
            return View(loan);
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,EmployeeId,LoanAmount,CreateDate,LoanReason,LoanDuration,Status,UpdateDate,UpdatedBy,Remarks")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(loan).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(_db.Employee, "Sl", "Code", loan.EmployeeId);
            ViewBag.UpdatedBy = new SelectList(_db.Employee, "Sl", "Code", loan.UpdatedBy);
            return View(loan);
        }

        // GET: Loans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = _db.Loan.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Loan loan = _db.Loan.Find(id);
            _db.Loan.Remove(loan);
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
