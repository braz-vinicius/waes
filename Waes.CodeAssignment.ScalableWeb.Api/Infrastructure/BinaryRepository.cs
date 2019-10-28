using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Waes.CodeAssignment.ScalableWeb.Api.Entities;

namespace Waes.CodeAssignment.ScalableWeb.Api.Infrastructure
{
    /// <summary>
    /// Represents a repository for acessing BinaryDiff persisted data
    /// </summary>
    public class BinaryRepository : IRepository<BinaryDiffComparisonEntity>
    {
        static readonly Dictionary<int, BinaryDiffComparisonEntity> dataSource = new Dictionary<int, BinaryDiffComparisonEntity>();

        /// <summary>
        /// Initializes a BinaryRepository instance
        /// </summary>
        public BinaryRepository()
        {
        }

        /// <summary>
        /// Deletes an item from the repository
        /// </summary>
        /// <param name="entityToDelete">entity to be deleted</param>
        public void Delete(BinaryDiffComparisonEntity entityToDelete)
        {
            dataSource.Remove(entityToDelete.Id);
        }

        /// <summary>
        /// Deletes an item from the repository
        /// </summary>
        /// <param name="id">id of the comparison operation</param>
        public void Delete(int id)
        {
            dataSource.Remove(id);
        }

        /// <summary>
        /// Returns a list of entities according to the specified <paramref name="filter"/>
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<BinaryDiffComparisonEntity> Get(Func<BinaryDiffComparisonEntity, bool> filter = null)
        {
            return dataSource.Values.AsEnumerable().Where(filter);
        }

        /// <summary>
        /// Inserts a new entity 
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(BinaryDiffComparisonEntity entity)
        {
            dataSource.Add(entity.Id, entity);
        }

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public void Update(BinaryDiffComparisonEntity entityToUpdate)
        {
            dataSource[entityToUpdate.Id] = entityToUpdate;
        }
    }
}
