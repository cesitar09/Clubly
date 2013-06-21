using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Negocio.Util;

namespace Web.Models
{
    public class Actividad
    {
        [DisplayName("Id de actividad")]
        //[DisplayFormat()]
        public short id { get; set; }

        [JsonProperty("Nombre")]
        [Required(ErrorMessage = "Debe ingresar un nombre")]
        [StringLength(50)]
        [DisplayName("Nombre")]
        public string nombre { get; set; }

        [JsonProperty("Descipción")]
        [Required(ErrorMessage = "Debe ingresar una descripción")]

        [StringLength(150)]
        [DisplayName("Descripción")]
        public string descripcion { get; set; }

        [JsonProperty("Fecha Inicio")]
        [Required(ErrorMessage = "Debe seleccionar una fecha")]
        [DisplayName ("Fecha Inicio")]
        public DateTime fechaInicio { get; set; }

        [JsonProperty("Fecha de Fin")]
        [Required(ErrorMessage = "Debe seleccionar una fecha")]
        [DisplayName("Fecha de fin")]
        public DateTime fechaFin { get; set; }

        [JsonProperty("Precio")]
        [Required(ErrorMessage = "Debe ingresar un precio")]
        [DisplayName("Precio")]
        public double? precio { get; set; }

        [Required(ErrorMessage = "Debe ingresar un Tipo de Actividad")]
        [DisplayName("Tipo de actividad")]
        public TipoActividad tipoActividad { get; set; }

        [JsonProperty("Estado")]
        public String estado { get; set; }

        //Esto es para que te muestre estados cuando es 0 y 1 
        public static ListaEstados listaEstados;
        static Actividad()
        {
            listaEstados = new ListaEstados();
            listaEstados.AgregarEstado(1, "En Proceso");
            //listaEstados.AgregarEstado(0, "Terminado");
        }


//CONSTRUCTORES
        public Actividad()
        {
        }

        public Actividad(Datos.Actividad actividad) 
        {
            id = actividad.id;
            
            nombre = actividad.nombre;
            descripcion = actividad.descripcion;
            precio = actividad.precio;
            fechaInicio = actividad.fechaInicio;
            fechaFin = actividad.fechaFin;
            estado = listaEstados.TextoEstado(actividad.estado);
            tipoActividad = new Models.TipoActividad(actividad.TipoActividad);
        }


//CONVERTIDORES
        //Convertir un arreglo de Datos a model
        public static IEnumerable<Models.Actividad> ConvertirLista(IEnumerable<Datos.Actividad> dListaActividades)
        {
            return dListaActividades.Select(actividad => new Models.Actividad(actividad));
            
        }

        //Convertir un Dato a Model
        public static Models.Actividad Convertir(Datos.Actividad actividades)
        {
           return   new Actividad(actividades);
        }

        //Convierte un Model a Dato
        public static Datos.Actividad Invertir(Models.Actividad mActividad)
        {
            Datos.Actividad dActividad = new Datos.Actividad();

            dActividad.id = mActividad.id;
            dActividad.TipoActividad = Negocio.TipoActividad.BuscarId(mActividad.tipoActividad.id);
            //dActividad.TipoActividad.id = mActividad.tipoActividad.id;
            //dActividad.TipoActividad.nombre = mActividad.tipoActividad.nombre;
            //dActividad.TipoActividad.descripcion = mActividad.tipoActividad.descripcion;
            //dActividad.TipoActividad.estado = mActividad.tipoActividad.estado;
            dActividad.nombre = mActividad.nombre;
            dActividad.descripcion = mActividad.descripcion;
            dActividad.precio = mActividad.precio;
            dActividad.fechaInicio = mActividad.fechaInicio;
            dActividad.fechaFin = mActividad.fechaFin;
            dActividad.estado = listaEstados.EstadoTexto(mActividad.estado);
            return dActividad;
        }
     
   
//QUERY DE BUSQUEDA
        //Se Utiliza para sacar un query de todas las actividades activas
        public static IEnumerable<Models.Actividad> SeleccionarTodo()
        {
            IEnumerable<Datos.Actividad> actividad = Negocio.Actividad.SeleccionarTodo();
            return ConvertirLista(actividad);
        }

        public static Models.Actividad SeleccionarporId(short id)
        {
            return Convertir(Negocio.Actividad.BuscarId(id));
        }
      
    }
}