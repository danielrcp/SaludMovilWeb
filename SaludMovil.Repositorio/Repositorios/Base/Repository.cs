// ***********************************************************************
// Assembly         : SaludMovil.Repositorio
// Author           : 
// Created          : 
//
// Last Modified By : 
// Last Modified On : 
// ***********************************************************************
// <copyright file="Repository.cs" company="Asesoftware">
//     Copyright (c) Asesoftware. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using SaludMovil.Entidades;
using SaludMovil.Transversales;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using SaludMovil.Modelo;

/// <summary>
/// The Repositorio namespace.
/// </summary>
namespace SaludMovil.Repositorio
{
    /// <summary>
    /// Class Repository.
    /// </summary>
    /// <typeparam name="TEntity">The type of the t entity.</typeparam>
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Variables

        /// <summary>
        /// The database set
        /// </summary>
        private readonly DbSet<TEntity> dbSet;

        #endregion Variables

        #region Constructor / Destructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}" /> class.
        /// </summary>
        /// <param name="context">The context or the ORM model.</param>
        /// <datetime>4/11/2014-4:37 PM</datetime>
        internal Repository(SaludMovilContext context)
        {
            Contexto = context;
            if (Contexto != null)
                dbSet = Contexto.Set<TEntity>();
        }

        #endregion Constructor / Destructor

        #region Propiedades

        /// <summary>
        /// Gets the contexto.
        /// </summary>
        /// <value>The contexto.</value>
        protected SaludMovilContext Contexto { get; private set; }

        #endregion Propiedades

        #region Metodos

        /// <summary>
        /// Insert item into repository
        /// </summary>
        /// <param name="entity">Item to Insert to repository</param>
        /// <exception cref="System.ArgumentNullException">entity</exception>
        /// <exception cref="SaludMovilExcepcionBD">
        /// </exception>
        public virtual void Insert(TEntity entity)
        {
            try
            {
                ((IObjectState)entity).ObjectState = ObjectState.Added;
                dbSet.Attach(entity);
                Contexto.SyncObjectState(entity);
            }
            catch (DbUpdateException exData)
            {
                throw new SaludMovilExceptionBD(exData);
            }
            catch (Exception ex)
            {
                throw new SaludMovilExceptionBD(ex.Message, ex);
            }
        }


        /// <summary>
        /// Inserts the add.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="System.ArgumentNullException">entity</exception>
        /// <exception cref="IntegraExcepcionBD">
        /// </exception>
        public virtual void InsertAdd(TEntity entity)
        {
            try
            {
                //check entity
                //if (entity == (TEntity)null)
                //    throw new ArgumentNullException("entity", Asesoftware.Recursos.Mensajes.exception_ItemArgumentIsNull);

                ((IObjectState)entity).ObjectState = ObjectState.Added;
                dbSet.Add(entity);
                Contexto.SyncObjectState(entity);
            }
            catch (DbUpdateException exData)
            {
                throw new SaludMovilExceptionBD(exData);
            }
            catch (Exception ex)
            {
                throw new SaludMovilExceptionBD(ex.Message, ex);
            }
        }

        /// <summary>
        /// Delete item by id
        /// </summary>
        /// <param name="id">Id Item to delete</param>
        /// <exception cref="System.ArgumentNullException">id</exception>
        /// <exception cref="IntegraExcepcionBD">
        /// </exception>
        public void Delete(object id)
        {
            try
            {
                var entity = dbSet.Find(id);
                Delete(entity);
            }
            catch (DbUpdateException exData)
            {
                throw new SaludMovilExceptionBD(exData);
            }
            catch (Exception ex)
            {
                throw new SaludMovilExceptionBD(ex.Message, ex);
            }
        }

        /// <summary>
        /// Delete item by key Values
        /// </summary>
        /// <param name="keyValues">keyValues Item to delete</param>
        /// <exception cref="System.ArgumentNullException">keyValues</exception>
        /// <exception cref="IntegraExcepcionBD">
        /// </exception>
        public void Delete(params object[] keyValues)
        {
            try
            {
                var entity = dbSet.Find(keyValues);
                Delete(entity);
            }
            catch (DbUpdateException exData)
            {
                throw new SaludMovilExceptionBD(exData);
            }
            catch (Exception ex)
            {
                throw new SaludMovilExceptionBD(ex.Message, ex);
            }
        }

