﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Newtonsoft.Json;
using Kendo.Mvc.UI;
namespace Web.Controllers
{
    public class ReservasController : Controller
    {
        //********************************RESERVAR CANCHA*******************************************************************************************

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult ReservarCancha(Models.ReservaCancha reserva)
        {
            ViewData["message"] = null;
            return View((IView)null);
        }

        public bool BuscarDisponibilidad(Models.ReservaCancha reserva) {
            //----Excepcion
            IEnumerable<Models.ReservaCancha> ListaReserva = Models.ReservaCancha.SeleccionarTodo();
            bool disponibilidad=true;

            if (ListaReserva != null) //Para que la Lista no este Vacio
            {
                foreach (var resev in ListaReserva)
                {
                    if (resev.cancha != null && reserva.cancha!=null)
                    {
                        if (resev.cancha.id == reserva.cancha.id)
                        {
                            if (resev.horaInicio.Date == reserva.horaInicio.Date)
                            {
                                if ((resev.horaInicio > reserva.horaInicio && resev.horaInicio < reserva.horaFin) ||
                                    (resev.horaInicio == reserva.horaInicio && resev.horaInicio == reserva.horaFin) ||
                                    (resev.horaInicio < reserva.horaInicio && resev.horaFin > reserva.horaInicio))
                                {
                                    disponibilidad = false;
                                }

                            }

                        }   //*Primero Valida Si la hora que eligo engloba otro hora
                    }       //*Segundo  Valida si la hora que eligo esta entre otra hora
                }           //*Tercero valida si la hora de incio esta antes de de la hora y la hora fin esta entre las horas de otra fecha
                            //*Cuarto valida si la hora de inicio esta entre la hora de incio de otra y la hora fin y la hora fin es mayor
            }               //*Quinto que no sea igual a las horas    
           return disponibilidad;
        }

        public bool ValidarHoras(Models.ReservaCancha reserva) {

            if (reserva.horaFin.Hour > reserva.horaInicio.Hour)
            {
                if (reserva.horaFin.Hour - reserva.horaInicio.Hour <= Models.Parametros.SeleccionarParametros().tiempo) return true; 
            }
            else
                if (reserva.horaFin.Hour == reserva.horaInicio.Hour)
                    if (reserva.horaFin.Minute >= reserva.horaInicio.Minute)
                        if (reserva.horaFin.Hour - reserva.horaInicio.Hour <= Models.Parametros.SeleccionarParametros().tiempo) return true;         
            
            return false;
        }

        public bool VecesMaxima(Models.ReservaCancha reserva) {
            int contador = 0;
            IEnumerable<Models.ReservaCancha> ListaReserva = Models.ReservaCancha.SeleccionarTodo();
            bool aceptable = false;
            foreach (var resev in ListaReserva)
            {
                if (resev.fechaInicio.Date == reserva.fechaInicio.Date && resev.idFamilia == reserva.idFamilia) {
                    contador = contador + 1;
                }
            }

            if (contador <= 2)
                aceptable = true;

            return aceptable;
        }

        public ActionResult IngresarReservaCancha(Models.ReservaCancha reserva)
        {
            int val;
            bool disponibilidad=false;
            short idusuario;


            if (ValidarHoras(reserva))
            {
                if (VecesMaxima(reserva))
                {
                    //Asigno horas para el model0
                    DateTime fechaInicial = new DateTime();
                    DateTime fechaFinal = new DateTime();

                    fechaInicial = reserva.fechaInicio.Add(reserva.horaInicio.TimeOfDay);
                    fechaFinal = reserva.fechaInicio.Add(reserva.horaFin.TimeOfDay);
                    reserva.horaInicio = fechaInicial;
                    reserva.horaFin = fechaFinal;

                    reserva.actividad = null;

                    //Si selecciona Cancha
                    if (reserva.idCancha != 0)
                    {   //-----excepcion
                        reserva.cancha = Models.Cancha.buscarId(reserva.idCancha);
                        if (BuscarDisponibilidad(reserva))
                        {   //-----Excepcion
                            idusuario = Models.Familia.buscarIdUsuario(Convert.ToInt16(Session["idUsuario"])).id;
                            reserva.familia = Models.Familia.buscarId(idusuario);
                            //-----Excepcion
                            val = Models.ReservaCancha.Insertar(reserva);
                            if (val == 0) ViewData["message"] = "F";
                            if (val == 1) ViewData["message"] = "R";
                            return View("ReservarCancha", reserva);

                        }
                        else
                        {
                            ViewData["message"] = "ND";
                            return View("ReservarCancha", reserva);
                        }
                    }
                    else //Sino selecciona Cancha
                    {   //------Excepcion
                        IEnumerable<Models.Cancha> ListaCancha = Models.Cancha.BuscarSedeTipo(reserva.idSede, reserva.idTipoCancha);

                        foreach (var cancha in ListaCancha)
                        {
                            reserva.cancha = cancha;
                            if (BuscarDisponibilidad(reserva))
                            {
                                disponibilidad = true;
                                break;
                            }
                        }

                        if (disponibilidad)
                        {
                            if (reserva.id == 0)
                                //----Excepcion
                                reserva.familia = Models.Familia.buscarId(10001);

                            //---Excepcion
                            reserva.idCancha = reserva.cancha.id;
                            val = Models.ReservaCancha.Insertar(reserva);
                            if (val == 0) ViewData["message"] = "F";
                            if (val == 1) ViewData["message"] = "R";
                            return View("ReservarCancha", reserva);

                        }
                        else
                        {
                            ViewData["message"] = "NDT";
                            return View("ReservarCancha", reserva);
                        }

                    }

                }
                else ViewData["message"] = "CVM";
            }
            else
            {
                ViewData["message"] = "HNV";
            }
            
            
            return View("ReservarCancha", reserva);
        }

