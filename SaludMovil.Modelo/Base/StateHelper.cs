// ***********************************************************************
// Assembly         : SaludMovil.Modelo
// Author           : 
// Created          : 
//
// Last Modified By : 
// Last Modified On : 
// ***********************************************************************
// <copyright file="StateHelper.cs" company="Asesoftware">
//     Copyright (c) Asesoftware. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using SaludMovil.Entidades;
using System;
using System.Data;

/// <summary>
/// The Modelo namespace.
/// </summary>
namespace SaludMovil.Modelo
{
    /// <summary>
    /// Class StateHelper.
    /// </summary>
    public static class StateHelper
    {
        /// <summary>
        /// Converts the state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>System.Data.EntityState.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">state</exception>
        public static System.Data.EntityState ConvertState(ObjectState state)
        {
            switch (state)
            {
                case ObjectState.Unchanged:
                    return System.Data.EntityState.Unchanged;
                case ObjectState.Added:
                    return System.Data.EntityState.Added;
                case ObjectState.Deleted:
                    return System.Data.EntityState.Deleted;
                case ObjectState.Modified:
                    return System.Data.EntityState.Modified;
                default:
                    throw new ArgumentOutOfRangeException("state");
            }
        }
    }
}
