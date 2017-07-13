using System;
using System.Linq;
using SSM.Models;

namespace SSM.Services
{
    public interface IHistoryService : IServices<History>
    {
        History GetHistory(long id);
        void Save(HistoryModel model);
        void UpdateHistory(HistoryModel model);
        bool SetUnLasted(long objId, string objType);
        HistoryModel GetModel(long id);
        HistoryModel GeModelLasted(long objId, string objType);

    }
    public class HistoryService : Services<History>, IHistoryService
    {
        public History GetHistory(long id)
        {
            var db = FindEntity(x => x.Id == id);
            return db;
        }

        public void Save(HistoryModel model)
        {
            var history = HistoryModel.ConvertToDb(model); 
            Insert(history);

        }

        public void UpdateHistory(HistoryModel model)
        {
            var db = GetHistory(model.Id);
            HistoryModel.CopyModelToDb(db, model);
            Commited();
        }

        public bool SetUnLasted(long objId, string objType)
        {
            var sql = string.Format("update History set IsLasted=0, IsRevisedRequest=0 where ObjectId={0} and ObjectType='{1}'", objId, objType);
            return ExecuteCommand(sql);
            /*var dbs = GetAll(x => x.IsLasted == true && x.ObjectId == objId && x.ObjectType == objType);
            foreach (var history in dbs)
            {
                history.IsLasted = false;
            }
            Commited();
            return true;*/
        }

        public HistoryModel GetModel(long id)
        {
            var db = GetHistory(id);
            return HistoryModel.ConvertToModel(db);
        }

        public HistoryModel GeModelLasted(long objId, string objType)
        {
            var db = FindEntity(x => x.IsLasted == true && x.ObjectId == objId && x.ObjectType == objType);
            return HistoryModel.ConvertToModel(db);
        }
    }
}