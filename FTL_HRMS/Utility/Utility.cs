using System;
using System.Web;

namespace FTL_HRMS.Utility
{
    public class Utility
    {
        public static DateTime GetDefaultDate()
        {
            return new DateTime(1999, 1, 1);
        }

        public static byte[] GetBytesFromImagePath(string path)
        {
            if (HttpContext.Current != null)
                return System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(path));
            return null;
        }
    }
}