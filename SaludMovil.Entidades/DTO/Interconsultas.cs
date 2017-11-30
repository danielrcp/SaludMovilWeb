using System.Runtime.Serialization;
using System;

namespace SaludMovil.Entidades
{
    public class Interconsultas
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string Codigo { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string FechaRegistro { get; set; }
        [DataMember]
        public string txt4 { get; set; }
        [DataMember]
        public string txt5 { get; set; }
        [DataMember]
        public string observaciones { get; set; }
        [DataMember]
        public decimal valor1 { get; set; }
        [DataMember]
        public decimal valor3 { get; set; }
        [DataMember]
        public decimal valor4 { get; set; }
        [DataMember]
        public DateTime? fechaEvento { get; set; }
        [DataMember]
        public int idEstado { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public string Forma { get; set; }
        [DataMember]
        public string Periodicidad { get; set; }
    }
}
