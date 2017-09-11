using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FTL_HRMS.Models.Hr;

namespace FTL_HRMS.Models.Payroll
{
    [Table("tbl_DeviceAttendance")]
    public class DeviceAttendance
    {       
        [Key]
        public int Sl { get; set; }
        [MaxLength(15)]
        public string EmployeeCode { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime CheckTime { get; set; }

        public bool IsCalculated { get; set; }
        
    }
}