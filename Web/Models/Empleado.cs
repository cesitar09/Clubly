using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
using System.Data.Objects.DataClasses;

namespace Web.Models
{
    public class Empleado
    {
        
        //public short id { get; set; }
        public Models.Persona persona { get; set; }

        [DisplayName("*Sueldo")]
        [Required(ErrorMessage = " Ingrese un valor")]
        [Range(00.1, 999999, ErrorMessage= "Ingrese un numero valido")]
        public double sueldo { get; set; }
        
        [DisplayName("*Cargo")]
        [Required(ErrorMessage = "Seleccione un tipo de empleado ")]
         public Models.TipoEmpleado tipoEmpleado { get; set; }

        [DisplayName("*Turno de trabajo")]
       public Models.TurnoDeTrabajo turnodetrabajo { get; set; }

        //[DisplayName("Usuario")]
        //public Models.Usuario usuario { get; set; }

      
       [DisplayName("*Sede")]
       [Required(ErrorMessage= " Seleccione una Sede" )]
        public Models.Sede sede {get; set;}
        
        //metodos para convertir
        public Empleado() {
            persona = new Persona();
            tipoEmpleado = new TipoEmpleado();
            turnodetrabajo = new TurnoDeTrabajo();
            sede = new Sede();
        }
        public Empleado(Datos.Empleado empleado)
        {
            //Datos.Persona persona = empleado.Persona;
            //Atributos heredados de Persona
          //  id = empleado.id;
            persona = Models.Persona.Convertir(empleado.Persona);
            //nombre = persona.nombre;
            //apPaterno = persona.apPaterno;
            //apMaterno = persona.apMaterno;
            //direccion = persona.direccion;
            //estadoCivil = persona.estadoCivil;
            //estado = persona.estado;
            //Atributos de Empleado
            sueldo = empleado.sueldo;
            tipoEmpleado = Models.TipoEmpleado.Convertir(empleado.TipoEmpleado);
            if (empleado.TurnoDeTrabajo != null)
            {
                turnodetrabajo = Models.TurnoDeTrabajo.Convertir(empleado.TurnoDeTrabajo);
            }
            sede = Models.Sede.Convertir(empleado.Sede);
        }
        public static Models.Empleado Convertir(Datos.Empleado empleado)
        {    
            return new Empleado(empleado);
        }
        public static IEnumerable<Models.Empleado> ConvertirLista(IEnumerable<Datos.Empleado> empleados)
        {
            return empleados.Select(empleado => new Models.Empleado(empleado));
        }

        //metodos para invertir
        public static Datos.Empleado Invertir(Models.Empleado empleado)
        {
            Datos.Empleado Dempleado = new Datos.Empleado();
            //Dempleado.id = empleado.id;
            Dempleado.sueldo = empleado.sueldo;
            Dempleado.TipoEmpleado = Negocio.TipoEmpleado.buscarId(empleado.tipoEmpleado.id);
            if (empleado.turnodetrabajo != null)
            {
                Dempleado.TurnoDeTrabajo = Negocio.TurnoDeTrabajo.buscarId(empleado.turnodetrabajo.id);
            }
            //Dempleado.EmpleadoXTurno = Models.EmpleadoXTurno.InvertirLista(empleado.empleadoxturno);
            //Dempleado.EmpleadoXSede = Models.EmpleadoXSede.InvertirLista(empleado.empleadoxsede);
            Datos.Persona per= Models.Persona.Invertir(empleado.persona);
            Dempleado.Persona = per;
            Dempleado.Persona.Empleado = Dempleado;
            Dempleado.Sede = Negocio.Sede.buscarId(empleado.sede.id);
            return Dempleado;
        }
        public static EntityCollection<Datos.Empleado> InvertirLista(IEnumerable<Models.Empleado> empleados)
        {
            EntityCollection<Datos.Empleado> a = new EntityCollection<Datos.Empleado>();
            foreach (var emp in empleados)
            {
                a.Add(Invertir(emp));
            }
            return a;
        }

        //Negocio
        public static void insertar(Models.Empleado empleado)
        {
            empleado.persona.estado = 1;
            Negocio.Empleado.insertar(Invertir(empleado));
                
        }
        public static void modificar(Models.Empleado emp, Models.Empleado empleado)
        {
            
            Datos.Empleado e = Negocio.Empleado.buscarId(emp.persona.id);
            e.Persona.apMaterno = empleado.persona.apMaterno;
            e.Persona.apPaterno = empleado.persona.apPaterno;
            e.Persona.nombre = empleado.persona.nombre;
            e.Persona.estadoCivil = Models.Persona.listaEstados.EstadoTexto(empleado.persona.estadoCivil);
           // e.Persona.estado = empleado.persona.estado;
            e.Persona.direccion = empleado.persona.direccion;
            e.Persona.dni = empleado.persona.dni;
            e.sueldo = empleado.sueldo;
            e.Sede = Negocio.Sede.buscarId(empleado.sede.id);
            e.TipoEmpleado = Negocio.TipoEmpleado.buscarId(empleado.tipoEmpleado.id);
            e.TurnoDeTrabajo = Negocio.TurnoDeTrabajo.buscarId(empleado.turnodetrabajo.id);
            Negocio.Empleado.modificar(e); 
         
        }
        public static void eliminar(Models.Empleado empleado)
        {
            Models.Persona persona = Models.Persona.buscarID(empleado.persona.id);
            Models.Persona.eliminar(persona);
            }
        public static IEnumerable<Models.Empleado> seleccionarTodo()
        {
            return ConvertirLista(Negocio.Empleado.seleccionarTodo());
        }
     
        public static Models.Empleado buscarId(long id)
        {
            Datos.Empleado emp = Negocio.Empleado.buscarId(id);
            if (emp != null) return Convertir(emp);
            else return null;
        }

        public static bool existeDni(Int32 dni, short id) {

            return Negocio.Empleado.existeDNI(dni, id);
        }
    }
}