using System.Linq.Expressions;

namespace Data.Repository.Interfaces;

public interface IRepository<TEntity, TId> where TEntity : class
{
    /// <summary>
    /// Asynchronously retrieves an entity by its identifier, optionally including related entities.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This overload provides a simple and type-safe way to eagerly load related entities using Include expressions.
    /// Use this when you only need to load navigation properties without additional filtering or query modifications.
    /// </para>
    /// <para>
    /// For more complex queries that require filtering, ordering, or other query modifications beyond simple includes,
    /// consider using <see cref="GetByIdAsync(TId, Func{IQueryable{TEntity}, IQueryable{TEntity}}?, CancellationToken)"/> instead.
    /// </para>
    /// </remarks>
    /// <param name="id">The identifier of the entity to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <param name="includes">
    /// Expressions specifying related entities to include in the query.
    /// If no expressions are provided, the entity will be retrieved without any related entities.
    /// Multiple navigation properties can be specified to eagerly load multiple relationships.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the entity if found; otherwise, <c>null</c>.
    /// </returns>
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);

    /// <summary>
    /// Asynchronously retrieves an entity by its identifier, allowing for custom query configuration.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This overload provides maximum flexibility for customizing the query, including adding includes, 
    /// filters, ordering, and projections. Use this when you need more control than simple includes provide.
    /// </para>
    /// <para>
    /// For more straightforward queries that only require eager loading of related entities, 
    /// consider using <see cref="GetByIdAsync(TId, CancellationToken, Expression{Func{TEntity, object}}[])"/> instead.
    /// </para>
    /// </remarks>
    /// <param name="id">The identifier of the entity to retrieve.</param>
    /// <param name="configureQuery">
    /// An optional function to configure the query before execution. 
    /// This allows for custom includes, filters, ordering, and other query modifications.
    /// If <c>null</c>, the entity will be retrieved without any modifications to the base query.
    /// </param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the entity if found; otherwise, <c>null</c>.
    /// </returns>
    Task<TEntity?> GetByIdAsync(TId id, Func<IQueryable<TEntity>, IQueryable<TEntity>>? configureQuery = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves all entities, optionally including related entities.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This overload provides a simple and type-safe way to eagerly load related entities using Include expressions.
    /// Use this when you only need to load navigation properties without additional filtering or query modifications.
    /// </para>
    /// <para>
    /// For more complex queries that require filtering, ordering, or other query modifications beyond simple includes,
    /// consider using <see cref="GetAllAsync(Func{IQueryable{TEntity}, IQueryable{TEntity}}?, CancellationToken)"/> instead.
    /// </para>
    /// </remarks>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <param name="includes">
    /// Expressions specifying related entities to include in the query.
    /// If no expressions are provided, the entity will be retrieved without any related entities.
    /// Multiple navigation properties can be specified to eagerly load multiple relationships.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a collection of all entities.
    /// If no entities exist, an empty collection is returned.
    /// </returns>
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);

    /// <summary>
    /// Asynchronously retrieves all entities, allowing for custom query configuration.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This overload provides maximum flexibility for customizing the query, including adding includes, 
    /// filters, ordering, and projections. Use this when you need more control than simple includes provide.
    /// </para>
    /// <para>
    /// For more straightforward queries that only require eager loading of related entities, 
    /// consider using <see cref="GetAllAsync(CancellationToken, Expression{Func{TEntity, object}}[])"/> instead.
    /// </para>
    /// </remarks>
    /// <param name="configureQuery">
    /// An optional function to configure the query before execution. 
    /// This allows for custom includes, filters, ordering, and other query modifications.
    /// If <c>null</c>, the entity will be retrieved without any modifications to the base query.
    /// </param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a collection of all entities.
    /// If no entities exist, an empty collection is returned.
    /// </returns>
    Task<IEnumerable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? configureQuery = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new entity to the repository
    /// </summary>
    /// <param name="entity">The entity to be saved</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The added entity</returns>
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing entity
    /// </summary>
    /// <param name="entity">The entity to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated entity</returns>
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an entity by its id
    /// </summary>
    /// <param name="id">The id of the entity to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if deleted successfully, false otherwise</returns>
    Task<bool> DeleteAsync(TId id, CancellationToken cancellationToken = default);
}