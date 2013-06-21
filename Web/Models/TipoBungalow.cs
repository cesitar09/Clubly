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
    public class TipoBungalow
    {
        public short id { get; set; }

        [DisplayName("Nombre")]
        public String nombre { get; set; }

        [DisplayName("Descripcion")]
        public String descripcion { get; set; }

        [DisplayName("Capacidad")]
        public short capacidad { get; set; }

        [DisplayName("Nro Habitaciones")]
        public short nrohabitaciones { get; set; }

         [DisplayName("Precio")]
        public double precio { get; set; }

         [DisplayName("Estado")]
        public short estado { get; set; }

        public TipoBungalow() { }

        public TipoBungalow(Datos.TipoBungalow tipo)
        {
            id = tipo.id;
            nombre = tipo.nombre;
            descripcion = tipo.descripcion;
            capacidad = tipo.capacidad;
            //nrohabitaciones = tipo.nrohabitaciones;
            precio = tipo.precio;
            estado = tipo.estado;
        }

        //public static IEnumerable<TipoBungalow> SeleccionarTodo()
        //{
        //    IEnumerable<Datos.TipoBungalow> listaTipos = Negocio.TipoBungalow.SeleccionarTodo();
        //    return ConvertirLista(listaTipos);
        //}

        public static IEnumerable<TipoBungalow> ConvertirLista(IEnumerable<Datos.TipoBungalow> listaTipos)
        {
            return listaTipos.Select(tipo => Convertir(tipo));
        }

        public static TipoBungalow Convertir(Datos.TipoBungalow tipo)
        {
            return new TipoBungalow(tipo);
        }

        public static Datos.TipoBungalow Invertir(Models.TipoBungalow mTipoBungalow)
        {
            Datos.TipoBungalow dTipoBungalow;
            if (mTipoBungalow.id == 0)
                dTipoBungalow = new Datos.TipoBungalow();
            else
                dTipoBungalow = Negocio.TipoBungalow.buscarId(mTipoBungalow.id);
            dTipoBungalow.nombre = mTipoBungalow.nombre;
            dTipoBungalow.descripcion = mTipoBungalow.descripcion;
            dTipoBungalow.precio = mTipoBungalow.precio;
            dTipoBungalow.capacidad = mTipoBungalow.capacidad;
            dTipoBungalow.nrohabitaciones = mTipoBungalow.nrohabitaciones;
            dTipoBungalow.estado = mTipoBungalow.estado;
            return dTipoBungalow;
        }

        public static IEnumerable<Datos.TipoBungalow> ConvertirListaInverso(IEnumerable<Models.TipoBungalow> mTipoBungalow)
        {
            return mTipoBungalow.Select(tipob => Invertir(tipob));
        }

        public static Models.TipoBungalow buscarId(short id)
        {
            return Convertir(Negocio.TipoBungalow.buscarId(id));
        }

        public static IEnumerable<TipoBungalow> SeleccionarTodo()
        {
            IEnumerable<Datos.TipoBungalow> tipob = Negocio.TipoBungalow.SeleccionarTodo();
            return ConvertirLista(tipob);
        }

        public static Models.TipoBungalow SeleccionarporId(short id)
        {
            return Convertir(Negocio.TipoBungalow.buscarId(id));
        }

        public static int modificarTipoBungalow(Models.TipoBungalow tipob)
        {
            if (Negocio.TipoBungalow.modificar(Invertir(tipob)) == null)
                return 1;
            else
                return 0;
        }

        public static int insertarTipoBungalow(Models.TipoBungalow tipob)
        {
            if (Negocio.TipoBungalow.insertar(Invertir(tipob)) == null)
                return 1;
            else
                return 0;
        }

        public static void eliminarTipoBungalow(Models.TipoBungalow tipob)
        {
            Negocio.TipoBungalow.eliminar(Invertir(tipob));
        }

    }
}
