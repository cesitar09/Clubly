using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Web.Models
{
    public class Evento
    {

        //ATRIBUTOS
        [Display(Name = "Id")]
        public short id { get; set; }

        [Display(Name = "*Nombre")]
        [Required(ErrorMessage = "Debe ingresar un nombre")]
        [StringLength(50)]
        public string nombre { get; set; }

        [Display(Name = "*Descripción")]
        [Required(ErrorMessage = "Debe ingresar una descripción")]
        [StringLength(200)]
        public string descripcion { get; set; }

        [Display(Name = "*Fecha de Inicio")]
        [Required(ErrorMessage = "Debe seleccionar una fecha")]
        public DateTime fechaInicio { get; set; }

        [Display(Name = "*Fecha de Fin")]
        [Required(ErrorMessage = "Debe seleccionar una fecha")]
        public DateTime fechaFin { get; set; }

        [Display(Name = "*Precio Socio")]
        [Required(ErrorMessage = "Debe ingresar un precio")]
        public double precioSocio { get; set; }

        [Display(Name = "*Precio Invitado")]
        public double? precioInvitado { get; set; }

        [Display(Name = "*Vacantes-Socios")]
        public short vacantesSocio { get; set; }

        [Display(Name = "*Vacantes-Invitados")]
        public short vacantesInvitado { get; set; }

        [Display(Name = "Estado")]
        public short estado { get; set; }

        [Display(Name = "*Empleado")]
        public Empleado empleado { get; set; }

        public ReservaAmbiente reserva { get; set; }

        //CONSTRUCTORES
        public Evento() { }
        
        public Evento(Datos.Evento evento)
        {
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
            empleado = Empleado.Convertir(evento.Empleado);
        }

        //CONVERSIONES
        public static Evento Convertir(Datos.Evento evento)
        {

            if (evento.EventoCorporativo != null)
                return Models.EventoCorporativo.ConvertirCorp(evento.EventoCorporativo);
            else if (evento.EventoPrivado != null)
                return Models.EventoPrivado.Convertir(evento.EventoPrivado);
            else
                return new Evento(evento);
        }

        public static IEnumerable<Models.Evento> ConvertirLista(IEnumerable<Datos.Evento> listaEventos)
        {
            return listaEventos.Select(evento => new Models.Evento(evento));
        }


        //INVERSIONES
        public static Datos.Evento Invertir(Models.Evento mEvento)
        {
            Datos.Evento dEvento;

            if (mEvento.id == 0)
                dEvento = new Datos.Evento();
            else
                dEvento = Negocio.Evento.buscarId(mEvento.id);

            dEvento.nombre = mEvento.nombre;
            dEvento.descripcion = mEvento.descripcion;
            dEvento.fechaInicio = mEvento.fechaInicio;
            dEvento.fechaFin = mEvento.fechaFin;
            dEvento.precioSocio = mEvento.precioSocio;
            dEvento.precioInvitado = mEvento.precioInvitado;
            dEvento.vacantesInvitado = mEvento.vacantesInvitado;
            dEvento.vacantesSocio = mEvento.vacantesSocio;
            dEvento.estado = mEvento.estado;

            dEvento.Empleado = Negocio.Empleado.buscarId(mEvento.empleado.persona.id);

            return dEvento;
        }

        public static Datos.ReservaAmbiente InvertirRes(Models.Evento mEvento)
        {
            Datos.ReservaAmbiente dreserva;

            if (mEvento.id == 0)
                dreserva = new Datos.ReservaAmbiente();
            else
                dreserva = Negocio.ReservaAmbiente.buscarId(mEvento.reserva.id);

            dreserva.Evento = Invertir(mEvento);
            dreserva.Ambiente = Negocio.Ambiente.buscarId(mEvento.reserva.ambiente.id);
            dreserva.horaInicio = mEvento.fechaInicio;
            dreserva.horaFin = mEvento.fechaFin;
            dreserva.estado = mEvento.estado;

            return dreserva;
        }
        public static IEnumerable<Datos.Evento> ConvertirListaInverso(IEnumerable<Models.Evento> mEventos)
        {
            return mEventos.Select(ev => Invertir(ev));
        }

        //METODOS CON LA BD

        public static int insertarEventREs(Models.Evento evento)
        {
            if (Negocio.ReservaAmbiente.insertar(InvertirRes(evento)) == null)
                return 1;
            else
                return 0;
        }

        public static int modificar(Models.Evento evento)
        {
            if (Negocio.Evento.modificar(Invertir(evento)) == null)
                return 1;
            else
                return 0;
        }

        public static void eliminar(Models.Evento evento)
        {
            Negocio.Evento.eliminar(Invertir(evento));
        }

        public static Models.Evento buscarId(short id)
        {
            return Convertir(Negocio.Evento.buscarId(id));
        }

        public static IEnumerable<Models.Evento> seleccionarTodo()
        {
            return ConvertirLista(Negocio.Evento.seleccionarTodo());
        }

        //LISTAS DE EVENTOS **************************************************
        public static IEnumerable<Evento> seleccionarEventosDisponibles()
        {
            return ConvertirLista(Negocio.Evento.seleccionarEventosDisponibles());
        }
        public static IEnumerable<Evento> seleccionarEventosNoCorp()
        {
            return ConvertirLista(Negocio.Evento.seleccionarEventoPrivado());
        }
        public static IEnumerable<Evento> seleccionarEventosCorp()
        {
            return ConvertirLista(Negocio.Evento.seleccionarEventoCoporativo());
        }
        //metodos de prueba
        public static void modificarPrueba()
        {
            Negocio.Evento.context().Evento.Single(evento => evento.id == 1).nombre="Nuevo Nombre";
            Negocio.Evento.context().SaveChanges();
        }
    }
}