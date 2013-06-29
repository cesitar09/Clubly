using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;

namespace Negocio
{
    public class Familia
    {   
        public const short VITALICIO = 1;   //atributo tipo
        public const short NOVITALICIO = 0; //atributo tipo

        public static Entities context()
        {
            return Context.context();
        }
//------------------
        public static Exception insertar(Datos.Familia familia)
        {
            try
            {
                familia.estado = 1;
                context().Familia.AddObject(familia);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

//------------------------

        public static IEnumerable<Datos.Familia> seleccionarTodo()
        {
            IEnumerable<Datos.Familia> listaFamilias = context().Familia.Where(p => p.estado == 1);
            return listaFamilias;
        }

//------------------------

        public static IEnumerable<Datos.Familia> SeleccionarNoVitalicio()
        {
            IEnumerable<Datos.Familia> listaFamilias = context().Familia.Where(p => p.estado == 1 && p.tipo== NOVITALICIO);
            return listaFamilias;
        }

//------------------------
        public static IEnumerable<Datos.Familia> seleccionarPuntual()
        {
            IEnumerable<Datos.Familia> listaFamilias = context().Familia.Where(p => p.estado == 1);
            return listaFamilias;
        }

//------------------------
        public static short PrimerId()
        {
            return seleccionarTodo().FirstOrDefault().id;
        }

//------------------------
        public static Datos.Familia buscarId(short id)
        {
            return context().Familia.Single(p => p.id == id);
        }

//------------------------
        public static Datos.Familia buscarPorSolicitud(short id)
        {
            return context().Familia.Single(p => p.SolicitudMembresia.id == id);
        }


//------------------------
        public static Exception modificar(Datos.Familia familia)
        {
            try
            {
                context().Familia.ApplyCurrentValues(familia);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

//------------------------
        public static Exception eliminar(Datos.Familia familia)
        {
            try
            {
                familia.estado = 0;
                context().Familia.ApplyCurrentValues(familia);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

//------------------------

        public static Datos.Familia buscarIdUsuario(short idUsuario)
        {
            return context().Familia.FirstOrDefault(f=>f.Usuario.id==idUsuario);
        }

//-------------------------- (no se por qué ponen esto pero yo tambien lo puse (Tavo).
//      Este método cuenta el numero de invitados que ya ha registrado la familia en el mes.
//      Sirve para compararlo con el número maximo de invitados y determinar si requiere pago.
//      No se si el método deberia ir aqui o en InvitadoXFamilia.
        public static int NumeroInvitados(short idFamilia)
        {
            DateTime fecha = DateTime.Today;
            int num =Context.context().InvitadoXFamilia.Where
                (i => ( (i.estado != 0) && (i.fechaIngreso.Year == fecha.Year) && (i.fechaIngreso.Month == fecha.Month) )).Count();
            return num;
        }

        internal static Datos.Familia buscarIdSocio(short idSocio)
        {
            return Context.context().Socio.FirstOrDefault(p=>p.id==idSocio).Familia;
        }

         public static IEnumerable<Datos.Pago> pagosPendientes(short idfamilia) {
             IEnumerable<Datos.Pago>dp = context().Pago.Where(q => q.estado == Pago.PENDIENTE && q.Familia.id == idfamilia);
             return dp;
        }

         public static IEnumerable<Datos.ReservaBungalow> reservaPendientes( short idfamilia) {

             return context().ReservaBungalow.Where(q => q.Familia.id == idfamilia && (q.estado == ReservaBungalow.PORPAGAR || q.estado == ReservaBungalow.NOINGRESADO));
         }

         public static IEnumerable<Datos.ReservaBungalowSorteo> SorteoPendiente(short idfamilia)
         {
             return context().ReservaBungalowSorteo.Where(q => q.Familia.id == idfamilia && (q.estado == ReservaBungalowSorteo.PENDIENTE));
         }

         public static IEnumerable<Datos.ReservaCancha> canchasPendientes(short idfamilia) 
         { 
            return context().ReservaCancha.Where(q => q.Familia.id == idfamilia && q.estado == ReservaCancha.PENDIENTE);     
         }

         public static IEnumerable<Datos.ReservaCamping> campingPendientes(short idfamilia) 
         {
             return context().ReservaCamping.Where(q => q.Familia.id == idfamilia && q.estado == ReservaCamping.PENDIENTE);
         }
    }

}