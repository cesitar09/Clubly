using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using Datos;

namespace Web.Models
{
    public class PuntoDeVenta
    {
        [DisplayName("Id")]
        public short id { get; set; }

        public short codSede { get; set; }

        [Required]
        [DisplayName("Sede")]
        public Models.Sede sede { get; set; }
        
        [DisplayName("Nombre")]
        public String nombre { get; set; }

        [DisplayName("Estado")]
        public short estado { get; set; }

        public PuntoDeVenta() { }

        public PuntoDeVenta(Datos.PuntoDeVenta puntoDeVenta)
        {
            id = puntoDeVenta.id;
            nombre = puntoDeVenta.nombre;
            estado = puntoDeVenta.estado;
            sede = Models.Sede.Convertir(puntoDeVenta.Sede);
        }

        public static PuntoDeVenta Convertir(Datos.PuntoDeVenta puntoDeVenta)
        {
            return new PuntoDeVenta(puntoDeVenta);
        }

        public static Datos.PuntoDeVenta Invertir(Models.PuntoDeVenta mPuntoDeVenta)
        {
            Datos.PuntoDeVenta dPuntoDeVenta;
            if (mPuntoDeVenta.id == 0)
            {
                dPuntoDeVenta = new Datos.PuntoDeVenta();
            }else{
                dPuntoDeVenta = Negocio.PuntoDeVenta.buscarId(mPuntoDeVenta.id);
            }
            dPuntoDeVenta.nombre = mPuntoDeVenta.nombre;
            dPuntoDeVenta.estado = mPuntoDeVenta.estado;
            dPuntoDeVenta.Sede = Negocio.Sede.buscarId(mPuntoDeVenta.sede.id);
            
            return dPuntoDeVenta;
        }

        public static IEnumerable<PuntoDeVenta> ConvertirLista(IEnumerable<Datos.PuntoDeVenta> listaPuntosDeVenta)
        {
            return listaPuntosDeVenta.Select(puntoDeVenta => Convertir(puntoDeVenta));
        }

        public static IEnumerable<PuntoDeVenta> SeleccionarTodo()
        {
            IEnumerable<Datos.PuntoDeVenta> puntoDeVenta = Negocio.PuntoDeVenta.seleccionarTodo();       
            return ConvertirLista(puntoDeVenta);
        }

        public static PuntoDeVenta buscarId(short id)
        {
            Datos.PuntoDeVenta puntoDeVenta = Negocio.PuntoDeVenta.buscarId(id);
            return Convertir(puntoDeVenta);
        }

        public static void modificar(Models.PuntoDeVenta puntoDeVenta)
        {
            Negocio.PuntoDeVenta.modificar(Invertir(puntoDeVenta));
        }

        public static void insertar(Models.PuntoDeVenta puntoDeVenta)
        {
            Negocio.PuntoDeVenta.insertar(Invertir(puntoDeVenta));
        }

        public static void eliminar(Models.PuntoDeVenta puntoDeVenta)
        {
            Negocio.PuntoDeVenta.eliminar(Invertir(puntoDeVenta));
        }
    }
}