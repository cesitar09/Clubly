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

        //atributos
        [Display(Name = "Id")]
        public short id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar un nombre")]
        [StringLength(50)]
        public string nombre { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Debe ingresar una descripción")]
        [StringLength(150)]
        public string descripcion { get; set; }

        [Display(Name = "Fecha de Inicio")]
        [Required(ErrorMessage = "Debe seleccionar una fecha")]
        public DateTime fechaInicio { get; set; }

        [Display(Name = "Fecha de Fin")]
        [Required(ErrorMessage = "Debe seleccionar una fecha")]
        public DateTime fechaFin { get; set; }

        [Display(Name = "Precio Socio")]
        [Required(ErrorMessage = "Debe ingresar un precio")]
        public double precioSocio { get; set; }

        [Display(Name = "Precio Invitado")]
        public double? precioInvitado { get; set; }

        [Display(Name = "Vacantes para Socios")]
        [Required(ErrorMessage = "Debe ingresar una cantidad de vacantes")]
        public short vacantesSocio { get; set; }

        [Display(Name = "Vacantes para Invitados")]
        [Required(ErrorMessage = "Debe ingresar una cantidad de vacantes")]
        public short vacantesInvitado { get; set; }

        [Display(Name = "Estado")]
        public short estado { get; set; }

        [Display(Name = "Empleado")]
        [Required(ErrorMessage = "Debe seleccionar un empleado")]
        public Models.Empleado Empleado { get; set; }

        public Evento() { }

        //metodos para convertir (no implementados)
        public static Models.Evento Convertir(Datos.Evento evento)
        {
            if (evento.EventoCorporativo != null)
                return Models.EventoCorporativo.Convertir(evento.EventoCorporativo);
            else if (evento.EventoPrivado != null)
                return Models.EventoPrivado.Convertir(evento.EventoPrivado);
            else if (evento.EventoPublico != null)
                return Models.EventoPublico.Convertir(evento.EventoPublico);
            return null;
        }
        public static IEnumerable<Models.Evento> ConvertirLista(IEnumerable<Datos.Evento> listaEventos)
        {
            return listaEventos.Select(evento => Models.Evento.Convertir(evento));
        }

        //metodos para invertir
        public static Datos.Evento Invertir(Models.Evento evento)
        {
            Datos.Evento dataEvento = new Datos.Evento();
            dataEvento.id = evento.id;
            dataEvento.nombre = evento.nombre;
            dataEvento.descripcion = evento.descripcion;
            dataEvento.fechaInicio = evento.fechaInicio;
            dataEvento.fechaFin = evento.fechaFin;
            dataEvento.precioSocio = evento.precioSocio;
            dataEvento.precioInvitado = evento.precioInvitado;
            dataEvento.estado = evento.estado;
            dataEvento.Empleado = Models.Empleado.Invertir(evento.Empleado);
            return dataEvento;
        }
        public static EntityCollection<Datos.Evento> InvertirLista(IEnumerable<Models.Evento> eventos)
        {
            EntityCollection<Datos.Evento> a = new EntityCollection<Datos.Evento>();
            foreach (var eve in eventos)
            {
                a.Add(Invertir(eve));
            }
            return a;
        } 

        //metodos para interactuar con la bd
        public static IEnumerable<Models.Evento> seleccionarTodo()
        {
            return ConvertirLista(Negocio.Evento.seleccionarTodo());
        }
        public static void insertar(Models.Evento evento)
        {
            Negocio.Evento.insertar(Invertir(evento));
        }
        public static void modificar(Models.Evento evento)
        {
            Negocio.Evento.modificar(Invertir(evento));
        }
        public static void eliminar(Models.Evento evento)
        {
            Negocio.Evento.eliminar(Invertir(evento));
        }
        public static IEnumerable<Evento> seleccionarEventosDisponibles()
        {
            return ConvertirLista(Negocio.Evento.seleccionarEventosDisponibles());
        }

        //metodos de prueba
        public static void modificarPrueba()
        {
            Negocio.Evento.context().Evento.Single(evento => evento.id == 1).nombre="Nuevo Nombre";
            Negocio.Evento.context().SaveChanges();
        }

        
    }
}