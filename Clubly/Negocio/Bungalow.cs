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

        //METODO QUE VERIFICA SI HAY RESERVAS LIGAS A UN BUNGALOW
        public static bool HayReserva(short id) {

            IEnumerable<Datos.ReservaBungalow> listaReservasBungalow = buscarId(id).ReservaBungalow.Where(p => p.estado == 1);
            if (listaReservasBungalow.Count() > 0) return true;
            else
                return false;
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


        //Comprueba si existe numero repetido
        public static bool ExisteNumero(short num) {
            IEnumerable<Datos.Bungalow> listaBungs = seleccionarTodo();
            //Datos.Bungalow b = new Datos.Bungalow();
            //b = listaBungs.ElementAtOrDefault().where(p => p.numero==num);
            if (listaBungs.SingleOrDefault(b=>b.numero==num && b.estado!=0) == null) return false;
            else
            return true;
        }


        
    }
}
