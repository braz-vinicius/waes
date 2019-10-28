using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Waes.CodeAssignment.ScalableWeb.Api.Infrastructure
{
    /// <summary>
    /// Simple generic IRepository interface 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Deletes an <typeparamref name="TEntity"/> given an <paramref name="entityToDelete"/>
        /// </summary>
        /// <param name="entityToDelete"></param>
        void Delete(TEntity entityToDelete);

        /// <summary>
        /// Deletes an <typeparamref name="TEntity"/> given an <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        /// Returns an enumarable of <typeparamref name="TEntity"/> according to the specified <paramref name="filter"/>
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(Func<TEntity, bool> filter = null);

        /// <summary>
        /// Inserts a <typeparamref name="TEntity"/> 
        /// </summary>
        /// <param name="entity"></param>
        void Insert(TEntity entity);

        /// <summary>
        /// Updates an <typeparamref name="TEntity"/>
        /// </summary>
        /// <param name="entityToUpdate"></param>
        void Update(TEntity entityToUpdate);
    }
}
