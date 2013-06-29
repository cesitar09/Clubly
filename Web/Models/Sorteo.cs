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
    public class Sorteo
    {
        public TemporadaAlta temporadaAlta { set; get;}

        public TipoBungalow tipoBungalow { set; get; }

        public DateTime FechaSorteo { set; get; }

        public short numGanadores { set; get; }

        public String comentarios { set; get; }

        public String estado { set; get; }

        public Sede sede { set; get; }

        public static ListaEstados listaestados { set; get; }

        static Sorteo()
        {
            listaestados = new ListaEstados();

            listaestados.AgregarEstado(1, "Pendiente");
            listaestados.AgregarEstado(2, "Realizado");
            listaestados.AgregarEstado(3, "Cancelado");
        }

        public Sorteo(Datos.Sorteo sorteo)
        {
            temporadaAlta = Models.TemporadaAlta.Convertir(sorteo.TemporadaAlta);
            tipoBungalow = Models.TipoBungalow.Convertir(sorteo.TipoBungalow);
            FechaSorteo = sorteo.fechaSorteo;
            numGanadores = sorteo.numGanadores;
            comentarios = sorteo.comentarios;
            estado = listaestados.TextoEstado(sorteo.estado);
            sede = Models.Sede.Convertir(sorteo.Sede);
        }

        public Sorteo(TemporadaAlta temp, TipoBungalow tipoBung, Sede sed)
        {
            temporadaAlta = temp;
            tipoBungalow = tipoBung;
            sede = sed;
            FechaSorteo = temp.fechaInicio.AddDays(-7);
            numGanadores = 0;
            comentarios = "";
            estado = "Pendiente";
        }


        public static Sorteo Convertir(Datos.Sorteo sorteo)
        {
            return new Sorteo(sorteo);
        }

        public static IEnumerable<Sorteo> ConvertirLista(IEnumerable<Datos.Sorteo> ListaSorteo)
        {
            return ListaSorteo.Select(sorteo=> Convertir(sorteo));

        }

        public static Datos.Sorteo Invertir(Models.Sorteo sorteo)
        {
            
            Datos.Sorteo datosSorteo = new Datos.Sorteo();

            datosSorteo.TemporadaAlta = Models.TemporadaAlta.Invertir(sorteo.temporadaAlta);
            datosSorteo.TipoBungalow = Models.TipoBungalow.Invertir(sorteo.tipoBungalow);
            datosSorteo.fechaSorteo = sorteo.FechaSorteo;
            datosSorteo.numGanadores = sorteo.numGanadores;
            datosSorteo.comentarios = sorteo.comentarios;
            datosSorteo.estado = listaestados.EstadoTexto(sorteo.estado);
            datosSorteo.Sede = Models.Sede.Invertir(sorteo.sede);
           
            return datosSorteo;
        }

        public static EntityCollection<Datos.Sorteo> InvertirLista(IEnumerable<Models.Sorteo> ListaSorteo)
        {
            EntityCollection<Datos.Sorteo> DatosListaSorteo= new EntityCollection<Datos.Sorteo>();

            foreach (Sorteo sol in ListaSorteo)
            {
                DatosListaSorteo.Add(Invertir(sol));
            }

            return DatosListaSorteo;
        }


        public static Datos.Sorteo InvertirParaEliminar(Models.Sorteo sorteo)
        {

            Datos.Sorteo datosSorteo = Negocio.Sorteo.BuscarSorteoIds(sorteo.sede.id, sorteo.tipoBungalow.id, sorteo.temporadaAlta.id);

            datosSorteo.TemporadaAlta = Models.TemporadaAlta.Invertir(sorteo.temporadaAlta);
            datosSorteo.TipoBungalow = Models.TipoBungalow.Invertir(sorteo.tipoBungalow);
            datosSorteo.fechaSorteo = sorteo.FechaSorteo;
            datosSorteo.numGanadores = sorteo.numGanadores;
            datosSorteo.comentarios = sorteo.comentarios;
            datosSorteo.estado = listaestados.EstadoTexto(sorteo.estado);
            datosSorteo.Sede = Models.Sede.Invertir(sorteo.sede);

            return datosSorteo;
        }

        public static EntityCollection<Datos.Sorteo> InvertirListaParaEliminar(IEnumerable<Models.Sorteo> ListaSorteo)
        {
            EntityCollection<Datos.Sorteo> DatosListaSorteo = new EntityCollection<Datos.Sorteo>();

            foreach (Sorteo sol in ListaSorteo)
            {
                DatosListaSorteo.Add(InvertirParaEliminar(sol));
            }

            return DatosListaSorteo;
        }



        public static void insertarSorteos(TemporadaAlta temporadaAlta)
        {
            IEnumerable<Models.Sede> listaSedes= Sede.SeleccionarTodo();
             IEnumerable<Models.TipoBungalow> listaTipos= TipoBungalow.SeleccionarTodo();
            for (int i = 0; i < listaSedes.Count();i++ )
            {
                for (int j = 0; j < listaTipos.Count()-1; j++)
                {
                    Sorteo sorteo = new Sorteo(temporadaAlta, listaTipos.ElementAt(j), listaSedes.ElementAt(i));
                    Negocio.Sorteo.Insertar(Invertir(sorteo));     
                }
               
           }

        }
        public static void eliminarSorteos(TemporadaAlta temporadaAlta)
        {
            IEnumerable<Sorteo> SorteosAEliminar = Models.Sorteo.SeleccionarTodo().Where(s => s.temporadaAlta.id == temporadaAlta.id);
           // Negocio.Sorteo.Eliminar(InvertirListaParaEliminar(SorteosAEliminar));
            int i = 0;
            while(i < SorteosAEliminar.Count())
            {
                Negocio.Sorteo.Eliminar(InvertirParaEliminar(SorteosAEliminar.ElementAt(i)));
                
            }
        }

        public static IEnumerable<Sorteo> SeleccionarTodo()
        {
            IEnumerable<Datos.Sorteo> sorteo = Negocio.Sorteo.SeleccionarTodo();
            return ConvertirLista(sorteo);
        }

        public static IEnumerable<Sorteo> SeleccionarPendientes()
        {
            IEnumerable<Datos.Sorteo> sorteo = Negocio.Sorteo.SeleccionarPendientes();
            return ConvertirLista(sorteo);
        }


        public static IEnumerable<Sorteo> ObtenerProximos()
        {
            IEnumerable<Sorteo> sorteos = SeleccionarPendientes().Where(s => s.FechaSorteo == DateTime.Now);
            if (sorteos.Count() > 0)
            {

                return sorteos;
            }
              else return null;

        }

        public static IEnumerable<int> UniqueRandom(int minInclusive, int maxInclusive)
        {
            List<int> candidates = new List<int>();
            for (int i = minInclusive; i <= maxInclusive; i++)
            {
                candidates.Add(i);
            }
            Random rnd = new Random();
            while (candidates.Count > 0)
            {
                int index = rnd.Next(candidates.Count);
                yield return candidates[index];
                candidates.RemoveAt(index);
            }
        }

        public static void RealizarSorteo()
        {

            IEnumerable<Sorteo> SorteosARealizar = ObtenerProximos();
        //    IEnumerable<Models.ReservaBungalowSorteo> personasSorteo = Models.ReservaBungalowSorteo.seleccionarProximo(SorteosARealizar); 
       //     int numInscritosSorteo = personasSorteo.count();

            /*          foreach (int i in UniqueRandom(0, 10))
                      {
                      //    int j =  Models.ReservaBungalow(personasSorteo.elementAt(i));
                          if (j == 0)
                          {
                              personasSorteo.elementAr(i).Resultado = "Ganador";
                          }
                          else personasSorteo.elementAr(i).Resultado = "Perdedor";
                            
             *            personasSorteo.elementAr(i).Estado = "Finalizado";
             *            
                              Models.ReservaBungalowSorteo.Modificar(personasSorteo.elementAr(i)); 
                      }*/



        }
    }
}