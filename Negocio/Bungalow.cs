using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;

namespace Negocio
{
    public class Bungalow
    {
        public static Entities context()
        {

            return Context.context();
        }

        public static Exception insertar(Datos.Bungalow bungalow)
        {
            try
            {
                context().Bungalow.AddObject(bungalow);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static IEnumerable<Datos.Bungalow> seleccionarTodo()
        {
            IEnumerable<Datos.Bungalow> listaBungalows = context().Bungalow.Where(p => p.estado>=1);
            return listaBungalows;
        }

        public static Datos.Bungalow seleccionarId(short id)
        {
            Datos.Bungalow bungalow = context().Bungalow.Single(p => p.id == id);
            return bungalow;
        }

        public static Datos.Bungalow buscarId(short id)
        {
            return context().Bungalow.Single(p => p.id == id);
        }

        public static Exception modificar(Datos.Bungalow bungalow)
        {

            try
            {
                context().Bungalow.ApplyCurrentValues(bungalow);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static Exception eliminar(Datos.Bungalow bungalow)
        {
            try
            {
                bungalow.estado = 0;
                context().Bungalow.ApplyCurrentValues(bungalow);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static Exception habilitar(Datos.Bungalow bungalow)
        {
            try
            {

                bungalow.estado = 1;
                context().Bungalow.ApplyCurrentValues(bungalow);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static Exception inhabilitar(Datos.Bungalow bungalow)
        {
            try
            {

                bungalow.estado = 2;
                context().Bungalow.ApplyCurrentValues(bungalow);
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
