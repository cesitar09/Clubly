using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using Negocio.Util;

namespace Negocio
{
    public class ReservaBungalow
    {
        public const short PORPAGAR = 1;
        public const short NOINGRESADO = 2;
        public const short INGRESADO = 3;
        public const short TERMINADO = 4;

        // Selecciona reservas con estado "Ingresado" o "No Ingresado" para mostrarlos en la vista "Registrar Ingreso"
        public static IEnumerable<Datos.ReservaBungalow> SeleccionarIngreso()
        {
            return Context.context().ReservaBungalow.Where(reserva=>
                (    reserva.estado >= 2)    &&      //compara los estados
                (   (reserva.fechaInicio.Day==DateTime.Now.Day) &&          //compara la fecha con la fecha de hoy
                    (reserva.fechaInicio.Month==DateTime.Now.Month) &&      //(no se puede registrar un ingreso para una reserva que no es de hoy)
                    (reserva.fechaInicio.Year==DateTime.Now.Year)  )
                );
        }


        public static void ModificarReserva(Datos.ReservaBungalow reserva)
        {
            Datos.ReservaBungalow auxReserva=Context.context().ReservaBungalow.Single(p => p.id == reserva.id);
            auxReserva.Bungalow = reserva.Bungalow;
            auxReserva.Evento = reserva.Evento;
            auxReserva.Familia = reserva.Familia;
            auxReserva.fechaFin = reserva.fechaFin;
            auxReserva.fechaInicio = reserva.fechaInicio;
            auxReserva.Pago = reserva.Pago;
            Context.context().SaveChanges();
        }

        public static void RegistrarReserva(Datos.ReservaBungalow reserva)
        {
            Context.context().ReservaBungalow.AddObject(reserva);
            Context.context().SaveChanges();
        }

        public static void RegistrarIngresoBungalow(short? id)
        {
            if (id.HasValue)
                Context.context().ReservaBungalow.FirstOrDefault(r => r.id == id).estado = INGRESADO;
        }

        public static void RegistrarSalidaBungalow(short? id)
        {
            if (id.HasValue)
                Context.context().ReservaBungalow.FirstOrDefault(r => r.id == id).estado = TERMINADO;
        }

        public static IList<DateTime> Disponibilidad(short idBungalow, DateTime fechaInicial, DateTime fechaFinal)
        {
            bool modificado = false;
            List<DateTime> listaFechas = new List<DateTime>();
            listaFechas.Add(fechaInicial);
            listaFechas.Add(fechaFinal);

            Datos.Bungalow bungalow = Negocio.Bungalow.buscarId(idBungalow);
            IEnumerable<Datos.ReservaBungalow> listaReservas=bungalow.ReservaBungalow.Where(r=>r.estado!=0);
            foreach (Datos.ReservaBungalow reserva in listaReservas)
            {
                for (int i = 0; i < listaFechas.Count; i += 2)
                {
                    fechaInicial = listaFechas[i];
                    fechaFinal = listaFechas[i + 1];
                    if (reserva.fechaInicio.Date<= fechaInicial.Date)
                    {
                        if (reserva.fechaFin.Date >= fechaInicial.Date) // reserva traslapa con hueco
                        {
                            if (reserva.fechaFin.Date < fechaFinal.Date) // hueco disminuye
                            {
                                modificado = true;
                                listaFechas[i] = fechaFinal.AddDays(1);
                            }
                            else    //reserva.fechaFin mayor a fechaFinal, ya no existe el hueco
                            {
                                modificado = true;
                                listaFechas.Remove(fechaInicial);
                                listaFechas.Remove(fechaFinal);
                            }
                        }
                    }
                    else
                    {
                        if (reserva.fechaInicio.Date <= fechaFinal.Date)
                        {
                            if (reserva.fechaFin.Date >= fechaFinal.Date) // se reduce el tiempo disponible
                            {
                                modificado = true;
                                fechaFinal = reserva.fechaInicio.AddDays(-1);
                            }
                            else //la reserva esta en medio del tiempo disponible, se generan dos huecos
                            {
                                modificado = true;
                                DateTime nuevaFechaInicial = reserva.fechaFin.AddDays(1);
                                DateTime nuevaFechaFinal = fechaFinal;
                                listaFechas[i+1] = reserva.fechaInicio.AddDays(-1);
                                listaFechas.Add(nuevaFechaInicial);
                                listaFechas.Add(nuevaFechaFinal);
                            }
                        }
                    }
                }
            }
            if (modificado)
                return listaFechas;
            else 
                return null; 
        }

