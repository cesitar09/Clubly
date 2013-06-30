using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Datos;
using Negocio.Util;
using System.Data.Objects.DataClasses;
using System.Data;

namespace Negocio
{
    public class TipoCancha
    {
        public static Entities context()
        {
            return Context.context();
        }

        public static IEnumerable<Datos.TipoCancha> SeleccionarTodo()
        {
            return context().TipoCancha.Where(tipoCancha => tipoCancha.estado != ListaEstados.ESTADO_ELIMINADO);
        }

        public static Datos.TipoCancha BuscarId(short idCancha)
        {
            return context().TipoCancha.FirstOrDefault(p => p.id == idCancha);
        }

        public static IEnumerable<Datos.TipoCancha> BuscarPorSede(short idSede)
        {
            return context().Cancha.Where(p => p.Sede.id == idSede).Select(p => p.TipoCancha);
        }
        
    }
}
