using System.Web.Mvc;
using FTL_HRMS.DAL;

namespace FTL_HRMS.Utility
{
    public class SqlExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            CheckDatabaseConnection();
            GetExceptionMessage(filterContext);

        }

        public void GetExceptionMessage(ExceptionContext filterContext)
        {
            
        }

        private void CheckDatabaseConnection()
        {
            using (HRMSDbContext dbContext = new HRMSDbContext())
            {
                if (!dbContext.Database.Exists())
                {
                    return;
                }
            }
            return;
        }
    }
}