using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;

namespace Negocio
{
    public class Parametros
    {
        public const short INHABILITADO = 1;
        public const short HABILITADO = 2;
        
        public static Entities context()
        {
            return Context.context();
        }

        public static void insertar(Datos.Parametros parametro)
        {
                
                context().Parametros.AddObject(parametro);
                context().SaveChanges();
        }

        public static Exception deshabilitar(Datos.Parametros parametro)
        {
            try
            {
                
                context().Parametros.ApplyCurrentValues(parametro);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static IEnumerable<Datos.Parametros> seleccionarTodo()
        {
            try
            {
                IEnumerable<Datos.Parametros> listaParametros = context().Parametros.Where(p => p.estado > 0);
                return listaParametros;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static Datos.Parametros buscarId(short id)
        {
            return context().Parametros.Single(p => p.id == id);
        }

        public static IEnumerable<Datos.Parametros> seleccionarValido()
        {
            try
            {
                IEnumerable<Datos.Parametros> listaParametros = context().Parametros.Where(p => p.estado > 0 );
                return listaParametros;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Datos.Parametros SeleccionarParametros()
        {
            return Context.context().Parametros.First(p=>p.estado==HABILITADO && p.fechaFinal==null);
        }

    }
}
