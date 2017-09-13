using System;
using System.ComponentModel.DataAnnotations;

namespace FTL_HRMS.Models.ViewModels
{
    public class VMTodaysAttendance
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:HH:mm:ss tt}",
        ApplyFormatInEditMode = true)]
        public DateTime CheckTime { get; set; }
    }
}