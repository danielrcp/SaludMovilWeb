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
    public partial class sm_TipoDispositivo : EntityBase
    {
        [DataMember] 
		public int idTipoDispositivo { get; set; }
        [DataMember] 
		public string nombreDispositivo { get; set; }
        [DataMember] 
		public Nullable<int> idEstado { get; set; }
        [DataMember] 
		public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}