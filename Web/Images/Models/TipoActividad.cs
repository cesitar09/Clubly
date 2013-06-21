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

        public static IList<Models.TipoActividad> Convertir(IEnumerable<Datos.TipoActividad> tiposAtividad)
        {
            return tiposAtividad.Select(tipoActividad => new Models.TipoActividad(tipoActividad)).ToList();
        }

        public static Datos.TipoActividad ConvertirInverso(Models.TipoActividad mTipoActividad)
        {
            Datos.TipoActividad tipoActividad = new Datos.TipoActividad();
            tipoActividad.id = mTipoActividad.id;
            tipoActividad.descripcion = mTipoActividad.descripcion;
            tipoActividad.nombre = mTipoActividad.nombre;
            tipoActividad.estado = mTipoActividad.estado;
            return tipoActividad;
        }
    }
}