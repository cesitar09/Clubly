using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using System.Collections;


namespace Web.Models
{
    public class SocioXActividad
    {
        //public static List<string> Estados =(new List<string>()).Add("Inactivo").Add("Activo");//{"String",""};

        [Display(Name = "Id Socio")]
        public short idSocio { get; set; }

        [Required]
        [Display(Name = "Id Actividad")]
        public short idActividad { get; set; }

        //[Display(Name = "Id Pago")]
        //public short idPago { get; set; }

        //Atributo Eliminado
        /*[Display(Name = "Hora Ingreso")]
        public TimeSpan? horaIngreso{ get; set; }*/

        [Display(Name = "Estado")]
        public short estado { get; set; }

        [Required]
        [Display(Name = "Actividad")]
        public Actividad Actividad { get; set; }

        [Required]
        [Display(Name = "Socio")]
        public Socio Socio { get; set; }

//CONSTRUCTORES
        public SocioXActividad() { }
        public SocioXActividad(Datos.SocioXActividad socioXActividad) 
        {
            try
            {
                idSocio = socioXActividad.idSocio;
                idActividad = socioXActividad.idActividad;
                //idPago = socioXActividad.Pago.id;
                //horaIngreso = socioXActividad.horaIngreso;
                estado = socioXActividad.estado;
                Socio = Socio.Convertir(socioXActividad.Socio);
                Actividad = Actividad.Convertir(socioXActividad.Actividad);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("{0} Exception caught.", e);
            }
        }
        public Models.SocioXActividad Convertir(Datos.SocioXActividad sxa)
        {
            return new SocioXActividad(sxa);
        }

 
 //CONVERSIONES
        //Convierte lista de socioxActividades de Datos a Models
        public static IEnumerable<Models.SocioXActividad> ConvertirLista(IEnumerable<Datos.SocioXActividad> listaSocioXActividad)
        {
            return listaSocioXActividad.Select(socioXActividad => new Models.SocioXActividad(socioXActividad));
        }


        public static EntityCollection<Datos.SocioXActividad> InvertirLista(IEnumerable<SocioXActividad> mListaSocioXActividad)
        {
            EntityCollection<Datos.SocioXActividad> listaSocioXActividad = new EntityCollection<Datos.SocioXActividad>();

            int count = mListaSocioXActividad.ToList().Count;
            for (int i = 0; i < count; i++)
            {
                listaSocioXActividad.Add(Invertir(mListaSocioXActividad.ToList()[i]));
            }

            return listaSocioXActividad;
        }

        
        public static Datos.SocioXActividad Invertir(Models.SocioXActividad sxa)
        {
            Datos.SocioXActividad socioXActividad = new Datos.SocioXActividad();

            socioXActividad.idSocio = sxa.idSocio;
            socioXActividad.idActividad = sxa.idActividad;
            //socioXActividad.Pago.id = sxa.idPago;
            socioXActividad.estado = sxa.estado;
            socioXActividad.Actividad = Negocio.Actividad.BuscarId(sxa.idActividad);
            socioXActividad.Socio = Negocio.Socio.BuscarId(sxa.idSocio);
            return socioXActividad;
        }


//QUERYS DE BUSQUEDA
        //Seleccionar todo SocioXActividad
        public static IEnumerable<Models.SocioXActividad> SeleccionarTodo()
        {
            return ConvertirLista(Negocio.SocioXActividad.SeleccionarTodo());
        }

        //Busca una activida especifica y un socio especifico 
        public static IEnumerable<SocioXActividad> BuscarIdActividadIdFamilia(int idActividad, int idFamilia)
        {
            return ConvertirLista(Negocio.SocioXActividad.BuscarIdActividadIdFamilia(idActividad,idFamilia));
        }

        //Metodo movido a Models.Actividad
        /*public static IEnumerable<Models.SocioXActividad> BuscarActividadIdFamilia(short idFamilia)
        {
            return ConvertirLista(Negocio.SocioXActividad.BuscarActividadIdFamilia(idFamilia));
        }*/
        internal static IEnumerable<SocioXActividad> BuscarIdActividad(short idActividad)
        {
            return ConvertirLista(Negocio.SocioXActividad.BuscarIdActividad(idActividad));
        }

//QUERYS DE ELIMINACION
        public static void Eliminar(SocioXActividad socioXActividad)
        {
            Negocio.SocioXActividad.Eliminar(Invertir(socioXActividad));
        }

        public static void Eliminar(short idSocio, short idActividad)
        {
            Negocio.SocioXActividad.Eliminar(idSocio, idActividad);
        }

        //Elimina todas las actividades de una familia
        public static void EliminarTodo(short idSocio,short idActividad)
        {
            Negocio.SocioXActividad.EliminarTodo(idSocio,idActividad);
        }


//QUERYS DE INSERTAR
        public static int Insertar(SocioXActividad modelsNuevo)
        {
            Datos.SocioXActividad datosNuevo = new Datos.SocioXActividad();
            datosNuevo.Socio = Negocio.Socio.BuscarId(modelsNuevo.idSocio);
            datosNuevo.Actividad = Negocio.Actividad.BuscarId(modelsNuevo.idActividad);
            return Negocio.SocioXActividad.Insertar(datosNuevo);
        }

        public static int Insertar(short idSocio, short idActividad)
        {
            Datos.SocioXActividad datosNuevo = new Datos.SocioXActividad();
            datosNuevo.idSocio = idSocio;
            datosNuevo.idActividad = idActividad;
            datosNuevo.estado = 1;
            return Negocio.SocioXActividad.Insertar(datosNuevo);
        }


    }
}