using System;
using System.IO;
using System.Text;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using FTL_HRMS.Models;
using System.Collections.Generic;
using System.Linq;
using FTL_HRMS.Utility;

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
            ViewBag.DepartmentGroupId = new SelectList(_db.DepartmentGroup, "Sl", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult EmployeeTypeReport(string employeeTypeId, string departmentGroupId,string ddl_dept, string ddl_designation)
        {
            int etid, dgid, did, dsid;
            Int32.TryParse(employeeTypeId, out etid);
            Int32.TryParse(departmentGroupId, out dgid);
            Int32.TryParse(ddl_dept, out did);
            Int32.TryParse(ddl_designation, out dsid);
            ViewBag.EmployeeTypeId = new SelectList(_db.EmployeeType, "Sl", "Name");
            ViewBag.DepartmentGroupId = new SelectList(_db.DepartmentGroup, "Sl", "Name");
            ViewBag.Status = "SelectType";
            TempData["etid"] = etid;
            TempData["dgid"] = dgid;
            TempData["did"] = did;
            TempData["dsid"] = dsid;
            List<Employee> employeeList = GetEmployeeList(etid, dgid, did, dsid);
            return View(employeeList);

        }

        public List<Employee> GetEmployeeList(int employeeTypeId, int departmentGroupId, int departmentId, int designationId)
        {
            List<Employee> employeeList = new List<Employee>();
            if (employeeTypeId>0)
            {
                if (departmentGroupId>0)
                {
                    if (departmentId>0)
                    {
                        if (designationId>0)
                        {
                            employeeList = _db.Employee.Where(x => x.EmployeeTypeId.Equals(employeeTypeId)).Where(x => x.Designation.Department.DepartmentGroup.Sl.Equals(departmentGroupId)).Where(x => x.Designation.Department.Sl.Equals(departmentId)).Where(x => x.Designation.Sl.Equals(designationId)).ToList();
                        }
                        else
                        {
                            employeeList = _db.Employee.Where(x => x.EmployeeTypeId.Equals(employeeTypeId)).Where(x => x.Designation.Department.DepartmentGroup.Sl.Equals(departmentGroupId)).Where(x => x.Designation.Department.Sl.Equals(departmentId)).ToList();
                        }
                    }
                    else
                    {
                        employeeList = _db.Employee.Where(x => x.EmployeeTypeId.Equals(employeeTypeId)).Where(x => x.Designation.Department.DepartmentGroup.Sl.Equals(departmentGroupId)).ToList();
                    }
                }
                else
                {
                    employeeList = _db.Employee.Where(x => x.EmployeeTypeId.Equals(employeeTypeId)).ToList();
                }
            }
            else
            {
                if (departmentGroupId > 0)
                {
                    if (departmentId > 0)
                    {
                        if (designationId > 0)
                        {
                            employeeList = _db.Employee.Where(x => x.Designation.Department.DepartmentGroup.Sl.Equals(departmentGroupId)).Where(x => x.Designation.Department.Sl.Equals(departmentId)).Where(x => x.Designation.Sl.Equals(designationId)).ToList();
                        }
                        else
                        {
                            employeeList = _db.Employee.Where(x => x.Designation.Department.DepartmentGroup.Sl.Equals(departmentGroupId)).Where(x => x.Designation.Department.Sl.Equals(departmentId)).ToList();
                        }
                    }
                    else
                    {
                        employeeList = _db.Employee.Where(x => x.Designation.Department.DepartmentGroup.Sl.Equals(departmentGroupId)).ToList();
                    }
                }
                else
                {
                    employeeList = _db.Employee.ToList();
                }
                employeeList = (List<Employee>)employeeList.Where(x => x.Status.Equals(true)).ToList();
            }
            return employeeList;
        }

        public string GetSelectedFormula(int employeeTypeId, int departmentGroupId, int departmentId, int designationId)
        {
            string selectedFormula;
            string statusFormula = "{tbl_Employee.Status} = true and ";
            string departmentGroupIdSelectedFormula = statusFormula+ " {tbl_DepartmentGroup.Sl} =" +departmentGroupId;
            string departmentIdSelectedFormula = statusFormula + " {tbl_Department.Sl} =" +departmentId;
            string employeeTypeIdSelectedFormula = statusFormula + " {tbl_Employee.EmployeeTypeId} =" +employeeTypeId;
            string designationIdSelectedFormula = statusFormula + " {tbl_Designation.Sl} =" + designationId;


            if (employeeTypeId > 0)
            {
                if (departmentGroupId > 0)
                {
                    if (departmentId > 0)
                    {
                        if (designationId > 0)
                        {
                            selectedFormula = employeeTypeIdSelectedFormula + " and " + designationIdSelectedFormula;
                        }
                        else
                        {
                            selectedFormula = employeeTypeIdSelectedFormula + " and " + departmentIdSelectedFormula;
                        }
                    }
                    else
                    {
                        selectedFormula = employeeTypeIdSelectedFormula+" and "+departmentGroupIdSelectedFormula;
                    }
                }
                else
                {
                    selectedFormula = employeeTypeIdSelectedFormula;
                }
            }
            else
            {
                if (departmentGroupId > 0)
                {
                    if (departmentId > 0)
                    {
                        if (designationId > 0)
                        {
                            selectedFormula = designationIdSelectedFormula;
                        }
                        else
                        {
                            selectedFormula = departmentIdSelectedFormula;
                        }
                    }
                    else
                    {
                        selectedFormula = departmentGroupIdSelectedFormula;
                    }
                }
                else
                {
                    selectedFormula = "";
                }
            }
            return selectedFormula;
        }
        public ActionResult PrintEmployeeList()
        {
            string selectedFormula = "";
            try
            {
                int etid = (int)TempData["etid"];
                int dgid = (int)TempData["dgid"];
                int did = (int)TempData["did"];
                int dsid = (int)TempData["dsid"];
                selectedFormula = GetSelectedFormula(etid, dgid, did, dsid);
            }
            catch (Exception)
            {
                return null;
            }
            
            
            return RedirectToAction("PrintReport", "Reports", new { sourceName = "EmployeeReport", fileName = "Employee Report", selectedFormula = selectedFormula });
        }
        #endregion

        #region Resign Report
        public ActionResult ResignReport()
        {
            List<Resignation> resignEmployeeList = new List<Resignation>();
            return View(resignEmployeeList);
        }
        public ActionResult ResignReportDateView()
        {
            DateTime fromDate = Convert.ToDateTime(Request["FromDate"]);
            DateTime toDate = Convert.ToDateTime(Request["ToDate"]);
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            List<Resignation> resignEmployeeList = _db.Resignation.Where(t => t.ResignDate >= fromDate && t.ResignDate <= toDate).ToList();
            return View("ResignReport", resignEmployeeList);
        }
        public ActionResult PrintResignReport()
        {
            DateTime fromDate = Convert.ToDateTime(Request["fromDate"]);
            DateTime toDate = Convert.ToDateTime(Request["toDate"]);
            return RedirectToAction("PrintReport", "Reports", new { sourceName = "ResignReport", fileName = "Resign Report", selectedFormula = "{tbl_Resignation.ResignDate}>=Date ("+fromDate.ToString("yyyy,MM,dd")+ ") and {tbl_Resignation.ResignDate}<= Date (" + toDate.ToString("yyyy,MM,dd") + " )" });
        }

        #endregion

        #region Department Transfer Report
        public ActionResult DepartmentTransferReport()
        {
            List<DepartmentTransfer> departmentTransferList = new List<DepartmentTransfer>();
            return View(departmentTransferList);
        }
        public ActionResult DepartmentTransferReportDateView()
        {
            DateTime fromDate = Convert.ToDateTime(Request["FromDate"]);
            DateTime toDate = Convert.ToDateTime(Request["ToDate"]);
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            List<DepartmentTransfer> departmentTransferList = _db.DepartmentTransfer.Where(t => t.TransferDate >= fromDate && t.TransferDate <= toDate).ToList();
            return View("DepartmentTransferReport", departmentTransferList);
        }

        public ActionResult PrintDepartmentTransferReport()
        {
            DateTime fromDate = Convert.ToDateTime(Request["fromDate"]);
            DateTime toDate = Convert.ToDateTime(Request["toDate"]);
            return RedirectToAction("PrintReport", "Reports", new { sourceName = "DepartmentTransferReport", fileName = "Department Transfer Report", selectedFormula = "{DepartmentTransferView.TransferDate}>=Date (" + fromDate.ToString("yyyy,MM,dd") + ") and {DepartmentTransferView.TransferDate}<= Date (" + toDate.ToString("yyyy,MM,dd") + " )" });
        }
        #endregion

        #region Branch Transfer Report
        public ActionResult BranchTransferReport()
        {
            List<BranchTransfer>  branchTransferList = new List<BranchTransfer>();
            return View(branchTransferList);
        }
        public ActionResult BranchTransferReportDateView()
        {
            DateTime fromDate = Convert.ToDateTime(Request["FromDate"]);
            DateTime toDate = Convert.ToDateTime(Request["ToDate"]);
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            List<BranchTransfer> branchTransferList = _db.BranchTransfer.Where(t => t.TransferDate >= fromDate && t.TransferDate <= toDate).ToList();
            return View("BranchTransferReport", branchTransferList);
        }
        public ActionResult PrintBranchTransferReport()
        {
            DateTime fromDate = Convert.ToDateTime(Request["fromDate"]);
            DateTime toDate = Convert.ToDateTime(Request["toDate"]);
            return RedirectToAction("PrintReport", "Reports", new { sourceName = "BranchTransferReport", fileName = "Branch Transfer Report", selectedFormula = "{BranchTransferView.TransferDate}>=Date (" + fromDate.ToString("yyyy,MM,dd") + ") and {BranchTransferView.TransferDate}<= Date (" + toDate.ToString("yyyy,MM,dd") + " )" });

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
            List<LeaveHistory> leaveList = new List<LeaveHistory>();
            return View(leaveList);
        }
        public ActionResult LeaveReportDateView()
        {
            DateTime fromDate = Convert.ToDateTime(Request["FromDate"]);
            DateTime toDate = Convert.ToDateTime(Request["ToDate"]);
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            List<LeaveHistory> leaveList = _db.LeaveHistories.Where(t => t.FromDate >= fromDate && t.FromDate <= toDate).ToList();
            return View("LeaveReport", leaveList);

        }
        public ActionResult PrintLeaveReport()
        {
            DateTime fromDate = Convert.ToDateTime(Request["fromDate"]);
            DateTime toDate = Convert.ToDateTime(Request["toDate"]);
            return RedirectToAction("PrintReport", "Reports", new { sourceName = "LeaveReport", fileName = "Leave Report", selectedFormula = "{tbl_LeaveHistory.FromDate}>=Date (" + fromDate.ToString("yyyy,MM,dd") + ") and {tbl_LeaveHistory.FromDate}<= Date (" + toDate.ToString("yyyy,MM,dd") + " )" });
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
            string connectionString = DbUtility.GetConnectionString();
            string databaseName = DbUtility.GetConectionStringProperty(connectionString,
                DbUtility.ConnectionStringProperty.DatabaseName);
            string dataSource = DbUtility.GetConectionStringProperty(connectionString,
                DbUtility.ConnectionStringProperty.DataSource);
            string user = DbUtility.GetConectionStringProperty(connectionString,
                DbUtility.ConnectionStringProperty.User);
            string password = DbUtility.GetConectionStringProperty(connectionString,
                DbUtility.ConnectionStringProperty.Password);
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), reportName + ".rpt"));
            rd.SetDatabaseLogon(user, password, dataSource, databaseName);
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