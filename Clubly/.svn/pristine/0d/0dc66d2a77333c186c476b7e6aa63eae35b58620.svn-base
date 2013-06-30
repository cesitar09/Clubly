using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Datos;

namespace Web.Models
{
    public class ReservaAmbiente
    {
        [DisplayName("Id")]
        public short id { get; set; }

        [DisplayName("Hora Inicio")]
        public DateTime horaInicio { get; set; }

        [DisplayName("Hora Fin")]
        public DateTime horaFin { get; set; }

        [DisplayName("Estado")]
        public short estado { get; set; }

        [DisplayName("Evento")]
        public Evento evento { get; set; }

        [DisplayName("Ambiente")]
        public Ambiente ambiente { get; set; }

        [DisplayName("Actividad")]
        public Actividad actividad { get; set; }

        public ReservaAmbiente() { }

        public ReservaAmbiente(Datos.ReservaAmbiente reserv) 
        {
            id = reserv.id;
            horaFin = reserv.horaFin;
            horaInicio = reserv.horaInicio;
            estado = reserv.estado;
            ambiente = Ambiente.Convertir(reserv.Ambiente);
            if(reserv.Evento != null)
                evento = Evento.Convertir(reserv.Evento);
            else
                actividad = Actividad.Convertir(reserv.Actividad);
        }

        public static ReservaAmbiente Convertir(Datos.ReservaAmbiente reserv){

            return new ReservaAmbiente(reserv);
        }

        public static IEnumerable<ReservaAmbiente> ConvertirLista(IEnumerable<Datos.ReservaAmbiente> listaReserv) {

            return listaReserv.Select(reserv => Convertir(reserv));
        }

        public static Datos.ReservaAmbiente Invertir(Models.ReservaAmbiente mReserv)
        {
            Datos.ReservaAmbiente dReserv;
            if (mReserv.id == 0)
                dReserv = new Datos.ReservaAmbiente();
            else
                dReserv = Negocio.ReservaAmbiente.buscarId(mReserv.id);

            dReserv.horaFin = mReserv.horaFin;
            dReserv.horaInicio = mReserv.horaInicio;
            dReserv.estado = mReserv.estado;
            dReserv.Ambiente = Negocio.Ambiente.buscarId(mReserv.ambiente.id);
            return dReserv;
        }

        public static IEnumerable<Datos.ReservaAmbiente> ConvertirListaInverso(IEnumerable<Models.ReservaAmbiente> mReserv)
        {
            return mReserv.Select(r => Invertir(r));
        }

        public static IEnumerable<ReservaAmbiente> SeleccionarTodoEvento()
        {
            IEnumerable<Datos.ReservaAmbiente> reserv = Negocio.ReservaAmbiente.seleccionarTodoEvento();
            return ConvertirLista(reserv);
        }

        public static IEnumerable<ReservaAmbiente> SeleccionarTodoActividad()
        {
            IEnumerable<Datos.ReservaAmbiente> reserv = Negocio.ReservaAmbiente.seleccionarTodaActividad();
            return ConvertirLista(reserv);
        }

        public static Models.ReservaAmbiente SeleccionarporId(short id)
        {
            return Convertir(Negocio.ReservaAmbiente.buscarId(id));
        }

        public static void modificarAmbiente(Models.ReservaAmbiente reserv)
        {
            Negocio.ReservaAmbiente.modificar(Invertir(reserv));
        }

        public static void insertarReservaAmbiente(Models.ReservaAmbiente reserv)
        {
            Negocio.ReservaAmbiente.insertar(Invertir(reserv));
        }

        public static void eliminarReservaAmbiente(Models.ReservaAmbiente reserv)
        {
            Negocio.ReservaAmbiente.eliminar(Invertir(reserv));
        }
    }
}