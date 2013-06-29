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

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Debe ingresar una descripción")]
        [StringLength(200)]
        public string descripcion { get; set; }

        [Display(Name = "*Fecha Inicio")]
        [Required(ErrorMessage = "Debe seleccionar una fecha")]
        public DateTime fechaInicio { get; set; }

        [Display(Name = "*Fecha Fin")]
        [Required(ErrorMessage = "Debe seleccionar una fecha")]
        public DateTime fechaFin { get; set; }

        [Display(Name = "*Precio\nSocio")]
        [Required(ErrorMessage = "Debe ingresar un precio")]
        public double? precioSocio { get; set; }

        [Display(Name = "*Precio\nInvitado")]
        public double? precioInvitado { get; set; }

        [Display(Name = "*Vacantes")]
        public short? vacantesSocio { get; set; }

        [Display(Name = "Invitados\npor\nSocio")]
        public short? vacantesInvitado { get; set; }

        [Display(Name = "Estado")]
        public short estado { get; set; }

        [Display(Name = "*Empleado")]
        public Empleado empleado { get; set; }

        [Display(Name = "*Ambiente")]
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
            else
                return new Evento(evento);
        }

        public static IEnumerable<Models.Evento> ConvertirLista(IEnumerable<Datos.Evento> listaEventos)
        {
            return listaEventos.Select(evento => new Models.Evento(evento));
        }


        //INVERSIONES
        public static Datos.Evento InvertirEve(Models.Evento mEvento)
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
            dEvento.estado = 1;

            dEvento.Empleado = Negocio.Empleado.buscarId(mEvento.empleado.persona.id);

            return dEvento;
        }
        //invertir con restricciones
        public static Datos.Evento InvertirEveRestringido(Models.Evento mEvento)
        {
            Datos.Evento dEvento;

            if (mEvento.id == 0)
                dEvento = new Datos.Evento();
            else
                dEvento = Negocio.Evento.buscarId(mEvento.id);

            dEvento.nombre = mEvento.nombre;
            dEvento.descripcion = mEvento.descripcion;
            //dEvento.fechaInicio = mEvento.fechaInicio;
            //dEvento.fechaFin = mEvento.fechaFin;
            //dEvento.precioSocio = mEvento.precioSocio;
            //dEvento.precioInvitado = mEvento.precioInvitado;
            Evento eve = Evento.buscarId(mEvento.id);

            //Solo se pueden aumentar las vacantes
            if (mEvento.vacantesInvitado > eve.vacantesInvitado)
                dEvento.vacantesInvitado = mEvento.vacantesInvitado;
            if (mEvento.vacantesSocio > eve.vacantesSocio)
                dEvento.vacantesSocio = mEvento.vacantesSocio;

            dEvento.estado = mEvento.estado;

            dEvento.Empleado = Negocio.Empleado.buscarId(mEvento.empleado.persona.id);

            return dEvento;
        }

        public static Datos.ReservaAmbiente InvertirRes(Models.Evento mEvento)
        {
            Datos.ReservaAmbiente dreserva;

            if (mEvento.id == 0)
            {
                dreserva = new Datos.ReservaAmbiente();
            }
            else
                dreserva = Negocio.ReservaAmbiente.buscarIdEvento(mEvento.id);

            dreserva.Evento = InvertirEve(mEvento);
            dreserva.Ambiente = Negocio.Ambiente.buscarId(mEvento.reserva.ambiente.id);            
            dreserva.horaInicio = mEvento.fechaInicio;
            dreserva.horaFin = mEvento.fechaFin;
            dreserva.estado = 1;

            return dreserva;
        }

        public static Datos.ReservaAmbiente InvertirResRestringido(Models.Evento mEvento)
        {
            Datos.ReservaAmbiente dreserva;

            if (mEvento.id == 0)
            {
                dreserva = new Datos.ReservaAmbiente();
            }
            else
                dreserva = Negocio.ReservaAmbiente.buscarIdEvento(mEvento.id);

            dreserva.Evento = InvertirEveRestringido(mEvento);
            dreserva.Ambiente = Negocio.Ambiente.buscarId(mEvento.reserva.ambiente.id);
            //dreserva.horaInicio = mEvento.fechaInicio;
            //dreserva.horaFin = mEvento.fechaFin;
            dreserva.estado = mEvento.estado;

            return dreserva;
        }

        public static IEnumerable<Datos.Evento> ConvertirListaInverso(IEnumerable<Models.Evento> mEventos)
        {
            return mEventos.Select(ev => InvertirEve(ev));
        }

        //METODOS CON LA BD

        public static int insertarEventRes(Models.Evento evento)
        {
            if (Negocio.ReservaAmbiente.insertar(InvertirRes(evento)) == null)
                return 1;
            return 0;
        }

        public static int CalcularDias(DateTime oldDate, DateTime newDate)
        {
            // Diferencia de fechas
            TimeSpan ts = newDate - oldDate;

            // Diferencia de días
            return ts.Days;
        }
        public static int modificarEventRes(Models.Evento evento)
        {
            Evento eve = Evento.buscarId(evento.id);
            //Días máximos antes del evento para que pueda modificarse completamente
            if (CalcularDias(DateTime.Today, eve.fechaInicio) >= 14)
            {
                Datos.ReservaAmbiente res = InvertirRes(evento);
                Negocio.ReservaAmbiente.eliminar(res);//Libero temporalmente la reserva
                if (Ambiente.AmbienteReservado(evento.fechaInicio, evento.fechaFin, evento.reserva.ambiente.id) == false)
                {
                    if (Negocio.Evento.modificar(InvertirEve(evento)) == null)
                        if (Negocio.ReservaAmbiente.modificar(InvertirRes(evento)) == null)
                            return 1;
                }
                res.estado = 1;
                Negocio.ReservaAmbiente.modificar(res);//Volvemos al estado anterior
            }
            else 
            {
                if (Negocio.Evento.modificar(InvertirEveRestringido(evento)) == null)
                    if (Negocio.ReservaAmbiente.modificar(InvertirResRestringido(evento)) == null)
                        return 1;
            }
            return 0;
        }

        public static int modificar(Models.Evento evento)
        {
            if (Negocio.Evento.modificar(InvertirEve(evento)) == null)
                return 1;
            else
                return 0;
        }

        public static void eliminar(Models.Evento evento)
        {
            Datos.Evento eve = Negocio.Evento.buscarId(evento.id);
            Datos.ReservaAmbiente reser = Negocio.ReservaAmbiente.buscarIdEvento(evento.id);

            Negocio.ReservaAmbiente.eliminar(reser);
            Negocio.Evento.eliminar(eve);
        }

        public static Models.Evento buscarId(short id)
        {
            Evento eve = Convertir(Negocio.Evento.buscarId(id));
            eve.reserva = ReservaAmbiente.Convertir(Negocio.ReservaAmbiente.buscarIdEvento(id));
            return eve;
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