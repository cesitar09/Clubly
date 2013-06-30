using System.Linq;
using System.Text;
using System.Collections;
using Datos;
using Negocio.Util;
using System.Data.Objects.DataClasses;
using System.Data;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class Servicio
    {
        public static Entities context()
        {
            return Context.context();
        }

       public static Exception insertar(Datos.Servicio servicio)
        {
            try
            {
                servicio.estado = ListaEstados.ESTADO_ACTIVO;
                context().Servicio.AddObject(servicio);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static IEnumerable<Datos.Servicio> seleccionarTodo()
        {
            return context().Servicio.Where(s => s.estado != ListaEstados.ESTADO_ELIMINADO);
        }

        public static Datos.Servicio buscarId(short id)
        {
            return context().Servicio.Single(p => p.id == id);
        }

        public static Exception modificar(Datos.Servicio servicio, IEnumerable<short> sedes)
        {
            try
            {
                context().Servicio.ApplyCurrentValues(servicio);
                servicio.Sede.Clear();
                if (sedes != null)
                    foreach (short codigo in sedes)
                    {
                        servicio.Sede.Add(Negocio.Sede.buscarId(codigo));
                    }
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static Exception eliminar(Datos.Servicio servicio)
        {
            try
            {
                Datos.Servicio eliminado = buscarId(servicio.id);
                eliminado.estado = ListaEstados.ESTADO_ELIMINADO;
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }
    }
}