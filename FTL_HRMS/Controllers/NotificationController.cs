using FTL_HRMS.DAL;
using FTL_HRMS.Models.Hr;
using FTL_HRMS.Models.Payroll;
using FTL_HRMS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using static FTL_HRMS.Utility.Utility;

namespace FTL_HRMS.Controllers
{
    class NotificationController : Controller
    {
        public HRMSDbContext Db;
        private static NotificationController _notificationController = null;
        // GET: Notification
        
        public static NotificationController GetInstant()
        {
            if (_notificationController==null)
            {
                _notificationController = new NotificationController();
            }
            return _notificationController;
        }
        public ActionResult Index()
        {
            return View();
        }
        public List<VMNotification> GetNotificationListByRoll(string rolll,string userName)
        {
            Db = new HRMSDbContext();
            List<VMNotification> notificationList = new List<VMNotification>();
            List<LeaveHistory> leaveHistories = new List<LeaveHistory>();
            List<Resignation> resignations = new List<Resignation>();
            List<Loan> loans = new List<Loan>();
            if (rolll == "Super Admin" || rolll == "Admin")
            {
                leaveHistories = Db.LeaveHistories.Where(x => x.Status == "Pending" || x.Status == "Recommended").ToList();
                resignations = Db.Resignation.Where(x => x.Status == "Pending").ToList();
                loans = Db.Loan.Where(x => x.Status == "Pending").ToList();
            }
            else if (rolll == "Department Head")
            {
                var customUserId = Db.Users.Where(i => i.UserName == userName).Select(s => s.CustomUserId).FirstOrDefault();
                int employeeId = Db.Employee.Where(x => x.Sl == customUserId).Select(x => x.Sl).FirstOrDefault();
                int departmentId = Db.Employee.Where(x => x.Sl == employeeId).Select(x => x.Designation.DepartmentId).FirstOrDefault();
                leaveHistories = Db.LeaveHistories.Where(x => x.Status == "Pending").Where(x => x.Employee.Designation.DepartmentId.Equals(departmentId)).ToList();
                resignations = Db.Resignation.Where(x => x.Status == "Pending").Where(x => x.Employee.Designation.DepartmentId.Equals(departmentId)).ToList();
                loans = Db.Loan.Where(x => x.Status == "Considered" || x.Status== "Canceled" || x.Status=="Approved").Where(x => x.EmployeeId == employeeId).ToList();
            }
            else if (rolll == "Employee")
            {
                int employeeId = Db.Users.Where(i => i.UserName == userName).Select(s => s.CustomUserId).FirstOrDefault();
                
                leaveHistories = Db.LeaveHistories.Where(x => x.Status == "Approved" || x.Status == "Cancled").Where(x => x.EmployeeId == employeeId).ToList();
                resignations = Db.Resignation.Where(x => x.Status == "Approved" || x.Status == "Cancled").Where(x => x.EmployeeId == employeeId).ToList();
                loans = Db.Loan.Where(x => x.Status == "Considered" || x.Status == "Canceled" || x.Status == "Approved").Where(x => x.EmployeeId == employeeId).ToList();
            }
            else
            {

            }
            if (leaveHistories != null)
            {
                foreach (var o in leaveHistories)
                {
                    var ord = new VMNotification
                    {
                        Sl = o.Sl,
                        Date = o.FromDate,
                        EmployeeCode = o.Employee.Code,
                        Type = "Leave",
                        Status = o.Status,
                    };

                    notificationList.Add(ord);
                }
            }
            if (resignations != null)
            {
                foreach (var o in resignations)
                {
                    var ord = new VMNotification
                    {
                        Sl = o.Sl,
                        Date = o.ResignDate,
                        EmployeeCode = o.Employee.Code,
                        Type = "Resign",
                        Status = o.Status,
                    };

                    notificationList.Add(ord);
                }
            }
            if (loans != null)
            {
                foreach (var o in loans)
                {
                    var ord = new VMNotification
                    {
                        Sl = o.Sl,
                        Date = o.CreateDate,
                        EmployeeCode = o.Employee.Code,
                        Type = "Loan",
                        Status = o.Status,
                    };

                    notificationList.Add(ord);
                }
            }
            return notificationList.OrderByDescending(d => d.Date).ToList();

        }

