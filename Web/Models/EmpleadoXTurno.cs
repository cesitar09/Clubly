using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects.DataClasses;

namespace Web.Models
{
    public class EmpleadoXTurno
    {
        //atributos
        public Models.Empleado empleado { set; get; }
        public Models.TurnoDeTrabajo turnodetrabajo { get; set; }
        public DateTime fecha { get; set; }

        //metodos para convertir
        public EmpleadoXTurno() { }
        public EmpleadoXTurno(Datos.EmpleadoXTurno empxturno)
        {
            empleado = new Models.Empleado(empxturno.Empleado);
            turnodetrabajo = new Models.TurnoDeTrabajo(empxturno.TurnoDeTrabajo);
            fecha = empxturno.fecha;
        }
        public static EmpleadoXTurno Convertir(Datos.EmpleadoXTurno empxturno)
        {
            return new EmpleadoXTurno(empxturno);
        }
        public static IEnumerable<Models.EmpleadoXTurno> ConvertirLista(IEnumerable<Datos.EmpleadoXTurno> empxturnos)
        {
            return empxturnos.Select(empxturno => new Models.EmpleadoXTurno(empxturno));
        }

        //metodos para invertir
        public static Datos.EmpleadoXTurno Invertir(Models.EmpleadoXTurno empxturno)
        {
            Datos.EmpleadoXTurno dempxturno = new Datos.EmpleadoXTurno();
            dempxturno.Empleado = Negocio.Empleado.buscarId(empxturno.empleado.persona.id);
            dempxturno.TurnoDeTrabajo = Negocio.TurnoDeTrabajo.buscarId(empxturno.turnodetrabajo.id);
            dempxturno.fecha = empxturno.fecha;
            return dempxturno;
        }

        public static EntityCollection<Datos.EmpleadoXTurno> InvertirLista(IEnumerable<Models.EmpleadoXTurno> empxturnos)
        {
            EntityCollection<Datos.EmpleadoXTurno> a = new EntityCollection<Datos.EmpleadoXTurno>();
            foreach (var emp in empxturnos)
            {
                a.Add(Invertir(emp));
            }
            return a;
        }

        //negocio

        //public  static void insertar(Models.EmpleadoXTurno empxturno)      
        //{
        //    empxturno.fecha = DateTime.Now;
        //    Negocio.EmpleadoXTurno.insertar(Invertir(empxturno));
        //}

        //public static void modificar(Models.EmpleadoXTurno empxturno)
        //{
        //    Negocio.EmpleadoXTurno.modificar(Invertir(empxturno));
        //}
    }

}