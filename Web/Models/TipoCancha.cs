using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Negocio.Util;

namespace Web.Models
{
    public class TipoCancha
    {
        public short id { get; set; }
        public String nombre { get; set; }
        public String descripcion { get; set; }
        public short estado { get; set; }

        public TipoCancha() { }

        public TipoCancha(Datos.TipoCancha tipoCancha)
        {
            id = tipoCancha.id;
            nombre = tipoCancha.nombre;
            descripcion = tipoCancha.descripcion;
            estado = tipoCancha.estado;
        }

//CONVERTIDORES       

        public static IEnumerable<TipoCancha> ConvertirLista(IEnumerable<Datos.TipoCancha> listaCanchas)
        {
            return listaCanchas.Select(cancha => Convertir(cancha));
        }

        public static TipoCancha Convertir(Datos.TipoCancha tipoCancha)
        {
            return new TipoCancha(tipoCancha);
        }
       
//QUERY
        public static IEnumerable<TipoCancha> SeleccionarTodo()
        {
            IEnumerable<Datos.TipoCancha> tiposCancha = Negocio.TipoCancha.SeleccionarTodo();
            return ConvertirLista(tiposCancha);
        }
        public static Models.TipoCancha BuscarId(short idCancha) {
            return Convertir(Negocio.TipoCancha.BuscarId(idCancha));
        }

        public static IEnumerable<Models.TipoCancha> BuscarPorSede(short idSede)
        {
            return ConvertirLista(Negocio.TipoCancha.BuscarPorSede(idSede));    
        }
    }
}