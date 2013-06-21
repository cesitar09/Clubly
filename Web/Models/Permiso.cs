using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Web.Models
{
    public class Permiso
    {

        public short id { get; set; }
        [Required(ErrorMessage="Debe ingresar la descripción")]
        [MaxLength(50)]
        public String descripcion { get; set; }
        public short estado { get; set; }

        public Permiso()
        {
        }

        public Permiso(Datos.Permiso dPermiso)
        {
            id = dPermiso.id;
            descripcion = dPermiso.descripcion;
            estado = dPermiso.estado;
        }

        public static Models.Permiso Convertir(Datos.Permiso dPermiso)
        {
            return new Permiso(dPermiso);
        }

        public static IEnumerable<Permiso> ConvertirLista(IEnumerable<Datos.Permiso> listaPermiso)
        {
            return listaPermiso.Select(p => Convertir(p));
        }

        public static Datos.Permiso Invertir(Models.Permiso mPermiso)
        {
            Datos.Permiso dPermiso = Negocio.Permiso.seleccionarTodo().Single(p => p.id == mPermiso.id);

            dPermiso.descripcion = mPermiso.descripcion;
            dPermiso.estado = mPermiso.estado;

            return dPermiso;
        }

        public static EntityCollection<Datos.Permiso> InvertirLista(IEnumerable<Models.Permiso> listaPermiso)
        {
            EntityCollection<Datos.Permiso> lista = new EntityCollection<Datos.Permiso>();
            foreach (var permiso in listaPermiso)
            {
                lista.Add(Invertir(permiso));
            }
            return lista;
        }

        public static IEnumerable<Permiso> SeleccionarTodo()
        {
            IEnumerable<Datos.Permiso> permiso = Negocio.Permiso.seleccionarTodo();
            return ConvertirLista(permiso);
        }
    }
}