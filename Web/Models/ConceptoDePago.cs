using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;


namespace Web.Models
{
    public class ConceptoDePago
    {
        //[ScaffoldColumn(false)]
        public short id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre de usuario")]
        [JsonProperty("Nombre del Concepto de Pago")]
        [StringLength(50)]
        [DisplayName("Nombre")]
        public String nombre { get; set; }

        [JsonProperty("Descripcion")]
        [StringLength(50)]
        [Required(ErrorMessage = "Debe ingresar una descripcion")]
        [DisplayName("Descripción")]
        public String descripcion { get; set; }

        [Required(ErrorMessage = "Debe ingresar un monto")]
        [JsonProperty("Monto")]
        [DisplayName("Monto")]
        public double? monto { get; set; }


        [JsonProperty("Estado")]
        [Required(ErrorMessage = "Debe ingresar un estado")]
        [DisplayName("Estado")]
        public short estado { get; set; }

        public ConceptoDePago()
        {
        }

        public ConceptoDePago(Datos.ConceptoDePago concepto)
        {
            id = concepto.id;
            nombre = concepto.nombre;
            descripcion = concepto.descripcion;
            monto = concepto.monto;
            estado = concepto.estado;
        }

        public static ConceptoDePago Convertir(Datos.ConceptoDePago concepto)
        {
            return new ConceptoDePago(concepto);
        }

        public static IEnumerable<Models.ConceptoDePago> ConvertirLista(IEnumerable<Datos.ConceptoDePago> listaConceptos)
        {
            //var northwind = new NorthwindDataContext();
            return listaConceptos.Select(concepto => new Models.ConceptoDePago(concepto));
            //return listaConceptos.Select(concepto => Convertir(concepto)).AsQueryable();
           
        }

        public static Datos.ConceptoDePago ConvertirInverso(Models.ConceptoDePago mConceptoDePago)
        {
            Datos.ConceptoDePago dCOnceptoDePago = new Datos.ConceptoDePago();

            dCOnceptoDePago.id = mConceptoDePago.id;
            dCOnceptoDePago.monto = mConceptoDePago.monto;
            dCOnceptoDePago.nombre = mConceptoDePago.nombre;
            dCOnceptoDePago.descripcion = mConceptoDePago.descripcion;
            dCOnceptoDePago.estado = mConceptoDePago.estado;

            return dCOnceptoDePago;
        }

        public static IEnumerable<Models.ConceptoDePago> SeleccionarTodo() 
        {
            return ConvertirLista(Negocio.ConceptoDePago.SeleccionarTodoTiposDePago());
        }

        public static Models.ConceptoDePago SeleccionarporId(short id)
        {
            return Convertir(Negocio.ConceptoDePago.buscarId(id));
        }

        public static void insertarConceptoDePago(Models.ConceptoDePago concepto)
        {
            Negocio.ConceptoDePago.insertar(ConvertirInverso(concepto));
        }

        public static void modificarConceptoDePago(Models.ConceptoDePago concepto)
        {
            Negocio.ConceptoDePago.modificar(ConvertirInverso(concepto));
        }
    }
}