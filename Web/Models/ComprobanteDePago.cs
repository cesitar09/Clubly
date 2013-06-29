using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Negocio.Util;
using System.Data.Objects.DataClasses;
namespace Web.Models
{
    public class ComprobanteDePago
    {
        [DisplayName("N° de Comprobante")]
        public short id { get; set; }

        [DisplayName("Fecha de Registro")]
        public DateTime fecha { get; set; }

        [DisplayName("Monto Total (S/.) ")]
        public double monto { get; set; }

        [DisplayName("Descuento (S/.) ")]
        public double descuento { get; set; }

        [DisplayName("Monto A Pagar (S/.) ")]
        public double montoAPagar { get; set; }

        [DisplayName("Estado")]
        public short estado { get; set; }

        public IEnumerable<Pago> listaPagos;

        //[DisplayName("Id de familia")]
        //public short idfamilia { get; set; }

        public ComprobanteDePago()
        {
        }

        public ComprobanteDePago(Datos.ComprobanteDePago comprobanteDePago)
        {
            id = comprobanteDePago.id;
            fecha = comprobanteDePago.fecha;
            monto = comprobanteDePago.montoTotal;
            estado = comprobanteDePago.estado;
            listaPagos = Pago.ConvertirLista(comprobanteDePago.Pago);
            descuento = comprobanteDePago.descuento;
            montoAPagar = comprobanteDePago.montoAPagar;
        }

//CONVERTIDORES

        //Convertir un Dato a Model
        public static Models.ComprobanteDePago Convertir(Datos.ComprobanteDePago comprobanteDePago)
        {
            return new ComprobanteDePago(comprobanteDePago);
        }

        //Convertir un arreglo de Datos a model
        public static IEnumerable<Models.ComprobanteDePago> ConvertirLista(IEnumerable<Datos.ComprobanteDePago> dListaComprobantes)
        {
            return dListaComprobantes.Select(comprobanteDePago => new Models.ComprobanteDePago(comprobanteDePago));
        }

        public static Datos.ComprobanteDePago Invertir(Models.ComprobanteDePago mComp)
        {
            Datos.ComprobanteDePago dComp = new Datos.ComprobanteDePago();
            dComp.id = mComp.id;
            dComp.fecha = mComp.fecha;
            dComp.montoTotal = mComp.monto;
            dComp.estado = mComp.estado;
            dComp.Pago = Pago.InvertirLista(mComp.listaPagos);
            return dComp;
        }

        public static EntityCollection<Datos.ComprobanteDePago> InvertirLista(IEnumerable<Models.ComprobanteDePago> mComp)
        {
            EntityCollection<Datos.ComprobanteDePago> a = new EntityCollection<Datos.ComprobanteDePago>();
            foreach (var comprobanteDePago in mComp)
            {
                a.Add(Invertir(comprobanteDePago));
            }
            return a;
        }

        public static IEnumerable<Models.ComprobanteDePago> SeleccionarTodo()
        {
            IEnumerable<Datos.ComprobanteDePago> comprobanteDePago = Negocio.ComprobanteDePago.SeleccionarTodo();
            return ConvertirLista(comprobanteDePago);
        }

        public static Models.ComprobanteDePago buscarId(short id)
        {
            return Convertir(Negocio.ComprobanteDePago.BuscarId(id));
        }

        //interaccion bd

        public static Models.ComprobanteDePago Insertar(List<short> IdPagos)
        {
            return Convertir(Negocio.ComprobanteDePago.Insertar(IdPagos));
             
        }


        internal static object BuscarId(short id)
        {
            return Convertir(Negocio.ComprobanteDePago.BuscarId(id));
        }
    }
   

}