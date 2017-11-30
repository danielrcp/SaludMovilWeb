using SaludMovil.Entidades;
using SaludMovil.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SaludMovil.Repositorio
{
    public sealed class RepositorioCiudad:Repository<sm_Ciudad>
    {
        internal RepositorioCiudad(SaludMovilContext context)
            : base(context)
        {

        }
        /// <summary>
        /// Retorna listado ciudades
        /// </summary>
        /// <returns></returns>
        public IList<sm_Ciudad> ListarCiudades()
        {
            try
            {
                return this.Query().Get().ToList();
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }

        /// <summary>
        /// Retorna ciudad por código de ciudad
        /// </summary>
        /// <returns></returns>
        public sm_Ciudad RetornarCiudadCodigo(string codigoCiudad)
        {
            try
            {
                return this.Contexto.sm_Ciudad.FirstOrDefault(c => c.codigo == codigoCiudad);
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }
    }
}
