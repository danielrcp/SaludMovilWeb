using System.Runtime.Serialization;
using System;


namespace SaludMovil.Entidades
{
    public class Calendario
    {
        [DataMember]
        public int idGuiaPaciente { get; set; }
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
        public DateTime fechaEvento { get; set; }
        [DataMember]
        public decimal SistolicaSuperior { get; set; }
        [DataMember]
        public decimal SistolicaInferior { get; set; }
        [DataMember]
        public decimal DiastolicaSuperior { get; set; }
        [DataMember]
        public decimal DiastolicaInferior { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string txt1 { get; set; }
        [DataMember]
        public string txt2 { get; set; }
    }
}