        public async Task<bool> SendMail(string email, string MailSubject, string EmailBody)
        {
            try
            {
                //Configuring webMail class to send emails  
                //gmail smtp server  
                WebMail.SmtpServer = "smtp.gmail.com";
                //gmail port to send emails  
                WebMail.SmtpPort = 587;
                WebMail.SmtpUseDefaultCredentials = true;
                //sending emails with secure protocol  
                WebMail.EnableSsl = true;
                //EmailId used to send emails from application  
                WebMail.UserName = "futuristictechdev@gmail.com";
                WebMail.Password = "wasimakram123";

                //Sender email address.  
                WebMail.From = "futuristictechdev@gmail.com";

                //Send email  
                WebMail.Send(to: email, subject: MailSubject, body: EmailBody, isBodyHtml: true);
                return true;
            }
            catch (Exception ex)
            {   
                return false;
            }
        }

        public bool IsEmailActive ()
        {

            return true;
        }

        public async Task<bool> SentMailToAll(NotificationType notificationType, NotificationStatus notificationStatus, int employeeId)
        {
            if (IsEmailActive())
            {
                List<string> emails = GetEmails(notificationType, notificationStatus, employeeId);
                string mailSubject = GetMailSubject(notificationType, notificationStatus);
                string mailBody = GetMailBody(notificationType, notificationStatus, employeeId);
                bool isSentMail = true;
                foreach (string email in emails)
                {
                    if (!await SendMail(email, mailSubject, mailBody))
                    {
                        isSentMail = false;
                    }
                }
                if (isSentMail)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public List<string> GetEmails(NotificationType notificationType,NotificationStatus notificationStatus, int employeeId)
        {
            Db = new HRMSDbContext();
            List<string> emails = new List<string>();
            if(notificationStatus.Equals(NotificationStatus.Pending) || notificationStatus.Equals(NotificationStatus.Recommendation))
            {
                //admin and super admin
                emails = Db.Employee.Where(x => x.Designation.RoleName.Equals("Super Admin") || x.Designation.RoleName.Equals("Admin")).Select(x => x.Email).ToList();
            }else if (notificationStatus.Equals(NotificationStatus.Approve) || notificationStatus.Equals(NotificationStatus.Cancel) || notificationStatus.Equals(NotificationStatus.Cancel))
            {
                //employee
                emails = Db.Employee.Where(x => x.Sl.Equals(employeeId)).Select(x => x.Email).ToList();
            }
            if (notificationStatus.Equals(NotificationStatus.Pending) && (notificationType.Equals(NotificationType.Leave) || notificationType.Equals(NotificationType.Resign)))
            {
                //Department Head
                int departmentId = Db.Employee.Where(x => x.Sl == employeeId).Select(x => x.Designation.DepartmentId).FirstOrDefault();
                emails.AddRange(Db.Employee.Where(x => x.Designation.DepartmentId.Equals(departmentId)).Where(x => x.Designation.RoleName.Equals("Department Head")).Select(x => x.Email).ToList());
                
            }

            return emails;
        }

        public string GetMailSubject(NotificationType notificationType, NotificationStatus notificationStatus)
        {
            return notificationType.ToString() + " " + notificationStatus.ToString();
        }
        public string GetMailBody(NotificationType notificationType, NotificationStatus notificationStatus, int employeeId)
        {
            string name = "X";
            name = Db.Employee.Where(x => x.Sl.Equals(employeeId)).Select(x => x.Name).FirstOrDefault();


            return "Have To Write A Body";
        }
    }
}