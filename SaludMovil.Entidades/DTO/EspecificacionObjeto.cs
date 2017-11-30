using System;
using System.Runtime.Serialization;

namespace SaludMovil.Entidades
{
    public partial class EspecificacionObjeto
    {
        [DataMember]
        public string column_name { get; set; }        
        [DataMember]
        public int column_id { get; set; }        
        [DataMember]
        public string type_schema { get; set; }        
        [DataMember]
        public string type_name { get; set; }        
        [DataMember]
        public bool is_user_defined { get; set; }        
        [DataMember]
        public bool is_assembly_type { get; set; }        
        [DataMember]
        public short max_length { get; set; }        
        [DataMember]
        public byte precision { get; set; }        
        [DataMember]
        public byte scale { get; set; }        
        [DataMember]
        public bool is_identity { get; set; }        
        [DataMember]
        public Nullable<bool> is_nullable { get; set; }        
        [DataMember]
        public int is_primary_key { get; set; }
    }
}
