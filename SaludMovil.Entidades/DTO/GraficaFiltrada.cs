using System.Runtime.Serialization;
using System;

namespace SaludMovil.Entidades
{
    public partial class GraficaFiltrada
    {
        [DataMember]
        public decimal valor1 { get; set; }
        [DataMember]
        public decimal valor2 { get; set; }
        [DataMember]
        public decimal valor3 { get; set; }
        [DataMember]
        public string Fecha { get; set; }
        [DataMember]
        public decimal limiteSuperiorSistolica { get; set; }
        [DataMember]
        public decimal limiteInferiorSistolica { get; set; }
        [DataMember]
        public decimal limiteSuperiorDiastolica { get; set; }
        [DataMember]
        public decimal limiteInferiorDiastolica { get; set; }
        [DataMember]
        public decimal limiteSuperiorGlucosa { get; set; }
        [DataMember]
        public decimal limiteInferiorGlucosa { get; set; }
    }
}
