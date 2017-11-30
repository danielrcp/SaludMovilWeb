using System.Runtime.Serialization;
using System;


namespace SaludMovil.Entidades
{
    public class HorasTomas
    {
        [DataMember]
        public int idGuia { get; set; }
        [DataMember]
        public string hora { get; set; }
    }
}
