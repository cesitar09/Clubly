using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class EventoPublico:Evento
    {
        public EventoPublico() { }
        
        //metodos para convertir
        public EventoPublico(Datos.EventoPublico eventoPublico)
        {
            Datos.Evento evento = eventoPublico.Evento;
            //atributos heredados de Evento
            id = evento.id;
            nombre = evento.nombre;
            descripcion = evento.descripcion;
            fechaInicio = evento.fechaInicio;
            fechaFin = evento.fechaFin;
            precioSocio = evento.precioSocio;
            precioInvitado = evento.precioInvitado;
            vacantesSocio = evento.vacantesSocio;
            vacantesInvitado = evento.vacantesInvitado;
            estado = evento.estado;
            //Atributos de Evento Publico
        }
        public static Models.EventoPublico Convertir(Datos.EventoPublico evento)
        {
            return new EventoPublico(evento);
        }
        public static IEnumerable<Models.EventoPublico> ConvertirLista(IEnumerable<Datos.EventoPublico> listaEventos)
        {
            return listaEventos.Select(evento => Convertir(evento));
        }

        //metodos para invertir
        public static Datos.Evento Invertir(Models.EventoPublico evento)
        {
            Datos.EventoPublico dataEvento = new Datos.EventoPublico();
            //Atributos
            dataEvento.id = evento.id;
            //Referencia circular
            dataEvento.Evento = Models.Evento.Invertir(evento);
            dataEvento.Evento.EventoPublico = dataEvento;
            return dataEvento.Evento;
        }
    }
}
