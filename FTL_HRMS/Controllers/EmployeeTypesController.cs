using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.DAL;
using FTL_HRMS.Models.Hr;
using FTL_HRMS.Utility;

namespace FTL_HRMS.Controllers
{
    public class EmployeeTypesController : Controller
    {
        private readonly HRMSDbContext _db = new HRMSDbContext();

        #region List
        // GET: EmployeeTypes
        public ActionResult Index()
        {
            return View(_db.EmployeeType.Where(i => i.Status).ToList());
        }
        #endregion

        #region Details
        // GET: EmployeeTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeType employeeType = _db.EmployeeType.Find(id);
            if (employeeType == null)
            {
                return HttpNotFound();
            }
            return View(employeeType);
        }
        #endregion

        #region Create
        // GET: EmployeeTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,Name,Status")] EmployeeType employeeType)
        {
            if (employeeType.Name != "")
            {
                employeeType.Status = true;
                EmployeeType type = _db.EmployeeType.ToList().FirstOrDefault(x => x.Name.Equals(employeeType.Name) && x.Status);
                if (type ==null)
                {
                    _db.EmployeeType.Add(employeeType);
                    _db.SaveChanges();
                    TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddSuccess);
                }
                else
                {
                    TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.Exist);
                }
               
                return RedirectToAction("Create");
            }
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddFailed);
            return View(employeeType);
        }
        #endregion

        #region Edit
        // GET: EmployeeTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeType employeeType = _db.EmployeeType.Find(id);
            if (employeeType == null)
            {
                return HttpNotFound();
            }
            return View(employeeType);
        }

        // POST: EmployeeTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,Name,Status")] EmployeeType employeeType)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(employeeType).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateSuccess);
                return View(employeeType);
            }
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateFailed);
            return View(employeeType);
        }
        #endregion

        #region Delete
        // GET: EmployeeTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeType employeeType = _db.EmployeeType.Find(id);
            if (employeeType == null)
            {
                return HttpNotFound();
            }
            return View(employeeType);
        }

        // POST: EmployeeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (_db.Employee.Where(i => i.EmployeeTypeId == id && i.Status).ToList().Count < 1)
            {
                EmployeeType employeeType = _db.EmployeeType.Find(id);
                if (employeeType != null)
                {
                    employeeType.Status = false;
                    _db.Entry(employeeType).State = EntityState.Modified;
                }
                _db.SaveChanges();
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.DeleteSuccess);
            }
            else
            {
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.NotAllowed);
            }
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
