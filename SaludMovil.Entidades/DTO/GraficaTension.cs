using System.Runtime.Serialization;
using System;


namespace SaludMovil.Entidades
{
   public class GraficaTension
    {
        [DataMember]
        public decimal valor1 { get; set; }
        [DataMember]
        public decimal valor2 { get; set; }
        [DataMember]
        public decimal valor3 { get; set; }
        [DataMember]
        public string Mes { get; set; }
        [DataMember]
        public int NumeroMes { get; set; }
        [DataMember]
        public string fechaEvento { get; set; }
        [DataMember]
        public decimal SistolicaSuperior { get; set; }
        [DataMember]
        public decimal SistolicaInferior { get; set; }
        [DataMember]
        public decimal DiastolicaSuperior { get; set; }
        [DataMember]
        public decimal DiastolicaInferior { get; set; }
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
        [DataMember]
        public decimal Imc { get; set; }
    }
}
