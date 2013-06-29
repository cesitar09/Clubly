using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;

namespace Negocio
{
    public class EventoCorp
    {
        //context Singleton
        public static Entities context()
        {
            return Datos.Context.context();
        }

        //insertar
        public static Exception insertarCorp(Datos.EventoCorporativo evento)
        {
            try
            {
                context().AddToEventoCorporativo(evento);  
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }
        //modificar
        public static Exception modificarCorp(Datos.EventoCorporativo evento)
        {
            try
            {
                context().EventoCorporativo.ApplyCurrentValues(evento);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }
        //eliminar
        public static Exception eliminarCorp(Datos.EventoCorporativo evento)
        {
            try
            {                
                evento.Evento.estado = 0;
                context().Evento.ApplyCurrentValues(evento.Evento);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }


        public static IEnumerable<Datos.EventoCorporativo> seleccionarEventoCorp()
        {
            return context().EventoCorporativo.Where(evento => evento.Evento.estado != 0);
        }


        //buscar
        public static Datos.EventoCorporativo buscarIdCorp(int id)
        {
            return context().EventoCorporativo.FirstOrDefault(p =>p.id == id);
        }

        //public static IEnumerable<Datos.Evento> buscarNombre(String nombre)
        //{
        //    return context().Evento.Where(p => p.nombre == nombre).ToList();
        //}
        //public static IEnumerable<Datos.Evento> buscarEstado(short estado)
        //{
        //    return context().Evento.Where(p => p.estado == estado).ToList();
        

        //public static IEnumerable<Datos.EventoCorporativo> seleccionarDisponibles()
        //{            
        //    IEnumerable<Datos.EventoCorporativo> listaEventos = context().EventoCorporativo.Where(evento => (evento.estado != 0)&&(evento.fechaFin<=DateTime.Now));
        //    System.Diagnostics.Debug.WriteLine("Eventos seleccionados: " + listaEventos.ToList().Count);
        //    return listaEventos;
        //}
    }
}
