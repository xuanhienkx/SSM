using System;
using System.Linq;
using AutoMapper;
using SSM.Common;

using SSM.Models;

namespace SSM.Services
{
    public interface IAreaService : IServices<Area>
    {
        Area GetById(long id);
        AreaModel GetModelById(long id);
        IQueryable<Area> GetAll(Area model);
        bool InsertArea(AreaModel model);
        bool UpdateArea(AreaModel model);
        bool DeleteArea(long id);
        bool UpdateTradingArea(long id, bool istrading);
    }
    public class AreaService : Services<Area>, IAreaService
    {
        public Area GetById(long id)
        {
            return GetQuery().FirstOrDefault(x => x.Id == id);
        }

        public AreaModel GetModelById(long id)
        {
            var db = GetById(id);
            if (db == null) return null;
            return Mapper.Map<AreaModel>(db);
        }

        public IQueryable<Area> GetAll(Area model)
        {
            var qr = GetQuery(s =>
                   (string.IsNullOrEmpty(model.AreaAddress) || (s.AreaAddress.Contains(model.AreaAddress)))
                && (model.CountryId == 0 || s.CountryId == model.CountryId)
                && (model.trading_yn == false || s.trading_yn == true)
              );
            return qr;
        }

        public bool InsertArea(AreaModel model)
        {
            try
            {
                var db = new Area();
                ConvertModel(model, db);
                Insert(db);
                Commited();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex); return false;
            }
        }

        public bool UpdateArea(AreaModel model)
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

        public bool DeleteArea(long id)
        {
            try
            {
                var db = new Area();
                Delete(db);
                Commited();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex); return false;
            }
        }
        public bool UpdateTradingArea(long id, bool istrading)
        {
            try
            {
                var area = GetById(id);
                if (area == null) return false;
                area.trading_yn = istrading;
                Commited();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex); return false;
            }
        }
        private void ConvertModel(AreaModel model, Area db)
        {
            db.Id = model.Id;
            db.AreaAddress = model.AreaAddress;
            db.CountryId = model.CountryId;
            db.Description = model.Description;
            db.trading_yn = model.IsTrading;
            var country = GetAllCountry().FirstOrDefault(x => x.Id == model.CountryId);
            if (country != null)
                db.CountryName = country.CountryName;
        }
    }
}