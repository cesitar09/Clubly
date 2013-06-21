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
    public class Servicio
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

        [Required]
        //[MaxLength(50, ErrorMessage="No se debe exceder de 50 carácteres.")]
        [DisplayName("Precio")]
        public double precio { get; set; }

        [Required]
        [DisplayName("Sedes")]
        public IEnumerable<Models.Sede> sedes { get; set; }

        public short estado { get; set; }

        //[CustomValidation(typeof(Concesionario), "NotEmpty")]
        public IEnumerable<short> sedesAux { get; set; }

        public Servicio()
        {
        }

       public Servicio(Datos.Servicio servicio)
        {
            id = servicio.id;
            nombre = servicio.nombre;
            descripcion = servicio.descripcion;
            precio = servicio.precio;
            estado = servicio.estado;
            sedes = Sede.ConvertirLista(servicio.Sede);

            List<short> lista = new List<short>();
            if (sedes != null)
                foreach (Sede sede in sedes)
                    lista.Add(sede.id);
            sedesAux = lista.AsEnumerable();
        }

        public static Servicio Convertir(Datos.Servicio servicio)
        {
            return new Servicio(servicio);
        }

        public static Datos.Servicio Invertir(Models.Servicio mServicio)
        {
            Datos.Servicio dServicio;
            if (mServicio.id == 0)
            {
                dServicio = new Datos.Servicio();
            }
            else
            {
                dServicio = Negocio.Servicio.buscarId(mServicio.id);
            }
            dServicio.nombre = mServicio.nombre;
            dServicio.descripcion = mServicio.descripcion;
            dServicio.precio = mServicio.precio;
            if(mServicio.sedes != null)
            foreach (Sede mSede in mServicio.sedes)
            {
                dServicio.Sede.Add(Negocio.Sede.buscarId(mSede.id));
            }
            dServicio.estado = mServicio.estado;
            return dServicio;
        }

        public static IEnumerable<Servicio> ConvertirLista(IEnumerable<Datos.Servicio> servicios)
        {
            return servicios.Select(servicio => Convertir(servicio));
        }

        public static IEnumerable<Servicio> SeleccionarTodo()
        {
            IEnumerable<Datos.Servicio> servicio = Negocio.Servicio.seleccionarTodo();
            return ConvertirLista(servicio);
        }

        public static Servicio buscarId(short id)
        {
            Datos.Servicio servicio = Negocio.Servicio.buscarId(id);
            return Convertir(servicio);
        }

        public static int modificar(Models.Servicio servicio)
        {
            if (Negocio.Servicio.modificar(Invertir(servicio), servicio.sedesAux) == null)
                return 1;
            else
                return 0;
        }

        public static int insertar(Models.Servicio servicio)
        {
            if (Negocio.Servicio.insertar(Invertir(servicio)) == null)
                return 1;
            else
                return 0;
        }

        public static void eliminar(Models.Servicio servicio)
        {
            Negocio.Servicio.eliminar(Invertir(servicio));
        }

        public static IEnumerable<String> listaSedes()
        {
            List<String> lista = new List<String>();
            IEnumerable<Sede> sedes = Sede.SeleccionarTodo();
            foreach (Sede sede in sedes)
            {
                lista.Add(sede.nombre);
            }
            return lista.AsEnumerable();
        }
        /*
        public static ValidationResult NotEmpty(IEnumerable<short> list)
        {
            if (list != null && list.Count() > 0)
                return ValidationResult.Success;
            else
                return new ValidationResult("Debe elegir al menos una sede.");
        }*/
    }
}