        public ActionResult LeerReservasCanchas([DataSourceRequest] DataSourceRequest request)
        {   //--Excepcion
            short idsocio = Models.Familia.buscarIdUsuario(Convert.ToInt16(Session["idUsuario"])).id;
            IEnumerable<Models.ReservaCancha> listaReservas = Models.ReservaCancha.BuscarIdFamilia(idsocio);
            return Json(listaReservas.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditarReservaCancha(Web.Models.ReservaCancha reserva)
        {
            Models.ReservaCancha rese = Models.ReservaCancha.BuscarId(reserva.id);
            if (rese.estado == "Cancelada")
                ViewData["message"] = "EC";
            else if (rese.estado == "Vencida")
                ViewData["message"] = "EV";
            else
            {
                return View("ReservarCancha", rese);
            }
            return View("ReservarCancha");
        }

       

        public ActionResult CancelarReservaCancha(short id)
        {

            Models.ReservaCancha reserva = Models.ReservaCancha.BuscarId(id);

            if (reserva.estado == "Cancelada")
                ViewData["message"] = "CC";
            else if (reserva.estado == "Vencida")
                ViewData["message"] = "CV";
            else
                Models.ReservaCancha.Eliminar(id);

            return View("ReservarCancha");
        
        }
        
        public ActionResult LeerSedes()
        {
            IEnumerable<Models.Sede> ListaSedes = Models.Sede.SeleccionarTodo();

            return Json(ListaSedes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LeerTipoCancha(string idSede)
        {
            short idsede = Convert.ToInt16(idSede);
            IEnumerable<Models.TipoCancha> ListaSedes = Models.TipoCancha.BuscarPorSede(idsede);
            return Json(ListaSedes, JsonRequestBehavior.AllowGet);
        }
      
        public ActionResult LeerNumeroCancha(string idTipo, string idSede)
        {
            short idtipo = Convert.ToInt16(idTipo);
            short idsede = Convert.ToInt16(idSede);
            IEnumerable<Models.Cancha> ListaSedes = Models.Cancha.BuscarSedeTipo(idsede, idtipo);// buscarTipo(idtipo);
            return Json(ListaSedes, JsonRequestBehavior.AllowGet);
        }

        
        //********************************RESERVAR BUNGALOW *******************************************************************************************

        public ActionResult ObtenerUrlImagen(String id)
        {
            return Json("../Content/img/" + id.Trim(new Char[] { '\\', '\"' }) + ".jpg", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ObtenerBungalow(String sede)
        {
            short id = short.Parse(sede);
            IEnumerable<Models.Bungalow> bungalows = Models.Bungalow.BuscarSede(id);
            return Json(bungalows, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerDisponibilidad(String sidBungalow, String sfinicial, String sffinal)
        {
            short idBungalow = short.Parse(sidBungalow.Trim(new Char[] { '\\', '\"' }));
            DateTime finicial = DateTime.Parse(sfinicial.Trim(new Char[] { '\\', '\"' }));
            DateTime ffinal = DateTime.Parse(sffinal.Trim(new Char[] { '\\', '\"' }));
            IList<DateTime> tiempos = Negocio.ReservaBungalow.Disponibilidad(idBungalow, finicial, ffinal);
            return Json(tiempos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerSedes()
        {
            var sedes = Web.Models.Sede.SeleccionarTodo();
            return Json(sedes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LeerFamilias()
        {
            var familia = Web.Models.Familia.SeleccionarTodo();
            return Json(familia, JsonRequestBehavior.AllowGet);
        }
        

        public ActionResult ReservarBungalow()
        {
            return View();
        }

        public ActionResult ReservarBungalowGlobal()
        {
            return View();
        }

        public ActionResult LeerReservasFamilia([DataSourceRequest]DataSourceRequest request)
        {
            short idUsuario = (short)Session["IdUsuario"];
            IEnumerable<Models.ReservaBungalow> reservas = Models.ReservaBungalow.SeleccionarReservasFamilia(Models.Familia.buscarIdUsuario(idUsuario).id);
            return Json(reservas.ToDataSourceResult(request));
        }

        public ActionResult LeerReservasGlobal([DataSourceRequest]DataSourceRequest request)
        {
            IEnumerable<Models.ReservaBungalow> reservas = Models.ReservaBungalow.SeleccionarReservas();
            return Json(reservas.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult ReservarBungalowGlobal(Models.ReservaBungalow reserva, DateTime finicial, DateTime ffinal, short AutocompleteId)
        {
            short idFamilia = AutocompleteId;
            reserva.fechaInicio = finicial;
            reserva.fechaFin = ffinal;
            Models.ReservaBungalow.AgregarReservaBungalowF(reserva, idFamilia);
            return View();
        }

        [HttpPost]
        public ActionResult ReservarBungalow(Models.ReservaBungalow reserva,DateTime finicial, DateTime ffinal)
        {
            short idUsuario = (short)Session["IdUsuario"];
            reserva.fechaInicio = finicial;
            reserva.fechaFin = ffinal;
            Models.ReservaBungalow.AgregarReservaBungalow(reserva,idUsuario);
            return View();
        }

    }
}

