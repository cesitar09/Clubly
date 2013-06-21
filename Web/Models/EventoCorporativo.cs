using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class EventoCorporativo:Evento
    {

        [Display(Name = "*Razon Social")]
        public string razonSocial { get; set; }

        [Display(Name = "Dirección")]
        public string direccion { get; set; }

        [Display(Name = "*RUC")]
        public string ruc { get; set; }

        [Display(Name = "Página Web")]
        public string paginaWeb { get; set; }

        [Display(Name = "*Núm. de Participantes")]
        public short numParticipantes { get; set; }

        //Para enlazar con reserva de ambiente
        [Display(Name = "*Ambiente")]
        public ReservaAmbiente reserva { get; set; }

        public EventoCorporativo() { }
        
        public EventoCorporativo(Datos.EventoCorporativo eventoCorp)
        {
            //atributos de Evento Corporativo
            ruc = eventoCorp.ruc;
            razonSocial = eventoCorp.razonSocial;
            direccion = eventoCorp.direccion;
            paginaWeb = eventoCorp.paginaWeb;
            numParticipantes = eventoCorp.numParticipantes;

            //atributos heredados de Evento 
            id = eventoCorp.Evento.id;
            nombre = eventoCorp.Evento.nombre;
            descripcion = eventoCorp.Evento.descripcion;
            fechaInicio = eventoCorp.Evento.fechaInicio;
            fechaFin = eventoCorp.Evento.fechaFin;
            precioSocio = eventoCorp.Evento.precioSocio;
            precioInvitado = eventoCorp.Evento.precioInvitado;
            vacantesSocio = eventoCorp.Evento.vacantesSocio;
            vacantesInvitado = eventoCorp.Evento.vacantesInvitado;
            estado = eventoCorp.Evento.estado;
            empleado = Models.Empleado.Convertir(eventoCorp.Evento.Empleado);
        }

        //CONVERSION
        public static Models.EventoCorporativo ConvertirCorp(Datos.EventoCorporativo eventoCorp)
        {
            return new EventoCorporativo(eventoCorp);
        }

        public static IEnumerable<Models.EventoCorporativo> ConvertirListaCorp(IEnumerable<Datos.EventoCorporativo> listaEventos)
        {
            return listaEventos.Select(evento => ConvertirCorp(evento));
        }

        //INVERSION
        public static Datos.EventoCorporativo InvertirEvCorp(Models.EventoCorporativo mEventoCorp)
        {
            Datos.EventoCorporativo dEventoCorp;

            if (mEventoCorp.id == 0)
                dEventoCorp = new Datos.EventoCorporativo();
            else
                dEventoCorp = Negocio.EventoCorp.buscarIdCorp(mEventoCorp.id);

            //Atributos
            dEventoCorp.ruc = mEventoCorp.ruc;
            dEventoCorp.razonSocial = mEventoCorp.razonSocial;
            dEventoCorp.direccion = mEventoCorp.direccion;
            dEventoCorp.paginaWeb = mEventoCorp.paginaWeb;
            dEventoCorp.numParticipantes = mEventoCorp.numParticipantes;

            //Referencia circular
            dEventoCorp.Evento = Models.Evento.Invertir(mEventoCorp);
            dEventoCorp.Evento.EventoCorporativo = dEventoCorp;
        
            return dEventoCorp;
        }

        public static Datos.ReservaAmbiente InvertirRes(Models.EventoCorporativo  mEventoCorp)
        {
            Datos.ReservaAmbiente dreserva;

            if (mEventoCorp.id == 0)
                dreserva = new Datos.ReservaAmbiente();
            else
                dreserva = Negocio.ReservaAmbiente.buscarId(mEventoCorp.reserva.id);

            dreserva.Evento = Invertir(mEventoCorp);
            dreserva.Ambiente = Negocio.Ambiente.buscarId(mEventoCorp.reserva.ambiente.id);
            dreserva.horaInicio = mEventoCorp.fechaInicio;
            dreserva.horaFin = mEventoCorp.fechaFin;
            dreserva.estado = mEventoCorp.estado;

            return dreserva;
        }

        public static IEnumerable<Datos.EventoCorporativo> ConvertirListaInversoCorp(IEnumerable<Models.EventoCorporativo> mEventos)
        {
            return mEventos.Select(ev => InvertirEvCorp(ev));
        }

        public static Models.EventoCorporativo buscarIdCorp(short id)
        {
            return ConvertirCorp(Negocio.EventoCorp.buscarIdCorp(id));
        }

        //INTERACCIÓN BD

        public static int insertarCorpRes(Models.EventoCorporativo corp)
        {
            if (Negocio.ReservaAmbiente.insertar(InvertirRes(corp)) == null)
                return 1;
            else
                return 0;
        }

        public static int modificarCorpRes(Models.EventoCorporativo corp)
        {
            if (Negocio.EventoCorp.modificarCorp(InvertirEvCorp(corp)) == null)
                if (Negocio.ReservaAmbiente.modificar(InvertirRes(corp)) == null)
                    return 1;
            return 0;
        }

   

        public static void eliminarCorp(Models.EventoCorporativo corp)
        {
            // Negocio.Actividad.Eliminar(Invertir(act));
            Datos.EventoCorporativo evento = Negocio.EventoCorp.buscarIdCorp(corp.id);
            Datos.ReservaAmbiente reser = Negocio.ReservaAmbiente.buscarIdActividad(corp.id);

            Negocio.ReservaAmbiente.eliminar(reser);
            Negocio.EventoCorp.eliminarCorp(evento);
        }
    }
}