using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using System.Collections;
using Negocio.Util;

namespace Negocio
{
 public   class Sorteo
    {
        public static Entities context()
        {
            return Context.context();
        }

        public static Exception Insertar(Datos.Sorteo sorteo)
        {
            try
            {
                context().Sorteo.AddObject(sorteo);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static IEnumerable<Datos.Sorteo> SeleccionarTodo()
        {
            return context().Sorteo.Where(p => p.estado != 0);

        }

        public static Datos.Sorteo BuscarSorteoIds(short idSede, short idBungalow, short idTempAlta)
        {
            Datos.Sorteo sorteo = context().Sorteo.FirstOrDefault(s => (s.Sede.id == idSede && s.TipoBungalow.id == idBungalow && s.TemporadaAlta.id == idTempAlta));
            return sorteo;
        }
        

        public static IEnumerable<Datos.Sorteo> SeleccionarPendientes()
        {
            return context().Sorteo.Where(p => p.estado == 1);
        }

        public static int Eliminar(Datos.Sorteo sorteo)
        {
  
                sorteo.estado = 0;
            

            return context().SaveChanges();

        }


    }
}
