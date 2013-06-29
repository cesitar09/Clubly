using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Datos;

namespace Web.Models
{
    public class Parametros
    {
        //
        // GET: /Parametros/
        [ScaffoldColumn(false)]
        [DisplayName("Id")]
        
        public short id { get; set; }

        [DisplayName("*Maximo de Invitados")]
        public Int16 invitados { get; set; }

        [DisplayName("*Mensualidad de membresia (S/.)")]
        public double membresia { get; set; }

        [DisplayName("*Pago por Invitado Adicional (S/.)")]
        public double pagoInvitado { get; set; }

        [DisplayName("*Dias para vencimiento")]
        public Int16 vencimiento { get; set; }

        [DisplayName("*Multa (S/.)")]
        public double multa { get; set; }

        [DisplayName("*Horas por Cancha")]
        public Int16 tiempo { get; set; }

        [JsonProperty("Fecha Inicio")]
        //[Required(ErrorMessage = "Debe seleccionar una fecha")]
        [DisplayName("Fecha Inicio")]
        public DateTime fechaIni{ get; set; }

        [JsonProperty("Fecha Fin")]
        //[Required(ErrorMessage = "Debe seleccionar una fecha")]
        [DisplayName("Fecha Fin")]
        public DateTime? fechaFin{ get; set; }
        public short estado;

        public Parametros() { }

        public Parametros(Datos.Parametros par)
        {
            id = par.id;
            fechaIni = par.fechaInicial;
            fechaFin = null;
            estado = par.estado;
            tiempo = par.tiempoMaximoCancha;
            vencimiento = par.diasLimitePago;
            membresia = par.costoMembresia;
            invitados = par.numInvitadosFamilia;
            multa = par.multa;
            pagoInvitado = par.pagoInvitado;
        }

        public static Parametros Convertir(Datos.Parametros par)
        {
            return new Parametros(par);
        }

        public static IEnumerable<Parametros> ConvertirLista(IEnumerable<Datos.Parametros> listapar)
        {

            return listapar.Select(par => Convertir(par));

        }

        public static Datos.Parametros Invertir(Models.Parametros mPar)
        {
            Datos.Parametros dPar;
            if (mPar.id == 0)
            {
                dPar = new Datos.Parametros();
                
            }
            else
            {
                dPar = Negocio.Parametros.buscarId(mPar.id);
               
            }
            dPar.fechaInicial = mPar.fechaIni;
            dPar.fechaFinal = mPar.fechaFin;
            dPar.estado = mPar.estado;
            dPar.tiempoMaximoCancha = mPar.tiempo;
            dPar.diasLimitePago = mPar.vencimiento;
            dPar.costoMembresia = mPar.membresia;
            dPar.numInvitadosFamilia = mPar.invitados;
            dPar.multa = mPar.multa;
            dPar.pagoInvitado = mPar.pagoInvitado;
            return dPar;
        }

        public static IEnumerable<Datos.Parametros> ConvertirListaInverso(IEnumerable<Models.Parametros> mParametro)
        {
            return mParametro.Select(par => Invertir(par));
        }

        public static Models.Parametros buscarId(short id)
        {
            return Convertir(Negocio.Parametros.buscarId(id));
        }

        public static Models.Parametros SeleccionarParametros()
        {
            return Convertir(Negocio.Parametros.SeleccionarParametros());
        }

        public static IEnumerable<Parametros> SeleccionarTodo()
        {
            IEnumerable<Datos.Parametros> par = Negocio.Parametros.seleccionarTodo();
            return ConvertirLista(par);
        }

        public static IEnumerable<Parametros> SeleccionarValido()
        {
            IEnumerable<Datos.Parametros> par = Negocio.Parametros.seleccionarValido();
            return ConvertirLista(par);
        }

        public static void insertarParametros(Models.Parametros par)
        {
            par.fechaIni = DateTime.Now;
            par.estado = Negocio.Parametros.HABILITADO;
            Negocio.Parametros.insertar(Invertir(par));
            ConceptoDePago concepto = ConceptoDePago.SeleccionarporId(3);
            concepto.monto = par.pagoInvitado;
            ConceptoDePago concepto1 = ConceptoDePago.SeleccionarporId(1);
            concepto1.monto = par.membresia;
            ConceptoDePago.modificarConceptoDePago(concepto);
            ConceptoDePago.modificarConceptoDePago(concepto);
            Datos.Parametros antiguo = Negocio.Parametros.SeleccionarParametros();
            if (antiguo != null)
                deshabilitarParametros(antiguo);
           
        }

        public static void deshabilitarParametros(Datos.Parametros par)
        {
            par.estado = Negocio.Parametros.INHABILITADO;
            par.fechaFinal = DateTime.Now;
            Negocio.Parametros.deshabilitar(par);
        }
    }
}

