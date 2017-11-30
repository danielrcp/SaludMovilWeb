// ***********************************************************************
// Assembly         : SaludMovil.Repositorio
// Author           : 
// Created          : 
//
// Last Modified By : 
// Last Modified On : 
// ***********************************************************************
// <copyright file="RepositoryQuery.cs" company="Asesoftware">
//     Copyright (c) Asesoftware. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

/// <summary>
/// The Repositorio namespace.
/// </summary>
namespace SaludMovil.Repositorio
{
    /// <summary>
    /// Class RepositoryQuery. This class cannot be inherited.
    /// </summary>
    /// <typeparam name="TEntity">The type of the t entity.</typeparam>
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public sealed class RepositoryQuery<TEntity> where TEntity : class
    {
        #region Private Fields

        /// <summary>
        /// The _include properties
        /// </summary>
        private readonly List<Expression<Func<TEntity, object>>> _includeProperties;
        /// <summary>
        /// The _repository
        /// </summary>
        private readonly Repository<TEntity> _repository;
        /// <summary>
        /// The _filter
        /// </summary>
        private Expression<Func<TEntity, bool>> _filter;
        /// <summary>
        /// The _order by querable
        /// </summary>
        private Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> _orderByQuerable;
        /// <summary>
        /// The _page
        /// </summary>
        private int? _page;
        /// <summary>
        /// The _page size
        /// </summary>
        private int? _pageSize;

        #endregion Private Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryQuery{TEntity}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        internal RepositoryQuery(Repository<TEntity> repository)
        {
            _repository = repository;
            _includeProperties = new List<Expression<Func<TEntity, object>>>();
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Filters the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>RepositoryQuery&lt;TEntity&gt;.</returns>
        public RepositoryQuery<TEntity> Filter(Expression<Func<TEntity, bool>> filter)
        {
            _filter = filter;
            return this;
        }

        /// <summary>
        /// Orders the by.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <returns>RepositoryQuery&lt;TEntity&gt;.</returns>
        public RepositoryQuery<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order)
        {
            _orderByQuerable = order;
            return this;
        }

        /// <summary>
        /// Includes the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>RepositoryQuery&lt;TEntity&gt;.</returns>
        public RepositoryQuery<TEntity> Include(Expression<Func<TEntity, object>> expression)
        {
            _includeProperties.Add(expression);
            return this;
        }

        /// <summary>
        /// Gets the page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns>IEnumerable&lt;TEntity&gt;.</returns>
        public IEnumerable<TEntity> GetPage(int page, int pageSize, out int totalCount)
        {
            _page = page;
            _pageSize = pageSize;
            totalCount = _repository.Get(_filter).Count();

            return _repository.Get(_filter, _orderByQuerable, _includeProperties, _page, _pageSize);
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>IEnumerable&lt;TEntity&gt;.</returns>
        public IEnumerable<TEntity> Get()
        {
            return _repository.Get(_filter, _orderByQuerable, _includeProperties, _page, _pageSize);
        }

        #endregion Public Methods
    }
}
