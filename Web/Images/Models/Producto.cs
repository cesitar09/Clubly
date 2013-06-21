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
    public class Producto
    {

        //[ScaffoldColumn(false)]
        [DisplayName("Id")]
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

        [Required(ErrorMessage = "Debe ingresar un precio")]
        [JsonProperty("Precio Unitario")]
        [DisplayName("Precio Unitario")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Ingrese solo numeros.")]
        public double precioUnitario { get; set; }


        [JsonProperty("Estado")]
        [Required(ErrorMessage = "Debe ingresar un estado")]
        [DisplayName("Estado")]
        public short estado { get; set; }

        public Producto()
        {
        }

        public Producto(Datos.Producto producto)
        {
            id = producto.id;
            nombre = producto.nombre;
            descripcion = producto.descripcion;
            precioUnitario = producto.precioUnitario;
            estado = producto.estado;
        }

        public static Producto Convertir(Datos.Producto producto)
        {
            return new Producto(producto);
        }

        public static IEnumerable<Models.Producto> ConvertirLista(IEnumerable<Datos.Producto> listaProductos)
        {
            //var northwind = new NorthwindDataContext();
            return listaProductos.Select(producto => new Models.Producto(producto));
            //return listaConceptos.Select(concepto => Convertir(concepto)).AsQueryable();
           
        }

        public static Datos.Producto ConvertirInverso(Models.Producto mProducto)
        {
            Datos.Producto dProducto = new Datos.Producto();

            dProducto.id = mProducto.id;
            dProducto.precioUnitario = mProducto.precioUnitario;
            dProducto.nombre = mProducto.nombre;
            dProducto.descripcion = mProducto.descripcion;
            dProducto.estado = mProducto.estado;

            return dProducto;
        }

        public static IEnumerable<Models.Producto> SeleccionarTodo() 
        {
            return ConvertirLista(Negocio.Producto.SeleccionarTodoProducto());
        }

        public static Models.Producto SeleccionarporId(short id)
        {
            return Convertir(Negocio.Producto.buscarId(id));
        }

        public static void insertarProducto(Models.Producto producto)
        {
            Negocio.Producto.insertar(ConvertirInverso(producto));
        }

        public static void modificarProducto(Models.Producto producto)
        {
            Negocio.Producto.modificar(ConvertirInverso(producto));
        }

        public static void eliminar(Models.Producto producto)
        {
            Negocio.Producto.eliminar(ConvertirInverso(producto));
        }
    }
}