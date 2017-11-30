#region Imports
using System;
using SaludMovil.Entidades;
using SaludMovil.Negocio;
using System.Collections;
using Telerik.Web.UI;

using System.Collections.Generic;
using System.Security.Cryptography;
using System.Net.Mail;
#endregion

/***
 * Descripción: Formulario que permite el cambio de contraseña
 * Fecha creacion: 28 Julio 2016
 * Creado por: Andres Felipe Silva Pascuas - afsp
 * Modificacion: 
 * 
 * 
 * 
 * */

namespace SaludMovil.Portal.ModGeneral
{
    public partial class frmCambioContrasena : System.Web.UI.Page
    {
        private AdministracionNegocio adminNegocio;
        protected void Page_Load(object sender, EventArgs e)
        {
            adminNegocio = new AdministracionNegocio();
            if (!Page.IsPostBack)
            {                

            }            
        }

        protected void btnCambiar_Click(object sender, EventArgs e)
        {
            if (!(txtContNue1.Text.Equals(string.Empty)) && !(txtContNue2.Text.Equals(string.Empty)))
            {
                if (txtContNue1.Text.Equals(txtContNue2.Text))
                {

                }
                else
                {
                    RadNotificationMensajes.Show("Las contraseñas no coinciden");
                }

                using (var client = new SmtpClient())
                {
                    MailMessage newMail = new MailMessage();
                    newMail.To.Add(new MailAddress("afsilva@iconoi.com"));
                    newMail.Subject = "Test Subject";
                    newMail.IsBodyHtml = true;

                    var inlineLogo = new LinkedResource(Server.MapPath("~/E:/Source/SaludMovil/Dev/SaludMovilWeb/SaludMovil.Portal/Images/LogoOriginal.png"));
                    inlineLogo.ContentId = Guid.NewGuid().ToString();

                    string body = string.Format(@"
                                                    <p>Lorum Ipsum Blah Blah</p>
                                                    <img src=""cid:{0}"" />
                                                    <p>Lorum Ipsum Blah Blah</p>
                                                ", inlineLogo.ContentId);

                    var view = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                    view.LinkedResources.Add(inlineLogo);
                    newMail.AlternateViews.Add(view);
                    client.Send(newMail);
                }


                //string contrasenaEncriptada = (string)Encrypt.encryptText(txtAntiguaContraseña.Text, login);
                //DataSet dsContraseñaAnterior = manApoyo.srvConsultar_ContrseñaAnterior(contrasenaEncriptada, login);
                //if (dsContraseñaAnterior.Tables[0].Rows[0]["contrasena"].ToString().Equals("1"))
                //{
                //    string contrasenaEncriptadaNueva = (string)Encrypt.encryptText(txtContraseñaNueva.Text, login);
                //    manRadicacion.srvActualizarContrasenaUsuario(contrasenaEncriptada, contrasenaEncriptadaNueva, login);

                //    DataSet dsUsuario = manApoyo.srvConsulta_idUsuario(login);

                //    DataSet dsCorreoDestino = this.manApoyo.srvConsultar_EMail(Convert.ToInt32(dsUsuario.Tables[0].Rows[0]["idUsuario"].ToString()));
                //    String emailDest = dsCorreoDestino.Tables[0].Rows[0]["CorreoElectronico"].ToString();

                //    MailMessage email = new MailMessage();
                //    String dirJur = ConfigurationManager.AppSettings["direccionSMTP"].ToString();
                //    email.To.Add(emailDest);
                //    email.From = new MailAddress(ConfigurationManager.AppSettings["direccionSMTP"].ToString());
                //    email.Subject = "WorkFlow Tenco Cambio de Contraseña";
                //    email.Body = "Tramite: Cambio de Contraseña\r\n\r\n" +
                //              "Detalle: Su contraseña se ha cambiado exitosamente\r\n\r\n\r\n" +
                //              "Nueva Contraseña: " + txtContraseñaNueva.Text + "\r\n\r\n\r\n" +
                //              "Ingrese A:  http://181.48.16.186/workflow para mayor información \r\n\r\n\r\n" +
                //              "Este mensaje ha sido enviado automaticamente desde Tenco WorkFlow \r\n" +
                //              "Visite Tenco Workflow para mayor información.\r\n\r\n\r\n" +
                //              "Este correo es informativo, favor no responder a esta dirección de correo, ya " +
                //              "que no se encuentra habilitada para recibir mensajes. " +
                //              "Si requiere mayor información sobre el contenido de este mensaje, Visite el Link anteriormente mencionado";

                //    SmtpClient ss = new SmtpClient(ConfigurationManager.AppSettings["servidorSMTP"].ToString());

                //    //   ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain,
                //    //      SslPolicyErrors sslPolicyErrors) { return true; };
                //    ss.EnableSsl = false;
                //    ss.Send(email);

                //    txtAntiguaContraseña.Text = "";
                //    txtContraseñaNueva.Text = "";

                //    showError("La contrseña se ha cambiado correctamente");
                //}
                //else
                //{
                //    showError("La contraseña anterior no es correcta!");
                //}
            }
            else
            {
                RadNotificationMensajes.Show("Uno o más campos están vacíos");
            }
        }
    }
}
