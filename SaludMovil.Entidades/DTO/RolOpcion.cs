using System.Runtime.Serialization;
using System;

namespace SaludMovil.Entidades
{
    public partial class RolOpcion
    {
        [DataMember]
        public int idRol { get; set; }
        [DataMember]
        public int idOpcion { get; set; }
        [DataMember]
        public bool? crear { get; set; }
        [DataMember]
        public bool? leer { get; set; }
        [DataMember]
        public bool? actualizar { get; set; }
        [DataMember]
        public bool? eliminar { get; set; }
        [DataMember]
        public string NombreOpcion { get; set; }
        [DataMember]
        public string URL { get; set; }
        [DataMember]
        public int orden { get; set; }
        [DataMember]
        public int idOpcionPadre { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public bool? activa { get; set; }
        [DataMember]
        public string URLImagen { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public DateTime? CreatedDate { get; set; }
        [DataMember]
        public string UpdatedBy { get; set; }
        [DataMember]
        public DateTime? UpdatedDate { get; set; }
        
    }
}