        /// <summary>
        /// Delete item
        /// </summary>
        /// <param name="entity">Item to delete</param>
        /// <exception cref="System.ArgumentNullException">entity</exception>
        private void Delete(TEntity entity)
        {
            try
            {
                ((IObjectState)entity).ObjectState = ObjectState.Deleted;
                dbSet.Remove(entity);
                Contexto.SyncObjectState(entity);
            }
            catch (DbUpdateException exData)
            {
                throw new SaludMovilExceptionBD(exData);
            }
            catch (Exception ex)
            {
                throw new SaludMovilExceptionBD(ex.Message, ex);
            }
        }

        /// <summary>
        /// Sets modified entity into the repository.
        /// When calling Commit() method in UnitOfWork
        /// these changes will be saved into the storage
        /// <remarks>
        /// Internally this method always calls Repository.Attach() and Context.SetChanges()
        /// </remarks>
        /// </summary>
        /// <param name="entity">Item with changes</param>
        /// <exception cref="System.ArgumentNullException">entity</exception>
        /// <exception cref="IntegraExcepcionBD">
        /// </exception>
        public virtual void Update(TEntity entity)
        {
            try
            {
                ((IObjectState)entity).ObjectState = ObjectState.Modified;
                dbSet.Attach(entity);
                Contexto.SyncObjectState(entity);
            }
            catch (DbUpdateException exData)
            {
                throw new SaludMovilExceptionBD(exData);
            }
            catch (Exception ex)
            {
                throw new SaludMovilExceptionBD(ex.Message, ex);
            }
        }

        /// <summary>
        /// Get element of type {TEntity} in repository by keyValues
        /// </summary>
        /// <param name="keyValues">Key Values</param>
        /// <returns>TEntity.</returns>
        /// <exception cref="System.ArgumentNullException">keyValues</exception>
        /// <exception cref="IntegraExcepcionBD">
        /// </exception>
        public virtual TEntity Find(params object[] keyValues)
        {
            try
            {
                return dbSet.Find(keyValues);
            }
            catch (DbUpdateException exData)
            {
                throw new SaludMovilExceptionBD(exData);
            }
            catch (Exception ex)
            {
                throw new SaludMovilExceptionBD(ex.Message, ex);
            }
        }

        /// <summary>
        /// Get element of type {TEntity} in repository
        /// </summary>
        /// <param name="id">Id of element do match</param>
        /// <returns>Selected element</returns>
        /// <exception cref="System.ArgumentNullException">id</exception>
        /// <exception cref="IntegraExcepcionBD">
        /// </exception>
        public virtual TEntity FindById(object id)
        {
            try
            {
                //check id
                //if (id == null)
                //    throw new ArgumentNullException("id", Asesoftware.Recursos.Mensajes.exception_ItemArgumentIsNull);

                return dbSet.Find(id);
            }
            catch (DbUpdateException exData)
            {
                throw new SaludMovilExceptionBD(exData);
            }
            catch (Exception ex)
            {
                throw new SaludMovilExceptionBD(ex.Message, ex);
            }
        }

        /// <summary>
        /// Generic Method for Query by filter, orderBy, includeProperties, page and pageSize
        /// </summary>
        /// <returns>RepositoryQuery&lt;TEntity&gt;.</returns>
        protected virtual RepositoryQuery<TEntity> Query()
        {
            var repositoryGetFluentHelper = new RepositoryQuery<TEntity>(this);

            return repositoryGetFluentHelper;
        }

        /// <summary>
        /// Get elements of type {TEntity} in repository
        /// </summary>
        /// <param name="filter">Filter that each element do match</param>
        /// <param name="orderBy">Order by expression</param>
        /// <param name="includeProperties">Include properties to {TEntity} for load Eager Loading ".Include()"</param>
        /// <param name="page">Index of page</param>
        /// <param name="pageSize">Number of elements in each page</param>
        /// <returns>List of selected elements</returns>
        internal IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includeProperties = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (includeProperties != null)
                includeProperties.ForEach(i => { query = query.Include(i); });

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);

            return query.ToArray();
        }

        #endregion Metodos
    }
}