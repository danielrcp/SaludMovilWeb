using System.Runtime.Serialization;
using System;
using System.Collections.Generic;

namespace SaludMovil.Entidades
{
    public partial class MedidasPaciente
    {
        [DataMember]
        public decimal valor1 { get; set; }
        [DataMember]
        public decimal valor2 { get; set; }
        [DataMember]
        public decimal valor3 { get; set; }
        [DataMember]
        public decimal valor4 { get; set; }
        [DataMember]
        public decimal valor5 { get; set; }
        [DataMember]
        public string fechaCadena { get; set; }
    }
}