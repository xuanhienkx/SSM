using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Reflection; 
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.Serialization;

using SSM.Models;
using SSM.Properties;

namespace SSM.Common
{
    public static class Helpers
    {
        public static Icon ExtractAssociatedIconEx()
        {
            Icon ico = Icon.ExtractAssociatedIcon(@"C:\WINDOWS\system32\notepad.exe");
            return ico;

        }

        public static List<string> PageList()
        {
            var pageSizes = new List<string> { "10", "50", "100", "150", "200", "250" };
            var str = ConfigurationManager.AppSettings["PageList"];
            if (string.IsNullOrEmpty(str) && str.Split(',').Any())
            {
                pageSizes = str.Split(',').ToList();
            }
            return pageSizes;
        }

        public static SelectList PageSelectList()
        {
            return new SelectList(PageList(), "Value");
        }

        public static int DaysView
        {
            get
            {
                var str = ConfigurationManager.AppSettings["DaysView"];
                int days = 10;
                int.TryParse(str, out days);
                return days;
            }
        }

        public static bool AllowTrading
        {
            get
            {
                var str = ConfigurationManager.AppSettings["AllowTrading"];
                bool isAlow = true;
                bool.TryParse(str, out isAlow);
                return isAlow;
            }
        }
        public static bool AllowUserSendMail
        {
            get
            {
                var str = ConfigurationManager.AppSettings["AllowUserSendMail"];
                bool isAlow = false;
                bool.TryParse(str, out isAlow);
                return isAlow;
            }
        }

