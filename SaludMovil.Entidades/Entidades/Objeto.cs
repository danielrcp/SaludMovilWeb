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
    public partial class Objeto : EntityBase
    {
        [DataMember] 
		public int IdObjeto { get; set; }
        [DataMember] 
		public string nombreObjeto { get; set; }
        [DataMember] 
		public short idTipoObjeto { get; set; }
        [DataMember] 
		public string nombreTablaMaestra { get; set; }
        [DataMember] 
		public Nullable<bool> esAdministrable { get; set; }
        [DataMember] 
		public Nullable<bool> admiteAtributosVariables { get; set; }
    }
}
