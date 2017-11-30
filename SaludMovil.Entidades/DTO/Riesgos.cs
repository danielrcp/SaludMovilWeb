using System.Runtime.Serialization;
using System;


namespace SaludMovil.Entidades
{
    public partial class Riesgos
    {
        [DataMember]
        public int idRiesgo { get; set; }
        [DataMember]
        public string nombre { get; set; }
    }
}
