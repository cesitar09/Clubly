using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using System.Collections;
namespace Negocio
{
    public class Empleado
    {
        public static Entities context()
        {
            return Context.context();
        }

        public static void insertar(Datos.Empleado empleado)
        {
            
                Datos.EmpleadoXTurno empxturno = new Datos.EmpleadoXTurno();
                empxturno.TurnoDeTrabajo = Negocio.TurnoDeTrabajo.buscarId(empleado.TurnoDeTrabajo.id);
                empxturno.fecha = DateTime.Now;
                Datos.EmpleadoXSede empxsede = new Datos.EmpleadoXSede();
                empxsede.Sede = Negocio.Sede.buscarId(empleado.Sede.id);
                empxsede.fecha = DateTime.Now;
                empxsede.estado = 1;
                context().Persona.AddObject(empleado.Persona);
                context().Empleado.AddObject(empleado);
                context().EmpleadoXTurno.AddObject(empxturno);
                context().EmpleadoXSede.AddObject(empxsede);
                context().SaveChanges();
                Negocio.Util.logito.ElLogeador("Se ingreso el empleado", empleado.Persona.nombre);
          
        }

        public static IEnumerable<Datos.Empleado> seleccionarTodo()
        {
            return context().Empleado.Where(empleado=>empleado.Persona.estado!=0);
        }

        public static Datos.Empleado buscarId(long id)
        {
          
               return  context().Empleado.Single(p => p.id == id);
           
            
    
        }

        public static void modificar(Datos.Empleado empleado )
        {
            
                Negocio.Persona.modificar(empleado.Persona);
                context().Empleado.ApplyCurrentValues(empleado);
                context().SaveChanges();
                Negocio.Util.logito.ElLogeador("Se modifico el empleado", empleado.Persona.nombre);
        }

        public static bool existeDNI(Int32 dni, short id) {
            return Negocio.Persona.existeDNI(dni, id);
        }
  
    }
}
