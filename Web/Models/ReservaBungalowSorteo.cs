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
           // familia = 

        }

    }
}