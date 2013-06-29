using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;


namespace Negocio
{
    public class Socio
    {
        public const bool TITULAR = true;   //atributo tipo
        public const bool NOTITULAR = false; //atributo tipo

        public static Entities context()
        {
            return Datos.Context.context();
        }

        //INSERTAR SOCIO TITULAR.. llamado desde Aceptar Solicitud de Membresia.
        public static void InsertarTitular(Datos.Socio socio)
        {

                socio.titular = true;
                context().Socio.AddObject(socio);
                context().SaveChanges();
            
        }

        //INSERTAR SOCIO NO TITULAR.. llamado desde la vista registrar socio no titular.
        public static void InsertarNoTitular(Datos.Socio socio)
        {
            socio.titular = false;
            context().Socio.AddObject(socio);
            context().SaveChanges();
          
        }

        //Lista de todos los socios
        public static IQueryable<Datos.Socio> SeleccionarTodo()
        {
            IQueryable<Datos.Socio> listaSocios = context().Socio.Where(socio => socio.Persona.estado != 0);
            return listaSocios;
        }

        //Socio buscado por Id
        public static Datos.Socio BuscarId(short id)
        {
            return context().Socio.Single(p => p.id == id);
        }

                        
        //Buscar por Id de familia
        public static IEnumerable<Datos.Socio> BuscarIdFamilia(short idFamilia)
        {
            IEnumerable<Datos.Socio> socios = context().Socio;
            return socios.Where(s => ((s.Familia.id == idFamilia) && (s.Persona.estado != 0)));
            //return socios;
        }

        //Buscar Socios Titulares
        public static IEnumerable<Datos.Socio> BuscarSociosTitulares()
        {
            IEnumerable<Datos.Socio> socios = context().Socio.Where(s => (s.titular == true) && (s.Persona.estado != 0));
            return socios;
        }

        public static Datos.Socio BuscarSocioTitular(short id)
        {
            Datos.Socio socio = context().Socio.FirstOrDefault(s => (s.Familia.id == id) && (s.titular == true) && (s.Persona.estado != 0));
            return socio;
        }

        //Buscar Socios No Titulares
        public static IEnumerable<Datos.Socio> BuscarSociosNoTitulares()
        {
            IEnumerable<Datos.Socio> socios = context().Socio.Where(s => (s.titular == false) && (s.Persona.estado != 0));
            return socios;
        }

        //El Eliminar y el modificar se manejan directamente desde Datos.Persona ya que son los unicos datos del socio que pueden modificarse.
    }
}
