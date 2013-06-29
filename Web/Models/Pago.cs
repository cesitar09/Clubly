using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Negocio.Util;
using Datos;
using System.Data.Objects.DataClasses;

namespace Web.Models
{
     public class Pago
    {
        [DisplayName("*Id Pago")]
        public short id { get; set; }

        [DisplayName("*Id Familia")]
        public short idfamilia { get; set; }

        [DisplayName("*Concepto de pago")]
        public ConceptoDePago conceptoDePago { get; set; }

        [DisplayName("*Descripción")]
        public String descripcion { get; set; }

        //[JsonProperty("Fecha Registro")]
        //[Required(ErrorMessage = "Debe seleccionar una fecha")]
        [DisplayName("*Fecha Registro")]
        public DateTime fechaRegistro { get; set; }

        //[JsonProperty("Fecha Limite")]
        //[Required(ErrorMessage = "Debe seleccionar una fecha")]
        [DisplayName("*Fecha Limite")]
        public DateTime fechaLimite { get; set; }

        [JsonProperty("*Monto")]
        [Required(ErrorMessage = "Debe ingresar un precio")]
        [DisplayName("Monto")]
        public double monto { get; set; }

        [JsonProperty("Estado")]
        public string estado { get; set; }

        public static ListaEstados listaEstados = new ListaEstados();
        //public static List<Estado_Pago> listestadopago { get; set; }

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
            listaEstados.AgregarEstado(Negocio.Pago.PENDIENTE, "Pendiente");
            listaEstados.AgregarEstado(Negocio.Pago.PORDEVOLVER, "Por Devolver");
            listaEstados.AgregarEstado(Negocio.Pago.DEVUELTO, "Devuelto");
            listaEstados.AgregarEstado(Negocio.Pago.VENCIDO, "Vencido");
            listaEstados.AgregarEstado(Negocio.Pago.CANCELADO, "Cancelado");

            //Estado_Pago estado1 = new Estado_Pago(1,"Pendiente");
            //Estado_Pago estado2 = new Estado_Pago(2,"Cancelado");
            //Estado_Pago estado3 = new Estado_Pago(3, "Por Devolver");
            //Estado_Pago estado4 = new Estado_Pago(4, "Vencido");
            //Estado_Pago estado5 = new Estado_Pago(5, "Devuelto");


            //listestadopago = new List<Estado_Pago>();
            //listestadopago.Add(estado1);
            //listestadopago.Add(estado2);
           
        }

        public Pago()
        {
        }

        public Pago(Datos.Pago pago)
        {
            id = pago.id;
            idfamilia = pago.Familia.id;
            monto = pago.monto;
            descripcion = pago.descripcion;
            fechaRegistro = pago.fechaRegistro;
            fechaLimite = pago.fechaLimite;
            DateTime fechaActual = DateTime.Today;
            //estado = pago.estado;
            conceptoDePago = Models.ConceptoDePago.SeleccionarporId(pago.ConceptoDePago.id);
            //    //if (fechaActual.Year > fechaLimite.Year)
            //    //{
            //    //    pago.estado = 4;
            //    //    modificar(this);
            //    //}
            //    //if (fechaActual.Year == fechaLimite.Year)
            //    //{
            //    //    if (fechaActual.Month > fechaLimite.Month)
            //    //    {
            //    //        pago.estado = 4;
            //    //    }
            //    //}
            //    //if (fechaActual.Month == fechaLimite.Month)
            //    //{
            //    //    if (fechaActual.Day > fechaLimite.Day)
            //    //    {
            //    //        pago.estado = 4;
            //    //    }
            //    //}
          
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
            dPago.descripcion = mPago.descripcion;
            dPago.fechaRegistro = mPago.fechaRegistro;
            dPago.fechaLimite = mPago.fechaLimite;
            //dPago.estado = mPago.estado;
            dPago.estado = listaEstados.EstadoTexto(mPago.estado);
            dPago.ConceptoDePago = Negocio.ConceptoDePago.buscarId(mPago.conceptoDePago.id);

            dPago.Familia = Negocio.Familia.buscarId(mPago.idfamilia);
            return dPago;
        }

        public static EntityCollection<Datos.Pago> InvertirLista(IEnumerable<Models.Pago> mPago)
        {
            EntityCollection<Datos.Pago> p = new EntityCollection<Datos.Pago>();
            foreach (var pago in mPago)
            {
                p.Add(Invertir(pago));
            }
            return p;
        }

        // querys de busqueda

        public static IEnumerable<Models.Pago> SeleccionarTodo()
        {
            IEnumerable<Datos.Pago> pago = Negocio.Pago.SeleccionarTodo();
            return ConvertirLista(pago);
        }

        public static IEnumerable<Models.Pago> SeleccionarCuotas()
        {
            IEnumerable<Datos.Pago> listapago = Negocio.Pago.SeleccionarCuotas();
            return ConvertirLista(listapago);
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

        public static void insertar(Models.Pago pago)
        {
            pago.fechaRegistro = DateTime.Now;
            Parametros par= Parametros.SeleccionarParametros();
            //Usando Parametros de memebresia, solo para pagos de membresia?
            if (pago.conceptoDePago.id ==1) pago.fechaLimite = DateTime.Now.AddDays(par.vencimiento);
            
            Negocio.Pago.Insertar(Invertir(pago));
        }

        public static void InsertarCuota(Models.Pago cuota)
        {
            cuota.conceptoDePago = ConceptoDePago.SeleccionarporId(6);
            cuota.fechaRegistro = DateTime.Now;

            Negocio.Pago.Insertar(Invertir(cuota));
        }

        public static void Cancelar(Models.Pago cuota)
        {
            
            Negocio.Pago.Cancelar(Invertir(cuota));
        }

        public static void modificar(Models.Pago pago)
        {
            Negocio.Pago.modificar(Invertir(pago));
        }

        public static void GenerarPagosMembresia()
        {
            //Pago pago = new Pago();
            //pago.conceptoDePago = ConceptoDePago.SeleccionarporId(1);
            //pago.monto = pago.conceptoDePago.monto.Value;
            Negocio.Pago.GenerarPagosMembresia();

           /*IEnumerable<Models.Familia> listafamilia = Familia.SeleccionarVitalicia();
            /*if (listafamilia != null)
                foreach (Familia fam in listafamilia)
                {
                    Pago pago = new Pago();
                    pago.conceptoDePago = ConceptoDePago.SeleccionarporId(1);
                    pago.monto = pago.conceptoDePago.monto.Value;
                    pago.idfamilia = fam.id;
                    Pago.insertar(pago);
                }//
            listafamilia.Select(fam => Pago.insertarPagoFamilia(fam));
        }

        public static bool insertarPagoFamilia(Familia fam)
        {
            Pago pago = new Pago();
            pago.conceptoDePago = ConceptoDePago.SeleccionarporId(1);
            pago.monto = pago.conceptoDePago.monto.Value;
            pago.idfamilia = fam.id;
            Pago.insertar(pago);
            return true;
        }

         /*
        public static void eliminar(Models.Pago pago)
        {
            Negocio.Pago.(Invertir(pago));*/
        }

    }
}