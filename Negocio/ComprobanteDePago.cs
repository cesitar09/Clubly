using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;


namespace Negocio
{
    public class ComprobanteDePago
    {
        //context Singleton
        public static Entities context()
        {
            return Datos.Context.context();
        }

        public static Datos.ComprobanteDePago Insertar(List<short> IdPagos)
        {
            try
            {
                Datos.ComprobanteDePago comprobanteDePago = new Datos.ComprobanteDePago();
                foreach (short idPago in IdPagos)
                {

                    Datos.Pago pago = Negocio.Pago.BuscarId(idPago);
                    pago.estado = Pago.CANCELADO;
                    comprobanteDePago.Pago.Add(pago);
                    comprobanteDePago.montoTotal += pago.monto;
                    //si es reserva de bungalow, cambia el estado de la reserva de Pago Pendiente a No Ingresado
                    if (pago.ConceptoDePago.id == ConceptoDePago.ID_RESERVABUNGALOW)
                        pago.ReservaBungalow.First().estado = ReservaBungalow.NOINGRESADO;
                    if (pago.ConceptoDePago.id == ConceptoDePago.ID_MEMBRESIA)
                    {
                        double monto=pago.monto;
                        IEnumerable<Datos.Pago> listaPagosPorDevolver = Pago.SeleccionarPagosPorDevolver(pago.Familia.id);
                        foreach (Datos.Pago pordevolver in listaPagosPorDevolver)
                        {
                            if (pordevolver.montoDevolver >= monto)
                            {
                                comprobanteDePago.descuento += monto;
                                pordevolver.montoDevolver -= monto;
                                break;
                            }
                            else
                            {
                                comprobanteDePago.descuento += pordevolver.montoDevolver;
                                monto -= pordevolver.montoDevolver;
                                pordevolver.montoDevolver = 0;
                                pordevolver.estado = Pago.DEVUELTO;
                            }
                        }
                    }
                }
                comprobanteDePago.montoAPagar = comprobanteDePago.montoTotal - comprobanteDePago.descuento;
                comprobanteDePago.estado = 1;
                comprobanteDePago.fecha = DateTime.Now;
                context().ComprobanteDePago.AddObject(comprobanteDePago);
                context().SaveChanges();
                return comprobanteDePago;
            }
            catch
            {
                throw;
            }   
        }
        //public static Exception Insertar(Datos.ComprobanteDePago comprobanteDePago)
        //{
        //    try
        //    {
        //        comprobanteDePago.estado = 1;
        //        context().ComprobanteDePago.AddObject(comprobanteDePago);
        //        context().SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex;
        //    }
        //    return null;
        //}

        public static IEnumerable<Datos.ComprobanteDePago> SeleccionarTodo()
        {

            IEnumerable<Datos.ComprobanteDePago> listaComprobantes = context().ComprobanteDePago.Where(p => p.estado != 0);
            return listaComprobantes;

        }

        public static Datos.ComprobanteDePago BuscarId(short id)
        {
            return context().ComprobanteDePago.FirstOrDefault(p => p.id == id);
        }
    }
}
