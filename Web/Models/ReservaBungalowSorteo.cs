using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using Negocio.Util;
namespace Web.Models
{
    public class ReservaBungalowSorteo
    {
        public short id {set;get;}
        public TipoBungalow tipobungalow { set; get; }
        public Familia familia { set; get; }
        public Pago pago { set; get; }
        public Sorteo sorteo { set; get; }
        public Sede sede { get; set; }
        public int numGanadores { set; get; }
        public String Comentarios { set; get; }
        public DateTime Fechainicio { set; get; }
        public DateTime FechaFin { set; get; }
        public String resultadoSorteo { set; get; }
        public static ListaEstados listaEstados;

        public ReservaBungalowSorteo()
        
        {
            listaEstados = new ListaEstados();
            listaEstados.AgregarEstado(1, "Pendiente");
            listaEstados.AgregarEstado(2, "Ganador");
            listaEstados.AgregarEstado(3, "Perdedor");
            
        }

        public ReservaBungalowSorteo(Datos.ReservaBungalowSorteo reservaBungalowSorteo)
        {
            id = reservaBungalowSorteo.id;
            tipobungalow = TipoBungalow.Convertir(reservaBungalowSorteo.TipoBungalow);
            familia = Familia.Convertir(reservaBungalowSorteo.Familia);
            pago = Pago.Convertir(reservaBungalowSorteo.Pago);
            //sorteo = Sorteo.Convertir(reservaBungalowSorteo.)
            //numGanadores = reservaBungalowSorteo.resultadoSorteo
            Fechainicio = reservaBungalowSorteo.fechaInicio;
            FechaFin = reservaBungalowSorteo.fechaFin;
            sede = Sede.Convertir(reservaBungalowSorteo.Sede);
        }

        public static Datos.ReservaBungalowSorteo Invertir(ReservaBungalowSorteo reservaBungSorteo)
        {
            Datos.ReservaBungalowSorteo rbs = new Datos.ReservaBungalowSorteo();
            rbs.Familia = Familia.Invertir(reservaBungSorteo.familia);
            rbs.fechaFin = reservaBungSorteo.FechaFin;
            rbs.fechaInicio = reservaBungSorteo.Fechainicio;
            //rbs.idSorteo = reservaBungSorteo.sorteo
            rbs.Pago = Pago.Invertir(reservaBungSorteo.pago);
            //rbs.resultadoSorteo = reservaBungSorteo.sorteo.
            rbs.Sede = Sede.Invertir(reservaBungSorteo.sede);
            rbs.TipoBungalow = TipoBungalow.Invertir(reservaBungSorteo.tipobungalow);
            return rbs;
        }

        public static void insertar(ReservaBungalowSorteo rbs)
        {
            Negocio.ReservaBungalowSorteo.insertar(Invertir(rbs));
        }

        public static void modificar(ReservaBungalowSorteo rbs)
        {
            Negocio.ReservaBungalowSorteo.modificar(Invertir(rbs));
        }

    }
}