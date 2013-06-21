using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;

namespace Negocio
{
    public class TemporadaAlta
    {
        public static Entities context()
        {

            return Context.context();
        }

        public static Exception insertar(Datos.TemporadaAlta tempAlta)
        {
            try
            {
                tempAlta.estado = 1;
                context().TemporadaAlta.AddObject(tempAlta);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static IEnumerable<Datos.TemporadaAlta> seleccionarTodo()
        {
            IEnumerable<Datos.TemporadaAlta> listaTemp = context().TemporadaAlta.Where(p => p.estado == 1);
            return listaTemp;
        }

        public static Datos.TemporadaAlta seleccionarId(short id)
        {
            Datos.TemporadaAlta tempAlta = context().TemporadaAlta.Single(p => p.id == id);
            return tempAlta;
        }

        public static Datos.TemporadaAlta buscarId(short id)
        {
            return context().TemporadaAlta.Single(p => p.id == id);
        }

        public static Exception modificar(Datos.TemporadaAlta tempAlta)
        {
            try
            {
                context().TemporadaAlta.ApplyCurrentValues(tempAlta);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static Exception eliminar(Datos.TemporadaAlta tempAlta)
        {
            try
            {
                tempAlta.estado = 0;
                context().TemporadaAlta.ApplyCurrentValues(tempAlta);
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
