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
        public static EventoCorporativo ConvertirCorp(Datos.EventoCorporativo eventoCorp)
        {
            return new EventoCorporativo(eventoCorp);
        }

        public static IEnumerable<Models.EventoCorporativo> ConvertirListaCorp(IEnumerable<Datos.EventoCorporativo> listaEventos)
        {
            return listaEventos.Select(evento => new EventoCorporativo(evento));
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
            dEventoCorp.Evento = Models.Evento.InvertirEve(mEventoCorp);
            dEventoCorp.Evento.EventoCorporativo = dEventoCorp;
        
            return dEventoCorp;
        }

        public static Datos.ReservaAmbiente InvertirRes(Models.EventoCorporativo mEventoCorp)
        {
            Datos.ReservaAmbiente dreserva;

            if (mEventoCorp.id == 0)
                dreserva = new Datos.ReservaAmbiente();
            else
                dreserva = Negocio.ReservaAmbiente.buscarIdEvento(mEventoCorp.id);

            dreserva.Evento = Negocio.Evento.buscarId(mEventoCorp.id);
            dreserva.Ambiente = Negocio.Ambiente.buscarId(mEventoCorp.reserva.ambiente.id);
            dreserva.horaInicio = mEventoCorp.fechaInicio;
            dreserva.horaFin = mEventoCorp.fechaFin;
            dreserva.estado = mEventoCorp.estado;

            return dreserva;
        }
        public static Datos.Evento InvertirEv(Models.EventoCorporativo mEventoCorp)
        {
            Datos.Evento dEvento;

            if (mEventoCorp .id == 0)
                dEvento = new Datos.Evento();
            else
                dEvento = Negocio.Evento.buscarId(mEventoCorp.id);

            mEventoCorp.estado = 1;
            dEvento.nombre = mEventoCorp.nombre;
            dEvento.descripcion = mEventoCorp.descripcion;
            dEvento.fechaInicio = mEventoCorp.fechaInicio;
            dEvento.fechaFin = mEventoCorp.fechaFin;
            dEvento.precioSocio = mEventoCorp.precioSocio;
            dEvento.precioInvitado = mEventoCorp.precioInvitado;
            dEvento.vacantesInvitado = mEventoCorp.vacantesInvitado;
            dEvento.vacantesSocio = mEventoCorp.vacantesSocio;
            dEvento.estado = mEventoCorp.estado;

            dEvento.Empleado = Negocio.Empleado.buscarId(mEventoCorp.empleado.persona.id);

            Datos.EventoCorporativo eve = new Datos.EventoCorporativo();
            eve.razonSocial = mEventoCorp.razonSocial;
            eve.ruc = mEventoCorp.ruc;
            eve.direccion = mEventoCorp.direccion;
            eve.paginaWeb = mEventoCorp.paginaWeb;
            eve.numParticipantes = mEventoCorp.numParticipantes;

            Datos.ReservaAmbiente reser = new Datos.ReservaAmbiente();
            reser.Ambiente = Negocio.Ambiente.buscarId(mEventoCorp.reserva.ambiente.id);
            reser.horaInicio = mEventoCorp.fechaInicio;
            reser.horaFin = mEventoCorp.fechaFin;
            reser.estado = mEventoCorp.estado;
            
            dEvento.EventoCorporativo = eve;
            dEvento.ReservaAmbiente = reser;

            return dEvento;
        }

        public static Datos.Evento InvertirEvRestringido(Models.EventoCorporativo mEventoCorp)
        {
            Datos.Evento dEvento;

            if (mEventoCorp.id == 0)
                dEvento = new Datos.Evento();
            else
                dEvento = Negocio.Evento.buscarId(mEventoCorp.id);

            dEvento.nombre = mEventoCorp.nombre;
            dEvento.descripcion = mEventoCorp.descripcion;
            //dEvento.fechaInicio = mEventoCorp.fechaInicio;
            //dEvento.fechaFin = mEventoCorp.fechaFin;
            //dEvento.precioSocio = mEventoCorp.precioSocio;
            //dEvento.precioInvitado = mEventoCorp.precioInvitado;

            Evento ev = Evento.buscarId(mEventoCorp.id);

            //Solo se pueden aumentar las vacantes
            if (mEventoCorp.vacantesInvitado > ev.vacantesInvitado)
                dEvento.vacantesInvitado = mEventoCorp.vacantesInvitado;
            if (mEventoCorp.vacantesSocio > ev.vacantesSocio)
                dEvento.vacantesSocio = mEventoCorp.vacantesSocio;

            dEvento.estado = mEventoCorp.estado;

            //dEvento.Empleado = Negocio.Empleado.buscarId(mEventoCorp.empleado.persona.id);

            Datos.EventoCorporativo eve = new Datos.EventoCorporativo();
            //eve.razonSocial = mEventoCorp.razonSocial;
            //eve.ruc = mEventoCorp.ruc;
            eve.direccion = mEventoCorp.direccion;
            eve.paginaWeb = mEventoCorp.paginaWeb;
            //eve.numParticipantes = mEventoCorp.numParticipantes;

            Datos.ReservaAmbiente reser = new Datos.ReservaAmbiente();
            reser.Ambiente = Negocio.Ambiente.buscarId(mEventoCorp.reserva.ambiente.id);
            //reser.horaInicio = mEventoCorp.fechaInicio;
            //reser.horaFin = mEventoCorp.fechaFin;
            reser.estado = mEventoCorp.estado;

            dEvento.EventoCorporativo = eve;
            dEvento.ReservaAmbiente = reser;

            return dEvento;
        }

        public static IEnumerable<Datos.EventoCorporativo> ConvertirListaInversoCorp(IEnumerable<Models.EventoCorporativo> mEventos)
        {
            return mEventos.Select(ev => InvertirEvCorp(ev));
        }

        //INTERACCIÓN BD

        public static int insertarCorp(Models.EventoCorporativo corp)
        {
            //
            if (Negocio.Evento.insertar(InvertirEv(corp)) == null)
                    return 1;
            return 0;
        }

        public static int modificarCorp(Models.EventoCorporativo corp)
        {
            EventoCorporativo eve = EventoCorporativo.buscarIdCorp(corp.id);
            //Días máximos antes del evento para que pueda modificarse completamente
            if (CalcularDias(DateTime.Today, eve.fechaInicio) >= 14)
            {
                Datos.ReservaAmbiente res = InvertirRes(corp);
                Negocio.ReservaAmbiente.eliminar(res);//Libero temporalmente la reserva
                if (Ambiente.AmbienteReservado(corp.fechaInicio, corp.fechaFin, corp.reserva.ambiente.id) == false)
                {
                    if (Negocio.Evento.modificar(InvertirEv(corp)) == null)
                        return 1;
                }
                res.estado = 1;
                Negocio.ReservaAmbiente.modificar(res);//Volvemos al estado anterior
            }
            else
            {
                if (Negocio.Evento.modificar(InvertirEvRestringido(corp)) == null)
                    return 1;
            }
            return 0;
        }

        public static void eliminarCorp(Models.EventoCorporativo corp)
        {
            Datos.EventoCorporativo evento = Negocio.EventoCorp.buscarIdCorp(corp.id);
            Datos.ReservaAmbiente reser = Negocio.ReservaAmbiente.buscarIdEvento(corp.id);

            Negocio.ReservaAmbiente.eliminar(reser);
            Negocio.EventoCorp.eliminarCorp(evento);
        }

        public static Models.EventoCorporativo buscarIdCorp(short id)
        {
            EventoCorporativo eve = ConvertirCorp(Negocio.EventoCorp.buscarIdCorp(id));
            eve.reserva = ReservaAmbiente.Convertir(Negocio.ReservaAmbiente.buscarIdEvento(id));
            return eve;
        }
    }
}