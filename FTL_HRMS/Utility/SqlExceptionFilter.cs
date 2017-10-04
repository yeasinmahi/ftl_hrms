using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FTL_HRMS.Models;

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