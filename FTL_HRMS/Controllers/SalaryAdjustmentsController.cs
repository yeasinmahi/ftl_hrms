  using System;
  using System.Data.Entity;
using System.Linq;
using System.Net;
  using System.Web.Mvc;
using FTL_HRMS.DAL;
using FTL_HRMS.Models.Payroll;
using FTL_HRMS.Utility;

namespace FTL_HRMS.Controllers
{
    public class SalaryAdjustmentsController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        // GET: SalaryAdjustments
        public ActionResult Index()
        {
            var salaryAdjustment = _db.SalaryAdjustment.Include(s => s.CreateEmployee).Include(s => s.Employee).Include(s => s.UpdateEmployee);
            return View(salaryAdjustment.ToList());
        }

        // GET: SalaryAdjustments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryAdjustment salaryAdjustment = _db.SalaryAdjustment.Find(id);
            if (salaryAdjustment == null)
            {
                return HttpNotFound();
            }
            return View(salaryAdjustment);
        }

        // GET: SalaryAdjustments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalaryAdjustments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,EmployeeId,Date,Amount,Remarks,CreatedBy,CreateDate,UpdatedBy,UpdateDate")] SalaryAdjustment salaryAdjustment)
        {
            string userName = User.Identity.Name;
            int userId = DbUtility.GetUserId(_db, userName);
            if (salaryAdjustment.Amount > 0)
            {
                string Type = Request["Type"].ToString();
                if(Type == "Addition")
                {
                    salaryAdjustment.Amount = +salaryAdjustment.Amount;
                }
                else
                {
                    salaryAdjustment.Amount = -salaryAdjustment.Amount;
                }
                salaryAdjustment.CreatedBy = userId;
                salaryAdjustment.CreateDate = DateTime.Now;
                _db.SalaryAdjustment.Add(salaryAdjustment);
                _db.SaveChanges();
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddSuccess);
                return RedirectToAction("Create");
            }
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddFailed);
            return View(salaryAdjustment);
        }

        // GET: SalaryAdjustments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryAdjustment salaryAdjustment = _db.SalaryAdjustment.Find(id);
            if(salaryAdjustment.Amount < 0)
            {
                ViewBag.Type = "Subtraction";
                salaryAdjustment.Amount = Math.Abs(salaryAdjustment.Amount);
            }
            else
            {
                ViewBag.Type = "Addition";
            }
            if (salaryAdjustment == null)
            {
                return HttpNotFound();
            }
            return View(salaryAdjustment);
        }

        // POST: SalaryAdjustments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,EmployeeId,Date,Amount,Remarks,CreatedBy,CreateDate,UpdatedBy,UpdateDate")] SalaryAdjustment salaryAdjustment)
        {
            if (ModelState.IsValid)
            {
                string Type = Request["Type"].ToString();
                if (Type == "Addition")
                {
                    salaryAdjustment.Amount = +salaryAdjustment.Amount;
                }
                else
                {
                    salaryAdjustment.Amount = -salaryAdjustment.Amount;
                }
                string userName = User.Identity.Name;
                int UserId = DbUtility.GetUserId(_db, userName);
                salaryAdjustment.UpdatedBy = UserId;
                salaryAdjustment.UpdateDate = DateTime.Now;
                _db.Entry(salaryAdjustment).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateSuccess);
                return RedirectToAction("Create");
            }
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateFailed);
            return View(salaryAdjustment);
        }

        // GET: SalaryAdjustments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryAdjustment salaryAdjustment = _db.SalaryAdjustment.Find(id);
            if (salaryAdjustment == null)
            {
                return HttpNotFound();
            }
            return View(salaryAdjustment);
        }

        // POST: SalaryAdjustments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalaryAdjustment salaryAdjustment = _db.SalaryAdjustment.Find(id);
            _db.SalaryAdjustment.Remove(salaryAdjustment);
            _db.SaveChanges();
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.DeleteSuccess);
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
