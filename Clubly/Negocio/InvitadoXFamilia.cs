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

        public static void insertar(Datos.InvitadoXFamilia ixf)
        {

                Datos.Parametros param=Parametros.SeleccionarParametros();
                if (Familia.NumeroInvitados(ixf.Familia.id) > param.numInvitadosFamilia)
                {
                    Datos.Pago pago = new Datos.Pago();
                    pago.fechaRegistro = DateTime.Now;
                    pago.fechaLimite = DateTime.Now;
                    pago.ConceptoDePago = ConceptoDePago.buscarId(ConceptoDePago.ID_INGRESOINVITADOS);
                    pago.monto = pago.ConceptoDePago.monto.Value;
                    pago.descripcion = "Ingreso de " + ixf.Invitado.nombre + " " + ixf.Invitado.apPaterno + " " + ixf.Invitado.apMaterno +
                        " con la familia " + ixf.Familia.id;
                    pago.Familia = ixf.Familia;
                    pago.estado = Pago.CANCELADO;
                    ixf.Pago = pago;
                }
                ixf.estado = 1;
                context().InvitadoXFamilia.AddObject(ixf);
                context().SaveChanges();
        }

        public static IEnumerable<Datos.InvitadoXFamilia> seleccionarTodo()
        {
            return context().InvitadoXFamilia.Where(p=> p.estado!=0);
        }

        public static Datos.InvitadoXFamilia buscarKey(EntityKey id)
        {
            return context().InvitadoXFamilia.FirstOrDefault(p => p.EntityKey == id && p.estado!=0);
        }

        //public static Exception modificar(Datos.InvitadoXFamilia concesionario)
        //{
        //    try
        //    {
        //        context().InvitadoXFamilia.ApplyCurrentValues(concesionario);
        //        context().SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex;
        //    }
        //    return null;
        //}

        //public static Exception eliminar(Datos.InvitadoXFamilia ifx)
        //{
        //    try
        //    {
        //        Datos.InvitadoXFamilia eliminado = buscarKey(ifx.EntityKey);
        //        //eliminado.estado = ListaEstados.ESTADO_ELIMINADO;
        //        context().SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex;
        //    }
        //    return null;
        //}
    }
}
