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
    public class LeaveHistoriesController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        #region List
        // GET: LeaveHistories
        public ActionResult Index()
        {
            string userName = User.Identity.Name;
            int userId = _db.Users.Where(i => i.UserName == userName).Select(s => s.CustomUserId).FirstOrDefault();
            List<LeaveHistory> LeaveHistoryList = new List<LeaveHistory>();
            LeaveHistoryList = _db.LeaveHistories.Where(i => i.EmployeeId == userId).Include(i=> i.LeaveType).ToList();
            return View(LeaveHistoryList);
        }
        #endregion

        #region Details
        // GET: LeaveHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveHistory leaveHistory = _db.LeaveHistories.Find(id);
            if (leaveHistory == null)
            {
                return HttpNotFound();
            }
            return View(leaveHistory);
        }
        #endregion

        #region Leave Application
        // GET: LeaveHistories/Create
        public ActionResult Create()
        {
            ViewBag.LeaveTypeId = new SelectList(_db.LeaveTypes, "Sl", "Name");
            return View();
        }

        // POST: LeaveHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,EmployeeId,LeaveTypeId,CreateDate,FromDate,ToDate,Day,Cause,UpdatedBy,UpdateDate,Status,Remarks")] LeaveHistory leaveHistory)
        {
            if (leaveHistory.Cause != null)
            {
                string userName = User.Identity.Name;
                int userId = _db.Users.Where(i => i.UserName == userName).Select(s => s.CustomUserId).FirstOrDefault();
                leaveHistory.EmployeeId = userId;
                leaveHistory.CreateDate = DateTime.Now;
                leaveHistory.Day = (leaveHistory.ToDate - leaveHistory.FromDate.AddDays(-1)).Days;
                leaveHistory.Status = "Pending";
                _db.LeaveHistories.Add(leaveHistory);
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Applied Successfully !!";
                return RedirectToAction("Index");
            }
            ViewBag.LeaveTypeId = new SelectList(_db.LeaveTypes, "Sl", "Name", leaveHistory.LeaveTypeId);
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(leaveHistory);
        }
        #endregion

        #region Leave Approval
        // GET: Resignations
        public ActionResult LeaveApproval()
        {
            return View(_db.LeaveHistories.Include(i=>i.Employee).Include(i => i.LeaveType).ToList());
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult LeaveApproval([Bind(Include = "Sl,EmployeeId,LeaveTypeId,CreateDate,FromDate,ToDate,Day,Cause,UpdatedBy,UpdateDate,Status,Remarks")] LeaveHistory leaveHistory)
        {
            int id = Convert.ToInt32(Request["field-1"]);
            string Status = Convert.ToString(Request["field-2"]);
            string Remarks = Convert.ToString(Request["field-3"]);
            string userName = User.Identity.Name;
            int userId = _db.Users.Where(i => i.UserName == userName).Select(s => s.CustomUserId).FirstOrDefault();

            leaveHistory = _db.LeaveHistories.Find(id);
            if (leaveHistory != null)
            {
                leaveHistory.Status = Status;
                leaveHistory.Remarks = Remarks;
                leaveHistory.UpdatedBy = userId;
                leaveHistory.UpdateDate = DateTime.Now;
                _db.Entry(leaveHistory).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Updated Successfully!";
                return View(leaveHistory);
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(leaveHistory);
        }
        #endregion

        #region Edit
        // GET: LeaveHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveHistory leaveHistory = _db.LeaveHistories.Find(id);
            if (leaveHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.LeaveTypeId = new SelectList(_db.LeaveTypes, "Sl", "Name", leaveHistory.LeaveTypeId);
            return View(leaveHistory);
        }

        // POST: LeaveHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,EmployeeId,LeaveTypeId,CreateDate,FromDate,ToDate,Day,Cause,UpdatedBy,UpdateDate,Status,Remarks")] LeaveHistory leaveHistory)
        {
            if (ModelState.IsValid)
            {
                leaveHistory.Day = (leaveHistory.ToDate - leaveHistory.FromDate.AddDays(-1)).Days;
                leaveHistory.UpdatedBy = leaveHistory.EmployeeId;
                leaveHistory.UpdateDate = DateTime.Now;
                _db.Entry(leaveHistory).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Updated Successfully!";
                ViewBag.LeaveTypeId = new SelectList(_db.LeaveTypes, "Sl", "Name", leaveHistory.LeaveTypeId);
                return View(leaveHistory);
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            ViewBag.LeaveTypeId = new SelectList(_db.LeaveTypes, "Sl", "Name", leaveHistory.LeaveTypeId);
            return View(leaveHistory);
        }
        #endregion

        #region Delete
        // GET: LeaveHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveHistory leaveHistory = _db.LeaveHistories.Find(id);
            if (leaveHistory == null)
            {
                return HttpNotFound();
            }
            return View(leaveHistory);
        }

        // POST: LeaveHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LeaveHistory leaveHistory = _db.LeaveHistories.Find(id);
            _db.LeaveHistories.Remove(leaveHistory);
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
