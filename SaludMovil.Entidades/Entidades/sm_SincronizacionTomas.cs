//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SaludMovil.Entidades
{
    using System.Runtime.Serialization; 
	using System;
    using System.Collections.Generic;
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class sm_SincronizacionTomas : EntityBase
    {
        [DataMember] 
		public int idSincronizacion { get; set; }
        [DataMember] 
		public string uuid { get; set; }
        [DataMember] 
		public Nullable<int> idTipoTipoIdentificacion { get; set; }
        [DataMember] 
		public string numeroIdentificacion { get; set; }
        [DataMember] 
		public Nullable<System.DateTime> ultimaSincronizacion { get; set; }
        [DataMember] 
		public string createdBy { get; set; }
        [DataMember] 
		public Nullable<System.DateTime> createdDate { get; set; }
        [DataMember] 
		public string updatedBy { get; set; }
        [DataMember] 
		public Nullable<System.DateTime> updatedDate { get; set; }
        [DataMember] 
		public string tokenPush { get; set; }
    
        public virtual sm_SincronizacionTomas sm_SincronizacionTomas1 { get; set; }
        public virtual sm_SincronizacionTomas sm_SincronizacionTomas2 { get; set; }
    }
}
