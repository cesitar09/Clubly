using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;

namespace Negocio
{
    public class Pago
    {
        public const short PENDIENTE = 1;
        public const short PORDEVOLVER = 2;
        public const short DEVUELTO = 3;
        public const short VENCIDO = 4;
        public const short CANCELADO = 5;

        public static Entities context()
        {
            return Datos.Context.context();
        }

        public static void Insertar(Datos.Pago pago)
        {
            
                pago.estado = PENDIENTE;
                context().Pago.AddObject(pago);
                context().SaveChanges();
            
           
        }

        //public static void GenerarPagosMembresia(Datos.Pago pago)
        //{ }
        public static void Cancelar(Datos.Pago pago)
        {
                pago.estado = CANCELADO;
                context().Pago.ApplyCurrentValues(pago);
                context().SaveChanges();
           
        }

        public static void modificar(Datos.Pago pago)
        {
           
                context().Pago.ApplyCurrentValues(pago);
                context().SaveChanges();
           
        }

        //Esto es un seleccionar todo para las pagos

        public static IEnumerable<Datos.Pago> SeleccionarTodo()
        {

            VerificarVencimiento();
            IEnumerable<Datos.Pago> listaPago = context().Pago.Where(p => p.estado != 0);
            
            return listaPago;

        }

        public static IEnumerable<Datos.Pago> SeleccionarCuotas()
        {

            //VerificarVencimiento();
            IEnumerable<Datos.Pago> listaPago = context().Pago.Where(p => p.ConceptoDePago.id==6 && (p.estado ==1 || p.estado == 4));

            return listaPago;

        }

        private static void VerificarVencimiento()
        {
            IEnumerable<Datos.Pago> listaPago = context().Pago.Where(p => (p.estado == 1 && p.fechaLimite.CompareTo(DateTime.Now)<0));
            foreach (Datos.Pago pago in listaPago)
            {
                pago.estado = VENCIDO;
                if (pago.ConceptoDePago.id == 2) // Si se vencio la membresia se le agrega la multa
                    pago.monto = pago.monto + Negocio.Parametros.SeleccionarParametros().multa;
                if (pago.ConceptoDePago.id == 3) // Si se vencio la actividad, se elimina la inscripcion
                    Negocio.SocioXActividad.Eliminar(pago.SocioXActividad.FirstOrDefault());
                //if (pago.ConceptoDePago.id == 4) 
                  
            }
            context().SaveChanges();
        }

        public static IEnumerable<Datos.Pago> SeleccionarPorFamilia(short id)
        {

            IEnumerable<Datos.Pago> listaPago = context().Pago.Where(p =>( p.Familia.id == id) && (p.estado != 0));
            return listaPago;

        }

        public static Datos.Pago BuscarId(short id)
        {
            return context().Pago.FirstOrDefault(p => p.id == id);
        }



        public static void GenerarPagosMembresia()
        {
            IEnumerable<Datos.Familia> listaFamilias = Familia.SeleccionarNoVitalicio();
           
            foreach (Datos.Familia familia in listaFamilias)
            {
                Datos.Pago pago = new Datos.Pago();
                pago.ConceptoDePago = ConceptoDePago.buscarId(ConceptoDePago.ID_MEMBRESIA);
                if(pago.ConceptoDePago!=null)
                        pago.monto = pago.ConceptoDePago.monto.Value ;
                else 
                        pago.monto = 0;
            //{
            //    foreach(Datos.Pago paguito in familia.Pago){
            //        if (paguito.estado == Negocio.Pago.PORDEVOLVER)
            //        {
            //            if (paguito.monto <= monto)
            //            {
            //                montoDevolver = paguito.monto;
            //                paguito.estado = Negocio.Pago.DEVUELTO;
            //            }
            //            else
            //            {

            //            }

            //        }
            //    }             
               
                pago.descripcion = "Membresia del mes "+DateTime.Today.Month.ToString();//cambiar
                pago.montoDevolver = 0;
                pago.fechaRegistro = DateTime.Now;
                pago.fechaLimite = DateTime.Now.AddDays(Parametros.SeleccionarParametros().diasLimitePago);
                
                pago.estado = PENDIENTE;
                familia.Pago.Add(pago);
            }
            Context.context().SaveChanges();
        }

        public static IEnumerable<Datos.Pago> SeleccionarPagosPorDevolver(short idFamilia)
        {
            return Context.context().Pago.Where(p => p.estado == PORDEVOLVER && p.Familia.id == idFamilia);
        }
    }
}