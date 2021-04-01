using MongoDB.Driver;
using OpenReferrals.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OpenReferrals.Repositories.Common
{
    public interface IMongoRepository<T> where T : IMongoDocument
    {
        IEnumerable<T> FilterBy(
                Expression<Func<T, bool>> filterExpression);

        Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression);

        Task<T> FindByIdAsync(string id);

        Task InsertOneAsync(T document);

        Task InsertManyAsync(ICollection<T> documents);

        Task ReplaceOneAsync(T document);

        Task DeleteOneAsync(Expression<Func<T, bool>> filterExpression);

        Task DeleteByIdAsync(string id);

        Task DeleteManyAsync(Expression<Func<T, bool>> filterExpression);
    }
}
