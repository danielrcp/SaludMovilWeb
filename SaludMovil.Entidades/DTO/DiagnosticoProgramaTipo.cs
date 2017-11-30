using System.Runtime.Serialization;
using System;

namespace SaludMovil.Entidades
{
    public partial class DiagnosticoProgramaTipo
    {
        [DataMember]
        public int idGuia { get; set; }
        [DataMember]
       
        public string Descripcion { get; set; }
     
    }
}
