using System.Runtime.Serialization;

namespace SaludMovil.Entidades
{
    public partial class TablaAdministrable
    {
        [DataMember]
        public string nombreObjeto { get; set; }
        [DataMember]
        public string nombreTablaMaestra { get; set; }
        [DataMember]
        public int idObjeto { get; set; }
    }
}
