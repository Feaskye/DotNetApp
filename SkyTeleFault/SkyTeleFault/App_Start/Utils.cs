using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyTeleFault.App_Start
{
    public class Utils
    {
        public static string ToDateString(object o)
        {
            return (o == null||o==DBNull.Value) ? "" : Convert.ToDateTime(o).ToString("yyyy-MM-dd");
        }

    }
}