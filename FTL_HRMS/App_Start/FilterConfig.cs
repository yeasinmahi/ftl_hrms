using FTL_HRMS.App_Start;
using System.Web;
using System.Web.Mvc;

namespace FTL_HRMS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new CustomFilter());
            //filters.Add(new AuthorizeAttribute());
        }
    }
}
