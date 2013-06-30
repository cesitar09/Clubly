using System.Linq;
using System.Text;
using System.Collections;
using Datos;
using Negocio.Util;
using System.Data.Objects.DataClasses;
using System.Data;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class Promocion
    {
        public static Entities context()
        {
            return Context.context();
        }

        /* public static Exception insertar(Datos.Promocion promo)
         {
             try
             {
                 promo.estado = ListaEstados.ESTADO_ACTIVO;
                 context().Servicio.AddObject(servicio);
                 context().SaveChanges();
             }
             catch (Exception ex)
             {
                 return ex;
             }
             return null;
         }

         public static IEnumerable<Datos.Promocion> seleccionarTodo()
         {
             return context().Promocion.Where(s => s.estado != ListaEstados.ESTADO_ELIMINADO);
         }

         public static Datos.Promocion buscarId(short id)
         {
             return context().Promocion.Single(p => p.id == id);
         }

         public static Exception modificar(Datos.Promocion promo, IEnumerable<short> familias)
         {
             try
             {
                 context().Promocion.ApplyCurrentValues(promo);
                 concesionario.Sede.Clear();
                 if (sedes != null)
                     foreach (short codigo in familias)
                     {
                         promo.Familia.Add(Negocio.Familia.buscarId(codigo));
                     }
                 context().SaveChanges();
             }
             catch (Exception ex)
             {
                 return ex;
             }
             return null;
         }

         public static Exception eliminar(Datos.Promocion promo)
         {
             try
             {
                 Datos.Promocion eliminado = buscarId(promo.id);
                 eliminado.estado = ListaEstados.ESTADO_ELIMINADO;
                 context().SaveChanges();
             }
             catch (Exception ex)
             {
                 return ex;
             }
             return null;
         }*/
    }
}
