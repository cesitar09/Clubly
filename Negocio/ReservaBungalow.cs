using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;

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
    }
}
