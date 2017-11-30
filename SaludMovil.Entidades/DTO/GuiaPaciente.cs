using System.Runtime.Serialization;
using System;

namespace SaludMovil.Entidades
{
   public class GuiaPaciente
    {
        [DataMember]
        public int idGuiaPaciente { get; set; }
        [DataMember]
        public int idTipoIdentificacion { get; set; }
        [DataMember]
        public string numeroIdentificacion { get; set; }
        [DataMember]
        public int idGuia { get; set; }
        [DataMember]
        public string observaciones { get; set; }
        [DataMember]
        public Nullable<int> idEstado { get; set; }
        [DataMember]
        public Nullable<int> idOrigen { get; set; }
        [DataMember]
        public Nullable<int> tipoEvento { get; set; }
        [DataMember]
        public System.DateTime createdDate { get; set; }
        [DataMember]
        public string createdBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> updatedDate { get; set; }
        [DataMember]
        public string updatedBy { get; set; }
        [DataMember]
        public string txt1 { get; set; }
        [DataMember]
        public string txt2 { get; set; }
        [DataMember]
        public string txt3 { get; set; }
        [DataMember]
        public string txt4 { get; set; }
        [DataMember]
        public string txt5 { get; set; }
        [DataMember]
        public Nullable<decimal> valor1 { get; set; }
        [DataMember]
        public Nullable<decimal> valor2 { get; set; }
        [DataMember]
        public Nullable<decimal> valor3 { get; set; }
        [DataMember]
        public Nullable<decimal> valor4 { get; set; }
        [DataMember]
        public Nullable<decimal> valor5 { get; set; }
        [DataMember]
        public string unidad1 { get; set; }
        [DataMember]
        public string unidad2 { get; set; }
        [DataMember]
        public string unidad3 { get; set; }
        [DataMember]
        public string unidad4 { get; set; }
        [DataMember]
        public string unidad5 { get; set; }

        [DataMember]
        public string Periodicidad { get; set; }

        [DataMember]
        public string Forma { get; set; }

        [DataMember]
        public string Causal { get; set; }

        [DataMember]
        public Nullable<System.DateTime> fechaEvento { get; set; }

        [DataMember]
        public Nullable<System.DateTime> fecha1 { get; set; }

        [DataMember]
        public Nullable<System.DateTime> fecha2 { get; set; }
    }
}
