using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaludMovil.Transversales
{
    public class Seguridad
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        public bool CifrarCadena(string cadena)
        {
            try
            {
                //Ciframos la cadena con un algoritmo
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        public bool DesCifrarCadena(string cadena)
        {
            try
            {
                //Ciframos la cadena con un algoritmo
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}