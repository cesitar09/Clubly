using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
namespace Negocio
{
    public class ReservaAmbiente
    {
        public static Entities context()
        {
            return Datos.Context.context();
        }

        public static Exception insertar(Datos.ReservaAmbiente reservAmb)
        {
            try
            {
                reservAmb.estado = 1;
                if(reservAmb.Actividad != null) 
                    reservAmb.Actividad.estado = 1;
                if (reservAmb.Evento != null)
                    reservAmb.Evento.estado = 1;

                context().ReservaAmbiente.AddObject(reservAmb);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static Exception modificar(Datos.ReservaAmbiente reservAmb)
        {
            try
            {
                context().ReservaAmbiente.ApplyCurrentValues(reservAmb);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static Exception eliminar(Datos.ReservaAmbiente reservAmb)
        {
            try
            {
                reservAmb.estado = 0;
                context().ReservaAmbiente.ApplyCurrentValues(reservAmb);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        //B
        public static Datos.ReservaAmbiente buscarId(short id)
        {
            return context().ReservaAmbiente.Single(p => p.id == id);
        }
        public static Datos.ReservaAmbiente buscarIdActividad(short id)
        {
            return context().ReservaAmbiente.FirstOrDefault(p => p.Actividad.id == id);
        }
        public static Datos.ReservaAmbiente buscarIdEvento(short id)
        {
            return context().ReservaAmbiente.FirstOrDefault(p => p.Evento.id == id);
        }


        public static Datos.ReservaAmbiente seleccionarId(short id)
        {
            Datos.ReservaAmbiente reservaAmb = context().ReservaAmbiente.Single(p => p.id == id);
            return reservaAmb;
        }

        public static IEnumerable<Datos.ReservaAmbiente> seleccionarTodoEvento()
        {
            IEnumerable<Datos.ReservaAmbiente> listaReservas = context().ReservaAmbiente.Where(p => p.estado == 1 && p.Evento != null);
            return listaReservas;
        }


        public static IEnumerable<Datos.ReservaAmbiente> seleccionarTodaActividad()
        {
            IEnumerable<Datos.ReservaAmbiente> listaReservas = context().ReservaAmbiente.Where(p => p.estado == 1 && p.Actividad != null);
            return listaReservas;
        }
    }
}
