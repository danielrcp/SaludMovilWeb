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
    public partial class sm_Departamento : EntityBase
    {
        public sm_Departamento()
        {
            this.sm_Ciudad = new HashSet<sm_Ciudad>();
        }
    
        [DataMember] 
		public int idDepartamento { get; set; }
        [DataMember] 
		public int idPais { get; set; }
        [DataMember] 
		public string codigo { get; set; }
        [DataMember] 
		public string nombre { get; set; }
        [DataMember] 
		public int estado { get; set; }
        [DataMember] 
		public System.DateTime createdDate { get; set; }
        [DataMember] 
		public string createdBy { get; set; }
        [DataMember] 
		public Nullable<System.DateTime> updatedDate { get; set; }
        [DataMember] 
		public string updatedBy { get; set; }
    
        public virtual ICollection<sm_Ciudad> sm_Ciudad { get; set; }
        public virtual sm_Pais sm_Pais { get; set; }
    }
}
