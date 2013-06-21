using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Negocio.Util;

namespace Web.Models
{
    public class Pago
    {
        [DisplayName("Id de pago")]
        public short id { get; set; }

        [DisplayName("Id de familia")]
        public short idfamilia { get; set; }

        [DisplayName("Concepto de pago")]
        public ConceptoDePago conceptoDePago { get; set; }

        [JsonProperty("Fecha Registro")]
        [Required(ErrorMessage = "Debe seleccionar una fecha")]
        [DisplayName("Fecha Registro")]
        public DateTime fechaRegistro { get; set; }

        [JsonProperty("Fecha Limite")]
        [Required(ErrorMessage = "Debe seleccionar una fecha")]
        [DisplayName("Fecha Limite")]
        public DateTime fechaLimite { get; set; }

        [JsonProperty("Monto")]
        [Required(ErrorMessage = "Debe ingresar un precio")]
        [DisplayName("Monto")]
        public double monto { get; set; }

        [JsonProperty("Estado")]
        public string estado { get; set; }

        public static ListaEstados listaEstados = new ListaEstados();
        public static List<Estado_Pago> listestadopago { get; set; }

        public class Estado_Pago
        {
            public short id { get; set; }
            public string nombre { get; set; }

            public Estado_Pago(short i, string n)
            {
                id = i;
                nombre = n;
            }
        }

        static Pago() {
            listaEstados = new ListaEstados();
            listaEstados.AgregarEstado(1, "Pendiente");
            listaEstados.AgregarEstado(2, "Cancelado");
            listaEstados.AgregarEstado(3, "Por Devolver");
            listaEstados.AgregarEstado(4, "Vencido");
            listaEstados.AgregarEstado(5, "Devuelto");
            Estado_Pago estado1 = new Estado_Pago(1,"Pendiente");
            Estado_Pago estado2 = new Estado_Pago(2,"Cancelado");
            Estado_Pago estado3 = new Estado_Pago(3, "Por Devolver");
            Estado_Pago estado4 = new Estado_Pago(4, "Vencido");
            Estado_Pago estado5 = new Estado_Pago(5, "Devuelto");
            listestadopago = new List<Estado_Pago>();
            listestadopago.Add(estado1);
            listestadopago.Add(estado2);
           
        }

        public Pago()
        {
        }

        public Pago(Datos.Pago pago)
        {
            id = pago.id;
            //idfamilia = pago.Familia.id;
            idfamilia = pago.Familia.id;
            monto = pago.monto;
            fechaRegistro = pago.fechaRegistro;
            fechaLimite = pago.fechaLimite;
            DateTime fechaActual = DateTime.Today;
            //estado = pago.estado;
            conceptoDePago = Models.ConceptoDePago.SeleccionarporId(pago.ConceptoDePago.id);

            if (pago.estado == 1)
            {
                if (fechaActual.Year > fechaLimite.Day)
                {
                    pago.estado = 4;
                }
                if (fechaActual.Year == fechaLimite.Day)
                {
                    if (fechaActual.Month > fechaLimite.Month)
                    {
                        pago.estado = 4;
                    }
                }
                if (fechaActual.Month == fechaLimite.Month)
                {
                    if (fechaActual.Day > fechaLimite.Day)
                    {
                        pago.estado = 4;
                    }
                }
            }
                
           estado = listaEstados.TextoEstado(pago.estado);
        }

        //Convertidores

        public static Models.Pago Convertir(Datos.Pago pagos)
        {
            return new Pago(pagos);
        }

        public static IEnumerable<Models.Pago> ConvertirLista(IEnumerable<Datos.Pago> dListaPagos)
        {
            return dListaPagos.Select(pago => new Models.Pago(pago));
        }

        public static Datos.Pago Invertir(Models.Pago mPago)
        {
            Datos.Pago dPago = new Datos.Pago();

            dPago.id = mPago.id;
            dPago.monto = mPago.monto;
            dPago.fechaRegistro = mPago.fechaRegistro;
            dPago.fechaLimite = mPago.fechaLimite;
            //dPago.estado = mPago.estado;
            dPago.estado = listaEstados.EstadoTexto(mPago.estado);
            dPago.ConceptoDePago = Negocio.ConceptoDePago.buscarId(mPago.conceptoDePago.id);

            dPago.Familia = Negocio.Familia.buscarId(mPago.idfamilia);
            return dPago;
        }

        public static IEnumerable<Datos.Pago> ConvertirListaInverso(IEnumerable<Models.Pago> mPago)
        {
            return mPago.Select(act => Invertir(act));
        }

        // querys de busqueda

        public static IEnumerable<Models.Pago> SeleccionarTodo()
        {
            IEnumerable<Datos.Pago> pago = Negocio.Pago.SeleccionarTodo();
            return ConvertirLista(pago);
        }

        public static IEnumerable<Models.Pago> SeleccionarPorFamilia(short id)
        {
            IEnumerable<Datos.Pago> pago = Negocio.Pago.SeleccionarPorFamilia(id);
            return ConvertirLista(pago);
        }

        public static Models.Pago buscarId(short id)
        {
            return Convertir(Negocio.Pago.BuscarId(id));
        }

        //interaccion bd

        public static int insertar(Models.Pago pago)
        {
            if (Negocio.Pago.Insertar(Invertir(pago)) == null)
                return 1;
            else
                return 0;
        }

        /*public static int modificar(Models.Pago pago)
        {
            if (Negocio.Pago.Modificar(Invertir(pago)) == null)
                return 1;
            else
                return 0;
        }

        public static void eliminar(Models.Pago pago)
        {
            Negocio.Pago.(Invertir(pago));
        }*/

    }
}