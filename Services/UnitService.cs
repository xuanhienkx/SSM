using System;
using System.Linq;
using AutoMapper;
using SSM.Common;

using SSM.Models;

namespace SSM.Services
{
    public interface IUnitService : IServices<Unit>
    {
        Unit GetById(long id);
        UnitModel GetModelById(long id);
        IQueryable<Unit> GetAll(Unit model);
        bool InsertUnit(UnitModel model);
        bool UpdateUnit(UnitModel model);
        bool DeleteUnit(long id);
    }
    public class UnitService : Services<Unit> , IUnitService{
        public Unit GetById(long id)
        {
            return GetQuery().FirstOrDefault(x => x.Id == id);
        }

        public UnitModel GetModelById(long id)
        {
            var db = GetById(id);
            if (db == null) return null;
            return Mapper.Map<UnitModel>(db);
        }

        public IQueryable<Unit> GetAll(Unit model)
        {
            throw new System.NotImplementedException();
        }

        public bool InsertUnit(UnitModel model)
        {
            try
            {
                 var db =new Unit();
                UnitModel.RevertUnit(model, db);
                Insert(db);
                Commited();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex); return false;
            }
        }

        public bool UpdateUnit(UnitModel model)
        {
            try
            {
                var db = GetById(model.Id);
                UnitModel.RevertUnit(model, db); 
                Commited();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex); return false;
            }
        }

        public bool DeleteUnit(long id)
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
    }
}