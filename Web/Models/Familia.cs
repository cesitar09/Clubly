using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using Negocio.Util;

namespace Web.Models
{
    public class Familia
    {
        [Display(Name = "Código Familia")]
        [Required]
        public short id { get; set; }

        [Display(Name = "Tipo")]
        [Required]
        public short tipo { get; set; }

        [Display(Name = "Saldo")]
        [Required]
        public double saldo { get; set; }

        [Display(Name = "Estado")]
        public short estado { get; set; }

        [Display(Name = "Solicitud")]
        public SolicitudMembresia solicitud { get; set; }

        
        public Usuario usuario { get; set; }

        /*public InvitadoxFamilia invitado { get; set; }
        public InvitadoxFamiliaxEventoPrivado invitadoEvento { get; set; }
        public Pago pago { get; set; }
        public Recomendacion rec { get; set; }
        public ReservaBungalow resBun { get; set; }
        public ReservaBungalowSorteo resSor { get; set; }
        public ReservaAmbiente resAm { get; set; }
        public ReservaCancha resCan { get; set; }
        public ReservaCamping resCam { get; set; }
        public Socio socio { get; set; }*/

        //metodos para convertir
        public Familia() 
        {

        }
        public Familia (Models.SolicitudMembresia sol)
        {
            solicitud = sol;
            tipo = 0;
            saldo = 0;
            estado = 0;
        }
        public Familia(Datos.Familia familia)
        {
            id = familia.id;
            tipo = familia.tipo;
            saldo = familia.saldo;
            estado = familia.estado;
            usuario = Usuario.Convertir(familia.Usuario);
        }
        public static Models.Familia Convertir(Datos.Familia familia)
        {
            return new Familia(familia);
        }
        public static IEnumerable<Models.Familia> ConvertirLista(IEnumerable<Datos.Familia> listaFamilia)
        {
            return listaFamilia.Select(persona => Convertir(persona));
        }
        public static void insertarFamilia(Models.Familia familia)
        {
            familia.estado = ListaEstados.ESTADO_ACTIVO;

            Negocio.Familia.insertar(Invertir(familia));
        }
        public static void modificarFamilia(Models.Familia familia)
        {
            Negocio.Familia.modificar(Invertir(familia));
        }
        public static void eliminarFamilia(Models.Familia familia)
        {
            Negocio.Familia.eliminar(Invertir(familia));
        }
        public static Datos.Familia Invertir(Models.Familia familia) 
        {
            Datos.Familia datosFamilia;
            if (familia.id == 0)
                datosFamilia = new Datos.Familia();
            else
                datosFamilia = Negocio.Familia.buscarId(familia.id);
            datosFamilia.id = familia.id;
            datosFamilia.tipo = familia.tipo;
            datosFamilia.saldo = familia.saldo;
            datosFamilia.estado = familia.estado;
            datosFamilia.SolicitudMembresia = SolicitudMembresia.Invertir(familia.solicitud);
            return datosFamilia; 
        }
        public static IEnumerable<Models.Familia> SeleccionarTodo()
        {
            return ConvertirLista(Negocio.Familia.seleccionarTodo());
        }

        public static IEnumerable<Models.Familia> SeleccionarVitalicia()
        {
            return ConvertirLista(Negocio.Familia.SeleccionarNoVitalicio());
        }

        public static Models.Familia buscarId(short id)
        {
            return Convertir(Negocio.Familia.buscarId(id));
        }

        /*Para agregar un socio no titular es necesario saber la familia, la unica forma de saberlo es
         * buscando la familia por la solicutud que creo a esta familia, que en teoria, es unica*/
        public static Models.Familia buscarPorSolicitud(SolicitudMembresia solicitud)
        {
            return Convertir(Negocio.Familia.buscarPorSolicitud(solicitud.id));
        }

        //Metodo agregado por Maki para buscar una familia por IdUsuario 
        internal static Familia buscarIdUsuario(short idUsuario)
        {

            return Convertir(Negocio.Familia.buscarIdUsuario(idUsuario));
        }

        public static string val_pagos_reservas(short idfamilia) {
            if (Negocio.Familia.pagosPendientes(idfamilia).Count() != 0)
            {
                return "1";
            }
            else if (Negocio.Familia.reservaPendientes(idfamilia).Count() != 0 || Negocio.Familia.SorteoPendiente(idfamilia).Count() != 0 || Negocio.Familia.campingPendientes(idfamilia).Count() != 0 || Negocio.Familia.canchasPendientes(idfamilia).Count() != 0)
            {
                return "2";
            }
            else return "3";
        
        }
     
        public static void eliminarpendientes(short idfamilia){
            IEnumerable<Datos.ReservaBungalow> rebulist = Negocio.Familia.reservaPendientes(idfamilia);
            if( rebulist != null){
                foreach (var emp in rebulist) {
                    emp.estado = 0;
                }
            }else {
                IEnumerable<Datos.ReservaBungalowSorteo> resorteo = Negocio.Familia.SorteoPendiente(idfamilia);
                if (resorteo != null)
                {
                    foreach (var emp in resorteo)
                    {
                        emp.estado = 0;
                    }
                }
                else {
                    IEnumerable<Datos.ReservaCamping> recamp = Negocio.Familia.campingPendientes(idfamilia);
                    if (recamp != null)
                        foreach (var emp in recamp)
                        {
                            emp.estado = 0;
                        }
                    else {
                        IEnumerable<Datos.ReservaCancha> recan = Negocio.Familia.canchasPendientes(idfamilia);
                        foreach (var emp in recan) {
                            emp.estado = 0;
                        }
                    }
                }
            }
        }
    }
}