using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SSM.Common;

using SSM.Models;

namespace SSM.Services
{
    public interface IAgentService : IServices<Agent>
    {
        Agent GetById(long id);
        AgentModel GetModelById(long id);
        IQueryable<Agent> GetAll(Agent agent);
        bool InsertAgent(AgentModel model, out string message);
        bool UpdateAgent(AgentModel model, out string message);
        bool DeleteAgent(long id, out string message);
        bool SetActive(int id, bool isActive);
    }
    public class AgentService : Services<Agent>, IAgentService
    {
        public Agent GetById(long id)
        {
            return GetQuery().FirstOrDefault(x => x.Id == id);
        }

        public AgentModel GetModelById(long id)
        {
            var db = GetById(id);
            if (db == null) return null;
            return Mapper.Map<AgentModel>(db);
        }

        public IQueryable<Agent> GetAll(Agent model)
        {
            var qr = GetQuery(s =>
                    (string.IsNullOrEmpty(model.AbbName) || (s.AbbName.Contains(model.AbbName)))
                 && (string.IsNullOrEmpty(model.AgentName) || (s.AgentName.Contains(model.AgentName)))
                 && (string.IsNullOrEmpty(model.Address) || (s.Address.Contains(model.Address)))
                 && (string.IsNullOrEmpty(model.CountryName) || s.CountryName.Contains(model.CountryName))
               );
            return qr;
        }

        public bool InsertAgent(AgentModel model, out string message)
        {
            try
            {
                var db = new Agent();
                AgentModel.ConvertModel(model, db); 
                Insert(db);
                Commited();
                message = Message.Successfully.ToString();
                return true;
            }
            catch (Exception ex)
            {
                message = Message.Failed.ToString() + " ==> " + ex.Message;
                Logger.Log(ex.Message);
                Logger.LogError(ex); return false;
            }
        }

        public bool UpdateAgent(AgentModel model, out string message)
        {
            try
            {
                var db = GetById(model.Id);
                AgentModel.ConvertModel(model, db);
                Commited();
                message = Message.Successfully.ToString();
                return true;
            }
            catch (Exception ex)
            {
                message = Message.Failed.ToString() + " ==> " + ex.Message;
                Logger.Log(ex.Message);
                Logger.LogError(ex); return false;
            }
        }

        public bool DeleteAgent(long id, out string message)
        {
            try
            {
                var db = GetById(id);
                Delete(db);
                Commited();
                message = Message.Successfully.ToString();
                return true;
            }
            catch (Exception ex)
            {
                message = Message.Failed.ToString() + " ==> " + ex.Message;
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
    }
}