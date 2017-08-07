namespace FTL_HRMS.ViewModels
{
    public class VMMenuByRole
    {
        public int Id { get; set; }
        public string ParentMenuName { get; set; }

        public int? ParentId { get; set; }

        public string MenuItemName { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }
        public string FunctionNames { get; set; }
        public string ViewNames { get; set; }
        public int MenuOrder { get; set; }

        public string Role { get; set; }

        public bool IsSelected { get; set; }
        public string MnuIcnClass { get; set; }
    }
}