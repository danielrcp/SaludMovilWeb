using System.Runtime.Serialization;
using System;

namespace SaludMovil.Entidades
{
    public class InterconsultasAgrupadas
    {
        [DataMember]
        public string Codigo { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
       
        [DataMember]
        public int valor1 { get; set; }
       
    }
}
