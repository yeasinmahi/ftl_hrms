using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FTL_HRMS.Models.Hr;
using System.IO;
using FTL_HRMS.DAL;
using FTL_HRMS.Utility;

namespace FTL_HRMS.Controllers
{
    public class FileStoragesController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();

        #region List        
        // GET: FileStorages
        public ActionResult Index()
        {
            var fileStorage = _db.FileStorage.Include(f => f.CreateEmployee).Include(f => f.Employee);
            return View(fileStorage.ToList());
        }
        #endregion

        #region Details (We dont use it)
        // GET: FileStorages/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    FileStorage fileStorage = _db.FileStorage.Find(id);
        //    if (fileStorage == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(fileStorage);
        //}
        #endregion

        #region Create
        // GET: FileStorages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FileStorages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,EmployeeId,Path,CreatedBy,CreateDate")] FileStorage fileStorage)
        {
            string userName = User.Identity.Name;
            int userId = DbUtility.GetUserId(_db, userName);

            string path = Server.MapPath("~/Uploads/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (Request["FileName"] != "")
            {

                string employeeCode = _db.Employee.Where(x => x.Sl == fileStorage.EmployeeId).Select(x => x.Code).FirstOrDefault();
                string filesName = Request["FileName"];
                var file = Request.Files["EmployeeFile"];
                string fullFileName;
                if (file != null && file.ContentLength > 0)
                {
                    string fileExtension = Path.GetExtension(file.FileName);
                    fullFileName = string.Concat(filesName, "_", employeeCode, fileExtension);
                    string fullPath = Path.Combine(path, fullFileName);
                    FileStorage existingFileStorage = _db.FileStorage.FirstOrDefault(x => x.Path.Equals(fullFileName));
                    if (existingFileStorage != null)
                    {
                        TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.Exist);
                        return View(fileStorage);
                    }

                    file.SaveAs(fullPath);

                    
                    fileStorage.EmployeeId = fileStorage.EmployeeId;
                    fileStorage.Path = fullFileName;
                    fileStorage.CreatedBy = userId;
                    fileStorage.CreateDate = Utility.Utility.GetCurrentDateTime();
                    _db.FileStorage.Add(fileStorage);
                    _db.SaveChanges();

                    TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddSuccess);
                    return RedirectToAction("Create");
                }
                else
                {
                    TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.Error);
                }
            }
            else
            {
                TempData["message"] =  DbUtility.GetStatusMessage(DbUtility.Status.NotFound);
            }
            return View(fileStorage);
        }
        #endregion

        #region Edit (we dont use it)
        // GET: FileStorages/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    FileStorage fileStorage = _db.FileStorage.Find(id);
        //    if (fileStorage == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CreatedBy = new SelectList(_db.Employee, "Sl", "Code", fileStorage.CreatedBy);
        //    ViewBag.EmployeeId = new SelectList(_db.Employee, "Sl", "Code", fileStorage.EmployeeId);
        //    return View(fileStorage);
        //}

        // POST: FileStorages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,EmployeeId,Path,CreatedBy,CreateDate")] FileStorage fileStorage)
        {

            if (ModelState.IsValid)
            {
                _db.Entry(fileStorage).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(_db.Employee, "Sl", "Code", fileStorage.EmployeeId);
            return View(fileStorage);
        }
        #endregion

        #region Delete
        // GET: FileStorages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileStorage fileStorage = _db.FileStorage.Find(id);
            if (fileStorage == null)
            {
                return HttpNotFound();
            }
            return View(fileStorage);
        }

        // POST: FileStorages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FileStorage fileStorage = _db.FileStorage.Find(id);
            string employeeCode = _db.Employee.Where(x => x.Sl == fileStorage.EmployeeId).Select(x => x.Code).FirstOrDefault();
            string fullPath = Request.MapPath("~/Uploads/File/" + employeeCode);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            if (fileStorage != null) _db.FileStorage.Remove(fileStorage);
            _db.SaveChanges();
            TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.DeleteSuccess);
            return RedirectToAction("Index");
        }
        #endregion

        #region Download Pdf From List
        public ActionResult DownloadPdf(int id)
        {
            FileStorage fileStorage = _db.FileStorage.Find(id);
            string path = Server.MapPath("~/Uploads/");
            string fullPath=string.Empty;
            if (fileStorage != null)
            {
                fullPath = path+ fileStorage.Path;
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename="+ fileStorage.Path);
                //Response.TransmitFile(fullPath);
            }
            else
            {
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.NotFound);
            }
            //Response.End();
            byte[] stream = System.IO.File.ReadAllBytes(fullPath);
            return File(stream, "application/pdf");
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
