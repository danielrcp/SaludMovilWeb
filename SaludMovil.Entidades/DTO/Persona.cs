using System.Runtime.Serialization;
using System;
using System.Collections.Generic;

namespace SaludMovil.Entidades
{
    public partial class Persona
    {
        [DataMember]
        public int idTipoIdentificacion { get; set; }
        [DataMember]
        public string numeroIdentificacion { get; set; }
        [DataMember]
        public string primerNombre { get; set; }
        [DataMember]
        public string segundoNombre { get; set; }
        [DataMember]
        public string primerApellido { get; set; }
        [DataMember]
        public string segundoApellido { get; set; }
        [DataMember]
        public DateTime? fechaNacimiento { get; set; }
        [DataMember]
        public int idCiudad { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string celular { get; set; }
        [DataMember]
        public string telefonoFijo { get; set; }
        [DataMember]
        public string correo { get; set; }
        [DataMember]
        public string segmento { get; set; }
        [DataMember]
        public string planMp { get; set; }
        [DataMember]
        public string tipoContrato { get; set; }
        [DataMember]
        public string nombreColectivo { get; set; }
        [DataMember]
        public int institucion { get; set; }
        [DataMember]
        public int idTipoIdentificacionMedico { get; set; }
        [DataMember]
        public string numeroIdentificacionMedico { get; set; }
        [DataMember]
        public int recibeNotParentesco { get; set; }
        [DataMember]
        public string nombresParantesco { get; set; }
        [DataMember]
        public int idParentesco { get; set; }
        [DataMember]
        public string parentesco { get; set; }
        [DataMember]
        public string celularParentesco { get; set; }
        [DataMember]
        public string telefonoFijoParentesco { get; set; }
        [DataMember]
        public string correoParentesco { get; set; }
        [DataMember]
        public int idTipoIngreso { get; set; }
        [DataMember]
        public string nombreTipoIngreso { get; set; }
        [DataMember]
        public int idMedioAtencion { get; set; }
        [DataMember]
        public string medioAtencion { get; set; }
        [DataMember]
        public DateTime? fechaRegistro { get; set; }
        [DataMember]
        public int riesgo { get; set; }
        [DataMember]
        public int idEstado { get; set; }
        [DataMember]
        public string nombreEstado { get; set; }
        [DataMember]
        public string nombreEspecialidad { get; set; }
        [DataMember]
        public int tipoEspecialidad { get; set; }
        [DataMember]
        public IList<sm_Rol> Roles { get; set; }
        [DataMember]
        public IList<RolOpcion> Opciones { get; set; }
        [DataMember]
        public string createdBy { get; set; }
        [DataMember]
        public DateTime? createdDate { get; set; }
        [DataMember]
        public string updatedBy { get; set; }
        [DataMember]
        public DateTime? updatedDate { get; set; }
    }
}