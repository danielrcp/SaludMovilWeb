// ***********************************************************************
// Assembly         : SaludMovil.Repositorio
// Author           : 
// Created          : 
//
// Last Modified By : 
// Last Modified On : 
// ***********************************************************************
// <copyright file="UnidadTrabajo.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using SaludMovil.Modelo;
using SaludMovil.Transversales;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Text;

namespace SaludMovil.Repositorio
{
    public sealed class UnidadTrabajo : IDisposable
    {
        #region Private Fields

        private readonly SaludMovilContext context;
        private RepositorioUsuario repositorioUsuario;
        private RepositorioPaciente repositorioPaciente;
        private RepositorioTablasAdministrables repositorioTablasAdministrables;
        private RepositorioTipoIdentificacion repositorioTipoIdentificacion;
        private RepositorioCiudad repositorioCiudad;
        private RepositorioParentesco repositorioParentesco;
        private RepositorioPrograma repositorioPrograma;
        private RepositorioMedioAtencion repositorioMedioAtencion;
        private RepositorioTipoIngreso repositorioTipoIngreso;
        private RepositorioTipoGuia repositorioTipoGuia;
        private RepositorioGuia repositorioGuia;
        private RepositorioProgramaPaciente repositorioProgramaPaciente;
        private RepositorioEstado repositorioEstado;
        private RepositorioPoblacion repositorioPoblacion;
        private RepositorioGuiaPaciente repositorioGuiaPaciente;
        private RepositorioPersona repositorioPersona;
        private RepositorioRolOpcion repositorioRolOpcion;
        private RepositorioEspecialidad repositorioEspecialidad;
        private RepositorioCentroSalud repositorioCentroSalud;
        private RepositorioPersonalMedico repositorioPersonalMedico;
        private RepositorioUsuarioRol repositorioUsuarioRol;
        private RepositorioOpcion repositorioOpcion;
        private RepositorioParametro repositorioParametro;
        #endregion

        #region Constuctor/Dispose

