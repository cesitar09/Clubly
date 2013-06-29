using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
using System.Data.Objects.DataClasses;
using Datos;

namespace Web.Models
{
    public class Promocion
    {
        [DisplayName("Id")]
        public short id { get; set; }

        [Required]
        [DisplayName("Nombre")]
        //[MaxLength(11, ErrorMessage = "No se debe exceder de 11 carácteres.")]
        public string nombre { get; set; }

        [Required]
        //[MaxLength(50, ErrorMessage="No se debe exceder de 50 carácteres.")]
        [DisplayName("Descripcion")]
        public string descripcion { get; set; }

        [DisplayName("Concepto")]
        public Models.ConceptoDePago concepto { get; set; }

        [Required]
        [DisplayName("Familias")]
        public IEnumerable<Models.Familia> familias { get; set; }

        //[Required(ErrorMessage = " Seleccione una opcion")]
        //[Display(Name = "Motivo")]
        //public string motivo { get; set; }
        public short estado;

        //[CustomValidation(typeof(Concesionario), "NotEmpty")]
        public IEnumerable<short> familiasAux { get; set; }

        public Promocion()
        {
        }

        /* public Promocion(Datos.Promocion promo)
         {
             id = promo.id;
             nombre = promo.nombre;
             descripcion = promo.descripcion;
             //concepto = promo.concepto;
             familias = Familia.ConvertirLista(promo.Familia);
             estado = promo.estado;
             motivo = promo.motivo;
             List<short> lista = new List<short>();
             if (familias != null)
                 foreach (Familia familia in familias)
                     lista.Add(familia.id);
             familiasAux = lista.AsEnumerable();
         }

         public static Promocion Convertir(Datos.Promocion promo)
         {
             return new Promocion(promo);
         }

         public static Datos.Promocion Invertir(Models.Promocion mPromo)
         {
             Datos.Promocion dPromo;
             if (mPromo.id == 0)
             {
                 dPromo = new Datos.Promocion();
             }
             else
             {
                 dPromo = Negocio.Promocion.buscarId(mPromocion.id);
             }
             dPromo.nombre = mPromocion.nombre;
             dPromo.descripcion = mPromo.descripcion;
             dPromo.concepto = mPromo.concepto;
             //dPromo.motivo = mPromo.motivo;
             dPromo.estado = mPromo.estado;
             return dPromo;
         }

         public static IEnumerable<Promocion> ConvertirLista(IEnumerable<Datos.Promocion> promos)
         {
             return promos.Select(promo => Convertir(promo));
         }

         public static IEnumerable<Promocion> SeleccionarTodo()
         {
             IEnumerable<Datos.Promocion> promo = Negocio.Promocion.seleccionarTodo();
             return ConvertirLista(promo);
         }

         public static Promocion buscarId(short id)
         {
             Datos.Promocion promo = Negocio.Promocion.buscarId(id);
             return Convertir(promo);
         }

         public static int modificar(Models.Promocion promo)
         {
             if (Negocio.Promocion.modificar(Invertir(promo), promo.familiasAux) == null)
                 return 1;
             else
                 return 0;
         }

         public static int insertar(Models.Promocion promo)
         {
             if (Negocio.Promocion.insertar(Invertir(promo)) == null)
                 return 1;
             else
                 return 0;
         }

         public static void eliminar(Models.Promocion promo)
         {
             Negocio.Promocion.eliminar(Invertir(promo));
         }

         public static IEnumerable<Short> listaFamilias()
         {
             List<Short> lista = new List<Short>();
             IEnumerable<Familia> familias = Familia.SeleccionarTodo();
             foreach (Familia familia in familias)
             {
                 lista.Add(familia.id);
             }
             return lista.AsEnumerable();
         }
         /*
         public static ValidationResult NotEmpty(IEnumerable<short> list)
         {
             if (list != null && list.Count() > 0)
                 return ValidationResult.Success;
             else
                 return new ValidationResult("Debe elegir al menos una familia.");
         }*/
    }
}
