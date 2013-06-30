using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using Negocio.Util;

namespace Negocio
{

    public class Sede
    {
        public const int HABILITADA = 1;
        public const int DESHABILITADA = 2;

        public static Entities context()
        {
            return Context.context();
        }

        //VALIDACION SI HAY AMBIENTES O BUNGALOWS LIGADOS A SEDES
        public static bool HayAmbienteoBungalow(short id) {
            IEnumerable<Datos.Ambiente> listaambientes = buscarId(id).Ambiente.Where(p => p.estado == 1);
            IEnumerable<Datos.Bungalow> listabungalows = buscarId(id).Bungalow.Where(p => p.estado == 1 || p.estado == 2);
            if (listaambientes.Count() > 0 || listabungalows.Count() > 0) return true;
            else
                return false;
        }

        public static int insertar(Datos.Sede sede)
        {
            sede.estado = ListaEstados.ESTADO_ACTIVO;
            context().Sede.AddObject(sede);
            context().SaveChanges();
            return sede.id;
        }

        public static IEnumerable<Datos.Sede> seleccionarTodo()
        {
            IEnumerable<Datos.Sede> listaSedes = context().Sede.Where(p => p.estado == 1);
            return listaSedes;
        }

        public static short PrimerId() {
            return seleccionarTodo().FirstOrDefault().id; 
        }

        public static Datos.Sede seleccionarId(short id)
        {
            Datos.Sede sede = context().Sede.Single(p => p.id == id);
            return sede;
        }

        public static Datos.Sede buscarId(short id)
        {
            return context().Sede.SingleOrDefault(p => p.id == id);
        }


        public static void crearSede(Datos.Sede sede)
        {
            Datos.Sede nuevasede = new Datos.Sede();
            nuevasede.nombre = sede.nombre;
            nuevasede.direccion = sede.direccion;
            nuevasede.descripcion = sede.descripcion;
            nuevasede.estado = sede.estado;
            context().Sede.AddObject(nuevasede);
            context().SaveChanges();
        }

        public static void modificar(Datos.Sede sede)
        {
            context().Sede.ApplyCurrentValues(sede);
            context().SaveChanges();
        }

        public static void eliminar(Datos.Sede sede)
        {
            sede.estado = 0;
            context().Sede.ApplyCurrentValues(sede);
            context().SaveChanges();
        }

        public static void habilitar(Datos.Sede sede) {
            sede.estado=Sede.HABILITADA;
            context().Sede.ApplyCurrentValues(sede);
            context().SaveChanges();
        }

        public static void inhabilitar(Datos.Sede sede)
        {
            sede.estado = Sede.DESHABILITADA;
            context().Sede.ApplyCurrentValues(sede);
            context().SaveChanges();
        }

    }
}
