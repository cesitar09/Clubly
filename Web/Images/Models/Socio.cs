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
    public class Socio
    {
        //public short id { get; set; }
        public Models.Persona persona { get; set; }
        //atributos
        //[ScaffoldColumn(false)]
        //public short id { get; set; }

        [Display(Name = "Familia")]
        [Required]
        public Familia familia { get; set; }

        public Socio() { }
        //Objeto Socio:
        public Socio(Datos.Socio socio)
        {
            persona = Models.Persona.Convertir(socio.Persona!=null? socio.Persona:Negocio.Persona.buscarID(socio.id));  //evita los errores cuando Socio no ha cargado la referencia a Persona
            /*
            id = socio.Persona.id;
            dni = socio.Persona.dni;
            nombre = socio.Persona.nombre;
            apPaterno = socio.Persona.apPaterno;
            apMaterno = socio.Persona.apMaterno;
            direccion = socio.Persona.direccion;
            estadoCivil = listaEstados.TextoEstado(socio.Persona.estadoCivil);
            estado = socio.Persona.estado;
            */
            while (socio.Familia == null)   //Lo mismo que para Persona
            {
                socio = Negocio.Socio.buscarId(socio.id);
            }
            familia = new Familia(socio.Familia);
           
        }
        //Convertir a objeto Socio:
        public static Models.Socio Convertir(Datos.Socio socio)
        {
            return new Socio(socio);
        }
        //Convertir a lista de objetos socio:
        public static IEnumerable<Models.Socio> ConvertirLista(IEnumerable<Datos.Socio> listaSocios)
        {
            return listaSocios.Select(socio => new Models.Socio(socio));
        }
        //Convertir a datos:
        public static Datos.Socio Invertir(Models.Socio socio)
        {
            Datos.Socio datosSocio = new Datos.Socio();
            datosSocio.id = socio.persona.id;
            datosSocio.Persona = Models.Persona.Invertir(socio.persona);
            datosSocio.Persona.Socio = datosSocio;
            datosSocio.Familia = Models.Familia.Invertir(socio.familia);
            return datosSocio;
        }

        //Negocio: Insertar, Modificar, Eliminar, Seleccionar
        public static void insertarNoTitular(Models.Socio socio)
        {
            socio.persona.estado = ListaEstados.ESTADO_ACTIVO;
            Negocio.Socio.insertarNoTitular(Invertir(socio));
        }

        public static void modificar(Models.Socio socio)
        {
            Negocio.Socio.modificar(Invertir(socio));
        }

        public static void eliminar(Models.Socio socio)
        {
            Negocio.Socio.eliminar(Invertir(socio));
        }

        public static IEnumerable<Models.Socio> seleccionarTodo()
        {
            return ConvertirLista(Negocio.Socio.seleccionarTodo());
        }

        public static Models.Socio buscarId(short id)
        {
            return Convertir(Negocio.Socio.buscarId(id));
        }

        public static IEnumerable<Models.Socio> BuscarIdFamilia(short id)
        {
              return ConvertirLista(Negocio.Socio.buscarIdFamilia(id));
        }


    }
}