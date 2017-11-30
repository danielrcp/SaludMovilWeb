using System;

namespace SaludMovil.Entidades
{
    /// <summary>
    /// Class EntityBase.
    /// </summary>
    [Serializable()]
    public abstract class EntityBase : IObjectState
    {         
        /// <summary>
        /// Gets or sets the state of the object.
        /// </summary>
        /// <value>The state of the object.</value>
        public ObjectState ObjectState { get; set; }
    }
}