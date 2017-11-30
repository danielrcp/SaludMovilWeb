using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SaludMovil.Transversales
{
    public class Constantes : System.Web.UI.Page
    {
        #region Generales
        public const string NOMBREEVENTLOG = "IconoiLog";
        public const string NOMBREAPLICACION = "Salud Movil";
        #endregion Generales

        #region ModuloPacientes

        //TIPOS EVENTOS
        public const int TIPOEVENTOGLUCOSA = 1;
        public const int TIPOEVENTODIAGNOSTICO = 2;
        public const int TIPOEVENTOOTROSDIAGNOSTICOS = 3;
        public const int TIPOEVENTOTENSION = 4;
        public const int TIPOEVENTOPESO = 5;
        public const int TIPOEVENTOTALLA = 6;
        public const int TIPOEVENTOINTERCONSULTAS = 7;
        public const int TIPOEVENTOOTRASINTERCONSULTAS = 8;
        public const int TIPOEVENTOMEDICAMENTOS = 12;
        public const int TIPOEVENTOEXAMENES = 10;
        public const int TIPOEVENTOOTRASAYUDAS = 11;
        public const int TIPOEVENTOEXAMENESOTROSPROCEDIMIENTOS = 13;
        public const int TIPOEVENTOOTROSEXAMENESPROCEDIMIENTOS = 17;
        public const int TIPOEVENTOMEDICIONESBIOMETRICAS = 14;
        public const int TIPOEVENTOCONTROLES = 15;
        public const int TIPOEVENTOASPECTOS = 16;
        public const int TIPOEVENTOCITASMEDICAS = 18;

        //OTROS
        public const int ORIGENTOMA = 2;
        public const int TIPOPACIENTE = 1;
        public const int TIPOMEDICO = 2;
        public const string version = "servicio";
        //GUIAS
        public const int IDGUIAOTROSPROCEDIMIENTOS = 4;
        public const int IDGUIAMEDICIONES = 1;
        public const int IDGUIACONTROLES = 2;
        public const int IDGUIAASPECTOS = 3;

        //TIPOGUIAS
        public const int TIPOGUIAEXAMEN = 6;
        public const int TIPOGUIAMEDICAMENTO = 8;
        public const int TIPOGUIAAYUDAS = 9;
        public const int TIPOGUIAOTROSPROCEDIMIENTOS = 7;
        public const int TIPOGUIAOTRASINTERCONSULTAS = 13;
        public const int TIPOGUIAOTROSDIAGNOSTICOS = 5;
        public const int TIPOGUIAINTERCONSULTAS = 2;
        public const int TIPOGUIADIAGNOSTICOS = 1;
        public const int TIPOGUIACITASMEDICAS = 14;
        
        //Constantes test web services  
        public const string INTERCONSULTA = "INTERCONSULTAS";
        public const string EXAMEN = "EXAMEN";
        public const string MEDICAMENTO = "MEDICAMENTO";
        public const string AYUDA = "AYUDA";
        public const string OTROSPROCEDIMIENTOS = "OTROS EXÁMENES Y PROCEDIMIENTOS";
        public const string OTROSDIAGNOSTICOS = "OTROS DIAGNÓSTICOS";
        //

        #endregion ModuloPacientes
    }
}