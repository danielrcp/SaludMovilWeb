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
    public partial class sm_TipoEvento : EntityBase
    {
        public sm_TipoEvento()
        {
            this.sm_GuiaPaciente = new HashSet<sm_GuiaPaciente>();
        }
    
        [DataMember] 
		public int idTipoEvento { get; set; }
        [DataMember] 
		public string descripcion { get; set; }
        [DataMember] 
		public string createdBy { get; set; }
        [DataMember] 
		public Nullable<System.DateTime> createdDate { get; set; }
    
        public virtual ICollection<sm_GuiaPaciente> sm_GuiaPaciente { get; set; }
    }
}
