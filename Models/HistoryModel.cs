using System;

namespace SSM.Models
{
    public enum HistoryAction : byte
    {
        [StringLabel("REVISED")]
        Revised,
        [StringLabel("REQUEST")]
        Request

    }
    public class HistoryModel
    {
        public long Id { get; set; }

        public DateTime CreateTime { get; set; }

        public long UserId { get; set; }

        public string ActionName { get; set; }

        public string HistoryMessage { get; set; }

        public long? ObjectId { get; set; }

        public string ObjectType { get; set; }

        public bool IsLasted { get; set; }
        public bool IsRevisedRequest { get; set; }
        public User User { get; set; }

        public static HistoryModel ConvertToModel(History history)
        {
            if (history == null) return null;
            var model = new HistoryModel
            {
                Id = history.Id,
                CreateTime = history.CreateTime,
                ActionName = history.ActionName,
                HistoryMessage = history.HistoryMessage,
                ObjectId = history.ObjectId ?? 0,
                ObjectType = history.ObjectType,
                IsLasted = history.IsLasted,
                UserId = history.UserId,
                User = history.User,
                IsRevisedRequest = history.IsRevisedRequest
                 
            };
            return model;


        }
        public static History ConvertToDb(HistoryModel history)
        {
            var model = new History
            {
                Id = history.Id,
                CreateTime = history.CreateTime,
                ActionName = history.ActionName,
                HistoryMessage = history.HistoryMessage,
                ObjectId = history.ObjectId ?? 0,
                ObjectType = history.ObjectType,
                IsLasted = history.IsLasted,
                UserId = history.UserId,
                IsRevisedRequest = history.IsRevisedRequest
            };
            return model;
        }
        public static void CopyModelToDb(History model, HistoryModel history)
        {
            model.Id = history.Id;
            model.CreateTime = history.CreateTime;
            model.ActionName = history.ActionName;
            model.HistoryMessage = history.HistoryMessage;
            model.ObjectId = history.ObjectId ?? 0;
            model.ObjectType = history.ObjectType;
            model.IsLasted = history.IsLasted;
            model.UserId = history.UserId;
            model.IsRevisedRequest = history.IsRevisedRequest;
        } 
    }
}