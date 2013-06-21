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
        [DisplayName("Id")]
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
        [DisplayName("Fecha Inicio")]
        public DateTime fechaInicio { get; set; }

        [JsonProperty("Fecha de Fin")]
        [Required(ErrorMessage = "Debe seleccionar una fecha")]
        [DisplayName("Fecha de Fin")]
        public DateTime fechaFin { get; set; }

        [JsonProperty("Precio")]
        [Required(ErrorMessage = "Debe ingresar un precio")]
        [DisplayName("Precio (S/.)")]
        public double precio { get; set; }

        [JsonProperty("Vacantes")]
        [Required(ErrorMessage = "Debe ingresar un número de vacantes")]
        [DisplayName("Vacantes")]
        public short vacantesTotales { get; set; }

        [JsonProperty("Vacantes Disponibles")]
        [DisplayName("Vacantes Disponibles")]
        public short vacantesDisponibles { get; set; }

        [Required(ErrorMessage = "Debe ingresar un Tipo de Actividad")]
        [DisplayName("Tipo de Actividad")]
        public TipoActividad tipoActividad { get; set; }

        [JsonProperty("Estado")]
        public string estado { get; set; }

        public ReservaAmbiente reserva { get; set; }

        private static ListaEstados listaEstados = null;
        public static ListaEstados ListaEstados()
        {
            if (listaEstados == null)
            {
                listaEstados = new ListaEstados();
            }
            return listaEstados;
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
            estado = ListaEstados().TextoEstado(actividad.estado);
            vacantesTotales = actividad.vacantesTotales;
            vacantesDisponibles = actividad.vacantesDisponibles;
            tipoActividad = Models.TipoActividad.Convertir(actividad.TipoActividad);
        }


//CONVERTIDORES

        //Convertir un Dato a Model
        public static Models.Actividad Convertir(Datos.Actividad actividades)
        {
            return new Actividad(actividades);
        }

        //Convertir un arreglo de Datos a model
        public static IEnumerable<Models.Actividad> ConvertirLista(IEnumerable<Datos.Actividad> dListaActividades)
        {
            return dListaActividades.Select(actividad => new Models.Actividad(actividad));
        }

        //Convierte un Model a Dato
        public static Datos.Actividad Invertir(Models.Actividad mActividad)
        {
            Datos.Actividad dActividad ;
            if (mActividad.id == 0)
                dActividad = new Datos.Actividad();
            else
                dActividad = Negocio.Actividad.BuscarId(mActividad.id);


            //dActividad.TipoActividad.id = mActividad.tipoActividad.id;
            //dActividad.TipoActividad.nombre = mActividad.tipoActividad.nombre;
            //dActividad.TipoActividad.descripcion = mActividad.tipoActividad.descripcion;
            //dActividad.TipoActividad.estado = mActividad.tipoActividad.estado;

            dActividad.nombre = mActividad.nombre;
            dActividad.descripcion = mActividad.descripcion;
            dActividad.precio = mActividad.precio;
            dActividad.fechaInicio = mActividad.fechaInicio;
            dActividad.fechaFin = mActividad.fechaFin;
            dActividad.estado = ListaEstados().EstadoTexto(mActividad.estado);
            dActividad.vacantesTotales = mActividad.vacantesTotales;
            dActividad.vacantesDisponibles = mActividad.vacantesDisponibles;
            dActividad.TipoActividad = Negocio.TipoActividad.buscarId(mActividad.tipoActividad.id);

            return dActividad;
        }
        //CONVIERTE LA PARTE DEL MODEL DE ACTIVIDAD A DATOS
        public static Datos.Actividad InvertirAct(Models.Actividad mActividad)
        {
            Datos.Actividad dActividad;

            if (mActividad.id == 0)
                dActividad = new Datos.Actividad();
            else
                dActividad = Negocio.Actividad.BuscarId(mActividad.id);

            dActividad.nombre = mActividad.nombre;
            dActividad.descripcion = mActividad.descripcion;
            dActividad.precio = mActividad.precio;
            dActividad.fechaInicio = mActividad.fechaInicio;
            dActividad.fechaFin = mActividad.fechaFin;
            dActividad.estado = ListaEstados().EstadoTexto(mActividad.estado);
            dActividad.vacantesTotales = mActividad.vacantesTotales;
            dActividad.vacantesDisponibles = mActividad.vacantesTotales;
            dActividad.TipoActividad = Negocio.TipoActividad.buscarId(mActividad.tipoActividad.id);

            return dActividad;
        }
        //CONVIERTE EL MODELO DE LA PARTE DE RESERVAS A DATOS
        public static Datos.ReservaAmbiente InvertirRes(Models.Actividad mActividad)
        {
            Datos.ReservaAmbiente dreserva;

            if (mActividad.id == 0)
                dreserva = new Datos.ReservaAmbiente();
            else
                dreserva = Negocio.ReservaAmbiente.buscarId(mActividad.reserva.id);

            dreserva.Actividad = InvertirAct(mActividad);
            dreserva.Ambiente = Negocio.Ambiente.buscarId(mActividad.reserva.ambiente.id);
            dreserva.horaInicio = mActividad.fechaInicio;
            dreserva.horaFin = mActividad.fechaFin;
            dreserva.estado = ListaEstados().EstadoTexto(mActividad.estado);

            return dreserva;
        }

        public static IEnumerable<Datos.Actividad> ConvertirListaInverso(IEnumerable<Models.Actividad> mActividad)
        {
            return mActividad.Select(act => Invertir(act));
        }


        //QUERY DE BUSQUEDA

        public static IEnumerable<Models.Actividad> SeleccionarTodo()
        {
            IEnumerable<Datos.Actividad> actividad = Negocio.Actividad.SeleccionarTodo();
            return ConvertirLista(actividad);
        }

        public static Models.Actividad buscarId(short id)
        {
            return Convertir(Negocio.Actividad.BuscarId(id));
        }

        //INTERACCIÓN BD


        public static int insertar(Models.Actividad act)
        {
            if (Negocio.Actividad.Insertar(Invertir(act)) == null)
                return 1;
            else
                return 0;
        }
        
        public static int insertarAR(Models.Actividad act)
        {
            //if (Negocio.Actividad.Insertar(InvertirAct(act)) == null)
                
                if (Negocio.ReservaAmbiente.insertar(InvertirRes(act)) == null)
                    return 1;
            return 0;
        }

        public static int modificarAR(Models.Actividad act)
        {
            if (Negocio.Actividad.Modificar(InvertirAct(act)) == null)
                if (Negocio.ReservaAmbiente.modificar(InvertirRes(act)) == null)
                    return 1;
            return 0;
        }

        public static int modificar(Models.Actividad act)
        {
            if (Negocio.Actividad.Modificar(Invertir(act)) == null)
                return 1;
            else
                return 0;
        }

        public static void eliminar(Models.Actividad act)
        {
           // Negocio.Actividad.Eliminar(Invertir(act));
            Datos.Actividad activ = Negocio.Actividad.BuscarId(act.id);
            Datos.ReservaAmbiente reser = Negocio.ReservaAmbiente.buscarIdActividad(act.id);

            Negocio.ReservaAmbiente.eliminar(reser);
            Negocio.Actividad.Eliminar(activ);
        }
    }
}