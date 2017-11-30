using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Globalization;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using Telerik.Web.UI;
namespace SaludMovil.Transversales
{
    public static class Comun
    {
        /// <summary>
        /// Verifica url si hay respuesta 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool isValidURL(string url)
        {
            WebRequest webRequest = WebRequest.Create(url);
            WebResponse webResponse;
            try
            {
                webResponse = webRequest.GetResponse();
            }
            catch //If exception thrown then couldn't get response from address
            {
                return false;
            }
            return true;
        }

        public static string EnumToDescription(this Enum value)
        {
            var attributes = (DescriptionAttribute[])value.GetType().GetField(
                Convert.ToString(value, CultureInfo.InvariantCulture)).GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : Convert.ToString(value, CultureInfo.InvariantCulture);
        }

        public static IEnumerable<SelectListItem> CargarGenero()
        {
            IEnumerable<Generos> types = Enum.GetValues(typeof(Generos)).Cast<Generos>();
            return from valor in types
                   select new SelectListItem
                   {
                       Text = valor.EnumToDescription().ToString(),
                       Value = valor.ToString()
                   };
        }

        public static IEnumerable<SelectListItem> CargarSiNo()
        {
            IEnumerable<Sino> types = Enum.GetValues(typeof(Sino)).Cast<Sino>();
            return from valor in types
                   select new SelectListItem
                   {
                       Text = valor.EnumToDescription().ToString(),
                       Value = valor.ToString()
                   };
        }

        #region Definicion programas
        /// <summary>
        /// Convierte los WS en una lista estandar
        /// </summary>
        /// <param name="componente"></param>
        /// <param name="wsURL"></param>
        /// <returns></returns>
        public static JArray listaGenerica(string componente, string wsURL)
        {
            WebClient proxy = new WebClient();
            string serviceURL = wsURL;
            byte[] _data = proxy.DownloadData(serviceURL);
            Stream _mem = new MemoryStream(_data);
            var reader = new StreamReader(_mem);
            var result = reader.ReadToEnd();
            JArray listaDiagnositcosREST = new JArray();
            JArray listanueva = new JArray();
            JObject nuevoObjeto = new JObject();
            try
            {
                listaDiagnositcosREST = JArray.Parse(result.ToString());//Es una lista lo que retorna el WS
            }
            catch (Exception ex)
            {
                nuevoObjeto = JObject.Parse(result.ToString());//Es un solo objeto
                if (!nuevoObjeto["Codigo"].ToString().Trim().Equals(string.Empty))
                {
                    listaDiagnositcosREST.Add(nuevoObjeto);
                    nuevoObjeto = new JObject();
                }
            }
            foreach (JObject o in listaDiagnositcosREST)
            {
                switch (componente)
                {
                    case "DIAGNÓSTICOS ASOCIADOS":
                    case "OTROS DIAGNÓSTICOS":
                        nuevoObjeto["Codigo"] = o["Codigo"].ToString();
                        nuevoObjeto["Descripcion"] = o["Codigo"].ToString() + " - " + o["Descripcion"].ToString();
                        break;
                    case "INTERCONSULTAS":
                    case "OTROS EXÁMENES Y PROCEDIMIENTOS":
                    case "EXAMENES DE LABORATORIO":
                        nuevoObjeto["Codigo"] = o["Codigo"].ToString() + o["CodigoApertura"].ToString();
                        nuevoObjeto["Descripcion"] = o["Codigo"].ToString() + " - " + o["CodigoApertura"].ToString() + " - " + o["Nombre"].ToString();
                        break;
                    case "EDUCACIÓN Y HÁBITOS DE VIDA SALUDABLE":
                        //TODO: PENDIENTE EL WS
                        break;
                    case "ENCUESTAS DE SALUD":
                        //TODO: PENDIENTE EL WS
                        break;
                    case "MEDICAMENTO":
                        nuevoObjeto["Codigo"] = o["Codigo"].ToString();
                        nuevoObjeto["Descripcion"] = o["Codigo"].ToString() + " - " + o["Descripcion"].ToString() + " - " + o["DescripcionGenerica"].ToString();
                        break;
                    case "AYUDAS DIAGNÓSTICAS":
                        //TODO: PENDIENTE EL WS
                        break;
                    case "TOMAS BIOMÉTRICAS":
                        //TODO: PENDIENTE EL WS¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿ESTO QUE ES???????????????????
                        break;
                    case "CONTROLES MÉDICOS":
                        //TODO: PENDIENTE EL WS¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿ESTO QUE ES???????????????????
                        break;
                    case "ASPECTOS MONITOREADOS":
                        //TODO: PENDIENTE EL WS¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿ESTO QUE ES???????????????????
                        break;
                }
                listanueva.Add(nuevoObjeto);
                nuevoObjeto = new JObject();
            }
            return listanueva;
        }

