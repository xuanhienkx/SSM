using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Xml.Serialization;

namespace SSM.Models
{
    public class CountryModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "{0} không được để trống!")]
        public string CountryName { get; set; }
        public string Iso { get; set; }
        public string Iso3 { get; set; }
        public string Fips { get; set; }
        public string Continent { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public string PhonePrefix { get; set; }
        public string PostalCode { get; set; }
        public string Languages { get; set; }
        public long GeonameId { get; set; }
        public object this[string propertyName]
        {
            get
            {
                // probably faster without reflection:
                // like:  return Properties.Settings.Default.PropertyValues[propertyName] 
                // instead of the following
                Type myType = typeof(CountryModel);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                return myPropInfo.GetValue(this, null);
            }
            set
            {
                Type myType = typeof(CountryModel);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                if (propertyName.ToLower().Contains("id"))
                {
                    myPropInfo.SetValue(this, Convert.ToInt64(value), null);
                }
                else
                {
                    myPropInfo.SetValue(this, value, null);
                }


            }

        }
    } 
    public class ProvinceModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string TimeZone { get; set; }
        public long CountryId { get; set; }
        public CountryModel Country { get; set; }
        public object this[string propertyName]
        {
            get
            {
                // probably faster without reflection:
                // like:  return Properties.Settings.Default.PropertyValues[propertyName] 
                // instead of the following
                Type myType = typeof(ProvinceModel);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                return myPropInfo.GetValue(this, null);
            }
            set
            {
                Type myType = typeof(ProvinceModel);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                if (propertyName.ToLower().Contains("id"))
                {
                    myPropInfo.SetValue(this, Convert.ToInt64(value), null);
                }
                else
                {
                    myPropInfo.SetValue(this, value, null);
                }


            }

        }

    }

    public class DataCountry
    {
        public List<CountryModel> CountryModels { get; set; }
        public List<ProvinceModel> ProvinceModels { get; set; }
    }
}