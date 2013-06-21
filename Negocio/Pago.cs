using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;

namespace Negocio
{
    public class Pago
    {
        public static Entities context()
        {
            return Datos.Context.context();
        }

        public static Exception Insertar(Datos.Pago pago)
        {
            try
            {
                pago.estado = 1;
                context().Pago.AddObject(pago);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public Exception modificar(Datos.Pago pago)
        {
            try
            {
                context().Pago.AddObject(pago);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        //Esto es un seleccionar todo para las pagos

        public static IEnumerable<Datos.Pago> SeleccionarTodo()
        {

            IEnumerable<Datos.Pago> listaPago = context().Pago.Where(p => p.estado != 0);
            return listaPago;

        }

        public static IEnumerable<Datos.Pago> SeleccionarPorFamilia(short id)
        {

            IEnumerable<Datos.Pago> listaPago = context().Pago.Where(p => p.Familia.id == id);
            return listaPago;

        }

        public static Datos.Pago BuscarId(short id)
        {
            return context().Pago.FirstOrDefault(p => p.id == id);
        }


    }
}