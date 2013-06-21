﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Datos;

namespace Web.Models
{
    public class Sede
    {
        [ScaffoldColumn(false)]
        [DisplayName("Id")]
        public short id { get; set; }

        [DisplayName("Nombre de la sede")]
        public String nombre { get; set; }
        [DisplayName("Dirección")]
        public String direccion { get; set; }
        [DisplayName("Descripción")]
        public String descripcion { get; set; }
        public short estado { get; set; }

        public Sede() { }

        public Sede(Datos.Sede sede)
        {
            id = sede.id;
            nombre = sede.nombre;
            direccion = sede.direccion;
            descripcion = sede.descripcion;
            estado = sede.estado;
        }

        public static Sede Convertir(Datos.Sede sede)
        {
            return new Sede(sede);
        }

        public static IEnumerable<Sede> ConvertirLista(IEnumerable<Datos.Sede> listasedes)
        {
            //var northwind = new NorthwindDataContext();
            return listasedes.Select(sede => Convertir(sede));
            //return listaConceptos.Select(concepto => Convertir(concepto)).AsQueryable();

        }

        public static Datos.Sede Invertir(Models.Sede mSede)
        {
           
            Datos.Sede dSede = new Datos.Sede();
            if (mSede.id == 0)
                dSede = new Datos.Sede();
            else
                dSede = Negocio.Sede.buscarId(mSede.id);
            //Datos.ConceptoDePago dCOnceptoDePago = new Datos.ConceptoDePago();
            
            dSede.nombre = mSede.nombre;
            dSede.direccion = mSede.direccion;
            dSede.descripcion = mSede.descripcion;
            dSede.estado = mSede.estado;

            return dSede;
        }

        public static IEnumerable<Datos.Sede> ConvertirListaInverso(IEnumerable<Models.Sede> mSedes)
        {
            return mSedes.Select(sede => Invertir(sede));
        }

        public static Models.Sede buscarId(short id)
        {
            return Convertir(Negocio.Sede.buscarId(id));
        }

        /////////////////////////////////////////////////////////

        public static IEnumerable<Sede> SeleccionarTodo()
        {
            IEnumerable<Datos.Sede> sede = Negocio.Sede.seleccionarTodo();
            return ConvertirLista(sede);
        }

        public static short primerId() {
            return 0;
            //return Negocio.Sede.PrimerId();
        }
        public static Models.Sede SeleccionarporId(short id)
        {
            return Convertir(Negocio.Sede.buscarId(id));
        }

        public static int modificarSede(Models.Sede sede)
        {
            if (Negocio.Sede.modificar(Invertir(sede)) == null)
                return 1;
            else
                return 0;
        }

        public static int insertarSede(Models.Sede nuevasede)
        {
            if (Negocio.Sede.insertar(Invertir(nuevasede)) == null)
                return 1;
            else
                return 0;
        }

        public static void eliminarSede(Models.Sede sede)
        {
            Negocio.Sede.eliminar(Invertir(sede));
        }

        public static void habilitarSede(Models.Sede sede)
        {
            if (sede.estado==2)
                Negocio.Sede.inhabilitar(Invertir(sede));
            if (sede.estado ==3)
                Negocio.Sede.habilitar(Invertir(sede));
        }

        public static String[] Enlistar(IEnumerable<Sede> sedes)
        {
            if (sedes != null)
            {
                String[] lista = new String[sedes.Count()];
                int i = 0;
                foreach (Sede sede in sedes)
                {
                    lista[i++] = sede.id.ToString();
                }
                return lista;
            }
            else
            {
                return new String[0];
            }
        }
    }
}
