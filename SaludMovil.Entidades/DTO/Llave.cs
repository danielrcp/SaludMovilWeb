using System.Runtime.Serialization;

namespace SaludMovil.Entidades
{
    public partial class Llave
    {
        [DataMember]
        public int idTablaTablaAnfitrion { get; set; }        
        [DataMember]
        public int idTablaDestino { get; set; }        
        [DataMember]
        public int idColumnaOrigen { get; set; }        
        [DataMember]
        public int idColumnaDestino { get; set; }        
        [DataMember]
        public string restriccion { get; set; }        
        [DataMember]
        public string tablaOrigen { get; set; }        
        [DataMember]
        public string columnaOrigen { get; set; }        
        [DataMember]
        public string esquemaDestino { get; set; }        
        [DataMember]
        public string tablaDestino { get; set; }        
        [DataMember]
        public string columnaDestino { get; set; }        
        [DataMember]
        public string nombreMostrar { get; set; }
    }
}
