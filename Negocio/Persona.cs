using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using System.Collections;
namespace Negocio
{
   public class Persona
    {
        
        public static Entities context()
        {
            return Context.context();
        }

        public static Datos.Persona buscarID(short id) {
           
            return  context().Persona.Single(p => p.id == id);
        }

        public static Datos.Persona buscarDNI(Int32 dni)
        {

            return context().Persona.Single(p => p.dni == dni);
        }
        public static IEnumerable<Datos.Persona> seleccionarTodo() {
            return context().Persona.Where(persona => persona.estado != 0);
        }
        public static  Exception modificar(Datos.Persona persona)
        {
            try
            {
                Datos.Persona per = context().Persona.Single(p => p.id == persona.id);
                per.nombre = persona.nombre;
                per.apMaterno = persona.apMaterno;
                per.apPaterno = persona.apPaterno;
                per.direccion = persona.direccion;
                per.dni = persona.dni;
                // per.estado = persona.estado;
                per.estadoCivil = persona.estadoCivil;
                context().Persona.ApplyCurrentValues(per);
                context().SaveChanges();
            }
            catch (Exception ex) {
                return ex;
            }
            return null;

        }
        public static Exception insertarPersona(Datos.Persona persona)
        {
            try
            {
                Datos.Persona per = new Datos.Persona(); 
                per.nombre = persona.nombre;
                per.apMaterno = persona.apMaterno;
                per.apPaterno = persona.apPaterno;
                per.direccion = persona.direccion;
                per.dni = persona.dni;
                per.estado = persona.estado;
                per.estadoCivil = persona.estadoCivil;
                context().Persona.AddObject(per);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;

        }
        public static Exception eliminar(Datos.Persona persona) {
            try
            {
                Datos.Persona eliminado = context().Persona.Single(p => p.id == persona.id);
                eliminado.estado = 0;
                context().Persona.ApplyCurrentValues(eliminado);
                context().SaveChanges();
            }
            catch (Exception ex) {
                return ex;
            }
            return null;
        }
     
    }
}
