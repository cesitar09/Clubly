using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using Negocio.Util;
namespace Web.Models
{
    public class SolicitudMembresia
    {
        [Required]
        [DisplayName("Id")]
        public short id {get; set;}

        [Required]
        [DisplayName("Dni")]
        public int dni {get; set;}

        [Required]
        [DisplayName("Nombre")]
        [StringLength(50)]
        public string nombre {get;set;}

        [Required]
        [DisplayName("Apellido Paterno")]
        [StringLength(50)]
        public string apPaterno { get; set; }

        [Required]
        [DisplayName("Apellido Materno")]
        [StringLength(50)]
        public string apMaterno { get; set; }

        [Required]
        [DisplayName("Correo")]
        [StringLength(50)]
        public string correo { get; set; }

        [Required]
        [DisplayName("Fecha Registro")]
        public DateTime fechaRegistro {get;set;}


        [Required]
        [DisplayName("Estado")]
        public String estado { get; set; }

        public static ListaEstados listaEstados;       
        public SolicitudMembresia()
        {
            listaEstados = new ListaEstados();

            listaEstados.AgregarEstado(1, "Pendiente");
            listaEstados.AgregarEstado(2, "Rechazado");
            listaEstados.AgregarEstado(3, "Aprobado");
        }

        public SolicitudMembresia(Datos.SolicitudMembresia solicitud)
        {
            id = solicitud.id;
            dni = solicitud.dni;
            nombre = solicitud.nombre;
            apPaterno = solicitud.apPaterno;
            apMaterno = solicitud.apMaterno;
            correo = solicitud.correo;
            fechaRegistro = solicitud.fechaRegistro;
          estado = listaEstados.TextoEstado(solicitud.estado);
          //  estado = solicitud.estado;
        }

        public static SolicitudMembresia convertir(Datos.SolicitudMembresia solicitud)
        {
            return new SolicitudMembresia(solicitud);
        }

        public static IEnumerable<SolicitudMembresia> ConvertirLista(IEnumerable<Datos.SolicitudMembresia> ListaSolicitudes)
        {
            return ListaSolicitudes.Select(solicitud => convertir(solicitud));
           
        }

        public static Datos.SolicitudMembresia Invertir(Models.SolicitudMembresia solicitud)
        {
             Datos.SolicitudMembresia DatosSolicitud;
             if (solicitud.id == 0)
             {
                 DatosSolicitud = new Datos.SolicitudMembresia();
             }
             else
             {
                 DatosSolicitud = Negocio.SolicitudMembresia.buscarId(solicitud.id);
             }
             DatosSolicitud.dni = solicitud.dni;
             DatosSolicitud.nombre = solicitud.nombre;
             DatosSolicitud.apPaterno = solicitud.apPaterno;
             DatosSolicitud.apMaterno = solicitud.apMaterno;
             DatosSolicitud.correo = solicitud.correo;
             DatosSolicitud.fechaRegistro = DateTime.Now;
             DatosSolicitud.estado = listaEstados.EstadoTexto(solicitud.estado);
            // DatosSolicitud.estado = solicitud.estado; 
            return DatosSolicitud;
        }

        public static EntityCollection<Datos.SolicitudMembresia> InvertirLista(IEnumerable<Models.SolicitudMembresia> ListaSolicitud)
        {
            EntityCollection<Datos.SolicitudMembresia> DatosListaSolicitud = new EntityCollection<Datos.SolicitudMembresia>();

            foreach (var sol in ListaSolicitud)
            {
                DatosListaSolicitud.Add(Invertir(sol));
            }

            return DatosListaSolicitud;
        }

        //Metodos del negocio

        public static SolicitudMembresia buscarId(short id)
        {
            Datos.SolicitudMembresia solicitud = Negocio.SolicitudMembresia.buscarId(id);
            return convertir(solicitud);
        }

        public static IEnumerable<SolicitudMembresia> SeleccionarTodo()
        {
            return ConvertirLista(Negocio.SolicitudMembresia.SeleccionarTodo());
        }

        public static void Insertar(Models.SolicitudMembresia solicitud)
        {
            Negocio.SolicitudMembresia.insertar(Invertir(solicitud));
        }

        public static void modificar(Models.SolicitudMembresia Solicitud)
        {
            Negocio.SolicitudMembresia.modificar(Invertir(Solicitud));
        }

        public static void aceptar(Models.SolicitudMembresia Solicitud)
        {
            Negocio.SolicitudMembresia.aceptar(Invertir(Solicitud));
        }

        public static void eliminar(Models.SolicitudMembresia Solicitud)
        {
            Negocio.SolicitudMembresia.eliminar(Invertir(Solicitud));
        }
        
    }
}