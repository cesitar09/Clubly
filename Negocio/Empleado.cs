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

        public static Exception insertar(Datos.Empleado empleado)
        {
            try
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
            }
            catch (Exception ex) {
                return ex;
            }
            return null;
          
        }

        public static IEnumerable<Datos.Empleado> seleccionarTodo()
        {
            return context().Empleado.Where(empleado=>empleado.Persona.estado!=0);
        }

        public static Datos.Empleado buscarId(short id)
        {
            return context().Empleado.Single(p => p.id == id);
        }

        public static Exception modificar(Datos.Empleado empleado )
        {
            try
            {
                Negocio.Persona.modificar(empleado.Persona);
                context().Empleado.ApplyCurrentValues(empleado);
                context().SaveChanges();
            }
            catch (Exception ex) {
                return ex;
            }
            return null;

        }
  
    }
}
