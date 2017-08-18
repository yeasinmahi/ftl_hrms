﻿using System;
using System.IO;
using System.Text;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using FTL_HRMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace FTL_HRMS.Controllers
{
    public class ReportsController : Controller
    {
        private HRMSDbContext _db = new HRMSDbContext();
        // GET: Reports
      
        #region Employee Report Print 
        public ActionResult EmployeeTypeReport()
        {
            ViewBag.EmployeeTypeId = new SelectList(_db.EmployeeType, "Sl", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult EmployeeTypeReport(string employeeTypeId)
        {
            List<Employee> EmployeeList = new List<Employee>();
            ViewBag.EmployeeTypeId = new SelectList(_db.EmployeeType, "Sl", "Name");
            ViewBag.Status = "SelectType";
            if (String.IsNullOrWhiteSpace(employeeTypeId))
            {
                EmployeeList = _db.Employee.ToList();
            }
            else
            {
                int EmployeeTypeId = Convert.ToInt32(Request["employeeTypeId"]);
                ViewBag.EmployeesTypeId = EmployeeTypeId;
                ViewBag.TypeName = _db.EmployeeType.Where(i => i.Sl == EmployeeTypeId).Select(p => p.Name).FirstOrDefault();
                EmployeeList = _db.Employee.Where(v => v.EmployeeTypeId == EmployeeTypeId).ToList();

            }
            return View(EmployeeList);

        }
        public ActionResult PrintEmployeeList()
        {
            int EmployeeTypeId = Convert.ToInt32(Request["employeeTypeId"]);
            string selectedFormula = "";
            if (EmployeeTypeId > 0)
            {
                selectedFormula = "{tbl_Employee.EmployeeTypeId} = " + EmployeeTypeId;
            }
            return RedirectToAction("PrintReport", "Reports", new { sourceName = "EmployeeReport", fileName = "Employee Report", selectedFormula = selectedFormula });
        }
        #endregion
        
        #region Resign Report
        public ActionResult ResignReport()
        {
            List<Employee> EmployeeList = new List<Employee>();
            EmployeeList = _db.Employee.Where(v => v.Status == false).ToList();
            return View(EmployeeList.ToList());
        }
        public ActionResult PrintResignReport()
        {
            return RedirectToAction("PrintReport", "Reports", new { sourceName = "ResignReport", fileName = "Resign Report", selectedFormula = "{tbl_Employee.Status} = False" });
        }

        #endregion

        #region Department Transfer Report
        public ActionResult TransferReport()
        {
            List<DepartmentTransfer> DepartmentTransferList = new List<DepartmentTransfer>();
            DepartmentTransferList = _db.DepartmentTransfer.ToList();
            return View(DepartmentTransferList.ToList());
        }
        public ActionResult PrintTransferReport()
        {
            return RedirectToAction("PrintReport", "Reports", new { sourceName = "TransferReport", fileName = "Transfer Report" });
        }
        #endregion

        #region Leave Report
        public ActionResult LeaveReport()
        {
            List<LeaveHistory> DepartmentLeaveList = new List<LeaveHistory>();
            DepartmentLeaveList = _db.LeaveHistories.ToList();
            return View(DepartmentLeaveList.ToList());
        }
        public ActionResult PrintLeaveReport()
        {
            return RedirectToAction("PrintReport", "Reports", new { sourceName = "LeaveReport", fileName = "Leave Report" });
        }
        #endregion

        #region Common
        public ActionResult PrintReport(string sourceName, string fileName, string selectedFormula)
        {
            if (String.IsNullOrEmpty(sourceName) && String.IsNullOrEmpty(fileName))
            {
                sourceName = "test";
                fileName = "test";
            }
            else if (String.IsNullOrEmpty(fileName))
            {
                fileName = sourceName;
            }
            ReportDocument rd = GetReport(sourceName);
            SetResponceProperty();
            if (!String.IsNullOrEmpty(selectedFormula))
            {
                rd = SetFolmula(rd, selectedFormula);
            }
            return ExportReport(rd, fileName);
        }

        private ReportDocument SetFolmula(ReportDocument rd, string selectedFormula)
        {
            rd.RecordSelectionFormula = selectedFormula;
            return rd;
        }
        private ReportDocument GetReport(string reportName)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), reportName + ".rpt"));
            rd.SetDatabaseLogon("sa", "sa2009", ".\\SQLEXPRESS", "FTL_HRMS");
            return rd;
        }

        private void SetResponceProperty()
        {
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
        }


        private ActionResult ExportReport(ReportDocument rd, string reportName)
        {
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", reportName + ".pdf");
            }
            catch (Exception exception)
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(exception.Message);
                //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
                MemoryStream stream = new MemoryStream(byteArray);
                return File(stream, "application/pdf", "Error.pdf");
            }
        }

        #endregion
    }
}