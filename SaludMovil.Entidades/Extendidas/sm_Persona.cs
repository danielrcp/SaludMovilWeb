using System;
using System.Runtime.Serialization; 

namespace SaludMovil.Entidades
{
    public partial class sm_Persona
    {
        [DataMember] 
        public int Edad {
            get 
            {
                DateTime now = DateTime.Today;
                int age = now.Year - fechaNacimiento.Year;
                if (now < fechaNacimiento.AddYears(age)) age--;

                return age;
            } 
            
        }

    }
}
