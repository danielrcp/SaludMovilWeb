using System;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Globalization;

namespace SaludMovil.Transversales
{
    [Serializable]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class SaludMovilException : Exception
    {
        private LogEventos log;
        public SaludMovilException()
            : base()
        {
            return;
        }

        public SaludMovilException(string mensaje)
            :base(mensaje)
        {
            log = new LogEventos();
            log.LogError(this);

            return;
        }



        public SaludMovilException(string mensaje, Exception excepcion)
            : base(mensaje, excepcion)
        {
            log = new LogEventos();
            log.LogError(excepcion);
            return;
        }

        public SaludMovilException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            return;
        }
    }

    [Serializable]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class SaludMovilExceptionBD : SaludMovilException
    {
        private LogEventos log;
        public SaludMovilExceptionBD InnerServerError { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public SaludMovilExceptionBD()
            : base()
        {
            return;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Mensaje de la Excepción</param>
        public SaludMovilExceptionBD(string message)
            : base(message)
        {
            log = new LogEventos();
            log.LogError(this);
            return;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Mensaje de la Excepción</param>
        /// <param name="innerException">Excepción</param>
        public SaludMovilExceptionBD(string message, Exception innerException)
            : base(message, innerException)
        {
            log = new LogEventos();
            log.LogError(innerException);
            return;
        }

         /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="innerException">Excepción</param>
        public SaludMovilExceptionBD(Exception innerException)
        {
            log = new LogEventos();
            log.LogError(innerException);

            if (innerException.InnerException != null)
                this.InnerServerError = new SaludMovilExceptionBD(innerException.InnerException);

            SqlException sqlEx = innerException as SqlException;
            if (sqlEx != null)
            {
                throw new SaludMovilExceptionBD("SQL Exception:" + sqlEx.Message);
            }
            else
                throw new Exception(innerException.Message, innerException);
        }
    }


    public class LogEventos : IDisposable
    {
        /*
         * 1. Nombre del EventLog      - Es una Constante
         * 2. Nombre de la Aplicación  - Es una Constante
         * 3. Excepción - 
         * 4. Auditoria
         * 5. Tipificación (Error, Warning, Información)
         */
        #region Variables
      
        #endregion

        #region Constructor/Destructor
        public LogEventos()
        {
            Inicializar();
        
        }

        public void Dispose()
        {

        }
        #endregion Constructor/Destructor

        #region Métodos Privados

        private void Inicializar()
        {
            try
            {
                if (!System.Diagnostics.EventLog.SourceExists(Constantes.NOMBREEVENTLOG))
                    System.Diagnostics.EventLog.CreateEventSource(Constantes.NOMBREEVENTLOG, Constantes.NOMBREEVENTLOG);
            }
            catch (Exception)
            {
                throw new Exception("El event Log no existe en el servidor actual y además el usuario actual que ejecuta la aplicación  no tiene permisos para crearlo. Verifique por favor.");
            }
        }

        public string LogError(Exception error)
        {
            System.ServiceProcess.ServiceController sc = new System.ServiceProcess.ServiceController("eventlog");
            if (sc.Status != System.ServiceProcess.ServiceControllerStatus.Stopped)
                return GrabarLog(error.ToString(), System.Diagnostics.EventLogEntryType.Error);
            else
                return "El Servicio Event Viewer en el servidor esta Detenido";
        }

        public void LogWarning(string mensaje)
        {
            if (mensaje.Length > 0)
                GrabarLog(mensaje, System.Diagnostics.EventLogEntryType.Warning);
        }

        public void LogInformacion(string mensaje)
        {
            if (mensaje.Length > 0)
                GrabarLog(mensaje, System.Diagnostics.EventLogEntryType.Information);
        }

        private string GrabarLog(string mensaje, System.Diagnostics.EventLogEntryType type)
        {
            try
            {
                Int32 eventID;
                using (System.Diagnostics.EventLog obLogEntry = new System.Diagnostics.EventLog())
                {
                    obLogEntry.Source = Constantes.NOMBREEVENTLOG;
                    if(mensaje.Length >= short.MaxValue)
                        mensaje = mensaje.Remove(short.MaxValue - 1);

                    Random obRnd = new Random();
                    eventID = obRnd.Next(65535);
                    obLogEntry.WriteEntry(mensaje, type, eventID);
                }

                return eventID.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error al momento de ingresar el event log", ex);
            }
        }
        
        #endregion Métodos Privados
    }
}