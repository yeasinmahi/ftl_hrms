using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using FTL_HRMS.Models;

namespace FTL_HRMS.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "test.rpt"));
            rd.SetDatabaseLogon("sa", "sa2009", ".\\SQLEXPRESS", "FTL_HRMS");

            IEnumerable<Employee> employees;
            using (HRMSDbContext dc = new HRMSDbContext())
            {
                employees = dc.Employee.Select(x => new Employee()
                {
                    Code = x.Code,
                    Name = x.Name,
                    FathersName = x.FathersName
                }).AsEnumerable();
                rd.SetDataSource(employees);
            }

            
            

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "EverestList.pdf");
            }
            catch (Exception ex)
            {
                throw;
            }
            //ReportDocument Report = new ReportDocument();
            //Report.Load(Server.MapPath("~/Reports/test.rpt"));
            //Report.SetDatabaseLogon("sa", "sa2009", ".\\SQLEXPRESS", "FTL_HRMS");



            //ExportOptions CrExportOptions;
            //DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            //PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            //CrDiskFileDestinationOptions.DiskFileName = "f:\\test.pdf";
            //CrExportOptions = Report.ExportOptions;
            //{
            //    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            //    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            //    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            //    CrExportOptions.FormatOptions = CrFormatTypeOptions;
            //}
            //Report.Export();
            return View();
        }

        private ActionResult GenerateJobCardReport()
        {
            List<Employee> allEverest;
            using (HRMSDbContext dc = new HRMSDbContext())
            {
                allEverest = dc.Employee.ToList();
            }

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "test.rpt"));
            rd.SetDatabaseLogon("sa", "sa2009", ".\\SQLEXPRESS", "FTL_HRMS");
            rd.SetDataSource(allEverest);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "EverestList.pdf");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}