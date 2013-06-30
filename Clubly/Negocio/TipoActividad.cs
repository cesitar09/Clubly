using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
namespace Negocio
{
    public class TipoActividad
    {
        public static Entities context()
        {
            return Context.context();
        }

        public static Exception insertar(Datos.TipoActividad tipoactividad)
        {
            try
            {
                context().TipoActividad.AddObject(tipoactividad);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static Exception modificar(Datos.TipoActividad tipoactividad)
        {
            try
            {
                context().TipoActividad.AddObject(tipoactividad);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static Exception eliminar(Datos.TipoActividad tact)
        {
            try
            {
                tact.estado = 0;
                context().TipoActividad.ApplyCurrentValues(tact);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static Datos.TipoActividad buscarId(short id)
        {
            return context().TipoActividad.Single(p => p.id == id);
        }

        public static IEnumerable<Datos.TipoActividad> seleccionarTodo()
        {
            IEnumerable<Datos.TipoActividad> lista = context().TipoActividad.Where(p => p.estado == 1);
            return lista;
        }
    }
}
