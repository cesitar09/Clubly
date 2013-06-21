using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Datos;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Ambiente
    {
        [ScaffoldColumn(false)]
        [DisplayName("Id")]
        public short id { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [StringLength(100)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DisplayName("Nombre del Ambiente")]
        public String nombre { get; set; }

        [DisplayName("Estado")]
        public short estado { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [Range(0.01, 200.00, ErrorMessage = "Área debe estar entre 0 y 200")]
        [DisplayName("Área del Ambiente")]
        public double? area { get; set; }

        //Un ambiente está asignado a una Sede
        [DisplayName("Sede")]
        public Models.Sede sede { get; set; }


        public Ambiente() { }

        public Ambiente(Datos.Ambiente amb)
        {
            id = amb.id;
            nombre = amb.nombre;
            estado = amb.estado;
            area = amb.area;
            sede = Sede.Convertir(amb.Sede);
        }

        public static Ambiente Convertir(Datos.Ambiente ambiente)
        {
            return new Ambiente(ambiente);
        }

        public static IEnumerable<Ambiente> ConvertirLista(IEnumerable<Datos.Ambiente> listaamb){

            return listaamb.Select( amb => Convertir(amb));

        }

        public static Datos.Ambiente Invertir(Models.Ambiente mAmbiente)
        {
            Datos.Ambiente dAmbiente;
            if (mAmbiente.id == 0)
                dAmbiente = new Datos.Ambiente();
            else
                dAmbiente = Negocio.Ambiente.buscarId(mAmbiente.id);
            dAmbiente.nombre = mAmbiente.nombre;
            dAmbiente.area = mAmbiente.area;
            dAmbiente.estado = mAmbiente.estado;
            dAmbiente.Sede = Negocio.Sede.buscarId(mAmbiente.sede.id);
            return dAmbiente;
        }

        public static IEnumerable<Datos.Ambiente> ConvertirListaInverso(IEnumerable<Models.Ambiente> mAmbiente)
        {
            return mAmbiente.Select(amb => Invertir(amb));
        }

        public static Models.Ambiente buscarId(short id)
        {
            return Convertir(Negocio.Ambiente.buscarId(id));
        }

        public static IEnumerable<Ambiente> SeleccionarTodo()
        {
            IEnumerable<Datos.Ambiente> ambiente = Negocio.Ambiente.seleccionarTodo();
            return ConvertirLista(ambiente);
        }

        public static Models.Ambiente SeleccionarporId(short id)
        {
            return Convertir(Negocio.Ambiente.buscarId(id));
        }

        public static void modificarAmbiente(Models.Ambiente ambiente)
        {
            Negocio.Ambiente.modificar(Invertir(ambiente));
        }

        public static void insertarAmbiente(Models.Ambiente ambiente)
        {
            Negocio.Ambiente.insertar(Invertir(ambiente));
        }

        public static void eliminarAmbiente(Models.Ambiente ambiente)
        {
            Negocio.Ambiente.eliminar(Invertir(ambiente));
        }
    }
}