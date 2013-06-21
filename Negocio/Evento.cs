using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;

namespace Negocio
{
    public class Evento
    {
        //context Singleton
        public static Entities context()
        {
            return Datos.Context.context();
        }

        //insertar
        public static Exception insertar(Datos.Evento evento)
        {
            try
            {
                evento.estado = 1;
                context().AddToEvento(evento);                

                if (evento.EventoCorporativo != null)
                    context().AddToEventoCorporativo(evento.EventoCorporativo);
                else
                    if (evento.EventoPrivado != null)
                        context().AddToEventoPrivado(evento.EventoPrivado);

                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }
        //modificar
        public static Exception modificar(Datos.Evento evento)
        {
            try
            {
                context().Evento.Attach(context().Evento.Single(p => p.id == evento.id));
                context().Evento.ApplyCurrentValues(evento);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }
        //eliminar
        public static Exception eliminar(Datos.Evento evento)//falta probar, podria no funcionar correctamente
        {
            try
            {
                context().Evento.SingleOrDefault(e => e.id == evento.id).estado = 0;
                evento.estado = 0;
                context().Evento.ApplyCurrentValues(evento);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        //seleccionar todos los eventos
        public static IEnumerable<Datos.Evento> seleccionarTodo()
        {
           return context().Evento.Where(evento => evento.estado != 0 && evento.EventoCorporativo == null);
        }
        //seleccionar solo eventos corporativos
        public static IEnumerable<Datos.Evento> seleccionarEventoCoporativo()
        {
            return context().Evento.Where(evento => (evento.EventoCorporativo != null) && (evento.EventoPrivado == null) && (evento.estado != 0));
        }
        //seleccionar solo eventos normales (los cuales estamos tomando como privados)
        public static IEnumerable<Datos.Evento> seleccionarEventoPrivado()
        {
            return context().Evento.Where(evento => (evento.EventoPrivado != null) && (evento.EventoCorporativo == null) && (evento.estado != 0));
        }

        public static IEnumerable<Datos.Evento> seleccionarEventosDisponibles()
        {            
            IEnumerable<Datos.Evento> listaEventos = context().Evento.Where(evento => (evento.estado != 0)&&(evento.fechaFin<=DateTime.Now));
            System.Diagnostics.Debug.WriteLine("Eventos seleccionados: " + listaEventos.ToList().Count);
            return listaEventos;
        }
        //buscar
        public static Datos.Evento buscarId(int id)
        {
            return context().Evento.FirstOrDefault(p => p.id == id);
        }
        public static IEnumerable<Datos.Evento> buscarNombre(String nombre)
        {
            return context().Evento.Where(p => p.nombre == nombre).ToList();
        }
        public static IEnumerable<Datos.Evento> buscarEstado(short estado)
        {
            return context().Evento.Where(p => p.estado == estado).ToList();
        }
    }
}           
