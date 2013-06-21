using System;
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

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult ReservarCancha(Models.ReservaCancha reserva)
        {
            ViewData["message"] = null;
            return View((IView)null);
        }

        public bool BuscarDisponibilidad(Models.ReservaCancha reserva) {
            IEnumerable<Models.ReservaCancha> ListaReserva = Models.ReservaCancha.SeleccionarTodo();
            bool disponibilidad=true;

            if (ListaReserva != null)
            {
                foreach (var resev in ListaReserva)
                {
                    if (resev.cancha != null)
                    {
                        if (resev.cancha.id == reserva.cancha.id)
                        {
                            if (resev.horaInicio.Date == reserva.horaInicio.Date)
                            {
                                if (resev.horaInicio >= reserva.horaInicio && resev.horaFin <= reserva.horaFin || (resev.horaFin >= reserva.horaInicio && resev.horaFin <= reserva.horaFin)
                                    || (resev.horaInicio >= reserva.horaInicio && resev.horaInicio <= reserva.horaFin) || (resev.horaInicio <= reserva.horaInicio && resev.horaFin >= reserva.horaFin)
                                    || (resev.horaInicio >= reserva.horaInicio && resev.horaInicio <= reserva.horaFin && resev.horaFin >= reserva.horaFin))
                                {
                                    disponibilidad = false;
                                }

                            }

                        }
                    }
                }

            }
           return disponibilidad;
        }


        public bool ValidarHoras(Models.ReservaCancha reserva) {

            if (reserva.horaFin.Hour > reserva.horaInicio.Hour) {
                if (reserva.horaFin.Hour - reserva.horaInicio.Hour <= 2) return true;         
            }

            return false;
        }

        public ActionResult IngresarReservaCancha(Models.ReservaCancha reserva)
        {
            int val = 4;


            if (ValidarHoras(reserva))
            {
                reserva.horaInicio = reserva.fechaInicio.AddHours(reserva.horaInicio.Hour);
                reserva.horaFin = reserva.fechaInicio.AddHours(reserva.horaFin.Hour);
                if (reserva.idActividad != 0)
                    reserva.actividad = Models.Actividad.buscarId(reserva.idActividad);
                else reserva.actividad = null;


                if (reserva.idCancha != 0)
                {
                    reserva.cancha = Models.Cancha.buscarId(reserva.idCancha);
                    if (BuscarDisponibilidad(reserva))
                    {
                        if (reserva.id == 0)
                        reserva.familia = Models.Familia.buscarId(10001);

                        val = Models.ReservaCancha.Insertar(reserva);
                        if (val == 0) ViewData["message"] = "F";
                        if (val == 1) ViewData["message"] = "R";
                        return View("ReservarCancha", reserva);

                    }
                    else
                    {
                        val = 2;
                        if (val == 2) ViewData["message"] = "ND";
                        return View("ReservarCancha", reserva);
                    }
                }
                else
                {
                    IEnumerable<Models.Cancha> ListaCancha = Models.Cancha.SeleccionarTodo();
                    foreach (var reser in ListaCancha)
                    {
                        if (BuscarDisponibilidad(reserva))
                        {
                            reserva.cancha = Models.Cancha.buscarId(reser.id);
                            if (reserva.id == 0)
                            {
                                reserva.familia = Models.Familia.buscarId(10001);
                            }
                            val = Models.ReservaCancha.Insertar(reserva);
                            if (val == 0) ViewData["message"] = "F";
                            if (val == 1) ViewData["message"] = "R";
                            return View("ReservarCancha", reserva);
                        }
                    }
                    val = 3;
                    if (val == 3) ViewData["message"] = "NDT";
                    return View("ReservarCancha", reserva);
                }
            }
            else {
                ViewData["message"] = "HNV";
            }
            
            
            return View("ReservarCancha", reserva);
        }

        
        


        public ActionResult LeerReservasCanchas([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Models.ReservaCancha> listaReservas = Models.ReservaCancha.SeleccionarTodo();
            return Json(listaReservas.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditarReservaCancha(Web.Models.ReservaCancha reserva)
        {
            reserva = Models.ReservaCancha.BuscarId(reserva.id);
            return View("ReservarCancha", reserva);
        
        }

        public ActionResult CancelarReservaCancha(short id)
        {
                  
            Models.ReservaCancha.Eliminar(id);
            return View("ReservarCancha");
        
        }

        public JsonResult GetCascadeNumeros(String tipoCancha, DataSourceRequest request)
        {
            IEnumerable<Models.Cancha> canchas = null;
            if (tipoCancha != null) //Sólo he logrado que entre un valor constante. Ni el cascadeFrom ni el ToComponent().Value me dan nada.
            {
                canchas = Models.Cancha.buscarTipo(Int16.Parse(tipoCancha));
                return Json(canchas.ToDataSourceResult(request)); //Falla, probablemente porque el request no existe, es null, inventado.
            }
            else
            {
                return null;
            }
        }
    }
}
