using System;

namespace FTL_HRMS.Models.ViewModels
{
    public class VMNotification
    {
        public int Sl { get; set; }
        public string EmployeeCode { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
    }
}