        public static Datos.TemporadaAlta FechaDeSorteo(DateTime fechaInicial, DateTime fechaFinal)
        {
            IEnumerable<Datos.TemporadaAlta> listaTemporadas = Negocio.TemporadaAlta.seleccionarTodo();
            foreach (Datos.TemporadaAlta temporada in listaTemporadas)
            {
                if(Fecha.CompararFechas(fechaInicial,temporada.fechaInicio)>=0 && 
                    Fecha.CompararFechas(fechaInicial,temporada.fechaFin)<=0)
                    return temporada;
                if (Fecha.CompararFechas(fechaFinal, temporada.fechaInicio) >= 0 &&
                    Fecha.CompararFechas(fechaFinal, temporada.fechaFin) <= 0)
                    return temporada;                       
            }
            return null;
        }

        public static void AgregarReservaBungalow(Datos.ReservaBungalow reservabungalow)
        {
            reservabungalow.estado = PORPAGAR;
            Datos.Pago pago = new Datos.Pago();
            pago.Familia = reservabungalow.Familia;
            pago.ConceptoDePago = ConceptoDePago.buscarId(ConceptoDePago.ID_RESERVABUNGALOW);
            pago.descripcion = "Reserva de la familia " + reservabungalow.Familia.id + " del bungalow " + reservabungalow.Bungalow.id + " del " + reservabungalow.fechaInicio.Day + "/" +
                reservabungalow.fechaInicio.Month + "/" + reservabungalow.fechaInicio.Year + " al " + reservabungalow.fechaFin.Day + "/" +
                reservabungalow.fechaFin.Month + "/" + reservabungalow.fechaFin.Year;
            pago.fechaRegistro = DateTime.Now;
            pago.fechaLimite = reservabungalow.fechaInicio.AddDays(-Parametros.SeleccionarParametros().diasLimitePago);
            if (pago.fechaLimite.Date < DateTime.Today) //si esta muy cerca de la fecha de Inicio, solo tendra un dia para pagar
                pago.fechaLimite = DateTime.Today;
            int numDias = (reservabungalow.fechaFin - reservabungalow.fechaInicio).Days + 1;
            pago.monto = reservabungalow.Bungalow.TipoBungalow.precio * numDias;
            pago.estado = Pago.PENDIENTE;
            reservabungalow.Pago = pago;
            ConceptoDePago.context().ReservaBungalow.AddObject(reservabungalow);
            Context.context().SaveChanges();
        }


        public static void Eliminar(short idReservaBungalow)
        {
            Datos.ReservaBungalow reserva = BuscarId(idReservaBungalow);
            Datos.Pago pago = reserva.Pago;
            if (pago != null)
            {
                if (pago.estado == Pago.CANCELADO)
                    pago.estado = Pago.PORDEVOLVER;
                if (pago.estado == Pago.PENDIENTE)
                    pago.estado = 0;
            }
            reserva.estado = 0;
            Context.context().SaveChanges();
        }

        private static Datos.ReservaBungalow BuscarId(short idReservaBungalow)
        {
            return Context.context().ReservaBungalow.FirstOrDefault(r=> r.id==idReservaBungalow && r.estado!= 0);
        }

        public static IEnumerable<Datos.ReservaBungalow> SeleccionarReservasFamilia(short idFamilia)
        {
            DateTime hoy = DateTime.Today;
            return Context.context().ReservaBungalow
                .Where(r => r.Familia.id==idFamilia && r.fechaFin >= hoy && r.estado != 0);
        }

        public static IEnumerable<Datos.ReservaBungalow> SeleccionarReservas()
        {
            DateTime hoy = DateTime.Today;
            return Context.context().ReservaBungalow
                .Where(r => r.fechaFin >= hoy && r.estado != 0);
        }
    }
}
