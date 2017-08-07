﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FTL_HRMS.Models
{
    [Table("tbl_BranchTransfer")]
    public class BranchTransfer
    {
        [Key]
        public int Sl { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "From Branch cannot be empty")]
        public int FromBranchId { get; set; }

        [Required(ErrorMessage = "To Branch cannot be empty")]
        public int ToBranchId { get; set; }

        [DataType(DataType.Date),
         DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
         ApplyFormatInEditMode = true)]
        public DateTime TransferDate { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Branch Branch { get; set; }
    }
}