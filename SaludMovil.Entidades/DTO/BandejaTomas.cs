using System.Runtime.Serialization;
using System;

namespace SaludMovil.Entidades
{
    public partial class BandejaTomas
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
        public string valor1 { get; set; }
        [DataMember]
        public string valor2 { get; set; }
        [DataMember]
        public decimal valor3 { get; set; }
        [DataMember]
        public DateTime? fechaEvento { get; set; }
        [DataMember]
        public string Mes { get; set; }
        [DataMember]
        public decimal Imc { get; set; }
        [DataMember]
        public string fechaEventos { get; set; }
        
    }
}
