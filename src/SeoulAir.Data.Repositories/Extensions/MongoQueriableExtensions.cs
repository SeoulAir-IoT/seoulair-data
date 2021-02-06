﻿using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SeoulAir.Data.Domain.Dtos;

namespace SeoulAir.Data.Repositories.Extensions
{
    public static class MongoQueryableExtension
    {
        public static IOrderedMongoQueryable<T> OrderBy<T>(this IMongoQueryable<T> query, string propertyName, IComparer<object> comparer = null)
        {
            return CallOrderedQueryable(query, "OrderBy", propertyName, comparer);
        }

        public static IOrderedMongoQueryable<T> OrderByDescending<T>(this IMongoQueryable<T> query, string propertyName, IComparer<object> comparer = null)
        {
            return CallOrderedQueryable(query, "OrderByDescending", propertyName, comparer);
        }

        public static IOrderedMongoQueryable<T> ThenBy<T>(this IOrderedMongoQueryable<T> query, string propertyName, IComparer<object> comparer = null)
        {
            return CallOrderedQueryable(query, "ThenBy", propertyName, comparer);
        }

        public static IOrderedMongoQueryable<T> ThenByDescending<T>(this IOrderedMongoQueryable<T> query, string propertyName, IComparer<object> comparer = null)
        {
            return CallOrderedQueryable(query, "ThenByDescending", propertyName, comparer);
        }

        /// <summary>
        /// Builds the Queryable functions using a TSource property name.
        /// </summary>
        private static IOrderedMongoQueryable<T> CallOrderedQueryable<T>(this IMongoQueryable<T> query, string methodName, string propertyName,
                IComparer<object> comparer = null)
        {
            var param = Expression.Parameter(typeof(T));

            var body = propertyName.Split('.').Aggregate<string, Expression>(param, Expression.PropertyOrField);

            return comparer != null
                ? (IOrderedMongoQueryable<T>)query.Provider.CreateQuery(
                    Expression.Call(
                        typeof(Queryable),
                        methodName,
                        new[] { typeof(T), body.Type },
                        query.Expression,
                        Expression.Lambda(body, param),
                        Expression.Constant(comparer)
                    )
                )
                : (IOrderedMongoQueryable<T>)query.Provider.CreateQuery(
                    Expression.Call(
                        typeof(Queryable),
                        methodName,
                        new[] { typeof(T), body.Type },
                        query.Expression,
                        Expression.Lambda(body, param)
                    )
                );
        }

        public static IMongoQueryable<T> FilterBy<T>(this IMongoQueryable<T> query, string propertyName,
            object propertyValue, FilterType? filterType)
        {
            var filterParam = Expression.Parameter(typeof(T), nameof(T));
            var filterProperty = propertyName.Split('.')
                .Aggregate<string, Expression>(filterParam, Expression.PropertyOrField);
            var propertyCastedValue = Convert.ChangeType(propertyValue, filterProperty.Type);

            
            BinaryExpression filterBody;
            switch (filterType)
            {
                case FilterType.Equal:
                    filterBody = Expression.Equal(filterProperty,
                        Expression.Constant(propertyCastedValue, filterProperty.Type));
                    break;
                case FilterType.GraterThen:
                    filterBody = Expression.GreaterThan(filterProperty,
                        Expression.Constant(propertyCastedValue, filterProperty.Type));
                    break;
                case FilterType.LessThen:
                    filterBody = Expression.LessThan(filterProperty,
                        Expression.Constant(propertyCastedValue, filterProperty.Type));
                    break;
                default:
                    filterBody = Expression.Equal(filterProperty,
                        Expression.Constant(propertyCastedValue, filterProperty.Type));
                    break;
            }
            var lambdaExpression = Expression.Lambda<Func<T, bool>>(filterBody, filterParam);

            return query.Where(lambdaExpression);
        }
    }
}
