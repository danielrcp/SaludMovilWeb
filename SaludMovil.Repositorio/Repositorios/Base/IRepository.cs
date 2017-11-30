// ***********************************************************************
// Assembly         : SaludMovil.Repositorio
// Author           : 
// Created          : 
//
// Last Modified By : 
// Last Modified On : 
// ***********************************************************************
// <copyright file="IRepository.cs" company="Asesoftware">
//     Copyright (c) Asesoftware. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace SaludMovil.Repositorio
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Insert item into repository
        /// </summary>
        /// <param name="entity">Item to Insert to repository</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Delete item by id
        /// </summary>
        /// <param name="id">Id Item to delete</param>
        void Delete(object id);

        /// <summary>
        /// Delete item by key values
        /// </summary>
        /// <param name="keyValues">keyValues Item to delete</param>
        void Delete(params object[] keyValues);

        /// <summary>
        /// Sets modified entity into the repository.
        /// When calling Commit() method in UnitOfWork
        /// these changes will be saved into the storage
        /// <remarks>
        /// Internally this method always calls Repository.Attach() and Context.SetChanges()
        /// </remarks>
        /// </summary>
        /// <param name="entity">Item with changes</param>
        void Update(TEntity entity);

        /// <summary>
        /// Get element of type {TEntity} in repository
        /// </summary>
        /// <param name="id">Id of element do match</param>
        /// <returns>Selected element</returns>
        TEntity FindById(object id);

        /// <summary>
        /// Get element of type {TEntity} in repository by keyValues
        /// </summary>
        /// <param name="keyValues">Key Values</param>
        /// <returns></returns>
        TEntity Find(params object[] keyValues);
    }
}