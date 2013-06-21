using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Datos;
namespace Web.Models
{
    public class Proveedor
    {
        [ScaffoldColumn(false)]
        public short id { get; set; }

        [Required(ErrorMessage = "*Debe ingresar el nombre del proveedor")]
        [JsonProperty("Nombre del Concepto de Pago")]
        [StringLength(50)]
        [DisplayName("Nombre")]
        public String nombre { get; set; }

        [Required(ErrorMessage = "*Debe ingresar la dirección")]
        [JsonProperty("Dirección")]
        [StringLength(100)]
        [DisplayName("Dirección")]
        public String direccion { get; set; }

        [Required(ErrorMessage = "*Debe ingresar el RUC")]
        [JsonProperty("RUC")]
        [StringLength(11)]
        [DisplayName("RUC")]
        public String ruc { get; set; }


        public short estado { get; set; }

        public Proveedor() { }

        public Proveedor(Datos.Proveedor proveedor)
        {
            id = proveedor.id;
            nombre = proveedor.nombre;
            direccion = proveedor.direccion;
            ruc = proveedor.ruc;
            estado = proveedor.estado;
        }

        public static Proveedor Convertir(Datos.Proveedor proveedor)
        {
            return new Proveedor(proveedor);
        }

        public static IEnumerable<Models.Proveedor> ConvertirLista(IEnumerable<Datos.Proveedor> listaProveedores)
        {
            return listaProveedores.Select(proveedor => Convertir(proveedor));
        }

        public static Datos.Proveedor Invertir(Models.Proveedor mProveedor)
        {
            Datos.Proveedor p = new Datos.Proveedor();

            p.id = mProveedor.id;
            p.nombre = mProveedor.nombre;
            p.direccion = mProveedor.direccion;
            p.ruc = mProveedor.ruc;
            p.estado = mProveedor.estado;

            return p;
        }

        public static IEnumerable<Datos.Proveedor> ConvertirListaInverso(IEnumerable<Models.Proveedor> mProveedor)
        {
            return mProveedor.Select(amb => Invertir(amb));
        }

        public static Models.Proveedor buscarId(short id)
        {
            return Convertir(Negocio.Proveedor.buscarId(id));
        }

        public static IEnumerable<Models.Proveedor> SeleccionarTodo()
        {
            IEnumerable<Datos.Proveedor> prov = Negocio.Proveedor.seleccionarTodo();
            return ConvertirLista(prov);
        }

        public static Models.Proveedor SeleccionarporId(short id)
        {
            return Convertir(Negocio.Proveedor.buscarId(id));
        }

        public static int modificar(Models.Proveedor proveedor)
        {
            if (Negocio.Proveedor.modificar(Invertir(proveedor)) == null)
                return 1;
            else 
                return 0;
        }

        public static int insertar(Models.Proveedor prov)
        {
            if (Negocio.Proveedor.insertar(Invertir(prov)) == null)
                return 1;
            else
                return 0;
        }

        public static void eliminar(Models.Proveedor prov)
        {
            Negocio.Proveedor.eliminar(Invertir(prov));
        }
    }
}