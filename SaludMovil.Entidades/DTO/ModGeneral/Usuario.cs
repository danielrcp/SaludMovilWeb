using System.Runtime.Serialization;
using System;
using System.Collections.Generic;

namespace SaludMovil.Entidades
{
    public partial class Usuario
    {
        [DataMember]
        public int idTipoIdentificacion { get; set; }
        [DataMember]
        public string nombreTipoIdentificacion { get; set; }
        [DataMember]
        public string numeroIdentificacion { get; set; }
        [DataMember]
        public string nombresPersona { get; set; }
        [DataMember]
        public string apellidosPersona { get; set; }
        [DataMember]
        public string primerNombre { get; set; }
        [DataMember]
        public string segundoNombre { get; set; }
        [DataMember]
        public string primerApellido { get; set; }
        [DataMember]
        public string segundoApellido { get; set; }
        [DataMember]
        public int idTipo { get; set; }
        [DataMember]
        public string tipoUsuario { get; set; }
        [DataMember]
        public DateTime? fechaNacimiento { get; set; }
        [DataMember]
        public int idCiudad { get; set; }
        [DataMember]
        public string nombreCiudad { get; set; }
        [DataMember]
        public string celular { get; set; }
        [DataMember]
        public string telefonoFijo { get; set; }
        [DataMember]
        public string correo { get; set; }
        [DataMember]
        public int idUsuario { get; set; }
        [DataMember]
        public string usuario { get; set; }
        [DataMember]
        public string contrasena { get; set; }
        [DataMember]
        public int idEmpresa { get; set; }
        [DataMember]
        public int estado{ get; set; }
        [DataMember]
        public string createdBy { get; set; }
        [DataMember]
        public System.DateTime? createdDate { get; set; }
        [DataMember]
        public IList<sm_Rol> Roles { get; set; }
    }
}