using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.Models;
using System.Web;
using System;
using FTL_HRMS.DAL;
using FTL_HRMS.Models.Hr;
using FTL_HRMS.Utility;

namespace FTL_HRMS.Controllers
{
    public class ImagesController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        #region List
        // GET: Images
        public ActionResult Index(int employeeId)
        {
            var images = _db.Images.Where(i => i.EmployeeId == employeeId).ToList();
            if(images.Count() == 0)
            {
                ViewBag.NewImage = true;
            }
            else
            {
                ViewBag.NewImage = false;
            }
            ViewBag.EmployeeId = employeeId;
            return View(images);
        }
        #endregion

        #region Details (We don't use it)
        // GET: Images/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Images images = _db.Images.Find(id);
            if (images == null)
            {
                return HttpNotFound();
            }
            return View(images);
        }
        #endregion

        #region Create
        // GET: Images/Create
        public ActionResult Create(int employeeId)
        {
            ViewBag.EmployeeId = employeeId;
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,Image,EmployeeId")] Images images, HttpPostedFileBase image1)
        {
            if (image1 != null)
            {
                images.Image = new byte[image1.ContentLength];
                image1.InputStream.Read(images.Image, 0, image1.ContentLength);
                int empId = Convert.ToInt32(Request["EmployeeId"]);
                images.EmployeeId = empId;
                _db.Images.Add(images);
                _db.SaveChanges();
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddSuccess);
                return RedirectToAction("Create", "Images", new { employeeId = empId });
            }
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddFailed);
            return View(images);
        }
        #endregion

        #region Edit
        // GET: Images/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Images images = _db.Images.Find(id);
            if (images == null)
            {
                return HttpNotFound();
            }
            return View(images);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,Image,EmployeeId")] Images images, HttpPostedFileBase image1)
        {
            if (image1 != null)
            {
                images.EmployeeId = images.EmployeeId;
                images.Image = new byte[image1.ContentLength];
                image1.InputStream.Read(images.Image, 0, image1.ContentLength);
                _db.Entry(images).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateSuccess);
                return RedirectToAction("Index", "Images", new { employeeId = images.EmployeeId });
            }
            return View(images);
        }
        #endregion

        #region Delete
        // GET: Images/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Images images = _db.Images.Find(id);
            if (images == null)
            {
                return HttpNotFound();
            }
            return View(images);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Images images = _db.Images.Find(id);
            int employeeId = images.EmployeeId;
            _db.Images.Remove(images);
            _db.SaveChanges();
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.DeleteSuccess);
            return RedirectToAction("Index", "Images", new { employeeId = employeeId });
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