using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FTL_HRMS.Models.ViewModels
{
    public class VMNotification
    {
        public int Sl { get; set; }
        public string EmployeeCode { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
    }
}