        /// <summary>
        /// Combinar los elementos de ambas listas y retonar una con el contenido de las dos
        /// </summary>
        /// <param name="lista1"></param>
        /// <param name="lista2"></param>
        /// <returns></returns>
        public static JArray CombinarListas(JArray lista1, JArray lista2)
        {
            foreach (JObject jo in lista2)
            {
                JObject jo2 = lista1.Children<JObject>().FirstOrDefault(o => o["Codigo"] != null && o["Codigo"].ToString() == jo["Codigo"].ToString());
                if (jo2 == null)//No exite el objeto en la lista 1
                {
                    lista1 = lista2;
                    break;
                }
            }
            return lista1;
        }
        #endregion Definicion programas

        #region Personal Medico
        public static JArray listarPersonalMedico(string wsURL)
        {
            WebClient proxy = new WebClient();
            string serviceURL = wsURL;
            byte[] _data = proxy.DownloadData(serviceURL);
            Stream _mem = new MemoryStream(_data);
            var reader = new StreamReader(_mem);
            var result = reader.ReadToEnd();
            JArray listaDiagnositcosREST = new JArray();
            JArray listanueva = new JArray();
            JObject nuevoObjeto = new JObject();
            foreach (JObject o in listaDiagnositcosREST)
            {
                nuevoObjeto["Codigo"] = o["Codigo"].ToString();
                nuevoObjeto["Descripcion"] = o["Codigo"].ToString() + " - " + o["Descripcion"].ToString();
                listanueva.Add(nuevoObjeto);
                nuevoObjeto = new JObject();
            }
            return listanueva;
        }

        /// <summary>
        /// Combinar la as lista recibidas en una sola
        /// </summary>
        /// <param name="lista1"></param>
        /// <param name="lista2"></param>
        /// <returns></returns>
        public static JArray CombinarListasPersonalMedico(JArray lista1, JArray lista2)
        {
            foreach (JObject jo in lista2)
            {
                JObject jo2 = lista1.Children<JObject>().FirstOrDefault(o => o["Codigo"] != null && o["Codigo"].ToString() == jo["Codigo"].ToString());
                if (jo2 == null)//No exite el objeto en la lista 1
                {
                    lista1 = lista2;
                    break;
                }
            }
            return lista1;
        }
        #endregion

