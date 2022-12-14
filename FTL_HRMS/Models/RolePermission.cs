using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTL_HRMS.Models
{
    public class RolePermission
    {
        [Key]
        public int Id { get; set; }
        public string RoleId { get; set; }

        public string MenuItemIdList { get; set; }
        [ForeignKey("RoleId")]
        public virtual IdentityRole Role { get; set; }

        public bool CanView { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }
}