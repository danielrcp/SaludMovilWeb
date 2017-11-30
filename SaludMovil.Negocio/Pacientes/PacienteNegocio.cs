using SaludMovil.Entidades;
using SaludMovil.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaludMovil.Negocio
{
    public class PacienteNegocio
    {
        private UnidadTrabajo unitOfWork;

        public PacienteNegocio()
        {

        }

        /// <summary>
        /// Consulta especialidades
        /// </summary>
        public IList<MedicoTratante> ListarMedicosTratantes(int idTipoPersona)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.PersonalMedicoRepository.ConsultaMedicosTratantes(idTipoPersona);
            }
        }


        /// <summary>
        /// Consulta especialidades
        /// </summary>
        public IList<sm_CentroSalud> ListarCentrosSalud()
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.CentroSaludRepository.ListarCentrosSalud();
            }
        }

        /// <summary>
        /// Consulta especialidades
        /// </summary>
        public IList<sm_Especialidad> ListarEspecialidades()
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.EspecialidadRepository.ListarEspecialidades();
            }
        }

        /// <summary>
        /// Consulta datos personal medico
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <returns></returns>
        public sm_PersonalMedico ConsultarMedicoTratante(int idTipoIdentificacion,string numeroIdentificacion)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.PersonalMedicoRepository.ConsultarPersonalMedico(idTipoIdentificacion, numeroIdentificacion);
            }
        }

        /// <summary>
        /// Consulta datos persona por idTipoIdentificacion, numeroIdentificacion
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <returns></returns>
        public sm_Persona ConsultarPersona(int idTipoIdentificacion,string numeroIdentificacion)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.PersonaRepository.ConsultarPersona(idTipoIdentificacion,numeroIdentificacion);
            }
        }

        /// <summary>
        /// Consulta pacientes por id
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <returns></returns>
        public sm_Paciente ConsultarPaciente(int idTipoIdentificacion, string numeroIdentificacion)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.PacienteRepository.ConsultarPaciente(idTipoIdentificacion,numeroIdentificacion);
            }
        }

        /// <summary>
        /// Consulta entidad paciente
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <returns></returns>
        public List<sm_PacientePrograma> ConsultarPacientePrograma(int idTipoIdentificacion,string numeroIdentificacion)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.ProgramaPacienteRepository.ListarProgramaPaciente(idTipoIdentificacion,numeroIdentificacion);
            }
        }

        /// <summary>
        /// Consulta listado de relacion entre programa y paciente
        /// </summary>
        /// <returns></returns>
        public IList<sm_TipoIdentificacion> listarTiposIdentificacion()
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.TipoIdentificacionRepository.ListarTiposIdentificacion();
            }
        }

        /// <summary>
        /// Consulta listado de tipos de identificacion
        /// </summary>
        /// <returns></returns>
        public IList<sm_Ciudad> listarCiudades()
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.CiudadRepository.ListarCiudades();
            }
        }

        /// <summary>
        /// Consulta lista de ciudades
        /// </summary>
        /// <returns></returns>
        public IList<sm_Parentesco> listarParentescos()
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.ParentescoRepository.ListarParentescos();
            }
        }

        /// <summary>
        /// Consulta listado de parentescos
        /// </summary>
        /// <returns></returns>
        public IList<sm_Programa> listarProgramas()
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.ProgramaRepository.ListarProgramas();
            }
        }

        /// <summary>
        /// Consultado listado de programas
        /// </summary>
        /// <returns></returns>
        public IList<sm_MedioAtencion> listarMediosAtencion()
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.MedioAtencionRepository.ListarMediosAtencion();
            }
        }

        /// <summary>
        /// Consulta listado de medios de atención
        /// </summary>
        /// <returns></returns>
        public IList<sm_TipoIngreso> listarTiposIngreso()
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.TipoIngresoRepository.ListarTiposIngreso();
            }
        }


        /// <summary>
        /// Consulta listado de tipos de ingreso
        /// </summary>
        /// <returns></returns>
        public IList<Riesgos> listarRiesgos()
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.PacienteRepository.ConsultaRiesgos();
            }
        }

        /// <summary>
        /// Consulta listado de tipos de ingreso
        /// </summary>
        /// <returns></returns>
        public IList<BandejaPaciente> listarPacientes()
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.PacienteRepository.ConsultaBandejaPacientes();
            }
        }

        public IList<BandejaDiagnostico> ConsultaBandejaDiagnostico(int idTipoIdentificacion, string numeroIdentificacion,int tipoDiagnostico,int tipoOtroDiagnostico)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.PacienteRepository.ConsultarBandejaDiagnostico(idTipoIdentificacion, numeroIdentificacion,tipoDiagnostico,tipoOtroDiagnostico);
            }
        }

        public IList<Interconsultas> ConsultarDetalleBandejaActividades(int idTipoIdentificacion, string numeroIdentificacion, int tipoInterconsultas, int tipoOtrasInterconsultas,string codigo)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.PacienteRepository.ConsultarDetalleBandejaActividades(idTipoIdentificacion, numeroIdentificacion, tipoInterconsultas, tipoOtrasInterconsultas,codigo);
            }
        }

        public IList<InterconsultasAgrupadas> ConsultaBandejaInterconsultasAgrupadas(int idTipoIdentificacion, string numeroIdentificacion, int tipoInterconsultas, int tipoOtrasInterconsultas)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.PacienteRepository.ConsultarBandejaInterconsultasAgrupadas(idTipoIdentificacion, numeroIdentificacion, tipoInterconsultas, tipoOtrasInterconsultas);
            }
        }

        public IList<GraficaMes> ConsultaGraficaMes(int idTipoIdentificacion, string numeroIdentificacion, int tipoEvento)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.PacienteRepository.ConsultarGraficaMes(idTipoIdentificacion, numeroIdentificacion, tipoEvento);
            }
        }

        public IList<GraficaTension> ConsultaGraficaTension(int idTipoIdentificacion, string numeroIdentificacion, int tipoEvento)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.PacienteRepository.ConsultarGraficaTension(idTipoIdentificacion, numeroIdentificacion, tipoEvento);
            }
        }

        public IList<FechasTomas> ConsultarFechasTomas(int idTipoIdentificacion, string numeroIdentificacion, int tipoEvento)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.PacienteRepository.ConsultarFechasTomas(idTipoIdentificacion, numeroIdentificacion, tipoEvento);
            }
        }

        public IList<HorasTomas> ConsultarHorasTomas(int idTipoIdentificacion, string numeroIdentificacion, int tipoEvento,string fecha)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.PacienteRepository.ConsultarHorasTomas(idTipoIdentificacion, numeroIdentificacion, tipoEvento,fecha);
            }
        }


        public IList<GraficaFiltrada> ConsultaGraficaFiltro(int idTipoIdentificacion, string numeroIdentificacion, int tipoEvento, string mes)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.PacienteRepository.ConsultaGraficaFiltro(idTipoIdentificacion, numeroIdentificacion, tipoEvento,mes);
            }
        }

        public IList<GraficaFiltrada> ConsultaGraficaFiltroHora(int idTipoIdentificacion, string numeroIdentificacion, int tipoEvento, DateTime fechaInicio,DateTime fechaFin,string rango)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.PacienteRepository.ConsultaGraficaFiltroHora(idTipoIdentificacion, numeroIdentificacion, tipoEvento, fechaInicio, fechaFin, rango);
            }
        }


        public IList<GraficaFiltrada> ConsultaGraficaFiltroFechas(int idTipoIdentificacion, string numeroIdentificacion, int tipoEvento, DateTime fechaInicio, DateTime fechaFin)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.PacienteRepository.ConsultaGraficaFiltroFechas(idTipoIdentificacion, numeroIdentificacion, tipoEvento, fechaInicio, fechaFin);
            }
        }

        public IList<BandejaTomas> ConsultaBandejaTomas(int idTipoIdentificacion, string numeroIdentificacion, int tipoEvento)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.PacienteRepository.ConsultarBandejaTomas(idTipoIdentificacion, numeroIdentificacion, tipoEvento);
            }
        }

        /// <summary>
        /// Guarda entidad sm_Persona
        /// </summary>
        /// <param name="paciente"></param>
        /// <returns></returns>
        public bool GuardarPersona(sm_Persona persona)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.PersonaRepository.Insert(persona);
                unitOfWork.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Guarda entidad sm_Paciente
        /// </summary>
        /// <param name="paciente"></param>
        /// <returns></returns>
        public bool GuardarPaciente(sm_Paciente paciente)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.PacienteRepository.Insert(paciente);
                unitOfWork.SaveChanges();
            }
            return true;
        }
        /// <summary>
        /// Actualiza entidad paciente
        /// </summary>
        /// <param name="paciente"></param>
        /// <returns></returns>
        public bool ActualizarPaciente(sm_Paciente paciente)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.PacienteRepository.Update(paciente);
                unitOfWork.SaveChanges();
            }
            return true;
        }


        /// <summary>
        /// Actualiza entidad persona
        /// </summary>
        /// <param name="paciente"></param>
        /// <returns></returns>
        public bool ActualizarPersona(sm_Persona persona)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.PersonaRepository.Update(persona);
                unitOfWork.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Actualiza registro paciente programa
        /// </summary>
        /// <param name="pacientePrograma"></param>
        /// <returns></returns>
        public bool ActualizarPacientePrograma(sm_PacientePrograma pacientePrograma)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.ProgramaPacienteRepository.Update(pacientePrograma);
                unitOfWork.SaveChanges();
            }
            return true;
        }

       
        /// <summary>
        /// Actualiza entidad paciente
        /// </summary>
        /// <param name="PacientePrograma"></param>
        /// <returns></returns>
        public bool GuardarPacientePrograma(sm_PacientePrograma PacientePrograma)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.ProgramaPacienteRepository.Insert(PacientePrograma);
                unitOfWork.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Guarda relacion guia por paciente
        /// </summary>
        /// <param name="GuiaPaciente"></param>
        /// <returns></returns>
        public bool GuardarGuiaPaciente(sm_GuiaPaciente GuiaPaciente)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.GuiaPacienteRepository.Insert(GuiaPaciente);
                unitOfWork.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Actualiza relacion guia por paciente
        /// </summary>
        /// <param name="GuiaPaciente"></param>
        /// <returns></returns>
        public bool ActualizarGuiaPaciente(sm_GuiaPaciente GuiaPaciente)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.GuiaPacienteRepository.Update(GuiaPaciente);
                unitOfWork.SaveChanges();
            }
            return true;
        }


        /// <summary>
        /// Retornar entidad sm_GuiaPaciente
        /// </summary>
        /// <param name="GuiaPaciente"></param>
        public sm_GuiaPaciente RetornarGuiaPaciente(int idGuiaPaciente)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.GuiaPacienteRepository.FindById(idGuiaPaciente);
            }
        }

        /// <summary>
        /// Consutla la historia de mediciones de un paciente
        /// </summary>
        /// <param name="idTipoIdentificacion"></param>
        /// <param name="numeroIdentificacion"></param>
        /// <param name="tipoEvento"></param>
        /// <returns></returns>
        public IList<MedidasPaciente> obtenerDatosLecturas(int idTipoIdentificacion, string numeroIdentificacion, int tipoEvento)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.UsuarioRepository.obtenerDatosLecturas(idTipoIdentificacion, numeroIdentificacion, tipoEvento);                
            }
        }


        /// <summary>
        /// Retorna lista de guias por tipo y programa
        /// </summary>
        /// <param name="idPrograma"></param>
        /// <param name="idTipoGuia"></param>
        /// <param name="riesgo"></param>
        /// <returns></returns>
        public IList<DiagnosticoProgramaTipo> ListarGuiasPorProgramaTipo(int idPrograma, int idTipoGuia, int riesgo)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.PacienteRepository.ConsultarGuiasPorProgramaTipo(idPrograma, idTipoGuia,riesgo);
            }
        }

        /// <summary>
        /// Elimina registros de interconsultas por tipo y codigo
        /// </summary>
        /// <param name="codigoGuiaPaciente"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        public int borrarInterconsultasCodigo(string codigoGuiaPaciente, string tipoIdentificacion, string numeroIdentificacion)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.PacienteRepository.borrarInterconsultasCodigo(codigoGuiaPaciente, tipoIdentificacion, numeroIdentificacion);
            }
        }

        /// <summary>
        /// Consulta registros límites de mediciones biométricas
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        public List<GuiaPaciente> ConsultaBandejaMediciones(int idTipoIdentificación, string numeroIdentificacion, int idGuiaMediciones,string tipo)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.GuiaPacienteRepository.consultarMediciones(idTipoIdentificación,numeroIdentificacion,idGuiaMediciones,tipo);
            }
        }

        /// <summary>
        /// Consulta registro de medicamentos por paciente
        /// </summary>
        /// <param name="idTipoIdentificación"></param>
        /// <param name="numeroIdentificacion"></param>
        /// <param name="idTipoMedicamento"></param>
        /// <returns></returns>
        public List<GuiaPaciente> ConsultaMedicamentosPaciente(int idTipoIdentificación, string numeroIdentificacion, int idTipoMedicamento)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.GuiaPacienteRepository.consultarMedicamentos(idTipoIdentificación, numeroIdentificacion, idTipoMedicamento);
            }
        }

        /// <summary>
        /// Consulta registro de controles médicos
        /// </summary>
        /// <param name="idTipoIdentificacion"></param>
        /// <param name="numeroIdentificacion"></param>
        /// <param name="idTipoControlMedico"></param>
        /// <returns></returns>
        public List<GuiaPaciente> ConsultaBandejaControles(int idTipoIdentificacion, string numeroIdentificacion, int idTipoControlMedico)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.GuiaPacienteRepository.consultarControlesMedicos(idTipoIdentificacion, numeroIdentificacion, idTipoControlMedico);
            }
        }


        /// <summary>
        /// Retorna lista de aspectos monitoreados por paciente
        /// </summary>
        /// <param name="idTipoIdentificacion"></param>
        /// <param name="numeroIdentificacion"></param>
        /// <param name="tipoAspecto"></param>
        /// <returns></returns>
        public List<sm_GuiaPaciente> ConsultarAspectosMonitoreados(int idTipoIdentificacion, string numeroIdentificacion, int tipoAspecto)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.GuiaPacienteRepository.ConsultarAspectosMonitoreados(idTipoIdentificacion, numeroIdentificacion, tipoAspecto);
            }
        }


        /// <summary>
        /// Elimina registros de la tabla guia paciente para guardar los registros modificados
        /// </summary>
        /// <param name="gp"></param>
        public void EliminarGuiaPaciente(sm_GuiaPaciente gp)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.GuiaPacienteRepository.Delete(gp.idGuiaPaciente);
            }
        }

        /// <summary>
        /// Consultar idciudad para hacer mach con información traida del web service Colmédica
        /// </summary>
        /// <param name="codigoCiudad"></param>
        /// <returns></returns>
        public sm_Ciudad ConsultarIdCiudad(string codigoCiudad)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.CiudadRepository.RetornarCiudadCodigo(codigoCiudad);
            }
        }

       /// <summary>
       ///  Consultar calendario de citas medicas paciente 
       /// </summary>
       /// <param name="idTipoIdentificacion"></param>
       /// <param name="numeroIdentificacion"></param>
       /// <param name="tipoCitas"></param>
       /// <returns></returns>
        public List<Calendario> ConsultarCalendarioPaciente(int idTipoIdentificacion, string numeroIdentificacion, int tipoCitas)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.GuiaPacienteRepository.ConsultarCalendario(idTipoIdentificacion, numeroIdentificacion, tipoCitas);
            }
        }


        /// <summary>
        /// Elimina registro guia paciente por idGuiaPaciente
        /// </summary>
        /// <param name="idGuiaPaciente"></param>
        public void EliminarGuiaPaciente(int idGuiaPaciente)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.PacienteRepository.EliminarGuiaPaciente(idGuiaPaciente);
            }
        }

        /// <summary>
        /// Retorna lista de guias por paciente filtradas por codigo tipo
        /// </summary>
        /// <param name="idCodigoTipo"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public List<sm_GuiaPaciente> ConsultarGuiasPorCodigo(string idCodigoTipo, string idTipoIdentificacion, string numeroIdentificacion)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.PacienteRepository.ConsultarGuiasPacientePorCodigo(idCodigoTipo, idTipoIdentificacion, numeroIdentificacion);
            }
        }


        /// <summary>
        /// Eliminar guias paciente por código
        /// </summary>
        /// <param name="codigoGuiaPaciente"></param>
        public int EliminarGuiasPacientePorCodigo(string codigoGuiaPaciente,string tipoIdentificacion,string numeroIdentificacion)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.PacienteRepository.EliminarGuiasPacientePorCodigo(codigoGuiaPaciente, tipoIdentificacion, numeroIdentificacion);
            }
        }


        /// <summary>
        /// Actualiza especialidad medico
        /// </summary>
        /// <param name="idTipoIdentificacionMedico"></param>
        /// <param name="numeroIdentificacionMedico"></param>
        /// <param name="idEspecialidad"></param>
        public bool ActualizarEspecialidadMedico(int idTipoIdentificacionMedico, string numeroIdentificacionMedico, int idEspecialidad)
        {
            sm_PersonalMedico medico = new sm_PersonalMedico();
            using (unitOfWork = new UnidadTrabajo())
            {
                medico = unitOfWork.PersonalMedicoRepository.Find(idTipoIdentificacionMedico, numeroIdentificacionMedico);
                medico.idEspecialidad = idEspecialidad;
                unitOfWork.PersonalMedicoRepository.Update(medico);
                unitOfWork.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Consulta listado de pacientes, con filtro de tipo y número de identificación
        /// </summary>
        /// <returns></returns>
        public IList<BandejaPaciente> listarPacientesFiltro(string idTipoIdentificacion, string numeroIdentificacion)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.PacienteRepository.ConsultaBandejaPacientesFiltro(idTipoIdentificacion,numeroIdentificacion);
            }
        }

        /// <summary>
        /// Consulta listado de pacientes en un rango de fehcas
        /// </summary>
        /// <param name="nullable1"></param>
        /// <param name="nullable2"></param>
        /// <returns></returns>
        public IList<BandejaPaciente> listarPacientesFiltroFechas(DateTime? fechaInicio, DateTime? fechaFin)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.PacienteRepository.ConsultaBandejaPacientesFiltroFechas(fechaInicio, fechaFin);
            }
        }
    }
}