        public static void CambiarIdiomaFilterGridTelerik(RadGrid grilla)
        {
            List<GridFilterMenu> grids = new List<GridFilterMenu>();
            grids.Add(grilla.FilterMenu);
            Parallel.ForEach(grids.ToList(), gridFilterMenu =>
            {
                GridFilterMenu menu = gridFilterMenu;
                foreach (RadMenuItem item in menu.Items)
                {
                    //change the text for the "StartsWith" menu item
                    if (item.Text == "NoFilter")
                    {
                        item.Text = "No Filtrar";
                    }
                    if (item.Text == "Contains")
                    {
                        item.Text = "Contiene";
                    }
                    if (item.Text == "DoesNotContain")
                    {
                        item.Text = "No Contiene";
                    }
                    if (item.Text == "StartsWith")
                    {
                        item.Text = "Empieza Con";
                    }
                    if (item.Text == "EndsWith")
                    {
                        item.Text = "Finaliza Con";
                    }
                    if (item.Text == "EqualTo")
                    {
                        item.Text = "Es igual a";
                    }
                    if (item.Text == "NotEqualTo")
                    {
                        item.Text = "No es igual a";
                    }
                    if (item.Text == "GreaterThan")
                    {
                        item.Text = "Es mayor que";
                    }
                    if (item.Text == "LessThan")
                    {
                        item.Text = "Es menor que";
                    }
                    if (item.Text == "GreaterThanOrEqualTo")
                    {
                        item.Text = "Es mayor o igual";
                    }
                    if (item.Text == "LessThanOrEqualTo")
                    {
                        item.Text = "Es menor o igual";
                    }
                    if (item.Text == "IsEmpty")
                    {
                        item.Text = "Es vacio";
                    }
                    if (item.Text == "NotIsEmpty")
                    {
                        item.Text = "No es vacio";
                    }
                    if (item.Text == "IsNull")
                    {
                        item.Text = "Es Nulo";
                    }
                    if (item.Text == "NotIsNull")
                    {
                        item.Text = "No es Nulo";
                    }
                    if (item.Text == "Between")
                    {
                        item.Text = "";
                    }
                    if (item.Text == "NotBetween")
                    {
                        item.Text = "";
                    }
                }
            }
            );
        }


        //public static void CambiarIdiomaFilterGridTelerik(RadGrid grilla)
        //{
        //    List<GridFilterMenu> grids = new List<GridFilterMenu>();
        //    grids.Add(grilla.FilterMenu);
        //    foreach (GridFilterMenu gridFilterMenu in grids)
        //    {
        //        //GridFilterMenu menu = RadGridCasosRadicados.FilterMenu;
        //        GridFilterMenu menu = gridFilterMenu;
        //        foreach (RadMenuItem item in menu.Items)
        //        {
        //            //change the text for the "StartsWith" menu item
        //            if (item.Text == "NoFilter")
        //            {
        //                item.Text = "No Filtrar";
        //            }
        //            if (item.Text == "Contains")
        //            {
        //                item.Text = "Contiene";
        //            }
        //            if (item.Text == "DoesNotContain")
        //            {
        //                item.Text = "No Contiene";
        //            }
        //            if (item.Text == "StartsWith")
        //            {
        //                item.Text = "Empieza Con";
        //            }
        //            if (item.Text == "EndsWith")
        //            {
        //                item.Text = "Finaliza Con";
        //            }
        //            if (item.Text == "EqualTo")
        //            {
        //                item.Text = "Es igual a";
        //            }
        //            if (item.Text == "NotEqualTo")
        //            {
        //                item.Text = "No es igual a";
        //            }
        //            if (item.Text == "GreaterThan")
        //            {
        //                item.Text = "Es mayor que";
        //            }
        //            if (item.Text == "LessThan")
        //            {
        //                item.Text = "Es menor que";
        //            }
        //            if (item.Text == "GreaterThanOrEqualTo")
        //            {
        //                item.Text = "Es mayor o igual";
        //            }
        //            if (item.Text == "LessThanOrEqualTo")
        //            {
        //                item.Text = "Es menor o igual";
        //            }
        //            if (item.Text == "IsEmpty")
        //            {
        //                item.Text = "Es vacio";
        //            }
        //            if (item.Text == "NotIsEmpty")
        //            {
        //                item.Text = "No es vacio";
        //            }
        //            if (item.Text == "IsNull")
        //            {
        //                item.Text = "Es Nulo";
        //            }
        //            if (item.Text == "NotIsNull")
        //            {
        //                item.Text = "No es Nulo";
        //            }
        //            if (item.Text == "Between")
        //            {
        //                item.Text = "";
        //            }
        //            if (item.Text == "NotBetween")
        //            {
        //                item.Text = "";
        //            }
        //        }
        //    }
        //}
    }
}
