using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects.DataClasses;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Familia
    {
        [Display(Name = "Codigo Familia")]
        [Required]
        public short id { get; set; }

        [Display(Name = "Tipo")]
        [Required]
        public short tipo { get; set; }

        [Display(Name = "Saldo")]
        [Required]
        public double saldo { get; set; }

        [Display(Name = "Estado")]
        public short estado { get; set; }

        //[Display(Name = "Solicitud de Membresia")]
        //public SolicitudMembresia SolicitudMembresia { get; set; }

        //metodos para convertir
        public Familia() { }
        public Familia(Datos.Familia familia)
        {
            id = familia.id;
            tipo = familia.tipo;
            saldo = familia.saldo;
            estado = familia.estado;
            //SolicitudMembresia = SolicitudMembresia.convertir(familia.SolicitudMembresia);
        }
        public static Models.Familia Convertir(Datos.Familia familia)
        {
            return new Familia(familia);
        }
        public static IEnumerable<Models.Familia> ConvertirLista(IEnumerable<Datos.Familia> listaFamilia)
        {
            return listaFamilia.Select(persona => Convertir(persona));
        }
        public static Datos.Familia Invertir(Models.Familia familia) 
        {
            Datos.Familia datosFamilia = new Datos.Familia();
            datosFamilia.id = familia.id;
            datosFamilia.tipo = familia.tipo;
            datosFamilia.saldo = familia.saldo;
            datosFamilia.estado = familia.estado;
            //datosFamilia.SolicitudMembresia = SolicitudMembresia.Invertir(familia.SolicitudMembresia);
            return datosFamilia; 
        }
        public static IEnumerable<Models.Familia> SeleccionarTodo()
        {
            return ConvertirLista(Negocio.Familia.seleccionarTodo());
        }
    }
}