        public static string SiteName
        {
            get
            {
                var str = SSM.Properties.Settings.Default.SiteName;
                if (string.IsNullOrEmpty(str))
                {
                    str = "SANCO HCM";
                }
                return str;
            }
        }
        public static bool IsValid(this string emailaddress, out string erroMessag)
        {
            erroMessag = string.Empty;
            try
            {
                var m = new MailAddress(emailaddress); 
                return true;
            }
            catch (FormatException ex)
            {
                erroMessag = ex.Message;
                Logger.LogError(ex);
                return false;
            }
        }
        public static bool IsValid(this string emailaddress)
        { 
            try
            {
                var m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException ex)
            { 
                Logger.LogError(ex);
                return false;
            }
        }
        public static string Logo
        {
            get
            {
                var str = SSM.Properties.Settings.Default.Logo;
                if (string.IsNullOrEmpty(str))
                {
                    str = "~/Content/ssmlogo.png";
                }
                return str;
            }
        }
        public static string Site
        {
            get
            {
                var str = SSM.Properties.Settings.Default.Site;
                if (string.IsNullOrEmpty(str))
                {
                    str = "http://localhost:9999";
                }
                return str;
            }
        }
        public static string SiteTitle
        {
            get
            {
                var str = SSM.Properties.Settings.Default.SiteTitle;
                if (string.IsNullOrEmpty(str))
                {
                    str = "SCF HCM";
                }
                return str;
            }
        }
        public static T DeserializeXMLFileToObject<T>(string XmlFilename)
        {
            T returnObject = default(T);
            if (string.IsNullOrEmpty(XmlFilename)) return default(T);

            try
            {
                StreamReader xmlStream = new StreamReader(XmlFilename);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                returnObject = (T)serializer.Deserialize(xmlStream);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
            }
            return returnObject;
        }
        public static string NonUnicode(string text)
        {
            string[] arr1 = new string[]
            {
                "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
                "đ",
                "é", "è", "ẻ", "ẽ", "ẹ", "ê", "ế", "ề", "ể", "ễ", "ệ",
                "í", "ì", "ỉ", "ĩ", "ị",
                "ó", "ò", "ỏ", "õ", "ọ", "ô", "ố", "ồ", "ổ", "ỗ", "ộ", "ơ", "ớ", "ờ", "ở", "ỡ", "ợ",
                "ú", "ù", "ủ", "ũ", "ụ", "ư", "ứ", "ừ", "ử", "ữ", "ự",
                "ý", "ỳ", "ỷ", "ỹ", "ỵ",
            };
            string[] arr2 = new string[]
            {
                "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                "d",
                "e", "e", "e", "e", "e", "e", "e", "e", "e", "e", "e",
                "i", "i", "i", "i", "i",
                "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o",
                "u", "u", "u", "u", "u", "u", "u", "u", "u", "u", "u",
                "y", "y", "y", "y", "y",
            };
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }
        public static string ConvertToUnsign3(string str)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = str.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty)
                        .Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        public static string ConvertToUnsign(this string str)
        {
            string strFormD = str.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < strFormD.Length; i++)
            {
                System.Globalization.UnicodeCategory uc =
                System.Globalization.CharUnicodeInfo.GetUnicodeCategory(strFormD[i]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(strFormD[i]);
                }
            }
            sb = sb.Replace('Đ', 'D');
            sb = sb.Replace('đ', 'd');
            return (sb.ToString().Normalize(NormalizationForm.FormD));
        }
        public static DataCountry LoadCitiesXmlData(string appDirectory)
        {
            DataCountry data = new DataCountry();
            XDocument xDoc = XDocument.Load(appDirectory);
            //    XDocument xDoc = XDocument.Parse(appDirectory);
            var regions = xDoc.Descendants("table").Where(n => n.Attribute("name").Value == "regions")
              .Select(b => new
              {
                  Field = b.Elements().Select(x => new
                  {
                      Attribute = CapitalizeFirstLetter(x.FirstAttribute.Value),
                      Value = x.Value.Trim()
                  })
              }).Distinct().ToList();
            var subregions = xDoc.Descendants("table").Where(n => n.Attribute("name").Value == "subregions")
              .Select(b => new
              {
                  Field = b.Elements().Select(x => new
                  {
                      Attribute = CapitalizeFirstLetter(x.FirstAttribute.Value),
                      Value = x.Value.Trim()
                  })
              }).Distinct().ToList();
            var countries = new List<CountryModel>();
            var cities = new List<ProvinceModel>();
            foreach (var region in regions)
            {
                var country = new CountryModel();
                foreach (var fl in region.Field.ToList())
                {
                    country[fl.Attribute] = fl.Value;
                }
                countries.Add(country);
            }
            foreach (var region in subregions)
            {
                var city = new ProvinceModel();
                foreach (var fl in region.Field.ToList())
                {
                    city[fl.Attribute] = fl.Value;
                }
                city.Country = countries.FirstOrDefault(c => c.Id == city.CountryId);
                cities.Add(city);
            }
            data.CountryModels = countries;
            data.ProvinceModels = cities;
            return data;
        }
        public static string GetMemberName<T, TValue>(Expression<Func<T, TValue>> memberAccess)
        {
            return ((MemberExpression)memberAccess.Body).Member.Name;
        }
        public static string CapitalizeFirstLetter(string s)
        {
            if (String.IsNullOrEmpty(s)) return s;
            if (s.Length == 1) return s.ToUpper();
            return s.Remove(1).ToUpper() + s.Substring(1);
        }
    }
    public static class MimeExtensionHelper
    {
        static object locker = new object();
        static object mimeMapping;
        static MethodInfo getMimeMappingMethodInfo;

        static MimeExtensionHelper()
        {
            Type mimeMappingType = Assembly.GetAssembly(typeof(HttpRuntime)).GetType("System.Web.MimeMapping");
            if (mimeMappingType == null)
                throw new SystemException("Couldnt find MimeMapping type");
            getMimeMappingMethodInfo = mimeMappingType.GetMethod("GetMimeMapping", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            if (getMimeMappingMethodInfo == null)
                throw new SystemException("Couldnt find GetMimeMapping method");
            if (getMimeMappingMethodInfo.ReturnType != typeof(string))
                throw new SystemException("GetMimeMapping method has invalid return type");
            if (getMimeMappingMethodInfo.GetParameters().Length != 1 && getMimeMappingMethodInfo.GetParameters()[0].ParameterType != typeof(string))
                throw new SystemException("GetMimeMapping method has invalid parameters");
        }
        public static string GetMimeType(string filename)
        {
            lock (locker)
                return (string)getMimeMappingMethodInfo.Invoke(mimeMapping, new object[] { filename });
        }
        public static string Encrypt(this string clearText)
        {
            const string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Decrypt(this string cipherText)
        {
            const string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}