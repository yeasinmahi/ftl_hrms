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
    public class DisciplinaryActionsController : Controller
    {
        private HRMSDbContext db = new HRMSDbContext();

        // GET: DisciplinaryActions
        public ActionResult Index()
        {
            return View(db.DisciplinaryAction.ToList());
        }

        // GET: DisciplinaryActions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisciplinaryAction disciplinaryAction = db.DisciplinaryAction.Find(id);
            if (disciplinaryAction == null)
            {
                return HttpNotFound();
            }
            return View(disciplinaryAction);
        }

        // GET: DisciplinaryActions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DisciplinaryActions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,EmployeeId,DisciplinaryActionTypeId,Date,Remarks")] DisciplinaryAction disciplinaryAction)
        {
            if (ModelState.IsValid)
            {
                db.DisciplinaryAction.Add(disciplinaryAction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(disciplinaryAction);
        }

        // GET: DisciplinaryActions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisciplinaryAction disciplinaryAction = db.DisciplinaryAction.Find(id);
            if (disciplinaryAction == null)
            {
                return HttpNotFound();
            }
            return View(disciplinaryAction);
        }

        // POST: DisciplinaryActions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,EmployeeId,DisciplinaryActionTypeId,Date,Remarks")] DisciplinaryAction disciplinaryAction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(disciplinaryAction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(disciplinaryAction);
        }

        // GET: DisciplinaryActions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisciplinaryAction disciplinaryAction = db.DisciplinaryAction.Find(id);
            if (disciplinaryAction == null)
            {
                return HttpNotFound();
            }
            return View(disciplinaryAction);
        }

        // POST: DisciplinaryActions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DisciplinaryAction disciplinaryAction = db.DisciplinaryAction.Find(id);
            db.DisciplinaryAction.Remove(disciplinaryAction);
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