        /// <summary>
        /// Initializes a new instance of the <see cref="UnidadTrabajo"/> class.
        /// </summary>
        /// <author></author>
        /// <datetime></datetime>
        public UnidadTrabajo()
        {
            context = new SaludMovilContext();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <author></author>
        /// <datetime></datetime>
        public void Dispose()
        {
            if (context != null)
                context.Dispose();
        }

        #endregion Constuctor/Dispose

        #region Properties

        public RepositorioPersonalMedico PersonalMedicoRepository
        {
            get
            {
                if (this.repositorioPersonalMedico == null)
                {
                    this.repositorioPersonalMedico = new RepositorioPersonalMedico(context);
                }
                return repositorioPersonalMedico;
            }
        }

        public RepositorioCentroSalud CentroSaludRepository
        {
            get
            {
                if (this.repositorioCentroSalud == null)
                {
                    this.repositorioCentroSalud = new RepositorioCentroSalud(context);
                }
                return repositorioCentroSalud;
            }
        }


        public RepositorioEspecialidad EspecialidadRepository
        {
            get
            {
                if (this.repositorioEspecialidad == null)
                {
                    this.repositorioEspecialidad = new RepositorioEspecialidad(context);
                }
                return repositorioEspecialidad;
            }
        }

        public RepositorioUsuario UsuarioRepository
        {
            get
            {
                if (this.repositorioUsuario == null)
                {
                    this.repositorioUsuario = new RepositorioUsuario(context);
                }
                return repositorioUsuario;
            }
        }

        public RepositorioPaciente PacienteRepository
        {
            get
            {
                if (this.repositorioPaciente == null)
                {
                    this.repositorioPaciente = new RepositorioPaciente(context);
                }
                return repositorioPaciente;
            }
        }

        public RepositorioTipoIdentificacion TipoIdentificacionRepository
        {
            get
            {
                if (this.repositorioTipoIdentificacion == null)
                {
                    this.repositorioTipoIdentificacion = new RepositorioTipoIdentificacion(context);
                }
                return repositorioTipoIdentificacion;
            }
        }

        public RepositorioCiudad CiudadRepository
        {
            get
            {
                if (this.repositorioCiudad == null)
                {
                    this.repositorioCiudad = new RepositorioCiudad(context);
                }
                return repositorioCiudad;
            }
        }

        public RepositorioParentesco ParentescoRepository
        {
            get
            {
                if (this.repositorioParentesco == null)
                {
                    this.repositorioParentesco = new RepositorioParentesco(context);
                }
                return repositorioParentesco;
            }
        }

        public RepositorioTablasAdministrables TablasAdministrablesRepository
        {
            get
            {
                if (this.repositorioTablasAdministrables == null)
                {
                    this.repositorioTablasAdministrables = new RepositorioTablasAdministrables(context);
                }
                return repositorioTablasAdministrables;
            }
        }

        public RepositorioPrograma ProgramaRepository
        {
            get
            {
                if (this.repositorioPrograma == null)
                {
                    this.repositorioPrograma = new RepositorioPrograma(context);
                }
                return repositorioPrograma;
            }
        }

        public RepositorioMedioAtencion MedioAtencionRepository
        {
            get
            {
                if (this.repositorioMedioAtencion == null)
                {
                    this.repositorioMedioAtencion = new RepositorioMedioAtencion(context);
                }
                return repositorioMedioAtencion;
            }
        }

        public RepositorioTipoIngreso TipoIngresoRepository
        {
            get
            {
                if (this.repositorioTipoIngreso == null)
                {
                    this.repositorioTipoIngreso = new RepositorioTipoIngreso(context);
                }
                return repositorioTipoIngreso;
            }
        }

        public RepositorioTipoGuia TipoGuiaRepository
        {
            get
            {
                if (this.repositorioTipoGuia == null)
                {
                    this.repositorioTipoGuia = new RepositorioTipoGuia(context);
                }
                return repositorioTipoGuia;
            }
        }

        public RepositorioGuia GuiaRepository
        {
            get
            {
                if (this.repositorioGuia == null)
                {
                    this.repositorioGuia = new RepositorioGuia(context);
                }
                return repositorioGuia;
            }
        }

        public RepositorioProgramaPaciente ProgramaPacienteRepository
        {
            get
            {
                if (this.repositorioProgramaPaciente == null)
                {
                    this.repositorioProgramaPaciente = new RepositorioProgramaPaciente(context);
                }
                return repositorioProgramaPaciente;
            }
        }

        public RepositorioEstado EstadoRepository
        {
            get
            {
                if (this.repositorioEstado == null)
                {
                    this.repositorioEstado = new RepositorioEstado(context);
                }
                return repositorioEstado;
            }
        }

        public RepositorioPoblacion poblacionRepository
        {
            get
            {
                if (this.repositorioPoblacion == null)
                {
                    this.repositorioPoblacion = new RepositorioPoblacion(context);
                }
                return repositorioPoblacion;
            }
        }

        public RepositorioGuiaPaciente GuiaPacienteRepository
        {
            get
            {
                if (this.repositorioGuiaPaciente == null)
                {
                    this.repositorioGuiaPaciente = new RepositorioGuiaPaciente(context);
                }
                return repositorioGuiaPaciente;
            }
        }

        public RepositorioPersona PersonaRepository
        {
            get
            {
                if (this.repositorioPersona == null)
                {
                    this.repositorioPersona = new RepositorioPersona(context);
                }
                return repositorioPersona;
            }
        }

        public RepositorioRolOpcion rolOpcionRepository
        {
            get
            {
                if (this.repositorioRolOpcion == null)
                {
                    this.repositorioRolOpcion = new RepositorioRolOpcion(context);
                }
                return repositorioRolOpcion;
            }
        }

        public RepositorioUsuarioRol usuarioRolRepository
        {
            get
            {
                if (this.repositorioUsuarioRol == null)
                {
                    this.repositorioUsuarioRol = new RepositorioUsuarioRol(context);
                }
                return repositorioUsuarioRol;
            }
        }

        public RepositorioOpcion opcionRepository
        {
            get
            {
                if (this.repositorioOpcion == null)
                {
                    this.repositorioOpcion = new RepositorioOpcion(context);
                }
                return repositorioOpcion;
            }
        }

        public RepositorioParametro ParametroRepository
        {
            get
            {
                if (this.repositorioParametro == null)
                {
                    this.repositorioParametro = new RepositorioParametro(context);
                }
                return repositorioParametro;
            }
        }
        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Método que devuelve el número de filas afectadas en la base de datos.
        /// </summary>
        /// <returns>Número de filas afectadas</returns>
        public int SaveChanges()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ResolveConcurrencyConflicts(ex);
                SaveWithConcurrencyResolution();
                return -1;
            }
            catch (DbEntityValidationException e)
            {
                var sb = new StringBuilder();

                foreach (var entry in e.EntityValidationErrors)
                {
                    foreach (var error in entry.ValidationErrors)
                    {
                        sb.AppendLine(error.ErrorMessage);
                    }
                }
                throw new SaludMovilException(sb.ToString());
            }
            catch (Exception e)
            {
                throw new SaludMovilExceptionBD(e);
            }
        }

        private void SaveWithConcurrencyResolution()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ResolveConcurrencyConflicts(ex);
                SaveWithConcurrencyResolution();
            }
        }

        private static void ResolveConcurrencyConflicts(DbUpdateConcurrencyException ex)
        {
            foreach (var entry in ex.Entries)
            {
                //Discard you changes or Store wins
                entry.Reload();
                //entry.OriginalValues.SetValues(entry.GetDatabaseValues());
            }
        }

        #endregion Public Methods
    }
}