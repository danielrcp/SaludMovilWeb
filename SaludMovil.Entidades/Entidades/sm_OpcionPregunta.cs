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
    public partial class sm_OpcionPregunta : EntityBase
    {
        public sm_OpcionPregunta()
        {
            this.sm_Respuesta = new HashSet<sm_Respuesta>();
        }
    
        [DataMember] 
		public int idOpcionPregunta { get; set; }
        [DataMember] 
		public string enunciadoPregunta { get; set; }
        [DataMember] 
		public int idPregunta { get; set; }
        [DataMember] 
		public string indiceOpcion { get; set; }
        [DataMember] 
		public int idEncuesta { get; set; }
    
        public virtual sm_Pregunta sm_Pregunta { get; set; }
        public virtual ICollection<sm_Respuesta> sm_Respuesta { get; set; }
    }
}
