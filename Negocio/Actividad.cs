    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;

namespace Negocio
{
    public class Actividad
    {
        public static Entities context()
        {
            return Datos.Context.context();
        }

        public static Exception Insertar(Datos.Actividad actividad)
        {
            try
            {
                actividad.estado = 1;
                context().Actividad.AddObject(actividad);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static Exception Modificar(Datos.Actividad actividad)  
        {
            try
            {
                context().Actividad.ApplyCurrentValues(actividad);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static Exception Eliminar(Datos.Actividad actividad)
        {
            try
            {
                actividad.estado = 0;                
                context().Actividad.ApplyCurrentValues(actividad);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }
        
        public static IEnumerable<Datos.Actividad> SeleccionarTodo() { 

            IEnumerable<Datos.Actividad> listaActividad = context().Actividad.Where(p => p.estado != 0);
            return listaActividad;

        }

        public static Datos.Actividad BuscarId(short id)
        {
            return context().Actividad.FirstOrDefault(p => p.id == id);
        }

        public static IEnumerable<Datos.Actividad> BuscarNombre(String nombre)
        {
            return context().Actividad.Where(p => p.nombre == nombre);
        }
    }
}
