using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SaludMovil.Servicios.PortalMedico
{
    public class ServiciosMedicos : IService
    {
        public string Saludo(string nombre)
        {
            return "HOLA " + nombre + ", bienvenido a Saludmovil!!!!";
        }

        public int GetDataUsingDataContract(int composite)
        {
            throw new NotImplementedException();
        }

        public bool ExistePaciente(int TipoDocumento, string NumeroDocumento) 
        {
            string constr = ConfigurationManager.ConnectionStrings["TestWebService"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT idPaciente FROM sm_Paciente WHERE idTipoIdentificacion = "+TipoDocumento+" AND numeroIdentificacion = '"+NumeroDocumento+"'"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            dt.TableName = "Paciente";
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
        }

        public DataTable diagnosticos(string ocurrencia, string tipoGuia)
        {
            string constr = ConfigurationManager.ConnectionStrings["TestWebService"].ConnectionString;
            string sql = "SELECT TOP 50 IDDIAGNOSTICOS, IDDIAGNOSTICOS + ' - ' + DIAGNOSTICOS as DIAGNOSTICOS FROM diagnosticos WHERE DIAGNOSTICOS LIKE '%" + ocurrencia + "%'";
            if (tipoGuia.ToUpper().Contains("INTERCONSULTA"))
                sql = "SELECT TOP 50 PRESTACIONESPRE as IDDIAGNOSTICOS, PRESTACIONESPRE + ' - ' + DESCRIPCION as DIAGNOSTICOS FROM Prestaciones WHERE DESCRIPCION LIKE '%" + ocurrencia + "%' AND DESCRIPCION LIKE '%INTERCONSULTA%'";
            else if (tipoGuia.ToUpper().Contains("EXAMEN"))
                sql = "SELECT TOP 50 codigo as IDDIAGNOSTICOS,CODIGO + ' - ' + descripcion + ' - ' + descripcionGenerica as DIAGNOSTICOS FROM sm_ComponentesWS WHERE (DESCRIPCION LIKE '%" + ocurrencia + "%' or descripcionGenerica like '%" + ocurrencia + "%') and tipoItem = 'EXAMENES'";
            else if (tipoGuia.ToUpper().Contains("MEDICAMENTO"))
                sql = "SELECT TOP 50 codigo as IDDIAGNOSTICOS,CODIGO + ' - ' + descripcion + ' - ' + descripcionGenerica as DIAGNOSTICOS FROM sm_ComponentesWS WHERE (DESCRIPCION LIKE '%" + ocurrencia + "%' or descripcionGenerica like '%" + ocurrencia + "%') and tipoItem = 'MEDICAMENTOS'";
            else if (tipoGuia.ToUpper().Contains("AYUDA"))
                sql = "SELECT TOP 50 codigo as IDDIAGNOSTICOS,CODIGO + ' - ' + descripcion + ' - ' + descripcionGenerica as DIAGNOSTICOS FROM sm_ComponentesWS WHERE (DESCRIPCION LIKE '%" + ocurrencia + "%' or descripcionGenerica like '%" + ocurrencia + "%') and tipoItem = 'AYUDA'";
            
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            dt.TableName = "diagnosticos";
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
    }
}