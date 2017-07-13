using System;
using System.Collections.Generic;
using System.Linq;
using CrystalDecisions.CrystalReports.ViewerObjectModel;
using SSM.Common;

using SSM.Models;

namespace SSM.Services
{
    public interface IGroupService : IServices<Group>
    {
        IQueryable<Group> GetQuerys(Group mode);
        bool CreateGroup(GroupModel model, out string message);
        bool UpdateGroup(GroupModel model, out string message);
        bool DeleteGroup(int id, out string message);
        GroupModel GetGroupModel(int id);
        Group Get(int id);
        bool SetActive(int id, bool isActive);
        bool CheckGroupFree(int id );
        IList<Group> GetGroupByUsers(long userId);
    }
    public class GroupService : Services<Group>, IGroupService
    {
        public IQueryable<Group> GetQuerys(Group mode)
        {
            return GetQuery(x =>
                string.IsNullOrEmpty(mode.Name) || x.Name.Contains(mode.Name) 
               // && x.IsActive==mode.IsActive
                );
        }
        public bool CreateGroup(GroupModel model, out string message)
        { 
            try
            {
                message = string.Empty;
                var db = ToGroup(model);
                Insert(db);
                AssignUserToGroup(db.Id, model.ListUserAccesses);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                message = ex.Message;
                return false;
            }
        }

        public bool UpdateGroup(GroupModel model, out string message)
        {
            try
            {
                message = string.Empty;
                var db = Get(model.Id);
                CopyModelToGroup(model, db); 
                Commited();
                AssignUserToGroup(db.Id, model.ListUserAccesses);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                message = ex.Message;
                return false;
            }
        }

        public bool DeleteGroup(int id, out string message)
        {

            try
            {
                message = string.Empty;
                var db = Get(id);
                Delete(db);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                message = ex.Message;
                return false;
            }
        }

        public GroupModel GetGroupModel(int id)
        {
            return ToGroupModel(Get(id));
        }

        public Group Get(int id)
        {
            return FindEntity(x => x.Id == id);
        }

        public bool SetActive(int id, bool isActive)
        {
            var db = Get(id);
            db.IsActive = isActive;
            Commited();
            return true;
        }

        public bool CheckGroupFree(int id)
        {
            var model = GetGroupModel(id);
            if(model.UserGroups.Any())
                return true;
            return false;
        }

        public IList<Group> GetGroupByUsers(long userId)
        {
            var db = GetAll(x => x.UserGroups.Any(u => u.UserId == userId));
            return db;
        }

        private void CopyModelToGroup(GroupModel model, Group db)
        {
            db.Id = model.Id;
            db.IsActive = model.IsActive;
            db.Description = model.Discription;
            db.Name = model.Name;
        }

        private GroupModel ToGroupModel(Group db)
        {
            
            var gModel = new GroupModel()
            {
                Id = db.Id,
                Name = db.Name,
                Discription = db.Description,
                IsActive = db.IsActive,
                UserGroups = db.UserGroups.ToList()
            };
            gModel.ListUserAccesses = gModel.UserGroups.Any() ? gModel.UserGroups.Select(x => x.UserId).ToArray() : new long[10];
            return gModel;
        }

        private Group ToGroup(GroupModel model)
        {
            var db = new Group()
            {
                IsActive = model.IsActive,
                Name = model.Name,
                Description = model.Discription
            };
            return db;
        }

        private void AssignUserToGroup(int id, long[] userIds)
        {
            var userGroup = Context.UserGroups.Where(x => x.GroupId == id);
            Context.UserGroups.DeleteAllOnSubmit(userGroup);
            if (userIds== null)return;
            List<UserGroup> groups = userIds.Select(userId => new UserGroup()
            {
                UserId = userId, GroupId = id,
            }).ToList();
            Context.UserGroups.InsertAllOnSubmit(groups);
            Commited();
        }
    }
}