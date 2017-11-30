using System.Linq;
using SaludMovil.Entidades;
using SaludMovil.Modelo;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Data;
using System.Collections.Generic;

namespace SaludMovil.Repositorio
{
    public class RepositorioPersonalMedico : Repository<sm_PersonalMedico>
    {
        internal RepositorioPersonalMedico(SaludMovilContext context)
            : base(context)
        {

        }

        #region Metodos Principales

        public sm_PersonalMedico ConsultarPersonalMedico(int idTipoIdentificacion, string numeroIdentificacion)
        {
            return this.Contexto.sm_PersonalMedico.AsParallel().Where(p => p.idTipoIdentificacion == idTipoIdentificacion).Where(p => p.numeroIdentificacion == numeroIdentificacion).FirstOrDefault();
        }

        /// <summary>
        /// Retorna listado medicos tratantes
        /// </summary>
        /// <returns></returns>
        public IList<MedicoTratante> ConsultaMedicosTratantes(int idTipoPersona)
        {
            IList<MedicoTratante> resultado = null;
            resultado = this.Contexto.Database.SqlQuery<MedicoTratante>("spRetornarMedicosTratantes {0}",
                new object[] { idTipoPersona }).ToList();
            return resultado;
        }

        #endregion Metodos Principales
    }
}
