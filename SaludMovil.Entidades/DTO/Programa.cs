using System.Runtime.Serialization;
using System;
using System.Collections.Generic;

namespace SaludMovil.Entidades
{
    public partial class Programa
    {    
        [DataMember] 
		public int idPrograma { get; set; }
        [DataMember] 
		public string descripcion { get; set; }
        [DataMember] 
		public int idEstado { get; set; }
        [DataMember] 
		public System.DateTime createdDate { get; set; }
        [DataMember] 
		public string createdBy { get; set; }
        [DataMember] 
		public Nullable<System.DateTime> updatedDate { get; set; }
        [DataMember] 
		public string updatedBy { get; set; }
        [DataMember] 
		public System.DateTime fechaInicio { get; set; }
        [DataMember] 
		public System.DateTime fechaFin { get; set; }
        [DataMember] 
		public int poblacionObjetivo { get; set; }
        [DataMember] 
		public Nullable<int> idRiesgoPrograma { get; set; }
        [DataMember] 
		public Nullable<int> orden { get; set; }
        [DataMember]
        public string descEstado { get; set; }
        [DataMember]
        public string descPoblacion { get; set; }
        [DataMember]
        public string descRiesgo { get; set; }
    }
}