using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Negocio.Util;

namespace Web.Models
{
    public class ReservaBungalow
    {
        [Display(Name = "Id")]
        public short id { get; set; }

        [Required]
        [Display(Name = "Fecha Inicio")]
        public DateTime fechaInicio { get; set; }

        [Required]
        [Display(Name = "Fecha Fin")]
        public DateTime fechaFin { get; set; }

        [Display(Name = "Estado")]
        public String estado { get; set; }
        
        [Display(Name = "Familia")]
        public Familia familia { get; set; }

        //[Display(Name = "Bungalow")]
        //public BungalowXReservaBungalow bungalowXReservaBungalow { get; set; }
        private static ListaEstados listaEstados =null;        
        public static ListaEstados ListaEstados()
        {
            if (listaEstados == null)
            {
                listaEstados = new ListaEstados();
                listaEstados.AgregarEstado(Negocio.ReservaBungalow.PORPAGAR, "Por Pagar");
                listaEstados.AgregarEstado(Negocio.ReservaBungalow.NOINGRESADO, "No Ingresado");
                listaEstados.AgregarEstado(Negocio.ReservaBungalow.INGRESADO, "Ingresado");
                listaEstados.AgregarEstado(Negocio.ReservaBungalow.TERMINADO, "Terminado");
            }
            return listaEstados;
        }
        //public ReservaBungalow()
        //{            
        //}

        //Metodos para convertir
        public static ReservaBungalow Convertir(Datos.ReservaBungalow reserva)
        {
            ReservaBungalow nuevo = new ReservaBungalow();

            nuevo.id = reserva.id;
            nuevo.fechaInicio = reserva.fechaInicio;
            nuevo.fechaFin = reserva.fechaFin;
            nuevo.estado = ListaEstados().TextoEstado(reserva.estado);
            nuevo.familia = Familia.Convertir(reserva.Familia);

            return nuevo;
        }
        public static IEnumerable<ReservaBungalow> ConvertirLista(IEnumerable<Datos.ReservaBungalow> listaReservas)
        {
            return listaReservas.Select(reserva => Convertir(reserva));
        }

        //Metodos para invertir
        public static Datos.ReservaBungalow Invertir(Models.ReservaBungalow modelReserva)
        {
            Datos.ReservaBungalow reserva = new Datos.ReservaBungalow();
            reserva.id = modelReserva.id;
            reserva.fechaFin = modelReserva.fechaFin;
            reserva.fechaInicio = modelReserva.fechaInicio;
            reserva.estado = ListaEstados().EstadoTexto(modelReserva.estado);
            reserva.Familia = Negocio.Familia.buscarId(modelReserva.familia.id);
            return reserva;
        }

        //Metodos de la BD

        // Selecciona reservas con estado "Ingresado" o "No Ingresado" para mostrarlos en la vista "Registrar Ingreso"
        public static IEnumerable<Models.ReservaBungalow> SeleccionarIngreso()  
        {
            return ConvertirLista(Negocio.ReservaBungalow.SeleccionarIngreso());
        }

        public static void RegistrarIngresoBungalow(short? id)
        {
            Negocio.ReservaBungalow.RegistrarIngresoBungalow(id);
        }

        public static void RegistrarSalidaBungalow(short? id)
        {
            Negocio.ReservaBungalow.RegistrarSalidaBungalow(id);
        }
    }
}