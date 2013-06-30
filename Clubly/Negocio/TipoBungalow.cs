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
    public class TipoBungalow
    {
        public static Entities context()
        {
            return Context.context();
        }

        //Metodo para verificar que no haya bungalows con ese tipo de bungalow

        public static bool HayBungalows(short id) {
            IEnumerable<Datos.Bungalow> listabungalows = buscarId(id).Bungalow;
            if (listabungalows.Count() > 0) return true;
            else
                return false;
        }

        public static Exception insertar(Datos.TipoBungalow tipobungalow)
        {
            try
            {
                tipobungalow.estado = 1;
                context().TipoBungalow.AddObject(tipobungalow);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }


        public static Datos.TipoBungalow seleccionarId(short id)
        {
            Datos.TipoBungalow tipobungalow = context().TipoBungalow.Single(p => p.id == id);
            return tipobungalow;
        }

        public static IEnumerable<Datos.TipoBungalow> SeleccionarTodo()
        {
            return context().TipoBungalow.Where(tipoBungalow => tipoBungalow.estado != ListaEstados.ESTADO_ELIMINADO);
        }

        public static Datos.TipoBungalow buscarId(short id)
        {
            return context().TipoBungalow.Single(p => p.id == id);
        }


        public static Exception modificar(Datos.TipoBungalow tipoBungalow)
        {

            try
            {
                context().TipoBungalow.ApplyCurrentValues(tipoBungalow);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static Exception eliminar(Datos.TipoBungalow tipoBungalow)
        {

            try
            {
                tipoBungalow.estado = 0;
                context().TipoBungalow.ApplyCurrentValues(tipoBungalow);
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
