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
    public partial class sm_AspectosMonitoreados : EntityBase
    {
        [DataMember] 
		public int idAspectoMonitoreado { get; set; }
        [DataMember] 
		public string descripcion { get; set; }
        [DataMember] 
		public string idEstado { get; set; }
        [DataMember] 
		public string createdBy { get; set; }
        [DataMember] 
		public System.DateTime createdDate { get; set; }
        [DataMember] 
		public string updatedBy { get; set; }
        [DataMember] 
		public Nullable<System.DateTime> updatedDate { get; set; }
    }
}
