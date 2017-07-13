using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper.Internal;
using SSM.Common;

using SSM.Controllers;
using SSM.Models;

namespace SSM.Services
{
    public enum Message
    {
        Successfully,
        Failed
    }
    public interface IServices<TEntity> where TEntity : class
    {
        IList<TEntity> GetAll();
        IQueryable<TEntity> GetQuery();
        IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> filter);
        TEntity FindEntity(Expression<Func<TEntity, bool>> filter);
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter);
        long Count();
        int Count(Expression<Func<TEntity, bool>> filter);
        object Insert(TEntity entity);
        void InsertAll(List<TEntity> entitys);
        void Delete(TEntity entity);
        void DeleteAll(List<TEntity> entitys);
        IEnumerable<TEntity> GetObjectQuery(string sql);
        bool ExecuteCommand(string sql);
        IEnumerable<TEntity> GetListPager(IQueryable<TEntity> query, int page, int pageSize);
        bool Any();
        bool Any(Expression<Func<TEntity, bool>> filter);
        void Commited();
        IList<User> GetAllUser(Expression<Func<User, bool>> filter);
        IList<User> GetAllUserTrading();
        IEnumerable<Country> GetAllCountry();
        IQueryable<Country> GetCountrys(Expression<Func<Country, bool>> filter);
    }
    public abstract class Services<TEntity> : IServices<TEntity> where TEntity : class
    {
        protected Services()
        {
            Context = new DataClasses1DataContext();
        }

        protected Services(string connection)
        {
            Context = new DataClasses1DataContext(connection);
        }

        protected DataClasses1DataContext Context { get; set; }
        protected System.Data.Linq.Table<TEntity> Set
        {
            get
            {
                return Context.GetTable<TEntity>();
            }
        }
        public IList<TEntity> GetAll()
        {
            return Set.ToList();
        }

        public IQueryable<TEntity> GetQuery()
        {
            return Set.AsQueryable();
        }

        public IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> filter)
        {
            return Set.Where(filter).AsQueryable();
        }

        public TEntity FindEntity(Expression<Func<TEntity, bool>> filter)
        {
            return Set.FirstOrDefault(filter);
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            return Set.Where(filter).ToList();
        }

        public long Count()
        {
            return Set.LongCount();
        }

        public int Count(Expression<Func<TEntity, bool>> filter)
        {
            return Set.Count(filter);
        }


        public object Insert(TEntity entity)
        {
            Set.InsertOnSubmit(entity);
            Commited();
            return entity;
        }

        public void InsertAll(List<TEntity> entitys)
        {
            Set.InsertAllOnSubmit(entitys);
            Commited();
        }


        public void Delete(TEntity entity)
        {
            Set.DeleteOnSubmit(entity);
            Commited();
        }

        public void DeleteAll(List<TEntity> entitys)
        {
            Set.DeleteAllOnSubmit(entitys);
            Commited();
        }

        public IEnumerable<TEntity> GetObjectQuery(string sql)
        {
            return Context.ExecuteQuery<TEntity>(sql);
        }

        public bool ExecuteCommand(string sql)
        {
            int cm = Context.ExecuteCommand(sql);
            if (cm > 0)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<TEntity> GetListPager(IQueryable<TEntity> query, int page, int pageSize)
        {
            var skip = (page - 1) * pageSize;
            return query.Skip(skip).Take(pageSize).ToList();
        }
        public bool Any()
        {
            return Set.Any();
        }

        public bool Any(Expression<Func<TEntity, bool>> filter)
        {
            return Set.Any(filter);
        }

        public void Commited()
        {
            try
            {
                Context.SubmitChanges();
            }
            catch (SqlException ex)
            {
                Logger.LogError(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
        }

        public IList<User> GetAllUser(Expression<Func<User, bool>> filter)
        {
            return Context.Users.Where(filter).ToList();
        }

        private IList<User> userTrading;
        public IList<User> GetAllUserTrading()
        {
            if (userTrading != null)
                return userTrading;
            return
                userTrading = Context.Users.Where(x => x.AllowTrading == true && x.IsActive == true).ToList();
        }
        private List<Country> _countries;
        public IEnumerable<Country> GetAllCountry()
        {
            try
            {
                if (_countries != null && _countries.Any())
                {
                    return _countries;
                }
                const string sql = @"select * from Country order by CountryName";
                var qr = Context.ExecuteQuery<Country>(sql);
                return _countries = qr.ToList();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return new List<Country>();
            }
        }
        public IQueryable<Country> GetCountrys(Expression<Func<Country, bool>> filter)
        {
            var qr = Context.Countries.Where(filter).AsQueryable();
            return qr;
        }
    }
}