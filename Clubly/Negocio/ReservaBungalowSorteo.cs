using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;

namespace Negocio
{
    public class ReservaBungalowSorteo
    {
        public const short PENDIENTE = 1;

        public static Entities context()
        {
            return Datos.Context.context();
        }

        public static IEnumerable<Datos.ReservaBungalowSorteo> seleccionarPendientes()
        {
            return context().ReservaBungalowSorteo.Where(s => s.estado == PENDIENTE);
        }

        public static void insertar(Datos.ReservaBungalowSorteo rbs)
        {
            context().ReservaBungalowSorteo.AddObject(rbs);
            context().SaveChanges();
        }

        public static void modificar(Datos.ReservaBungalowSorteo rbs)
        {
            var rbsaux = context().ReservaBungalowSorteo.SingleOrDefault(p => p.id == rbs.id);
            rbsaux.estado = rbs.estado;
            rbsaux.Familia = rbs.Familia;
            rbsaux.fechaFin = rbs.fechaFin;
            rbsaux.fechaInicio = rbs.fechaInicio;
            rbsaux.idSorteo = rbs.idSorteo;
            rbsaux.Pago = rbs.Pago;
            rbsaux.resultadoSorteo = rbs.resultadoSorteo;
            rbsaux.Sede = rbs.Sede;
            rbsaux.TipoBungalow = rbs.TipoBungalow;
            context().SaveChanges();
        }

       
      
    }
}
