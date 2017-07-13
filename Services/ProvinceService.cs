using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SSM.Common;

using SSM.Models;

namespace SSM.Services
{
    public interface IProvinceService : IServices<Province>
    {
        Province GetById(long id);
        ProvinceModel GetModelById(long id);
        List<Province> GetAllByCountryId(long id);
        bool InsertProvince(ProvinceModel model);
        bool UpdateProvince(ProvinceModel model);
        bool DeleteProvince(long id);
        ProvinceModel ToModels(Province province);

    }
    public class ProvinceService : Services<Province>, IProvinceService
    {
        public Province GetById(long id)
        {
            return GetQuery().FirstOrDefault(x => x.Id == id);
        }

        public ProvinceModel GetModelById(long id)
        {
            var db = GetById(id);
            if (db == null) return null;
            return ToModels(db);
        }

        public List<Province> GetAllByCountryId(long id)
        {
            var query = GetQuery(x => x.CountryId == id).OrderBy(x => x.Name).ToList();
            return query;
        } 

        public bool InsertProvince(ProvinceModel model)
        {
            try
            {
                var db = ToDbModel(model);
                Insert(db);
                Commited();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex); return false;
            }
           
        }

        public bool UpdateProvince(ProvinceModel model)
        {
            try
            {
                var db = GetById(model.Id);
                ConvertModel(model, db);
                Commited();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex); return false;
            }
        }

        public bool DeleteProvince(long id)
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
                Logger.LogError(ex);
                return false;
            }
        }
        public Province ToDbModel(ProvinceModel model)
        {
            var db = new Province
            { 
                Name = model.Name,
                CountryId = model.CountryId,
                TimeZone = model.TimeZone
            };
            return db;
        }
        private void ConvertModel(ProvinceModel model, Province db)
        { 
            db.Name = model.Name;
            db.CountryId = model.CountryId;
            db.TimeZone = model.TimeZone;
        }
        public ProvinceModel ToModels(Province province)
        {
            if (province == null) return null;
            return Mapper.Map<ProvinceModel>(province);
        }
    }
}