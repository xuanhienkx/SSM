using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using SSM.Common;


namespace SSM.Utils
{
    public class DateUtils
    {
        public static DateTime Convert2DateTime(String dateValue)
        {
            try
            {
                IFormatProvider culture = new CultureInfo("en-US", true);
                DateTime DateTime1 = DateTime.ParseExact(dateValue, "dd/MM/yyyy", culture);

                return DateTime1;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return DateTime.Now;
        }

        public static String ConvertDay(String day) {
            if (day == null || day == "") {
                return "01";
            }
            if (day.Length <= 1) {
                day = "0" + day;
            }
            return day;
        }
    }
    
}