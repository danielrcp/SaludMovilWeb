using System.Runtime.Serialization;
using System;
using System.Collections.Generic;

namespace SaludMovil.Entidades
{
    public partial class TipoGuia
    {
        [DataMember]
        public int idTipoGuia { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int idEstado { get; set; }
        [DataMember]
        public string nombreEstado { get; set; }
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
    }
}