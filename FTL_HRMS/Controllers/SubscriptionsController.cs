using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FTL_HRMS.DAL;
using FTL_HRMS.Models.Hr;
using FTL_HRMS.Utility;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace FTL_HRMS.Controllers
{
    public class SubscriptionsController : Controller
    {
        private HRMSDbContext db = new HRMSDbContext();

        #region List (We don't use it)
        // GET: Subscriptions
        public ActionResult Index()
        {
            return View(db.Subscription.ToList());
        }
        #endregion 

        #region Details (We don't use it)
        // GET: Subscriptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscription subscription = db.Subscription.Find(id);
            if (subscription == null)
            {
                return HttpNotFound();
            }
            return View(subscription);
        }
        #endregion

        #region Create (We don't use it)
        // GET: Subscriptions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subscriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sl,Code,Date")] Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                db.Subscription.Add(subscription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subscription);
        }
        #endregion

        #region Edit
        // GET: Subscriptions/Edit/5
        public ActionResult Edit()
        {
            if (db.Subscription.Select(i => i.Sl).Count() > 0)
            {
                int id = db.Subscription.Select(i => i.Sl).FirstOrDefault();
                Subscription subscription = db.Subscription.Find(id);
                if (subscription == null)
                {
                    return HttpNotFound();
                }
                return View(subscription);
            }
            else
            {
                return View();
            }
        }

        // POST: Subscriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sl,Code,Date")] Subscription subscription)
        {
            string strEncrypted = "▓╖▓╖▓╖";
            subscription.Code = EncryptString(strEncrypted);

            if (subscription.Sl != 0)
            {
                db.Entry(subscription).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.UpdateSuccess);
                return View(subscription);
            }
            else
            {
                db.Subscription.Add(subscription);
                db.SaveChanges();
                TempData["message"] = DbUtility.GetStatusMessage(DbUtility.Status.AddSuccess);
                return View(subscription);
            }
        }
        #endregion

        #region Delete (We don't use it)
        // GET: Subscriptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscription subscription = db.Subscription.Find(id);
            if (subscription == null)
            {
                return HttpNotFound();
            }
            return View(subscription);
        }

        // POST: Subscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subscription subscription = db.Subscription.Find(id);
            db.Subscription.Remove(subscription);
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

        #region Encrypt
        public string EncryptString(string str)
        {
            string EncrptKey = "2013;[pnuLIT)WebCodeExpert";
            byte[] byKey = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
            byKey = System.Text.Encoding.UTF8.GetBytes(EncrptKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(str);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }
        #endregion
    }
    }
