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
using static FTL_HRMS.Utility.Utility;

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

        #region Loan Application
        // GET: Loans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Loans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,EmployeeId,LoanAmount,CreateDate,LoanReason,LoanDuration,Status,UpdateDate,UpdatedBy,Remarks")] Loan loan)
        {
            if (loan.LoanAmount != 0)
            {
                string userName = User.Identity.Name;
                int userId = DbUtility.GetUserId(_db, userName);
                loan.EmployeeId = userId;
                loan.CreateDate = DateTime.Now;
                loan.Status = "Pending";
                _db.Loan.Add(loan);
                _db.SaveChanges();
                int employeeId = _db.Users.Where(i => i.UserName == userName).Select(s => s.CustomUserId).FirstOrDefault();
                NotificationController.GetInstant().SentMailToAll(NotificationType.Loan, NotificationStatus.Pending, employeeId);
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddSuccess);
                return RedirectToAction("Create");
            }
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddFailed);
            return View(loan);
        }

        #endregion

        #region Edit Loan Application
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
            if (loan.LoanAmount != 0)
            {
                string userName = User.Identity.Name;
                int userId = DbUtility.GetUserId(_db, userName);
                loan.EmployeeId = userId;
                loan.CreateDate = DateTime.Now;
                loan.Status = "Pending";
                _db.Entry(loan).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateSuccess);
                return RedirectToAction("Edit");
            }
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateFailed);
            return View(loan);
        }
        #endregion

        #region Loan Approval

        public ActionResult LoanApproval()
        {
            string userName = User.Identity.Name;
            int userId = DbUtility.GetUserId(_db, userName);
            List<Loan> loanHistoryList = _db.Loan.Include(i => i.Employee).Include(a => a.UpdateEmployee).Where(x => x.EmployeeId != userId).ToList();
            return View(loanHistoryList);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult LoanApproval([Bind(Include = "Sl,EmployeeId,LoanAmount,CreateDate,LoanReason,LoanDuration,Status,UpdateDate,UpdatedBy,Remarks")] Loan loan)
        {
            int id = Convert.ToInt32(Request["field-1"]);
            string status = Convert.ToString(Request["field-2"]);
            string remarks = Convert.ToString(Request["field-3"]);
            string userName = User.Identity.Name;
            int userId = DbUtility.GetUserId(_db, userName);

            loan = _db.Loan.Find(id);
            if (loan != null)
            {
                loan.Status = status;
                loan.Remarks = remarks;
                loan.UpdatedBy = userId;
                loan.UpdateDate = DateTime.Now;
                _db.Entry(loan).State = EntityState.Modified;
                _db.SaveChanges();
                int employeeId = loan.EmployeeId;
                if (status == "Considered")
                {
                    NotificationController.GetInstant().SentMailToAll(NotificationType.Loan, NotificationStatus.Consider, employeeId);
                }
                else if (status == "Canceled")
                {
                    NotificationController.GetInstant().SentMailToAll(NotificationType.Loan, NotificationStatus.Cancel, employeeId);
                }
                if(status == "Approved")
                {
                    LoanCalculation calculation = new LoanCalculation();
                    calculation.EmployeeId = loan.EmployeeId;
                    calculation.LoanId = loan.Sl;
                    calculation.LoanAmount = loan.LoanAmount;
                    calculation.LoanDuration = loan.LoanDuration;
                    _db.LoanCalculation.Add(calculation);
                    _db.SaveChanges();
                    NotificationController.GetInstant().SentMailToAll(NotificationType.Loan, NotificationStatus.Approve, employeeId);
                }
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateSuccess);
                return RedirectToAction("LoanApproval", "Loans");
            }
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateFailed);
            return RedirectToAction("LoanApproval", "Loans");
        }
        #endregion

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
