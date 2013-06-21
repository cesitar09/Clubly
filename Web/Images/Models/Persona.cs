using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Negocio.Util;

namespace Web.Models
{
    public class Persona
    {
        //atributos
        [Display(Name = "Id")]
        public short id { get; set; }

        [Required(ErrorMessage = " Ingrese un nombre")]
        [Display(Name = "Nombre")]
        
        public string nombre { get; set; }

        [Required(ErrorMessage = " Ingrese un apellido paterno")]
        [Display(Name = "Ap. Paterno")]
        public string apPaterno { get; set; }

        [Required(ErrorMessage = " Ingrese un apellido materno")]
        [Display(Name = "Ap. Materno")]
        public string apMaterno { get; set; }

        [Required(ErrorMessage = " Ingrese DNI")]
        [Display(Name = "DNI")]
        public Int32 dni { get; set; }

        [Required(ErrorMessage = " Ingrese una direccion")]
        [Display(Name = "Dirección")]
        public string direccion { get; set; }

        [Required(ErrorMessage = " Seleccione una opcion")]
        [Display(Name = "Estado Civil")]
        public string estadoCivil { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public short estado { get; set; }

        public static ListaEstados listaEstados = new ListaEstados();
        public static List<Estado_Civil> listestadocivil { get; set; }
       
        public class Estado_Civil {
            public short id { get; set; }
            public string nombre { get; set; }

           public Estado_Civil(short i, string n) {
                id = i;
                nombre = n;
            }
        }
        static Persona() {
            listaEstados = new ListaEstados();
            listaEstados.AgregarEstado(1, "Soltero(a)");
            listaEstados.AgregarEstado(2, "Casado(a)");
            Estado_Civil estado1 = new Estado_Civil(1,"Soltero(a)");
            Estado_Civil estado2 = new Estado_Civil(2,"Casado(a)");
            listestadocivil = new List<Estado_Civil>();
            listestadocivil.Add(estado1);
            listestadocivil.Add(estado2);
        }
        public Persona() {
         
        }

        //metodos para convertir
        public Persona(Datos.Persona persona)
        {
            id = persona.id;
            dni = persona.dni;
            nombre = persona.nombre;
            apPaterno = persona.apPaterno;
            apMaterno = persona.apMaterno;
            direccion = persona.direccion;
            estadoCivil = listaEstados.TextoEstado(persona.estadoCivil);
            estado = persona.estado;
        }
        public static Persona Convertir(Datos.Persona persona)
        {
            return new Persona(persona);
        }

        public static IList<Models.Persona> ConvertirLista(IEnumerable<Datos.Persona> personas)
        {
            return personas.Select(persona => new Models.Persona(persona)).ToList();
        }

        //metodos para invertir
        public static Datos.Persona Invertir(Models.Persona mPersona)
        {
            Datos.Persona dPersona = new Datos.Persona();
            dPersona.id = mPersona.id;
            dPersona.dni = mPersona.dni;
            dPersona.nombre = mPersona.nombre;
            dPersona.apPaterno = mPersona.apPaterno;
            dPersona.apMaterno = mPersona.apMaterno;
            dPersona.direccion = mPersona.direccion;
            dPersona.estadoCivil = listaEstados.EstadoTexto(mPersona.estadoCivil);
            dPersona.estado = mPersona.estado;
            return dPersona;
        }
        public static IList<Datos.Persona> InvertirLista(IEnumerable<Models.Persona> personas)
        {
            return personas.Select(persona => Models.Persona.Invertir(persona)).ToList();
        }

        public static Models.Persona buscarID(short id) {

            return Convertir(Negocio.Persona.buscarID(id));
        }

        public static IEnumerable<Models.Persona> seleccionarTodo() {
            return ConvertirLista(Negocio.Persona.seleccionarTodo());
        }

        public static void eliminar(Models.Persona persona){
            Negocio.Persona.eliminar(Invertir(persona));
        }
    }
}