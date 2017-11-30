using System.Runtime.Serialization;
using System;

namespace SaludMovil.Entidades
{
    public partial class MedicoTratante
    {
        [DataMember]
        public string idPersonalMedico { get; set; }
        [DataMember]
        public string Nombres { get; set; }
    }
}
