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
        
        public EventoPublico(Datos.EventoPublico eventoPublico)
        {


            Datos.Evento evento = eventoPublico.Evento;

            //atributos heredados de Evento                
            id = eventoPublico.Evento.id;
            nombre = eventoPublico.Evento.nombre;
            descripcion = eventoPublico.Evento.descripcion;
            fechaInicio = eventoPublico.Evento.fechaInicio;
            fechaFin = eventoPublico.Evento.fechaFin;
            precioSocio = eventoPublico.Evento.precioSocio;
            precioInvitado = eventoPublico.Evento.precioInvitado;
            vacantesSocio = eventoPublico.Evento.vacantesSocio;
            vacantesInvitado = eventoPublico.Evento.vacantesInvitado;
            estado = eventoPublico.Evento.estado;

        }

        //CONVERSION
        public static Models.EventoPublico Convertir(Datos.EventoPublico evento)
        {
            return new EventoPublico(evento);
        }

        public static IEnumerable<Models.EventoPublico> ConvertirLista(IEnumerable<Datos.EventoPublico> listaEventos)
        {
            return listaEventos.Select(evento => Convertir(evento));
        }

        //INVERSION
        public static Datos.EventoPublico Invertir(Models.EventoPublico mEventoPub)
        {
            Datos.EventoPublico dEventoPub;

            if (mEventoPub.id == 0)
                dEventoPub = new Datos.EventoPublico();
            else
                dEventoPub = Negocio.Evento.buscarId(mEventoPub.id).EventoPublico;

            //Atributos

            //Referencia circular
            dEventoPub.Evento = Models.Evento.Invertir(mEventoPub);
            dEventoPub.Evento.EventoPublico = dEventoPub;

            return dEventoPub;
        }

        public static IEnumerable<Datos.EventoPublico> ConvertirListaInverso(IEnumerable<Models.EventoPublico> mEventos)
        {
            return mEventos.Select(ev => Invertir(ev));
        }
    }
}
