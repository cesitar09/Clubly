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
           
 //-------------Atributos del objeto *modelo*        
        public Models.Persona persona { get; set; }
        
        [Display(Name = "Familia")]
        [Required]
        public Familia familia { get; set; }
        public bool estado { get; set; }

//--------------Constructor de objeto *modelo*
        public Socio() {
            persona = new Persona();
            familia = new Familia();
        }
        public Socio(Models.Persona per, Models.Familia fam)
        {
            persona = per;
            familia = fam;
        }

//--------------Convertir de Datos a Objeto Socio:
        public Socio(Datos.Socio socio)
        {
            persona = Models.Persona.Convertir(socio.Persona!=null? socio.Persona:Negocio.Persona.buscarID(socio.id));  //evita los errores cuando Socio no ha cargado la referencia a Persona
           
            while (socio.Familia == null)   //Lo mismo que para Persona
            {
                socio = Negocio.Socio.BuscarId(socio.id);
            }
            familia = new Familia(socio.Familia);

            estado = socio.titular;
           
        }


        public static Models.Socio Convertir(Datos.Socio socio)
        {
            return new Socio(socio);
        }

//------------Convertir a lista de objetos socio:
        public static IEnumerable<Models.Socio> ConvertirLista(IEnumerable<Datos.Socio> listaSocios)
        {
            return listaSocios.Select(socio => new Models.Socio(socio));
        }

//-----------Convertir de objeto a datos:
        public static Datos.Socio Invertir(Models.Socio socio)
        {
            Datos.Socio datosSocio = new Datos.Socio();
            Datos.Familia fam;
            if (socio.familia.id == 0)
            {
                fam = Models.Familia.Invertir(socio.familia);
                Negocio.Familia.insertar(fam);
            }
            else
            {
                fam=Negocio.Familia.buscarId(socio.familia.id);
            }
            Datos.Persona per = Models.Persona.Invertir(socio.persona);
            datosSocio.Persona = per;
            datosSocio.Persona.Socio = datosSocio;
            datosSocio.Familia = fam;
            
            return datosSocio;
        }

//--------Llamada a metodos de Negocio: 
        //Insertar No Titular
        public static void InsertarNoTitular(Models.Socio socio)
        {
            socio.persona.estado = ListaEstados.ESTADO_ACTIVO;
            Negocio.Socio.InsertarNoTitular(Invertir(socio));
        }

        //Insertar titular
        public static void InsertarTitular(Models.Socio socio)
        {
            socio.persona.estado = ListaEstados.ESTADO_ACTIVO;
            Negocio.Socio.InsertarTitular(Invertir(socio));
        }

        //Listo de Socios activos
        public static IEnumerable<Models.Socio> SeleccionarTodo()
        {
            return ConvertirLista(Negocio.Socio.SeleccionarTodo());
        }

        //Buscar Socio por Id
        public static Models.Socio BuscarId(short id)
        {
            return Convertir(Negocio.Socio.BuscarId(id));
        }

        //Buscar Socio por familia
        public static IEnumerable<Models.Socio> BuscarIdFamilia(short id)
        {
            return ConvertirLista(Negocio.Socio.BuscarIdFamilia(id));
        }

        //Buscar Socios Titulares
        public static IEnumerable<Models.Socio> BuscarTitulares()
        {
            return ConvertirLista(Negocio.Socio.BuscarSociosTitulares());
        }

        //Buscar Socios No Titulares
        public static IEnumerable<Models.Socio> BuscarNoTitulares()
        {
            return ConvertirLista(Negocio.Socio.BuscarSociosNoTitulares());
        }

//--------Llamada a metodos de Negocio de Persona>
        //Modificar
        public static void Modificar(Models.Socio socio)
        {
            Negocio.Persona.modificar(Models.Persona.Invertir(socio.persona));
        }

        //Eliminar
        public static void Eliminar(Models.Socio socio)
        {
            Negocio.Persona.eliminar(Models.Persona.Invertir(socio.persona));
        }

        

    }
}