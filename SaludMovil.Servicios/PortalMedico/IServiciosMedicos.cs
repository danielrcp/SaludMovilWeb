using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SaludMovil.Servicios.PortalMedico
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IService
    {

        [OperationContract]
        string Saludo(string nombre);

        [OperationContract]
        int GetDataUsingDataContract(int composite);

        [OperationContract]
        bool ExistePaciente(int TipoDocumento,string NumeroDocumento);

        [OperationContract]
        System.Data.DataTable diagnosticos(string ocurrencia, string tipoGuia);

    }
}