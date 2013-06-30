using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Datos;
using Negocio.Util;

namespace Negocio
{
    public class PuntoDeVenta
    {
        public static Entities context()
        {
            return Context.context();
        }

        public static int insertar(Datos.PuntoDeVenta puntoDeVenta)
        {
            context().PuntoDeVenta.AddObject(puntoDeVenta);
            return context().SaveChanges();
        }

        public static IEnumerable<Datos.PuntoDeVenta> seleccionarTodo()
        {
            return context().PuntoDeVenta.Where(PuntoDeVenta => PuntoDeVenta.estado != ListaEstados.ESTADO_ELIMINADO);
        }

        public static Datos.PuntoDeVenta buscarId(short id)
        {
            return context().PuntoDeVenta.Single(p => p.id == id);
        }

        public static int modificar(Datos.PuntoDeVenta puntoDeVenta)
        {
            context().PuntoDeVenta.ApplyCurrentValues(puntoDeVenta);
            return context().SaveChanges(); 
        }

        public static int eliminar(Datos.PuntoDeVenta puntoDeVenta)
        {
            Datos.PuntoDeVenta eliminado = buscarId(puntoDeVenta.id);
            eliminado.estado = ListaEstados.ESTADO_ELIMINADO;
            return context().SaveChanges();
        }
    }
}
