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
    public partial class sm_MedioAtencion : EntityBase
    {
        public sm_MedioAtencion()
        {
            this.sm_Paciente = new HashSet<sm_Paciente>();
        }
    
        [DataMember] 
		public int idMedioAtencion { get; set; }
        [DataMember] 
		public string descripcion { get; set; }
        [DataMember] 
		public int idEstado { get; set; }
        [DataMember] 
		public System.DateTime CreatedDate { get; set; }
        [DataMember] 
		public string CreatedBy { get; set; }
        [DataMember] 
		public Nullable<System.DateTime> UpdatedDate { get; set; }
        [DataMember] 
		public string UpdatedBy { get; set; }
    
        public virtual ICollection<sm_Paciente> sm_Paciente { get; set; }
    }
}
