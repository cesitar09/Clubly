using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using System.Collections;
using Negocio.Util;

namespace Negocio
{
    public class SolicitudMembresia
    {
   

        public static Entities context()
        {
            return Context.context();
        }


        public static Exception Insertar(Datos.SolicitudMembresia SolicitudMembresia)
        {
            try
            {
                SolicitudMembresia.estado = 1;
                SolicitudMembresia.fechaRegistro = DateTime.Now;
              
                context().SolicitudMembresia.AddObject(SolicitudMembresia);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static IEnumerable<Datos.SolicitudMembresia> SeleccionarTodo()
        {
            return context().SolicitudMembresia.Where(p => p.estado != 0);
        
        }

        public static Datos.SolicitudMembresia BuscarId(short id)
        {
            return context().SolicitudMembresia.Single(p => p.id == id);
        }

        public static Exception Modificar(Datos.SolicitudMembresia solicitudMembresia)
        {
            try
            {
                context().SolicitudMembresia.ApplyCurrentValues(solicitudMembresia);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static int Aprobar(Datos.SolicitudMembresia solicitudMembresia)
            {
                //Crear usuario
                Datos.Usuario usuario = new Datos.Usuario();
                usuario.nomUsuario = solicitudMembresia.apPaterno + "." + solicitudMembresia.apMaterno;
                usuario.contrasena = "1234";
                usuario.estado = 0;
                //Asignar Perfil
                usuario.Perfil = Perfil.BuscarId(12);   //perfil Socio

                //Crear Familia
                Datos.Familia familia = new Datos.Familia();
                familia.SolicitudMembresia = solicitudMembresia;
                familia.Usuario = usuario;
                familia.tipo = Familia.NOVITALICIO;
                familia.saldo = 0;
                familia.estado = 1;
                //Crear Socio
                Datos.Socio socio = new Datos.Socio();
                socio.Familia = familia;
                socio.titular = Socio.TITULAR;
                //Crear Persona
                Datos.Persona persona = new Datos.Persona();
                persona.dni = solicitudMembresia.dni;
                persona.nombre = solicitudMembresia.nombre;
                persona.apPaterno = solicitudMembresia.apPaterno;
                persona.apMaterno = solicitudMembresia.apMaterno;
                persona.correo = solicitudMembresia.correo;
                persona.estado = 1;
                persona.estadoCivil = solicitudMembresia.estadoCivil;
                persona.direccion = solicitudMembresia.direccion;
                socio.Persona = persona;
                //Insertar Socio
                context().Socio.AddObject(socio);
                solicitudMembresia.estado = 3;
            
                return context().SaveChanges();

            }
            public static int Eliminar(Datos.SolicitudMembresia solicitudMembresia)
            {
                solicitudMembresia.estado = 0;
                return context().SaveChanges();

            }

            public static int Rechazar(Datos.SolicitudMembresia solicitudMembresia)
            {

                solicitudMembresia.estado = 2;
                return context().SaveChanges();

            }

            //public static int ValidarAprobacion(Datos.SolicitudMembresia solicitudMembresia) { 
            //    if(context().)
        
            //}

        }
    }
