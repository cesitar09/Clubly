using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class EventoPrivado:Evento
    {

        [Display(Name = "Número de Invitados")]
        [Required(ErrorMessage = "Debe ingresar un número de Invitados")]
        public short numeroInvitados { get; set; }

        public EventoPrivado() { }

        //metodos para convertir
        public EventoPrivado(Datos.EventoPrivado eventoPrivado)
        {
            Datos.Evento evento = eventoPrivado.Evento;
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
            //atributos de Evento Privado
            numeroInvitados = eventoPrivado.numeroInvitados;
        }
        public static Models.EventoPrivado Convertir(Datos.EventoPrivado evento)
        {
            return new EventoPrivado(evento);
        }
        public static IEnumerable<Models.EventoPrivado> ConvertirLista(IEnumerable<Datos.EventoPrivado> listaEventos)
        {
            return listaEventos.Select(evento => Convertir(evento));
        }

        //metodos para invertir
        public static Datos.Evento Invertir(Models.EventoPrivado eventoPriv)
        {
            Datos.EventoPrivado dataEvento = new Datos.EventoPrivado();
            //Atributos
            dataEvento.id = eventoPriv.id;
            dataEvento.numeroInvitados = eventoPriv.numeroInvitados;
            //Referencia circular
            dataEvento.Evento = Models.Evento.Invertir(eventoPriv);
            dataEvento.Evento.EventoPrivado = dataEvento;
            return dataEvento.Evento;
        }
    }
}
