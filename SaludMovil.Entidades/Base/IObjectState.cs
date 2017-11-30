namespace SaludMovil.Entidades
{
    /// <summary>
    /// Interface IObjectState
    /// </summary>
    public interface IObjectState
    {
        /// <summary>
        /// Gets or sets the state of the object.
        /// </summary>
        /// <value>The state of the object.</value>
        //[NotMapped]
        ObjectState ObjectState { get; set; }
    }
}