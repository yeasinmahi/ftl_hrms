using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.Models;
using System.Collections.Generic;

namespace FTL_HRMS.Controllers
{
    public class DisciplinaryActionsController : Controller
    {
        private HRMSDbContext db = new HRMSDbContext();

        #region List
        // GET: DisciplinaryActions
        public ActionResult Index()
        {
            return View(db.DisciplinaryAction.Include(i => i.DisciplinaryActionType).Include(i => i.Employee).ToList());
        }
        #endregion

        #region Details
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
        #endregion

        #region Create
        // GET: DisciplinaryActions/Create
        public ActionResult Create()
        {
            List<Employee> EmployeeList = new List<Employee>();
            EmployeeList = db.Employee.Where(i => i.Status == true).ToList();
            ViewBag.EmployeeId = new SelectList(EmployeeList, "Sl", "Name");
            ViewBag.DisciplinaryActionTypeId = new SelectList(db.DisciplinaryActionType, "Sl", "Name");
            return View();
        }

        // POST: DisciplinaryActions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,EmployeeId,DisciplinaryActionTypeId,Date,Remarks")] DisciplinaryAction disciplinaryAction)
        {
            List<Employee> EmployeeList = new List<Employee>();
            EmployeeList = db.Employee.Where(i => i.Status == true).ToList();
            if (ModelState.IsValid)
            {
                db.DisciplinaryAction.Add(disciplinaryAction);
                db.SaveChanges();
                TempData["SuccessMsg"] = "Added Successfully !!";
                ViewBag.EmployeeId = new SelectList(EmployeeList, "Sl", "Name");
                ViewBag.DisciplinaryActionTypeId = new SelectList(db.DisciplinaryActionType, "Sl", "Name");
                return RedirectToAction("Create");
            }
            ViewBag.EmployeeId = new SelectList(EmployeeList, "Sl", "Name", disciplinaryAction.EmployeeId);
            ViewBag.DisciplinaryActionTypeId = new SelectList(db.DisciplinaryActionType, "Sl", "Name", disciplinaryAction.DisciplinaryActionTypeId);
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(disciplinaryAction);
        }
        #endregion

        #region Edit
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
            List<Employee> EmployeeList = new List<Employee>();
            EmployeeList = db.Employee.Where(i => i.Status == true).ToList();
            ViewBag.EmployeeId = new SelectList(EmployeeList, "Sl", "Name", disciplinaryAction.EmployeeId);
            ViewBag.DisciplinaryActionTypeId = new SelectList(db.DisciplinaryActionType, "Sl", "Name", disciplinaryAction.DisciplinaryActionTypeId);
            return View(disciplinaryAction);
        }

        // POST: DisciplinaryActions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,EmployeeId,DisciplinaryActionTypeId,Date,Remarks")] DisciplinaryAction disciplinaryAction)
        {
            List<Employee> EmployeeList = new List<Employee>();
            EmployeeList = db.Employee.Where(i => i.Status == true).ToList();
            if (ModelState.IsValid)
            {
                db.Entry(disciplinaryAction).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMsg"] = "Updated Successfully!";
                ViewBag.EmployeeId = new SelectList(EmployeeList, "Sl", "Name");
                ViewBag.DisciplinaryActionTypeId = new SelectList(db.DisciplinaryActionType, "Sl", "Name");
                return RedirectToAction("Create");
            }
            ViewBag.EmployeeId = new SelectList(EmployeeList, "Sl", "Name", disciplinaryAction.EmployeeId);
            ViewBag.DisciplinaryActionTypeId = new SelectList(db.DisciplinaryActionType, "Sl", "Name", disciplinaryAction.DisciplinaryActionTypeId);
            TempData["WarningMsg"] = "Something went wrong !!";
            return View(disciplinaryAction);
        }
        #endregion

        #region Delete
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
        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
