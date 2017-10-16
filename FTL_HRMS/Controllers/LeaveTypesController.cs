using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.Models.Hr;
using System.Collections.Generic;
using FTL_HRMS.DAL;
using FTL_HRMS.Utility;

namespace FTL_HRMS.Controllers
{
    public class LeaveTypesController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        #region List
        // GET: LeaveTypes
        public ActionResult Index()
        {
            return View(_db.LeaveTypes.ToList());
        }
        #endregion

        #region Details
        // GET: LeaveTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveType leaveType = _db.LeaveTypes.Find(id);
            if (leaveType == null)
            {
                return HttpNotFound();
            }
            return View(leaveType);
        }
        #endregion

        #region Create
        // GET: LeaveTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,Name,Day,IsEditable")] LeaveType leaveType)
        {
            if (ModelState.IsValid)
            {
                leaveType.IsEditable = true;
                _db.LeaveTypes.Add(leaveType);
                _db.SaveChanges();

                #region Add Leave Count                
                if(_db.LeaveCounts.Where(i=> i.LeaveTypeId == leaveType.Sl).Select(i=> i.Sl).Count() < 1)
                {
                    List<Employee> employeeList = new List<Employee>();
                    employeeList = _db.Employee.Where(i => i.Status != false && i.IsSystemOrSuperAdmin != true).ToList();

                    for (int i = 0; i < employeeList.Count; i++)
                    {
                        LeaveCount leaveCount = new LeaveCount();
                        leaveCount.EmployeeId = employeeList[i].Sl;
                        leaveCount.LeaveTypeId = leaveType.Sl;
                        leaveCount.AvailableDay = leaveType.Day;
                        _db.LeaveCounts.Add(leaveCount);
                        _db.SaveChanges();
                    }
                }
                #endregion

                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddSuccess);
                return RedirectToAction("Create");
            }
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddFailed);
            return View(leaveType);
        }
        #endregion

        #region Edit
        // GET: LeaveTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveType leaveType = _db.LeaveTypes.Find(id);
            if (leaveType == null)
            {
                return HttpNotFound();
            }
            return View(leaveType);
        }

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,Name,Day,IsEditable")] LeaveType leaveType)
        {
            if (ModelState.IsValid)
            {
                double OldDays = _db.LeaveTypes.Where(i => i.Sl == leaveType.Sl).Select(i => i.Day).FirstOrDefault();
                double NewDays = leaveType.Day;
                double DifferenceDays = OldDays - NewDays;

                _db.Entry(leaveType).State = EntityState.Modified;
                _db.SaveChanges();

                #region Edit Leave Count                
                if (_db.LeaveCounts.Where(i => i.LeaveTypeId == leaveType.Sl).Select(i => i.Sl).Count() > 0)
                {
                    List<LeaveCount> leaveCountList = new List<LeaveCount>();
                    leaveCountList = _db.LeaveCounts.Where(i => i.LeaveTypeId == leaveType.Sl).ToList();

                    for (int i = 0; i < leaveCountList.Count; i++)
                    {
                        LeaveCount leaveCount = _db.LeaveCounts.Find(leaveCountList[i].Sl);
                        leaveCount.AvailableDay = leaveCount.AvailableDay - DifferenceDays;
                        _db.Entry(leaveCount).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                }
                #endregion

                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateSuccess);
                return View(leaveType);
            }
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateFailed); ;
            return View(leaveType);
        }
        #endregion

        #region Delete
        // GET: LeaveTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveType leaveType = _db.LeaveTypes.Find(id);
            if (leaveType == null)
            {
                return HttpNotFound();
            }
            return View(leaveType);
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            #region Delete Leave Count                
            if (_db.LeaveCounts.Where(i => i.LeaveTypeId == id).Select(i => i.Sl).Count() > 0)
            {
                List<LeaveCount> leaveCountList = new List<LeaveCount>();
                leaveCountList = _db.LeaveCounts.Where(i => i.LeaveTypeId == id).ToList();

                for (int i = 0; i < leaveCountList.Count; i++)
                {
                    LeaveCount leaveCount = _db.LeaveCounts.Find(leaveCountList[i].Sl);
                    _db.LeaveCounts.Remove(leaveCount);
                    _db.SaveChanges();
                }
            }
            #endregion

            #region Delete Leave Histories                
            if (_db.LeaveHistories.Where(i => i.LeaveTypeId == id).Select(i => i.Sl).Count() > 0)
            {
                List<LeaveHistory> leaveHistoryList = new List<LeaveHistory>();
                leaveHistoryList = _db.LeaveHistories.Where(i => i.LeaveTypeId == id).ToList();

                for (int i = 0; i < leaveHistoryList.Count; i++)
                {
                    LeaveHistory leaveHistory = _db.LeaveHistories.Find(leaveHistoryList[i].Sl);
                    _db.LeaveHistories.Remove(leaveHistory);
                    _db.SaveChanges();
                }
            }
            #endregion

            LeaveType leaveType = _db.LeaveTypes.Find(id);
            _db.LeaveTypes.Remove(leaveType);
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
