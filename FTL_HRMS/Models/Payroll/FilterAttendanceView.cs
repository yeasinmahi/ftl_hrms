using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FTL_HRMS.Models.Hr;
using System.ComponentModel;

namespace FTL_HRMS.Models.Payroll
{
    [Table("dbo.FilterAttendanceView")]
    public class FilterAttendanceView
    {    
        [Key]   
        public int EmployeeId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:HH:mm:ss tt}",
        ApplyFormatInEditMode = true)]
        public DateTime InTime { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:HH:mm:ss tt}",
        ApplyFormatInEditMode = true)]
        public DateTime OutTime { get; set; }

        public string Status { get; set; }
    }
}