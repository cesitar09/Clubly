﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;

namespace Negocio
{
    public class Sede
    {
        public static Entities context()
        {
            return Context.context();
        }

        public static Exception insertar(Datos.Sede sede)
        {
            try
            {
                sede.estado = 1;
                context().Sede.AddObject(sede);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static IEnumerable<Datos.Sede> seleccionarTodo()
        {
            try
            {
                IEnumerable<Datos.Sede> listaSedes = context().Sede.Where(p => p.estado == 1);
                return listaSedes;
            }
            catch(Exception)
            {
                return null;
            }
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
            return context().Sede.Single(p => p.id == id);
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

        public static Exception modificar(Datos.Sede sede)
        {
            //Datos.Sede s = context().Sede.Single(p => p.id == sede.id);
            //s.nombre = sede.nombre;
            //s.descripcion = sede.descripcion;
            //s.direccion = sede.direccion;
            //s.estado = sede.estado;
            //context().Sede.ApplyCurrentValues(s);
            //context().SaveChanges();

            try
            {
                context().Sede.ApplyCurrentValues(sede);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static Exception eliminar(Datos.Sede sede)
        {
            try
            {
                sede.estado = 0;
                context().Sede.ApplyCurrentValues(sede);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static Exception habilitar(Datos.Sede sede) {
            try{
            
                sede.estado=1;
                context().Sede.ApplyCurrentValues(sede);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static Exception inhabilitar(Datos.Sede sede)
        {
            try
            {

                sede.estado = 2;
                context().Sede.ApplyCurrentValues(sede);
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
