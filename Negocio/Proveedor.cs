using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using System.Collections;
namespace Negocio
{
    public class Proveedor
    {
        public static Entities context()
        {

            return Context.context();
        }

        public static Exception insertar(Datos.Proveedor proveedor)
        {
            try
            {
                proveedor.estado = 1;
                context().Proveedor.AddObject(proveedor);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }


        public static IEnumerable<Datos.Proveedor> seleccionarTodo()
        {
            IEnumerable<Datos.Proveedor> listaProveedores = context().Proveedor.Where(p => p.estado == 1);
            return listaProveedores;
        }
        /*
        public static Datos.Proveedor seleccionarId(short id)
        {
            Datos.Proveedor proveedor = context().Proveedor.Single(p => p.id == id);
            return proveedor;
        }
        */
        public static Datos.Proveedor buscarId(short id)
        {
            return context().Proveedor.Single(p => p.id == id);
        }

        /*public static void crearProveedor(Datos.Proveedor proveedor)
        {
            Datos.Proveedor prov = new Datos.Proveedor();
            prov.nombre = proveedor.nombre;
            prov.direccion = proveedor.direccion;
            prov.ruc = proveedor.ruc;
            prov.estado = proveedor.estado;
            context().Proveedor.AddObject(prov);
            context().SaveChanges();
        }*/


        public static Exception modificar(Datos.Proveedor proveedor)
        {
            try
            {
                context().Proveedor.ApplyCurrentValues(proveedor);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static Exception eliminar(Datos.Proveedor proveedor)
        {
            try
            {
                proveedor.estado = 0;
                context().Proveedor.ApplyCurrentValues(proveedor);
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
