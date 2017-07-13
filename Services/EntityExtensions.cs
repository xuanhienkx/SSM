using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SSM.Common;
using SSM.ViewModels.Shared;

namespace SSM.Services
{
    public class SortField
    {
        public SortField(string fieldName, bool isAsc)
        {
            FieldName = fieldName;
            IsAscending = isAsc;
        }
        public string FieldName { get; set; }
        public bool IsAscending { get; set; }
    }
    public static class EntityExtensions
    {
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, IList<SortField> sortFields)
          where T : class
        {
            return sortFields.Aggregate(query, (current, sortField) => current.OrderBy(sortField)) as IOrderedQueryable<T>;
        }

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, SortField sortField) where T : class
        {
            string method = string.Format("OrderBy{0}", sortField.IsAscending ? string.Empty : "Descending");
            var parameter = Expression.Parameter(query.ElementType, "p");
            var memberAccess = sortField.FieldName.Split('.')
                .Aggregate<string, Expression>(null, (current, name) => Expression.Property(current ?? parameter, name));
            var orderByLambda = Expression.Lambda(memberAccess, parameter);

            var result = Expression.Call(
                typeof(Queryable),
                method,
                new[] { query.ElementType, memberAccess.Type },
                query.Expression,
                Expression.Quote(orderByLambda));
            return query.Provider.CreateQuery<T>(result) as IOrderedQueryable<T>;
        }
        public static IOrderedQueryable<T> ThenBy<T>(this IQueryable<T> query, SortField sortField) where T : class
        {
            string method = string.Format("ThenBy{0}", sortField.IsAscending ? string.Empty : "Descending");
            var parameter = Expression.Parameter(query.ElementType, "p");
            var memberAccess = sortField.FieldName.Split('.')
                .Aggregate<string, Expression>(null, (current, name) => Expression.Property(current ?? parameter, name));
            var orderByLambda = Expression.Lambda(memberAccess, parameter);

            var result = Expression.Call(
                typeof(Queryable),
                method,
                new[] { query.ElementType, memberAccess.Type },
                query.Expression,
                Expression.Quote(orderByLambda));
            return query.Provider.CreateQuery<T>(result) as IOrderedQueryable<T>;
        } 
    }
}