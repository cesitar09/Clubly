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

        [Display(Name = "Razon Social")]
        [Required(ErrorMessage="Debe ingresar una razon social")]
        [StringLength(50)]
        public string razonSocial { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "Debe ingresar una dirección")]
        [StringLength(150)]
        public string direccion { get; set; }

        [Display(Name = "RUC")]
        [Required(ErrorMessage = "Debe ingresar un RUC")]
        public string ruc { get; set; }

        [Display(Name = "Página Web")]
        [Required(ErrorMessage = "Debe ingresar una página web")]
        [StringLength(50)]
        public string paginaWeb { get; set; }

        [Display(Name = "Número de Participantes")]
        [Required(ErrorMessage = "Debe ingresar un número de participantes")]
        public short numParticipantes { get; set; }

        public EventoCorporativo() { }
        
        //metodos para convertir
        public EventoCorporativo(Datos.EventoCorporativo eventoCorp)
        {
            Datos.Evento evento = eventoCorp.Evento;
            //Atributos de Evento
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
            Empleado = Models.Empleado.Convertir(evento.Empleado);
            ruc = eventoCorp.ruc;
            razonSocial = eventoCorp.razonSocial;
            direccion = eventoCorp.direccion;
            paginaWeb = eventoCorp.paginaWeb;
            //atributos de Evento Corporativo
            numParticipantes = eventoCorp.numParticipantes;
        }
        public static Models.EventoCorporativo Convertir(Datos.EventoCorporativo evento)
        {
            return new EventoCorporativo(evento);
        }
        public static IEnumerable<Models.EventoCorporativo> ConvertirLista(IEnumerable<Datos.EventoCorporativo> listaEventos)
        {
            return listaEventos.Select(evento => Convertir(evento));
        }

        //metodos para invertir
        public static Datos.Evento Invertir(Models.EventoCorporativo eventoCorp)
        {
            Datos.EventoCorporativo dataEvento = new Datos.EventoCorporativo();
            dataEvento.id = eventoCorp.id;
            dataEvento.ruc = eventoCorp.ruc;
            dataEvento.razonSocial = eventoCorp.razonSocial;
            dataEvento.direccion = eventoCorp.direccion;
            dataEvento.paginaWeb = eventoCorp.paginaWeb;
            dataEvento.numParticipantes = eventoCorp.numParticipantes;
            dataEvento.Evento = Models.Evento.Invertir(eventoCorp);
            dataEvento.Evento.EventoCorporativo = dataEvento;
            return dataEvento.Evento;
        }
    }
}