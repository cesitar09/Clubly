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
        public Int32 dni {get; set;}

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

        [DisplayName("Familia de Traslado")]
        public short familiatraslado { get; set; }

        public int tiposolicitud { get; set; }

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
            if(solicitud.Familia1!= null){
                familiatraslado = solicitud.Familia1.id;
                }
          //  estado = solicitud.estado;
        }

        public static SolicitudMembresia Convertir(Datos.SolicitudMembresia solicitud)
        {
            return new SolicitudMembresia(solicitud);
        }

        public static IEnumerable<SolicitudMembresia> ConvertirLista(IEnumerable<Datos.SolicitudMembresia> ListaSolicitudes)
        {
            return ListaSolicitudes.Select(solicitud => Convertir(solicitud));
           
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
                 DatosSolicitud = Negocio.SolicitudMembresia.BuscarId(solicitud.id);
             }
             DatosSolicitud.dni = solicitud.dni;
             DatosSolicitud.nombre = solicitud.nombre;
             DatosSolicitud.apPaterno = solicitud.apPaterno;
             DatosSolicitud.apMaterno = solicitud.apMaterno;
             DatosSolicitud.correo = solicitud.correo;
             DatosSolicitud.fechaRegistro = DateTime.Now;
             DatosSolicitud.estado = listaEstados.EstadoTexto(solicitud.estado);
             if (solicitud.familiatraslado != 0)
             {
                 DatosSolicitud.Familia1 = Negocio.Familia.buscarId(solicitud.familiatraslado);
             }
             else
             {
                 DatosSolicitud.Familia1 = null;
             }
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

        public static SolicitudMembresia BuscarId(short id)
        {
            Datos.SolicitudMembresia solicitud = Negocio.SolicitudMembresia.BuscarId(id);
            return Convertir(solicitud);
        }

        public static IEnumerable<SolicitudMembresia> SeleccionarTodo()
        {
            return ConvertirLista(Negocio.SolicitudMembresia.SeleccionarTodo());
        }

        public static int Insertar(Models.SolicitudMembresia solicitud)
        {
            if (Negocio.SolicitudMembresia.Insertar(Invertir(solicitud)) == null)
                return 1;
            else
                return 0;
        }

        public static int Modificar(Models.SolicitudMembresia Solicitud)
        {
            if (Negocio.SolicitudMembresia.Modificar(Invertir(Solicitud)) == null)
                return 1;
            else
                return 0;
        }

//-----Inicio Modificado por Dorita------
        public static void Aceptar(Models.SolicitudMembresia solicitud) 
        {
            //if (Models.Familia.val_pagos_reservas(solicitud.familiatraslado).Equals("2"))
            //{
            //    Models.Familia.eliminarpendientes(solicitud.familiatraslado);
            //    Models.Familia.modificarFamilia(Models.Familia.buscarId(solicitud.familiatraslado));
            //}
            Negocio.SolicitudMembresia.Aprobar(Invertir(solicitud));
            
            //Models.Familia familia = new Familia(solicitud);//Cuando se Acepta se crea una nueva familia, *se crea Model familia*
            ////Para crear un socio es necesario crearle una persona, llamamos al constructor de persona;
            //Models.Persona persona = new Persona(solicitud.dni, solicitud.nombre, solicitud.apMaterno, solicitud.apPaterno );
            //Models.Socio socio = new Socio(persona, familia); //llamamos a constructor del modelo socio;
            //Models.Socio.InsertarTitular(socio);// Creamos un nuevo socio *crea familia, y *crea persona :D
            //Negocio.SolicitudMembresia.Aceptar(Invertir(solicitud));//La Solicitud pasa a estado "Aprobada"
        }
//-----Fin Modificado por Dorita------

        public static void Rechazar(Models.SolicitudMembresia Solicitud)
        {
            Negocio.SolicitudMembresia.Rechazar(Invertir(Solicitud));
        }

        public static void Eliminar(Models.SolicitudMembresia Solicitud)
        {
            Negocio.SolicitudMembresia.Eliminar(Invertir(Solicitud));
        }
        
    }
}