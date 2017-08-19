using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.Models;
using System;
using System.Collections.Generic;

namespace FTL_HRMS.Controllers
{
    public class ResignationsController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        #region List
        // GET: Resignations
        public ActionResult Index()
        {
            string userName = User.Identity.Name;
            int userId = _db.Users.Where(i => i.UserName == userName).Select(s => s.CustomUserId).FirstOrDefault();
            List<Resignation> resignationList = new List<Resignation>();
            resignationList = _db.Resignation.Where(i => i.EmployeeId == userId).ToList();
            return View(resignationList);
        }
        #endregion

        #region Details
        // GET: Resignations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resignation resignation = _db.Resignation.Find(id);
            if (resignation == null)
            {
                return HttpNotFound();
            }
            return View(resignation);
        }
        #endregion

        #region Resign Application
        // GET: Resignations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Resignations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,ResignDate,Reason,Suggestion,Status,CreateDate,UpdatedBy,UpdateDate,Remarks,EmployeeId")] Resignation resignation)
        {
            if (resignation.ResignDate != null)
            {
                string userName = User.Identity.Name;
                int userId = _db.Users.Where(i => i.UserName == userName).Select(s => s.CustomUserId).FirstOrDefault();
                resignation.EmployeeId = userId;
                resignation.CreateDate = DateTime.Now;
                resignation.Status = "Pending";
                _db.Resignation.Add(resignation);
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Applied Successfully !!";
                return RedirectToAction("Index");
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(resignation);
        }
        #endregion

        #region Resign Approval
        // GET: Resignations
        public ActionResult ResignationApproval()
        {
            string userName = User.Identity.Name;
            int userId = _db.Users.Where(i => i.UserName == userName).Select(s => s.CustomUserId).FirstOrDefault();
            List<Resignation> resignationList = _db.Resignation.Where(x => x.EmployeeId != userId).ToList();
            return View(resignationList);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ResignationApproval([Bind(Include = "Sl,ResignDate,Reason,Suggestion,Status,CreateDate,UpdatedBy,UpdateDate,Remarks,EmployeeId")] Resignation resignation)
        {
            int id = Convert.ToInt32(Request["field-1"]);
            string status = Convert.ToString(Request["field-2"]);
            string remarks = Convert.ToString(Request["field-3"]);
            string userName = User.Identity.Name;
            int userId = _db.Users.Where(i => i.UserName == userName).Select(s => s.CustomUserId).FirstOrDefault();

            resignation = _db.Resignation.Find(id);
            if (resignation != null)
            {
                resignation.Status = status;
                resignation.Remarks = remarks;
                resignation.UpdatedBy = userId;
                resignation.UpdateDate = DateTime.Now;
                _db.Entry(resignation).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Updated Successfully!";
                return RedirectToAction("ResignationApproval", "Resignations");
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            return RedirectToAction("ResignationApproval", "Resignations");
        }
        #endregion

        #region Edit
            // GET: Resignations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resignation resignation = _db.Resignation.Find(id);
            if (resignation == null)
            {
                return HttpNotFound();
            }
            return View(resignation);
        }

        // POST: Resignations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,ResignDate,Reason,Suggestion,Status,CreateDate,UpdatedBy,UpdateDate,Remarks,EmployeeId")] Resignation resignation)
        {
            if (ModelState.IsValid)
            {
                resignation.UpdatedBy = resignation.EmployeeId;
                resignation.UpdateDate = DateTime.Now;
                _db.Entry(resignation).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["SuccessMsg"] = "Updated Successfully!";
                return View(resignation);
            }
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(resignation);
        }
        #endregion

        #region Delete
        // GET: Resignations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resignation resignation = _db.Resignation.Find(id);
            if (resignation == null)
            {
                return HttpNotFound();
            }
            return View(resignation);
        }

        // POST: Resignations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Resignation resignation = _db.Resignation.Find(id);
            _db.Resignation.Remove(resignation);
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
