using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class TipoActividad
    {
        [Display(Name = "Id")]
        public short id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar un nombre")]
        [StringLength(150)]
        public string nombre { get; set; }

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "Debe ingresar una descripción")]
        [StringLength(150)]
        public string descripcion { get; set; }

        [Display(Name = "Estado")]
        public short estado { get; set; }

        public TipoActividad()
        {
        }

        public TipoActividad(Datos.TipoActividad tipoActividad)
        {
            id = tipoActividad.id;
            nombre = tipoActividad.nombre;
            descripcion = tipoActividad.descripcion;
            estado = tipoActividad.estado;
        }

        public static Models.TipoActividad Convertir(Datos.TipoActividad tipoActividad)
        {
            return new TipoActividad(tipoActividad);
        }

        public static IEnumerable<Models.TipoActividad> ConvertirLista(IEnumerable<Datos.TipoActividad> listaTipoActividad)
        {
            return listaTipoActividad.Select(proveedor => Convertir(proveedor));
        }

        public static Datos.TipoActividad Invertir(Models.TipoActividad mTipoActividad)
        {
            Datos.TipoActividad tipoActividad = new Datos.TipoActividad();

            tipoActividad.id = mTipoActividad.id;
            tipoActividad.descripcion = mTipoActividad.descripcion;
            tipoActividad.nombre = mTipoActividad.nombre;
            tipoActividad.estado = mTipoActividad.estado;

            return tipoActividad;
        }

        public static IEnumerable<Datos.TipoActividad> ConvertirListaInverso(IEnumerable<Models.TipoActividad> mTipoActividad)
        {
            return mTipoActividad.Select(amb => Invertir(amb));
        }

        public static Models.TipoActividad buscarId(short id)
        {
            return Convertir(Negocio.TipoActividad.buscarId(id));
        }

        public static IEnumerable<TipoActividad> seleccionarTodo()
        {
            IEnumerable<Datos.TipoActividad> list = Negocio.TipoActividad.seleccionarTodo();
            return ConvertirLista(list);
        }

        public static int modificar(Models.TipoActividad tipoAct)
        {
            if (Negocio.TipoActividad.modificar(Invertir(tipoAct)) == null)
                return 1;
            else
                return 0;
        }

        public static int insertar(Models.TipoActividad tipoAct)
        {
            if (Negocio.TipoActividad.insertar(Invertir(tipoAct)) == null)
                return 1;
            else
                return 0;
        }

        public static void eliminar(Models.TipoActividad tipoAct)
        {
            Negocio.TipoActividad.eliminar(Invertir(tipoAct));
        }

        public static IEnumerable<Models.TipoActividad> SeleccionarTodo()
        {
            IEnumerable<Datos.TipoActividad> act = Negocio.TipoActividad.seleccionarTodo();
            return ConvertirLista(act);
        }
    }
}