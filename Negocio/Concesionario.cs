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
    public class Concesionario
    {
        public static Entities context()
        {
            return Context.context();
        }

        public static Exception insertar(Datos.Concesionario concesionario)
        {
            try
            {
                concesionario.estado = ListaEstados.ESTADO_ACTIVO;
                context().Concesionario.AddObject(concesionario);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static IEnumerable<Datos.Concesionario> seleccionarTodo()
        {
            return context().Concesionario.Where(concesionario => concesionario.estado != ListaEstados.ESTADO_ELIMINADO);
        }

        public static Datos.Concesionario buscarId(short id)
        {
            return context().Concesionario.Single(p => p.id == id);
        }

        public static Exception modificar(Datos.Concesionario concesionario, IEnumerable<short> sedes)
        {
            try
            {
                context().Concesionario.ApplyCurrentValues(concesionario);
                concesionario.Sede.Clear();
                if (sedes != null)
                    foreach (short codigo in sedes)
                    {
                        concesionario.Sede.Add(Negocio.Sede.buscarId(codigo));
                    }
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static Exception eliminar(Datos.Concesionario concesionario)
        {
            try
            {
                Datos.Concesionario eliminado = buscarId(concesionario.id);
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
