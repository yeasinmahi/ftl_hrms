﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FTL_HRMS.Models.Hr;

namespace FTL_HRMS.Models
{
    [Table("tbl_DeviceAttendance")]
    public class DeviceAttendance
    {       
        [Key]
        public int Sl { get; set; }
        
        public int EmployeeCode { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime Datetime { get; set; }

        public bool IsCalculated { get; set; }

        public virtual Employee Employee { get; set; }
    }
}