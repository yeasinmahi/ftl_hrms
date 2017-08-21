using System;
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
            List<Employee> employeeList = new List<Employee>();
            ViewBag.EmployeeTypeId = new SelectList(_db.EmployeeType, "Sl", "Name");
            ViewBag.Status = "SelectType";
            if (String.IsNullOrWhiteSpace(employeeTypeId))
            {
                employeeList = _db.Employee.ToList();
            }
            else
            {
                int EmployeeTypeId = Convert.ToInt32(Request["employeeTypeId"]);
                ViewBag.EmployeesTypeId = EmployeeTypeId;
                ViewBag.TypeName = _db.EmployeeType.Where(i => i.Sl == EmployeeTypeId).Select(p => p.Name).FirstOrDefault();
                employeeList = _db.Employee.Where(v => v.EmployeeTypeId == EmployeeTypeId).ToList();

            }
            return View(employeeList);

        }
        public ActionResult PrintEmployeeList()
        {
            int employeeTypeId = Convert.ToInt32(Request["employeeTypeId"]);
            string selectedFormula = "";
            if (employeeTypeId > 0)
            {
                selectedFormula = "{tbl_Employee.EmployeeTypeId} = " + employeeTypeId;
            }
            return RedirectToAction("PrintReport", "Reports", new { sourceName = "EmployeeReport", fileName = "Employee Report", selectedFormula = selectedFormula });
        }
        #endregion

        #region Resign Report
        public ActionResult ResignReport()
        {
            List<Resignation> ResignEmployeeList = new List<Resignation>();
            return View(ResignEmployeeList);
        }
        public ActionResult ResignReportDateView()
        {
            DateTime FromDate = Convert.ToDateTime(Request["FromDate"]);
            DateTime ToDate = Convert.ToDateTime(Request["ToDate"]);
            ViewBag.FromDate = FromDate;
            ViewBag.ToDate = ToDate;
            List<Resignation> ResignEmployeeList = _db.Resignation.Where(t => t.ResignDate >= FromDate && t.ResignDate <= ToDate).ToList();
            return View("ResignReport", ResignEmployeeList);
        }
        public ActionResult PrintResignReport()
        {
            DateTime FromDate = Convert.ToDateTime(Request["fromDate"]);
            DateTime ToDate = Convert.ToDateTime(Request["toDate"]);
            return RedirectToAction("PrintReport", "Reports", new { sourceName = "ResignReport", fileName = "Resign Report", selectedFormula = "{tbl_Resignation.ResignDate}>=Date ("+FromDate.ToString("yyyy,MM,dd")+ ") and {tbl_Resignation.ResignDate}<= Date (" + ToDate.ToString("yyyy,MM,dd") + " )" });
        }

        #endregion

        #region Department Transfer Report
        public ActionResult DepartmentTransferReport()
        {
            List<DepartmentTransfer> DepartmentTransferList = new List<DepartmentTransfer>();
            return View(DepartmentTransferList);
        }
        public ActionResult DepartmentTransferReportDateView()
        {
            DateTime FromDate = Convert.ToDateTime(Request["FromDate"]);
            DateTime ToDate = Convert.ToDateTime(Request["ToDate"]);
            ViewBag.FromDate = FromDate;
            ViewBag.ToDate = ToDate;
            List<DepartmentTransfer> DepartmentTransferList = _db.DepartmentTransfer.Where(t => t.TransferDate >= FromDate && t.TransferDate <= ToDate).ToList();
            return View("DepartmentTransferReport", DepartmentTransferList);
        }

        public ActionResult PrintDepartmentTransferReport()
        {
            DateTime FromDate = Convert.ToDateTime(Request["fromDate"]);
            DateTime ToDate = Convert.ToDateTime(Request["toDate"]);
            return RedirectToAction("PrintReport", "Reports", new { sourceName = "DepartmentTransferReport", fileName = "Department Transfer Report", selectedFormula = "{DepartmentTransferView.TransferDate}>=Date (" + FromDate.ToString("yyyy,MM,dd") + ") and {DepartmentTransferView.TransferDate}<= Date (" + ToDate.ToString("yyyy,MM,dd") + " )" });
        }
        #endregion

        #region Branch Transfer Report
        public ActionResult BranchTransferReport()
        {
            List<BranchTransfer>  BranchTransferList = new List<BranchTransfer>();
            return View(BranchTransferList);
        }
        public ActionResult BranchTransferReportDateView()
        {
            DateTime FromDate = Convert.ToDateTime(Request["FromDate"]);
            DateTime ToDate = Convert.ToDateTime(Request["ToDate"]);
            ViewBag.FromDate = FromDate;
            ViewBag.ToDate = ToDate;
            List<BranchTransfer> BranchTransferList = _db.BranchTransfer.Where(t => t.TransferDate >= FromDate && t.TransferDate <= ToDate).ToList();
            return View("BranchTransferReport", BranchTransferList);
        }
        public ActionResult PrintBranchTransferReport()
        {
            DateTime FromDate = Convert.ToDateTime(Request["fromDate"]);
            DateTime ToDate = Convert.ToDateTime(Request["toDate"]);
            return RedirectToAction("PrintReport", "Reports", new { sourceName = "BranchTransferReport", fileName = "Branch Transfer Report", selectedFormula = "{BranchTransferView.TransferDate}>=Date (" + FromDate.ToString("yyyy,MM,dd") + ") and {BranchTransferView.TransferDate}<= Date (" + ToDate.ToString("yyyy,MM,dd") + " )" });

        }
        #endregion

        #region Department
        public ActionResult PrintDepartmentReport()
        {
            return RedirectToAction("PrintReport", "Reports", new { sourceName = "DepartmentReport", fileName = "Department Report"});
        }
        #endregion

        #region Designation
        public ActionResult PrintDesignationReport()
        {
            return RedirectToAction("PrintReport", "Reports", new { sourceName = "DesignationReport", fileName = "Designation Report" });
        }
        #endregion

        #region Leave Report
        public ActionResult LeaveReport()
        {
            List<LeaveHistory> LeaveList = new List<LeaveHistory>();
            return View(LeaveList);
        }
        public ActionResult LeaveReportDateView()
        {
            DateTime FromDate = Convert.ToDateTime(Request["FromDate"]);
            DateTime ToDate = Convert.ToDateTime(Request["ToDate"]);
            ViewBag.FromDate = FromDate;
            ViewBag.ToDate = ToDate;
            List<LeaveHistory> LeaveList = _db.LeaveHistories.Where(t => t.FromDate >= FromDate && t.FromDate <= ToDate).ToList();
            return View("LeaveReport", LeaveList);

        }
        public ActionResult PrintLeaveReport()
        {
            DateTime FromDate = Convert.ToDateTime(Request["fromDate"]);
            DateTime ToDate = Convert.ToDateTime(Request["toDate"]);
            return RedirectToAction("PrintReport", "Reports", new { sourceName = "LeaveReport", fileName = "Leave Report", selectedFormula = "{tbl_LeaveHistory.FromDate}>=Date (" + FromDate.ToString("yyyy,MM,dd") + ") and {tbl_LeaveHistory.FromDate}<= Date (" + ToDate.ToString("yyyy,MM,dd") + " )" });
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