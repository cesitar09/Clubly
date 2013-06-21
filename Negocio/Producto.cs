using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using System.Collections;

namespace Negocio
{
    public class Producto
    {
        public static Entities context()
        {
            return Datos.Context.context();
        }

        public static Exception insertar(Datos.Producto producto)
        {
            try
            {
                producto.estado = 1;
                context().Producto.AddObject(producto);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static IEnumerable<Datos.Producto> SeleccionarTodoProducto()
        {
            return context().Producto.Where(producto => producto.estado != 0);
        }


        public static Datos.Producto buscarId(short id)
        {
            return context().Producto.FirstOrDefault(p => p.id == id);
        }

        public static Exception modificar(Datos.Producto producto)
        {
            try
            {
                context().Producto.ApplyCurrentValues(producto);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static Exception eliminar(Datos.Producto producto)
        {
            try
            {
                producto.estado = 0;
                context().Producto.ApplyCurrentValues(producto);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        //public static IEnumerable<Datos.Actividad> seleccionarTodo()
        //{
        //    List<Datos.Actividad> listaActividad = context().Actividad.Where(p => p.estado == 1).ToList();
        //    return listaActividad;
        //}

        //public static Datos.Actividad seleccionarId(short id)
        //{
        //    Datos.Actividad actividad = context().Actividad.Single(p => p.id == id);
        //    return actividad;
        //}

    }
}
