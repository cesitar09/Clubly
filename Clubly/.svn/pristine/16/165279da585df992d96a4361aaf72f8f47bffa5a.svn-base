using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Negocio.Util;
using System.Data.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;


namespace Web.Models
{
    public class Cancha
    {
        public const int OBSOLETA = 2;

        public Models.Sede sede { get; set; }
        public Models.TipoCancha tipoCancha { get; set; }
        public short id { get; set; }
        public String descripcion { get; set; }
        public short numero { get; set; }
        public short estado { get; set; }
        public static ListaEstados listaEstados = new ListaEstados();

        static Cancha()
        {
            listaEstados.AgregarEstado(OBSOLETA, "Obsoleta");
        }
        
//CONSTRUCTOR
         public Cancha(Datos.Cancha cancha) 
        {
            id = cancha.id;
            descripcion = cancha.descripcion;
            estado = cancha.estado;
            tipoCancha = Models.TipoCancha.Convertir(cancha.TipoCancha);
            sede = Models.Sede.Convertir(cancha.Sede);
            numero = cancha.numero;
        }

//CONVERTIDORES
        //Convertir un Dato a Model
        public static Models.Cancha Convertir(Datos.Cancha cancha)
        {
            return new Cancha(cancha);
        }

        //Convertir un arreglo de Datos a model
        public static IEnumerable<Models.Cancha> ConvertirLista(IEnumerable<Datos.Cancha> dListaCanchas)
        {
            return dListaCanchas.Select(cancha => new Models.Cancha(cancha));
        }

        //Convierte un Model a Dato
        public static Datos.Cancha Invertir(Models.Cancha modelCancha)
        {
            Datos.Cancha dCancha = new Datos.Cancha();

            dCancha.id = modelCancha.id;
            dCancha.descripcion = modelCancha.descripcion;
            dCancha.numero = modelCancha.numero;
            dCancha.estado = modelCancha.estado;
            dCancha.TipoCancha = Negocio.TipoCancha.BuscarId(modelCancha.tipoCancha.id);
            dCancha.Sede = Negocio.Sede.buscarId(modelCancha.sede.id);

            return dCancha;
        }

        public static IEnumerable<Datos.Cancha> ConvertirListaInverso(IEnumerable<Models.Cancha> mCancha)
        {
            return mCancha.Select(p => Invertir(p));
        }

//QUERY
        public static IEnumerable<Models.Cancha> SeleccionarTodo(){
            return(ConvertirLista(Negocio.Cancha.SeleccionarTodo()));
        }

        public static Models.Cancha buscarId(short id)
        {
            return Convertir(Negocio.Cancha.BuscarId(id));
        }

        public static IEnumerable<Models.Cancha> buscarTipo(short tipoCancha)
        {
            return ConvertirLista(Negocio.Cancha.BuscarTipo(tipoCancha));
        }

        public static IEnumerable<Models.Cancha> BuscarSedeTipo(short sede, short tipoCancha)
        {
            return Models.Cancha.ConvertirLista(Negocio.Cancha.BuscarSedeTipo(sede, tipoCancha));
        }
    }
}