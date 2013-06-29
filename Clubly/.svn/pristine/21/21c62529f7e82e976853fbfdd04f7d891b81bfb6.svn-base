using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using System.Collections;
using Negocio.Util;

namespace Negocio
{
    public class SolicitudMembresia
    {
   

        public static Entities context()
        {
            return Context.context();
        }


        public static Exception Insertar(Datos.SolicitudMembresia SolicitudMembresia)
        {
            try
            {
                SolicitudMembresia.estado = 1;
                SolicitudMembresia.fechaRegistro = DateTime.Now;
                context().SolicitudMembresia.AddObject(SolicitudMembresia);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static IEnumerable<Datos.SolicitudMembresia> SeleccionarTodo()
        {
            return context().SolicitudMembresia.Where(p => p.estado != 0);
        
        }

        public static Datos.SolicitudMembresia BuscarId(short id)
        {
            return context().SolicitudMembresia.Single(p => p.id == id);
        }

        public static Exception Modificar(Datos.SolicitudMembresia solicitudMembresia)
        {
            try
            {
                context().SolicitudMembresia.ApplyCurrentValues(solicitudMembresia);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static int Aceptar(Datos.SolicitudMembresia solicitudMembresia)
        {

            solicitudMembresia.estado = 3;
            return context().SaveChanges();

        }
        public static int Eliminar(Datos.SolicitudMembresia solicitudMembresia)
        {

            solicitudMembresia.estado = 0;
            return context().SaveChanges();

        }

        public static int Rechazar(Datos.SolicitudMembresia solicitudMembresia)
        {

            solicitudMembresia.estado = 2;
            return context().SaveChanges();

        }

        

    }
}
