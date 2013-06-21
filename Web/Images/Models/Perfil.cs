using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Web.Models
{
    public class Perfil
    {
        [ScaffoldColumn(false)]
        public short id { get; set; }
        [DisplayName("Nombre")]
        [Required(ErrorMessage="Debe ingresar un nombre")]
        [MaxLength(50)]
        public String nombre { get; set; }
        [DisplayName("Descripción")]
        [Required(ErrorMessage = "Debe ingresar una descripción")]
        [MaxLength(50)]
        public String descripcion { get; set; }
        short estado { get; set; }
        public IEnumerable<short> listaPermiso;

        public Perfil()
        {
            listaPermiso = new List<short>();
        }

        public Perfil(Datos.Perfil dperfil)
        {
            id = dperfil.id;
            nombre = dperfil.nombre;
            descripcion = dperfil.descripcion;
            estado = dperfil.estado;
            listaPermiso = dperfil.Permiso.Select(p => p.id);
        }

        public static Models.Perfil Convertir(Datos.Perfil perfil)
        {
            return new Perfil(perfil);
        }

        public static IEnumerable<Perfil> ConvertirLista(IEnumerable<Datos.Perfil> listaPerfil)
        {
            return listaPerfil.Select(p => Convertir(p));
        }

        public static Datos.Perfil Invertir(Models.Perfil mPerfil)
        {
            Datos.Perfil dPerfil;
            if(mPerfil.id == 0)
                dPerfil = new Datos.Perfil();
            else
                dPerfil = Negocio.Perfil.seleccionarTodo().Single(p => p.id == mPerfil.id);
            dPerfil.id = mPerfil.id;
            dPerfil.nombre = mPerfil.nombre;
            dPerfil.descripcion = mPerfil.descripcion;
            dPerfil.estado = mPerfil.estado;
            List<int> insertar = new List<int>();
            List<int> eliminar = new List<int>();
            var permisos = Negocio.Permiso.seleccionarTodo();
            int j = 0, i = 0;
            for (; j < dPerfil.Permiso.Count() && i < mPerfil.listaPermiso.Count() ; )
            {
                if (mPerfil.listaPermiso.ElementAt(i) == dPerfil.Permiso.ElementAt(j).id)
                {
                    i++;
                    j++;
                }
                else if (mPerfil.listaPermiso.ElementAt(i) < dPerfil.Permiso.ElementAt(j).id)
                {
                    insertar.Add(mPerfil.listaPermiso.ElementAt(i));
                    i++;
                }
                else
                {
                    eliminar.Add(dPerfil.Permiso.ElementAt(j).id);
                    j++;
                }
            }
            for (; j < dPerfil.Permiso.Count(); j++ )
            {
                eliminar.Add(dPerfil.Permiso.ElementAt(j).id);
            }
            for (; i < mPerfil.listaPermiso.Count(); i++)
            {
                insertar.Add(mPerfil.listaPermiso.ElementAt(i));
            }

            for (i = 0; i < eliminar.Count(); i++)
            {
                dPerfil.Permiso.Remove(dPerfil.Permiso.Single(p => p.id == eliminar.ElementAt(i)));
            }

            for (j = 0; j < insertar.Count(); j++)
            {
                dPerfil.Permiso.Add(Negocio.Permiso.seleccionarTodo().Single(p => p.id == insertar.ElementAt(j)));
            }
                return dPerfil;
        }

        public static IEnumerable<Perfil> SeleccionarTodo()
        {
            IEnumerable<Datos.Perfil> perfil = Negocio.Perfil.seleccionarTodo();
            return ConvertirLista(perfil);
        }



        public static Perfil buscarId(short id)
        {
            var perfil = Negocio.Perfil.seleccionarTodo().Single(p => p.id == id);
            return Convertir(perfil);
        }

        internal static void modificar(Perfil perfil)
        {
            Negocio.Perfil.Modificar(Invertir(perfil));
        }

        internal static void insertar(Perfil perfil)
        {
            Negocio.Perfil.insertar(Invertir(perfil));
        }
    }
}