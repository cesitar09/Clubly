using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;

namespace Negocio
{
    public class Cancha
    {
        public static Entities context()
        {
            return Datos.Context.context();
        }

        public static Exception Insertar(Datos.Cancha cancha)
        {
            try
            {
                context().Cancha.AddObject(cancha);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static Exception Modificar(Datos.Cancha cancha)
        {
            try
            {
                context().Cancha.ApplyCurrentValues(cancha);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static Exception Eliminar(Datos.Cancha cancha)
        {
            try
            {
                context().Cancha.ApplyCurrentValues(cancha);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static IEnumerable<Datos.Cancha> SeleccionarTodo()
        {

            IEnumerable<Datos.Cancha> listaCancha = context().Cancha.Where(p => p.estado != 0);
            return listaCancha;

        }

        public static Datos.Cancha BuscarId(short id)
        {
            return context().Cancha.FirstOrDefault(p => p.id == id);
        }

        public static IEnumerable<Datos.Cancha> BuscarTipo(short tipoCancha)
        {
            return context().Cancha.Where(p => p.TipoCancha.id == tipoCancha);
        }

        public static IEnumerable<Datos.Cancha> BuscarSedeTipo(short sede, short tipoCancha)
        {
            return context().Cancha.Where(cancha => cancha.Sede.id == sede && cancha.TipoCancha.id == tipoCancha);
        }

    }
}
