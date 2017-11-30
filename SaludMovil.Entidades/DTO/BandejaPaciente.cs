using System.Runtime.Serialization;
using System;

namespace SaludMovil.Entidades
{
    public partial class BandejaPaciente
    {
        [DataMember]
        public int idTipoIdentificacion { get; set; }
        [DataMember]
        public string TipoIdentificacion { get; set; }
        [DataMember]
        public string NumeroIdentifacion { get; set; }
        [DataMember]
        public string Nombres { get; set; }
        [DataMember]
        public string Apellidos { get; set; }
        [DataMember]
        public DateTime FechaRegistro { get; set; }
        [DataMember]
        public string medicoTratante { get; set; }
        [DataMember]
        public string institucion { get; set; }
        [DataMember]
        public int riesgo { get; set; }
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
        public decimal Glucosa { get; set; }
        [DataMember]
        public decimal Sistolica { get; set; }
        [DataMember]
        public decimal Diastolica { get; set; }
        [DataMember]
        public DateTime? FechaGlucosa { get; set; }
        [DataMember]
        public DateTime? FechaSistolica { get; set; }
        [DataMember]
        public DateTime? FechaDiastolica { get; set; }
        [DataMember]
        public DateTime? UltimoControl { get; set; }
        [DataMember]
        public string TipoControl { get; set; }
        [DataMember]
        public DateTime? RegistroControl { get; set; }
        [DataMember]
        public string correo { get; set; }
        [DataMember]
        public string ciudad { get; set; }
        [DataMember]
        public string segmento { get; set; }
        [DataMember]
        public string planMp { get; set; }
        [DataMember]
        public string tipoContrato { get; set; }
        [DataMember]
        public string programa { get; set; }
        [DataMember]
        public string riesgodes { get; set; }
        [DataMember]
        public decimal Imc { get; set; }
    }
}