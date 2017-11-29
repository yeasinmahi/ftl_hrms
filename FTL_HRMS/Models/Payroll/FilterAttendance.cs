using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FTL_HRMS.Models.Hr;
using System.ComponentModel;

namespace FTL_HRMS.Models.Payroll
{
    [Table("tbl_FilterAttendance")]
    public class FilterAttendance
    {       
        [Key]
        public int Sl { get; set; }
        
        public int EmployeeId { get; set; }

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

        [DefaultValue(false)]
        public bool IsCalculated { get; set; }
    }
}