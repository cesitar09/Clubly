using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using System.Collections;

namespace Negocio
{
    public class ConceptoDePago
    {
        public const short ID_MEMBRESIA = 1;
        public const short ID_ACTIVIDAD = 2;
        public const short ID_INGRESOINVITADOS = 3;
        public const short ID_RESERVABUNGALOW = 4;
        public const short ID_SOLICITUDMEMBRESIA = 5;
        public const short ID_CUOTAEXTRAORDINADIA = 6;

        public static Entities context()
        {
            return Datos.Context.context();
        }

        public static Exception insertar(Datos.ConceptoDePago conceptoDePago)
        {
            try
            {
                context().ConceptoDePago.AddObject(conceptoDePago);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static IEnumerable<Datos.ConceptoDePago> SeleccionarTodoTiposDePago()
        {
            return context().ConceptoDePago.Where(conceptoDePago => conceptoDePago.estado != 0);
        }

      
        public static Datos.ConceptoDePago buscarId(short id)
        {
            return context().ConceptoDePago.FirstOrDefault(p => p.id == id);
        }

        public static Exception modificar(Datos.ConceptoDePago conceptoDePago)
        {
            try
            {
                context().ConceptoDePago.ApplyCurrentValues(conceptoDePago);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        //public static IEnumerable<Datos.Actividad> seleccionarTodo()
        //{
        //    List<Datos.Actividad> listaActividad = context().Actividad.Where(p => p.estado == 1).ToList();
        //    return listaActividad;
        //}

        //public static Datos.Actividad seleccionarId(short id)
        //{
        //    Datos.Actividad actividad = context().Actividad.Single(p => p.id == id);
        //    return actividad;
        //}

    }
}
