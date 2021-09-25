using Application.Domain.Core.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Data.Repositories
{
    /// <summary>
    /// Repositorio
    /// </summary>
    public interface IRepository<T> where T : Entity
    {
        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        T GetById(object id);

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        T Insert(T entity);


        Task InsertAsync(T entity);

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Insert(IEnumerable<T> entities);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(T entity);

        Task UpdateAsync(T entity);

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        IEnumerable<T> Update(IEnumerable<T> entities);

        Task UpdateCollectionAsync(IEnumerable<T> entities);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(T entity);

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Delete(IEnumerable<T> entities);

        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<T> Table { get; }
    }
}
