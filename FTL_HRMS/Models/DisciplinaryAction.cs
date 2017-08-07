using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTL_HRMS.Models
{
    [Table("tbl_DisciplinaryAction")]
    public class DisciplinaryAction
    {
        [Key]
        public int Sl { get; set; }

        [Required(ErrorMessage = "Employee cannot be empty")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Disciplinary Action Type cannot be empty")]
        public int DisciplinaryActionTypeId { get; set; }

        [DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd-MM-yy}",
        ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        
        public string Remarks { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual DisciplinaryActionType DisciplinaryActionType { get; set; }

    }
}