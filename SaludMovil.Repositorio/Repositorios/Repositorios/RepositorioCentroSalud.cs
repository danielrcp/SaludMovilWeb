using SaludMovil.Entidades;
using SaludMovil.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SaludMovil.Repositorio
{
    public sealed class RepositorioCentroSalud : Repository<sm_CentroSalud>
    {
        internal RepositorioCentroSalud(SaludMovilContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Retornar lista centros de salud activos
        /// </summary>
        /// <returns></returns>
        public IList<sm_CentroSalud> ListarCentrosSalud()
        {
            try
            {
                return this.Contexto.sm_CentroSalud.Where(g => g.idEstado == 1).ToList<sm_CentroSalud>();
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }
    }
}
