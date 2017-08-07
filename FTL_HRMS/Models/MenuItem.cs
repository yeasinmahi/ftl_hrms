using System.ComponentModel.DataAnnotations;

namespace FTL_HRMS.Models
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }


        public int? ParentItemId { get; set; }

        [Display(Name="Menu Name")]
        [Required(ErrorMessage = "Menu Item's name is required.")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Display(Name = "Controller")]
        [Required(ErrorMessage = "Controller name is required.")]
        public string ControllerName { get; set; }
        [Display(Name = "Action")]
        [Required(ErrorMessage = "Action name is required.")]
        public string ActionName { get; set; }
        [Display(Name = "Functions")]
        public string AllFunctions { get; set; }
        [Display(Name = "Views")]
        public string ViewNames { get; set; }
        public string Remarks { get; set; }
        [Display(Name = "Menu Order")]
        public int MenuOrder { get; set; }
        [Display(Name = "CSS Class")]
        public string IcnClass { get; set; }
    }
}