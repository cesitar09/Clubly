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
        [DisplayName("Invitados por Socio")]
        public short numeroInvitados { get; set; }

        public EventoPrivado() { }
      
        public EventoPrivado(Datos.EventoPrivado eventoPrivado)
        {
            //atributos de Evento Privado
            numeroInvitados = eventoPrivado.numeroInvitados;

            Datos.Evento evento = eventoPrivado.Evento;

            //atributos heredados de Evento                
            id = eventoPrivado.Evento.id;
            nombre = eventoPrivado.Evento.nombre;
            descripcion = eventoPrivado.Evento.descripcion;
            fechaInicio = eventoPrivado.Evento.fechaInicio;
            fechaFin = eventoPrivado.Evento.fechaFin;
            precioSocio = eventoPrivado.Evento.precioSocio;
            precioInvitado = eventoPrivado.Evento.precioInvitado;
            vacantesSocio = eventoPrivado.Evento.vacantesSocio;
            vacantesInvitado = eventoPrivado.Evento.vacantesInvitado;
            estado = eventoPrivado.Evento.estado;
            empleado = Models.Empleado.Convertir(evento.Empleado);
        }

        //CONVERSION
        public static Models.EventoPrivado Convertir(Datos.EventoPrivado eventoPri)
        {
            return new EventoPrivado(eventoPri);
        }

        public static IEnumerable<Models.EventoPrivado> ConvertirLista(IEnumerable<Datos.EventoPrivado> listaEventos)
        {
            return listaEventos.Select(evento => Convertir(evento));
        }

        //INVERSION
        public static Datos.EventoPrivado Invertir(Models.EventoPrivado mEventoPriv)
        {
            Datos.EventoPrivado dEventoPriv;

            if (mEventoPriv.id == 0)
                dEventoPriv = new Datos.EventoPrivado();
            else
                dEventoPriv = Negocio.Evento.buscarId(mEventoPriv.id).EventoPrivado;

            //Atributos
            dEventoPriv.numeroInvitados = mEventoPriv.numeroInvitados;

            //Referencia circular
            dEventoPriv.Evento = Models.Evento.Invertir(mEventoPriv);
            dEventoPriv.Evento.EventoPrivado = dEventoPriv;

            return dEventoPriv;
        }

        public static IEnumerable<Datos.EventoPrivado> ConvertirListaInverso(IEnumerable<Models.EventoPrivado> mEventos)
        {
            return mEventos.Select(ev => Invertir(ev));
        }
    }
}
