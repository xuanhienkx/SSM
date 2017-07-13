using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using SSM.Common;
using SSM.Models;

namespace SSM.Services
{
    public interface INewsServices : IServices<ScfNew>
    {
        IEnumerable<NewsModel> GetByHeader(string term);
        ScfNew GetNew(int id);
        NewsModel GetNewsModel(int id);

        int Created(NewsModel model);
        void SaveUpdate(NewsModel model);
        bool DeleteNew(int id);
        IList<Catelory> ListCatelories(NewType newType);
        void Approval(int id);
        string SetIsViewed(User user, int id);
        bool CheckAboutNewByUser(User user);
        IQueryable<ScfNew> GetScfNewsByUser(User user);

    }
    public class NewsServices : Services<ScfNew>, INewsServices
    {
        public IEnumerable<NewsModel> GetByHeader(string term)
        {
            var data = GetAll(x => string.IsNullOrEmpty(term.Trim()) || x.Header.Contains(term));
            return data.Select(x => ToModel(x));
        }

        public ScfNew GetNew(int id)
        {
            return FindEntity(x => x.Id == id);
        }

        public NewsModel GetNewsModel(int id)
        {
            var dbNew = GetNew(id);
            return ToModel(dbNew);
        }

        public int Created(NewsModel model)
        {
            var db = ToDbModel(model);
            //var canupdate = db.UsersCanUpdate + ";";
            //db.UsersCanUpdate = canupdate.Replace(";;", ";");
            db.DatePromulgate = DateTime.Now;
            // db.CateloryId = model.Catelory.Id;
            Context.ScfNews.InsertOnSubmit(db);
            Context.SubmitChanges();
            int id = db.Id;
            AssignAccessPermission(id, model.ListUserAccesses);
            return id;
        }

        public void SaveUpdate(NewsModel model)
        {

            var dbNew = GetNew(model.Id);
            if (dbNew == null)
                throw new SqlNotFilledException("Not found Warehouse with id");

            model.CreaterBy = dbNew.User;
            model.DateCreate = dbNew.DateCreate;
            model.IsApproved = dbNew.IsApproved;
            model.ApprovedBy = dbNew.User2;
            model.DateApproved = dbNew.DateApproved;
            model.DatePromulgate = dbNew.DatePromulgate;
            if (dbNew.Header != model.Header || dbNew.Content != model.Contents)
            {
                model.DatePromulgate = DateTime.Now;
                model.IsApproved = false;
            }

            CopyToDbModel(model, dbNew);

            //var canupdate = model.UsersCanUpdate + ";";
            //dbNew.UsersCanUpdate = canupdate.Replace(";;", ";");
            Context.SubmitChanges();
            AssignAccessPermission(model.Id, model.ListUserAccesses);
        }

        public bool DeleteNew(int id)
        {
            var db = GetNew(id);
            if (db == null) return false;

            var oldPermisstion = Context.GroupAccessPermissions.Where(x => x.AboutId == id).ToList();
            Context.GroupAccessPermissions.DeleteAllOnSubmit(oldPermisstion);
            var files =
                Context.ServerFiles.Where(
                    x => x.ObjectId == id && x.ObjectType == new SSM.Models.NewsModel().GetType().ToString());
            Context.ServerFiles.DeleteAllOnSubmit(files);
            Delete(db);
            return true;
        }

        public IList<Catelory> ListCatelories(NewType newType)
        {
            return Context.Catelories.Where(x => x.Type == (byte)newType).ToList();
        }

        public void Approval(int id)
        {
            var db = GetNew(id);
            db.IsApproved = true;
            Commited();
        }

        public string SetIsViewed(User user, int id)
        {
            var db = GetNew(id);
            var viewed = string.Format("{0};{1};", db.UserView, user.Id);
            db.UserView = viewed.Replace(";;", ";");
            Commited();
            return viewed;
        }

        private ScfNew ToDbModel(NewsModel model)
        {
            var db = new ScfNew()
            {
                Content = model.Contents,
                Header = model.Header,
                DateCreate = model.DateCreate,
                CreatedBy = model.CreaterBy.Id,
                IsAllowAnotherUpdate = model.IsAllowAnotherUpdate,
                Sourse = model.Sourse,
                CateloryId = model.CateloryId,
                Type = (byte)model.Type,
                IsApproved = model.IsApproved,
                RefDoc = model.RefDoc,
                DateApproved = model.DateApproved,
                DatePromulgate = model.DatePromulgate,
                UsersCanUpdate = model.UsersCanUpdate

            };
            if (!model.IsAllowAnotherUpdate)
            {
                db.UsersCanUpdate = null;
            }
            return db;
        }

