﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FTL_HRMS.Models
{
    [Table("tbl_Experience")]
    public class Experience
    {
        [Key]
        public int Sl { get; set; }

        [Required(ErrorMessage = "Institute Name cannot be empty")]
        [MaxLength(250)]
        public string InstituteName { get; set; }

        [Required(ErrorMessage = "Institute Address cannot be empty")]
        [MaxLength(250)]
        public string InstituteAddress { get; set; }

        [MaxLength(250)]
        public string Website { get; set; }

        [MaxLength(450)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Designation cannot be empty")]
        [MaxLength(250)]
        public string Designation { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime FromDate { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime ToDate { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
    }
}