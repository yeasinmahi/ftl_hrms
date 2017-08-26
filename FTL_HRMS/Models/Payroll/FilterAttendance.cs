﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTL_HRMS.Models
{
    [Table("tbl_FilterAttendance")]
    public class FilterAttendance
    {       
        [Key]
        public int Sl { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime InTime { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime OutTime { get; set; }

        public bool IsCalculated { get; set; }

        public virtual Employee Employee { get; set; }
    }
}