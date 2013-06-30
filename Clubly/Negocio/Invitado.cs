using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;

namespace Negocio
{
    public class Invitado
    {
        public static Entities context()
        {
            return Context.context();
        }
        //------------------
        public static Exception Insertar(Datos.Invitado invitado)
        {
            try
            {
                invitado.estado = 1;
                context().Invitado.AddObject(invitado);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        //------------------------

        public static IEnumerable<Datos.Invitado> SeleccionarTodo()
        {
            IEnumerable<Datos.Invitado> listaInvitado = context().Invitado.Where(p => p.estado == 1);
            return listaInvitado;
        }

        //------------------------
        public static Exception modificar(Datos.Invitado invitado)
        {
            try
            {
                context().Invitado.ApplyCurrentValues(invitado);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        //------------------------
        public static Exception eliminar(Datos.Invitado invitado)
        {
            try
            {
                invitado.estado = 0;
                context().Invitado.ApplyCurrentValues(invitado);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

    }
}
