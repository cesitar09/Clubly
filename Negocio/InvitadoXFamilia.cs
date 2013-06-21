using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Datos;
using Negocio.Util;
using System.Data.Objects.DataClasses;
using System.Data;

namespace Negocio
{
    public class InvitadoXFamilia
    {
        public static Entities context()
        {
            return Context.context();
        }

        public static Exception insertar(Datos.InvitadoXFamilia ixf)
        {
            try
            {
                if (Familia.NumeroInvitados(ixf.idFamilia) > Parametros.SeleccionarParametros().numInvitadosFamilia)
                {
                    Datos.Pago pago = new Datos.Pago();
                    pago.fechaRegistro = DateTime.Now;
                    pago.fechaLimite = DateTime.Now;
                    pago.monto = Parametros.SeleccionarParametros().costoInvitados;
                    ixf.Pago = pago;
                }
                context().InvitadoXFamilia.AddObject(ixf);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static IEnumerable<Datos.InvitadoXFamilia> seleccionarTodo()
        {
            return context().InvitadoXFamilia;
        }

        public static Datos.InvitadoXFamilia buscarKey(EntityKey id)
        {
            return context().InvitadoXFamilia.FirstOrDefault(p => p.EntityKey == id);
        }

        public static Exception modificar(Datos.InvitadoXFamilia concesionario)
        {
            try
            {
                context().InvitadoXFamilia.ApplyCurrentValues(concesionario);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static Exception eliminar(Datos.InvitadoXFamilia ifx)
        {
            try
            {
                Datos.InvitadoXFamilia eliminado = buscarKey(ifx.EntityKey);
                //eliminado.estado = ListaEstados.ESTADO_ELIMINADO;
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
