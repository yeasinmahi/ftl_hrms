﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FTL_HRMS.Models
{
    [Table("tbl_LeaveHistory")]
    public class LeaveHistory
    {
        [Key]
        public int Sl { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Leave Type cannot be empty")]
        public int LeaveTypeId { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime FromDate { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime ToDate { get; set; }

        [Required(ErrorMessage = "Day cannot be empty")]
        public int Day { get; set; }

        [Required(ErrorMessage = "Cause cannot be empty")]
        [MaxLength(250)]
        public string Cause { get; set; }

        public int? UpdatedBy { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime? UpdateDate { get; set; }

        public string Status { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual LeaveType LeaveType { get; set; }
    }
}