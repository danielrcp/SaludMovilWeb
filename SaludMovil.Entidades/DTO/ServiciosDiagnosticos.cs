using System.Runtime.Serialization;
using System;


namespace SaludMovil.Entidades
{
    public partial class ServiciosDiagnosticos
    {
        [DataMember]
        public string IDDIAGNOSTICOS { get; set; }
        [DataMember]
        public string DIAGNOSTICOS { get; set; }
    }
}
