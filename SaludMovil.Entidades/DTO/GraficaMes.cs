using System.Runtime.Serialization;
using System;

namespace SaludMovil.Entidades
{
    public partial class GraficaMes
    {
        [DataMember]
        public decimal valor1 { get; set; }
        [DataMember]
        public decimal valor2 { get; set; }
        [DataMember]
        public decimal valor3 { get; set; }
        [DataMember]
        public string Mes { get; set; }
    }
}
