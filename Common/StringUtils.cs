using System;
using System.Collections.Generic;
using System.Globalization;

namespace SSM.Common
{
    public static class StringUtils
    {
        public static string FormatDate(DateTime? dateTime)
        {
            if (dateTime == null)
                return "";

            return dateTime.Value.ToString("MMM d, yyyy");
        }

        public static string HtmlFilter(String value)
        {
            if (value == null)
            {
                return "";
            }
            return value.Replace("\r\n", "<br/>").Replace("\r", "<br/>");
        }
        public static bool isNullOrEmpty(String value) {
            if (value == null)
            {
                return true;
            }
            if ("".Equals(value.Trim()))
            {
                return true;
            }
            return false;
        }
        public static string FormatPhone(String phone)
        {
            if (phone == null)
                return "";

            string result;
            if (phone.Length == 10)
            {
                result = phone.Substring(0, 10);
                result = "(" + result.Substring(0, 3) + ") " + result.Substring(3, 3) + "-" + result.Substring(6, 4);
            }
            else
            {
                result = phone;
            }

            return result;

        }
        public static List<string> GetCountryList()
        {

            List<string> cultureList = new List<string>();
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.InstalledWin32Cultures);
            RegionInfo country = new RegionInfo(new CultureInfo("en-US", false).LCID);
            foreach (CultureInfo cul in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                country = new RegionInfo(new CultureInfo(cul.Name, false).LCID);
                if (!cultureList.Contains(country.DisplayName.ToString()))
                {
                   cultureList.Add(country.EnglishName);
                }
            }

            cultureList.Sort();
            return cultureList;
        }
    }
}