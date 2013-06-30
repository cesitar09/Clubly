using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Negocio.Util;
using System.Data.Objects.DataClasses;

namespace Web.Models
{
    public class EmpleadoXSede
    {
        public Empleado empleado{get; set;}
        
        [DisplayName("sede")]
        public Sede sede { get; set; }

        public DateTime fecha{get; set;}

        public short? estado { get; set; }


        public EmpleadoXSede() { 
        }

        public EmpleadoXSede(Datos.EmpleadoXSede empxsede) {
            empleado = new Empleado(empxsede.Empleado);
            sede = new Sede(empxsede.Sede);
            fecha = empxsede.fecha;
            estado = empxsede.estado;
        }

        public static EmpleadoXSede Convertir(Datos.EmpleadoXSede empxsede) {
            return new EmpleadoXSede(empxsede);
        }

        public static IEnumerable<Models.EmpleadoXSede> ConvertirLista(IEnumerable<Datos.EmpleadoXSede> empxsedes)
        {
            return empxsedes.Select(empxsede => new Models.EmpleadoXSede(empxsede));
        }

        public static Datos.EmpleadoXSede Invertir(EmpleadoXSede empxsede){
            Datos.EmpleadoXSede dempxsede = new Datos.EmpleadoXSede();
            dempxsede.Empleado = Negocio.Empleado.buscarId(empxsede.empleado.persona.id);
            dempxsede.Sede = Negocio.Sede.buscarId(empxsede.sede.id);
            dempxsede.fecha = empxsede.fecha;
            dempxsede.estado = empxsede.estado;
            return dempxsede;
        }
        public static EntityCollection<Datos.EmpleadoXSede> InvertirLista(IEnumerable<Models.EmpleadoXSede> empxsedes)
        {
            EntityCollection<Datos.EmpleadoXSede> a = new EntityCollection<Datos.EmpleadoXSede>();
            foreach (var emp in empxsedes)
            {
                a.Add(Invertir(emp));
            }
            return a;
        }


    }
}