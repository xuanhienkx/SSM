using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SSM.Common;

using SSM.Models;

namespace SSM.Services
{
    public interface ICarrierService : IServices<CarrierAirLine>
    {
        CarrierAirLine GetById(long id);
        CarrierModel GetModelById(long id);
        IQueryable<CarrierAirLine> GetAll(CarrierAirLine agent);
        IEnumerable<CarrierAirLine> GetAllByType(string type);
        bool InsertCarrier(CarrierModel model);
        bool UpdateCarrier(CarrierModel model);
        bool DeleteCarrier(long id);
        bool SetActive(int id, bool isActive);
    }
    public class CarrierService : Services<CarrierAirLine>, ICarrierService
    {
        public CarrierAirLine GetById(long id)
        {
            return GetQuery().FirstOrDefault(x => x.Id == id);
        }
        

        public CarrierModel GetModelById(long id)
        {
            var db = GetById(id);
            if (db == null) return null;
            return Mapper.Map<CarrierModel>(db);
        }

        public IQueryable<CarrierAirLine> GetAll(CarrierAirLine model)
        {
            var qr = GetQuery(s =>
                (string.IsNullOrEmpty(model.CarrierAirLineName) ||
                 (s.CarrierAirLineName.Contains(model.CarrierAirLineName)))
                && (string.IsNullOrEmpty(model.AbbName) || s.AbbName.Contains(model.AbbName))
                && (string.IsNullOrEmpty(model.Type) || s.Type.Contains(model.Type))
                );
            return qr;
        }

        public IEnumerable<CarrierAirLine> GetAllByType(string type)
        {
            var qr = GetQuery(x => x.IsActive && x.IsSee && x.IsHideUser == false &&
                x.Type.Equals(type)
                || x.Type.Equals(CarrierType.Fowarder.ToString())
                || x.Type.Equals(CarrierType.Other.ToString()));
            
            return qr.ToList();
        }

        public bool InsertCarrier(CarrierModel model)
        {
            try
            {
                var db = new CarrierAirLine();
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

        public bool UpdateCarrier(CarrierModel model)
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

        public bool DeleteCarrier(long id)
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

        public bool SetActive(int id, bool isActive)
        {
            var db = GetById(id);
            if (db == null) return false;
            db.IsActive = isActive;
            Commited();
            return true;
        }

        private void ConvertModel(CarrierModel model, CarrierAirLine db)
        {
            db.Type = model.Type;
            db.PhoneNumber = model.PhoneNumber;
            db.Description = model.Description;
            db.AbbName = model.AbbName;
            db.Address = model.Address;
            db.CarrierAirLineName = model.CarrierAirLineName;
            db.Fax = model.Fax;
        }
    }
}