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
    public class Concesionario
    {
        [DisplayName("Id")]
        public short id { get; set; }

        [Required]
        [DisplayName("RUC")]
        [MaxLength(11, ErrorMessage = "No se debe exceder de 11 carácteres.")]
        public string ruc { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage="No se debe exceder de 50 carácteres.")]
        [DisplayName("Nombre")]
        public string nombre { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "No se debe exceder de 100 carácteres.")]
        [DisplayName("Dirección")]
        public string direccion { get; set; }

        [Required]
        [DisplayName("Estado")]
        public short estado { get; set; }

        [Required]
        [DisplayName("Sedes")]
        public IEnumerable<Models.Sede> sedes { get; set; }

        [CustomValidation(typeof(Concesionario), "NotEmpty")]
        public IEnumerable<short> sedesAux { get; set; }

        public Concesionario()
        {
        }

        public Concesionario(Datos.Concesionario concesionario)
        {
            id = concesionario.id;
            ruc = concesionario.ruc;
            nombre = concesionario.nombre;
            direccion = concesionario.direccion;
            estado = concesionario.estado;
            sedes = Sede.ConvertirLista(concesionario.Sede);

            List<short> lista = new List<short>();
            if (sedes != null)
                foreach (Sede sede in sedes)
                    lista.Add(sede.id);
            sedesAux = lista.AsEnumerable();
        }

        public static Concesionario Convertir(Datos.Concesionario concesionario)
        {
            return new Concesionario(concesionario);
        }

        public static Datos.Concesionario Invertir(Models.Concesionario mConcesionario)
        {
            Datos.Concesionario dConcesionario;
            if (mConcesionario.id == 0)
            {
                dConcesionario = new Datos.Concesionario();
            }
            else
            {
                dConcesionario = Negocio.Concesionario.buscarId(mConcesionario.id);
            }
            dConcesionario.ruc = mConcesionario.ruc;
            dConcesionario.nombre = mConcesionario.nombre;
            dConcesionario.direccion = mConcesionario.direccion;
            /*if(mConcesionario.sedes != null)
            foreach (Sede mSede in mConcesionario.sedes)
            {
                dConcesionario.Sede.Add(Negocio.Sede.buscarId(mSede.id));
            }*/
            dConcesionario.estado = mConcesionario.estado;
            return dConcesionario;
        }

        public static IEnumerable<Concesionario> ConvertirLista(IEnumerable<Datos.Concesionario> concesionarios)
        {
            return concesionarios.Select(concesionario => Convertir(concesionario));
        }

        public static IEnumerable<Concesionario> SeleccionarTodo()
        {
            IEnumerable<Datos.Concesionario> concesionario = Negocio.Concesionario.seleccionarTodo();
            return ConvertirLista(concesionario);
        }

        public static Concesionario buscarId(short id)
        {
            Datos.Concesionario concesionario = Negocio.Concesionario.buscarId(id);
            return Convertir(concesionario);
        }

        public static void modificar(Models.Concesionario concesionario)
        {
            Negocio.Concesionario.modificar(Invertir(concesionario), concesionario.sedesAux);
        }

        public static void insertar(Models.Concesionario concesionario)
        {
            Negocio.Concesionario.insertar(Invertir(concesionario));
        }

        public static void eliminar(Models.Concesionario concesionario)
        {
            Negocio.Concesionario.eliminar(Invertir(concesionario));
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

        public static ValidationResult NotEmpty(IEnumerable<short> list)
        {
            if (list != null && list.Count() > 0)
                return ValidationResult.Success;
            else
                return new ValidationResult("Debe elegir al menos una sede.");
        }
    }
}