using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SSM.Common;

using SSM.Models;

namespace SSM.Services
{
    public interface ICountryService : IServices<Country>
    {
        Country GetById(long id);
        CountryModel GetModelById(long id);
        CountryModel GetModelByName(string name);
        IQueryable<Country> GetAll(Country model);
        bool InsertCountry(CountryModel model);
        bool InsertCountry(List<CountryModel> models);
        bool UpdateCountry(CountryModel model);
        bool DeleteCountry(long id);
        CountryModel ToModels(Country country);
        long GetIdByName(string name);
    }
    public class CountryService : Services<Country>, ICountryService
    {
        public Country GetById(long id)
        {
            return FindEntity(x => x.Id == id);
        }

        public CountryModel GetModelById(long id)
        {
            var db = GetById(id);
            if (db == null) return null;
            return ToModels(db);
        }

        public CountryModel GetModelByName(string name)
        {
            var db = FindEntity(x => x.CountryName.ToLower() == name.ToLower());
            if (db == null) return null;
            return ToModels(db);
        }

        public IQueryable<Country> GetAll(Country model)
        {
            var qr = GetQuery(s =>  string.IsNullOrEmpty(model.CountryName) || s.CountryName.Contains(model.CountryName.Trim()));
            return qr;
        }

        public bool InsertCountry(CountryModel model)
        {
            try
            {
                var db = ToDbModel(model);
                db.Id = 0;
                Insert(db);
                Commited();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex); return false;
            }
        }

        public bool InsertCountry(List<CountryModel> models)
        {
            foreach (var countryModel in models)
            {
                var db = ToDbModel(countryModel);
                db.Id = 0;
                Insert(db);
            }
            Commited();
            return true;
        }

        public bool UpdateCountry(CountryModel model)
        {
            try
            {
                SSM.Models.Country db = GetById(model.Id);
                ConvertModel(model, db);
                Commited();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex); return false;
            }
        }

        public bool DeleteCountry(long id)
        {
            try
            {
                var db = GetById(id);
                Delete(db);
                Commited();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex); return false;
            }
        }
        public Country ToDbModel(CountryModel model)
        {
            var db = new Country
            {
                Id = model.Id,
                CountryName = model.CountryName
            };
            return db;
        }
        private void ConvertModel(CountryModel model, Country db)
        { 
            db.CountryName = model.CountryName;
        }
        public CountryModel ToModels(Country country)
        {
            if (country == null) return null;
            return Mapper.Map<CountryModel>(country);
        }

        public long GetIdByName(string name)
        {
            var db = FindEntity(x => x.CountryName.Trim().ToLower().Equals(name.Trim().ToLower()));
            return db==null ? 0 : db.Id;
        }
    }
   
}