        private void CopyToDbModel(NewsModel model, ScfNew db)
        {
            db.Content = model.Contents;
            db.Header = model.Header;
            db.DateCreate = model.DateCreate;
            db.DateModify = model.DateModify;
            if (model.ModifiedBy != null)
                db.ModifiedBy = model.ModifiedBy.Id;
            db.CreatedBy = model.CreaterBy.Id;
            db.IsAllowAnotherUpdate = model.IsAllowAnotherUpdate;
            db.CateloryId = model.CateloryId;
            db.Sourse = model.Sourse;
            db.Type = (byte)model.Type;
            db.IsApproved = model.IsApproved;
            db.RefDoc = model.RefDoc;
            if (model.ApprovedBy != null)
                db.Appvovedby = model.ApprovedBy.Id;
            else
                db.Appvovedby = (long?)null;
            db.DateApproved = model.DateApproved;
            db.DatePromulgate = model.DatePromulgate;
            db.UsersCanUpdate = model.UsersCanUpdate;
            if (!model.IsAllowAnotherUpdate)
            {
                db.UsersCanUpdate = null;
            }

        }

        public static NewsModel ToModel(ScfNew model)
        {
            var md = new NewsModel();
            md.Contents = model.Content;
            md.Id = model.Id;
            md.Header = model.Header;
            md.DateCreate = model.DateCreate;
            md.DateModify = model.DateModify;
            md.ModifiedBy = model.User1;
            md.CreaterBy = model.User;
            md.IsAllowAnotherUpdate = model.IsAllowAnotherUpdate;
            md.CateloryId = model.CateloryId;
            md.Sourse = model.Sourse;
            md.Type = (NewType)model.Type;
            md.Catelory = model.Catelory;
            md.IsApproved = model.IsApproved;
            var oldPermisstion = model.GroupAccessPermissions.Where(x => x.AboutId == model.Id).ToList();
            md.NewAccessPermissions = oldPermisstion;
            md.ListUserAccesses = oldPermisstion.Select(x => x.GroupId).ToArray();
            md.RefDoc = model.RefDoc;
            md.ApprovedBy = model.User2;
            md.DateApproved = model.DateApproved;
            md.DatePromulgate = model.DatePromulgate;
            md.UsersCanUpdate = model.UsersCanUpdate;
            if (!string.IsNullOrEmpty(model.UsersCanUpdate))
            {
                md.ListUserUpdate =
                    model.UsersCanUpdate.Split(';')
                        .Where(x => !string.IsNullOrEmpty(x))
                        .Select(x => long.Parse(x))
                        .ToArray();
            }
            else
            {
                md.ListUserUpdate = new long[100];
            }
            md.UserView = model.UserView;


            return md;
        }

        private void AssignAccessPermission(int newId, int[] listUser)
        {
            var oldPermisstion = Context.GroupAccessPermissions.Where(x => x.AboutId == newId).ToList();
            Context.GroupAccessPermissions.DeleteAllOnSubmit(oldPermisstion);
            if (listUser != null && listUser.Count() > 0)
            {
                var nPermissions = listUser.Select(userId => new GroupAccessPermission { AboutId = newId, GroupId = userId }).ToList();
                Context.GroupAccessPermissions.InsertAllOnSubmit(nPermissions);
            }
            Commited();
        }

        public IQueryable<ScfNew> GetScfNewsByUser(User user)
        {
            bool checkEdit = user.IsEditNew(null);
            var grpermission = user.UserGroups.Select(x => x.GroupId).ToList();
            var listNew =
                GetQuery(x => (checkEdit || x.GroupAccessPermissions.Any(u => grpermission.Contains(u.GroupId)))
                              || x.UsersCanUpdate.Contains(string.Format(";{0};", user.Id)));
            return listNew;
        }

        public bool CheckAboutNewByUser(User user)
        {

            var listNew =
                GetScfNewsByUser(user)
                    .Where(x => x.Type == (byte)NewType.News && (DateTime.Now - x.DateCreate).Days <= Helpers.DaysView)
                    .ToList();
            var check = true;
            if (!listNew.Any()) return false;
            check = listNew.Any(x => x.UserView == null);
            if (check) return true;
            foreach (var scfNew in listNew)
            {
                check = scfNew.UserView.Contains(string.Format(";{0};", user.Id));
                if (check == false)
                    return true;
            }
            return false;
        }

    }
}