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
    public partial class sm_TipoGuia : EntityBase
    {
        public sm_TipoGuia()
        {
            this.sm_Guia = new HashSet<sm_Guia>();
            this.sm_TipoGuiaXPrograma = new HashSet<sm_TipoGuiaXPrograma>();
        }
    
        [DataMember] 
		public int idTipoGuia { get; set; }
        [DataMember] 
		public string descripcion { get; set; }
        [DataMember] 
		public int idEstado { get; set; }
        [DataMember] 
		public string version { get; set; }
        [DataMember] 
		public System.DateTime createdDate { get; set; }
        [DataMember] 
		public string createdBy { get; set; }
        [DataMember] 
		public Nullable<System.DateTime> updatedDate { get; set; }
        [DataMember] 
		public string updatedBy { get; set; }
        [DataMember] 
		public Nullable<bool> esPonderadoPorGrupo { get; set; }
        [DataMember] 
		public Nullable<decimal> ponderadorGrupo { get; set; }
    
        public virtual sm_Estado sm_Estado { get; set; }
        public virtual ICollection<sm_Guia> sm_Guia { get; set; }
        public virtual ICollection<sm_TipoGuiaXPrograma> sm_TipoGuiaXPrograma { get; set; }
    }
}