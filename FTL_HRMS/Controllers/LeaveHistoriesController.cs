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
using static FTL_HRMS.Utility.Utility;

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
            int userId = DbUtility.GetUserId(_db, userName);
            List<LeaveHistory> leaveHistoryList = new List<LeaveHistory>();
            leaveHistoryList = _db.LeaveHistories.Include(a => a.UpdateEmployee).Where(i => i.EmployeeId == userId).Include(i=> i.LeaveType).ToList();
            return View(leaveHistoryList);
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
            string userName = User.Identity.Name;
            int userId = DbUtility.GetUserId(_db, userName);
            List<LeaveCount> leaveCountList = new List<LeaveCount>();
            leaveCountList = _db.LeaveCounts.Where(i => i.EmployeeId == userId).ToList();
            ViewBag.LeaveCount = leaveCountList;
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
            string userName = User.Identity.Name;
            int userId = DbUtility.GetUserId(_db, userName);
            leaveHistory.EmployeeId = userId;
            leaveHistory.CreateDate = Utility.Utility.GetCurrentDateTime();
            leaveHistory.Day = (leaveHistory.ToDate - leaveHistory.FromDate.AddDays(-1)).Days;
            leaveHistory.Status = "Pending";

            if (_db.LeaveTypes.Where(i => i.Sl == leaveHistory.LeaveTypeId).Select(i => i.Name).FirstOrDefault() == "Without Pay")
            {
                _db.LeaveHistories.Add(leaveHistory);
                _db.SaveChanges();
                int employeeId = _db.Users.Where(i => i.UserName == userName).Select(s => s.CustomUserId).FirstOrDefault();
                NotificationController.GetInstant().SentMailToAll(NotificationType.Leave, NotificationStatus.Pending, employeeId);
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddSuccess);
                return RedirectToAction("Index");
            }
            else
            {
                if (_db.LeaveCounts.Where(i => i.EmployeeId == userId && i.LeaveTypeId == leaveHistory.LeaveTypeId).Select(i => i.AvailableDay).FirstOrDefault() >= leaveHistory.Day)
                {
                    _db.LeaveHistories.Add(leaveHistory);
                    _db.SaveChanges();
                    int employeeId = _db.Users.Where(i => i.UserName == userName).Select(s => s.CustomUserId).FirstOrDefault();
                    NotificationController.GetInstant().SentMailToAll(NotificationType.Leave, NotificationStatus.Pending, employeeId);
                    TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddSuccess);
                    return RedirectToAction("Index");
                }
                else
                {
                    List<LeaveCount> leaveCountList = new List<LeaveCount>();
                    leaveCountList = _db.LeaveCounts.Where(i => i.EmployeeId == userId).ToList();
                    ViewBag.LeaveCount = leaveCountList;
                    ViewBag.LeaveTypeId = new SelectList(_db.LeaveTypes, "Sl", "Name", leaveHistory.LeaveTypeId);
                    TempData["message"] = "0Exceeds available days !!";
                }
            }
            
            return View(leaveHistory);
        }
        #endregion

        #region Leave Recommendation
        // GET: Resignations
        public ActionResult LeaveRecommendation()
        {
            string userName = User.Identity.Name;
            int userId = DbUtility.GetUserId(_db, userName);
            List<LeaveHistory> leaveHistoryList = _db.LeaveHistories.Include(i => i.Employee).Include(i => i.LeaveType).Include(a => a.UpdateEmployee).Where(x => x.EmployeeId != userId && x.Status != "Approved").ToList();
            return View(leaveHistoryList);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult LeaveRecommendation([Bind(Include = "Sl,EmployeeId,LeaveTypeId,CreateDate,FromDate,ToDate,Day,Cause,UpdatedBy,UpdateDate,Status,Remarks")] LeaveHistory leaveHistory)
        {
            int id = Convert.ToInt32(Request["field-1"]);
            string status = Convert.ToString(Request["field-2"]);
            string remarks = Convert.ToString(Request["field-3"]);
            string userName = User.Identity.Name;
            int userId = DbUtility.GetUserId(_db, userName);

            leaveHistory = _db.LeaveHistories.Find(id);
            if (leaveHistory != null)
            {
                leaveHistory.Status = status;
                leaveHistory.Remarks = remarks;
                leaveHistory.UpdatedBy = userId;
                leaveHistory.UpdateDate = Utility.Utility.GetCurrentDateTime();
                _db.Entry(leaveHistory).State = EntityState.Modified;
                _db.SaveChanges();
                int employeeId = _db.Users.Where(i => i.UserName == userName).Select(s => s.CustomUserId).FirstOrDefault();
                NotificationController.GetInstant().SentMailToAll(NotificationType.Leave, NotificationStatus.Recommendation, employeeId);
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateSuccess);
                string rolll = DbUtility.GetRoll(_db, User.Identity.GetUserId());
                Session["NotifyList"] = NotificationController.GetInstant().GetNotificationListByRoll(rolll, User.Identity.Name);
                return RedirectToAction("LeaveRecommendation", "LeaveHistories");
            }
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateFailed);
            return RedirectToAction("LeaveRecommendation", "LeaveHistories");
        }
        #endregion

        #region Leave Approval
        // GET: Resignations
        public ActionResult LeaveApproval()
        {
            string userName = User.Identity.Name;
            int userId = DbUtility.GetUserId(_db, userName);
            List<LeaveHistory> leaveHistoryList = _db.LeaveHistories.Include(i => i.Employee).Include(i => i.LeaveType).Include(a => a.UpdateEmployee).Where(x => x.EmployeeId != userId && x.Status != "Pending").ToList();
            return View(leaveHistoryList);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult LeaveApproval([Bind(Include = "Sl,EmployeeId,LeaveTypeId,CreateDate,FromDate,ToDate,Day,Cause,UpdatedBy,UpdateDate,Status,Remarks")] LeaveHistory leaveHistory)
        {
            int id = Convert.ToInt32(Request["field-1"]);
            string status = Convert.ToString(Request["field-2"]);
            string remarks = Convert.ToString(Request["field-3"]);
            string userName = User.Identity.Name;
            int userId = DbUtility.GetUserId(_db, userName);

            leaveHistory = _db.LeaveHistories.Find(id);
            if (leaveHistory != null)
            {
                leaveHistory.Status = status;
                leaveHistory.Remarks = remarks;
                leaveHistory.UpdatedBy = userId;
                leaveHistory.UpdateDate = Utility.Utility.GetCurrentDateTime();
                _db.Entry(leaveHistory).State = EntityState.Modified;
                _db.SaveChanges();

                #region Edit Leave Count                
                if (_db.LeaveCounts.Where(i => i.EmployeeId == leaveHistory.EmployeeId && i.LeaveTypeId == leaveHistory.LeaveTypeId).Select(i => i.Sl).Count() > 0)
                {
                    int countId = _db.LeaveCounts.Where(i => i.EmployeeId == leaveHistory.EmployeeId && i.LeaveTypeId == leaveHistory.LeaveTypeId).Select(i => i.Sl).FirstOrDefault();
                    LeaveCount leaveCount = _db.LeaveCounts.Find(countId);
                    if(leaveHistory.LeaveType.Name == "Without Pay")
                    {
                        leaveCount.AvailableDay = leaveCount.AvailableDay + leaveHistory.Day;
                    }
                    else
                    {
                        leaveCount.AvailableDay = leaveCount.AvailableDay - leaveHistory.Day;
                    }
                    _db.Entry(leaveCount).State = EntityState.Modified;
                    _db.SaveChanges();
                }
                #endregion
                string rolll = DbUtility.GetRoll(_db, User.Identity.GetUserId());
                Session["NotifyList"] = NotificationController.GetInstant().GetNotificationListByRoll(rolll, User.Identity.Name);
                int employeeId = leaveHistory.EmployeeId;
                if (status == "Approved")
                {
                    NotificationController.GetInstant().SentMailToAll(NotificationType.Leave, NotificationStatus.Approve, employeeId);
                }
                else if (status == "Canceled")
                {
                    NotificationController.GetInstant().SentMailToAll(NotificationType.Leave, NotificationStatus.Cancel, employeeId);
                }
               
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateSuccess);
                return RedirectToAction("LeaveApproval", "LeaveHistories");
            }
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateFailed);
            return RedirectToAction("LeaveApproval", "LeaveHistories");
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
                leaveHistory.UpdateDate = Utility.Utility.GetCurrentDateTime();
                _db.Entry(leaveHistory).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateSuccess);
                ViewBag.LeaveTypeId = new SelectList(_db.LeaveTypes, "Sl", "Name", leaveHistory.LeaveTypeId);
                return View(leaveHistory);
            }
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateFailed);
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
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.DeleteSuccess);
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
