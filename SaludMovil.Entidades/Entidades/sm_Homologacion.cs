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
    public partial class sm_Homologacion : EntityBase
    {
        [DataMember] 
		public int idHomologacion { get; set; }
        [DataMember] 
		public string tablaOrigen { get; set; }
        [DataMember] 
		public string campoOrigen { get; set; }
        [DataMember] 
		public string tablaDestino { get; set; }
        [DataMember] 
		public string campoDestino { get; set; }
        [DataMember] 
		public string nombreMostrar { get; set; }
        [DataMember] 
		public string createdBy { get; set; }
        [DataMember] 
		public Nullable<System.DateTime> createdDate { get; set; }
        [DataMember] 
		public string updatedBy { get; set; }
        [DataMember] 
		public Nullable<System.DateTime> updatedDate { get; set; }
    }
}
