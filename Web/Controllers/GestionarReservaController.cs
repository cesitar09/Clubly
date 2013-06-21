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
    public class GestionarReservaController : Controller
    {
    // VISTAS
        //Vistas de Reservar Cancha
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GestionarReservarCancha(Models.ReservaCancha reserva)
        {
            return View((IView)null);
        }

        public ActionResult IngresarReservaCancha(Models.ReservaCancha reserva)
        {

            reserva.actividad = Models.Actividad.buscarId(reserva.idActividad);
            reserva.cancha = Models.Cancha.buscarId(reserva.idCancha);
            if (reserva.id == 0)
            {
                reserva.familia = Models.Familia.buscarId(reserva.familia.id);

            }
            Models.ReservaCancha.Insertar(reserva);
            return View("GestionarReservarCancha", reserva);
        }
        public ActionResult EditarReservaCancha(Web.Models.ReservaCancha reserva)
        {
            reserva = Models.ReservaCancha.BuscarId(reserva.id);
            return View("GestionarReservarCancha", reserva);

        }

        public ActionResult CancelarReservaCancha(short id)
        {

            Models.ReservaCancha.Eliminar(id);
            return View("GestionarReservarCancha");

        }

        //Vistas de Registrar Ingreso a Bungalow
        public ActionResult RegistrarIngresoBungalow(short? id)
        {
            if (id.HasValue)
                Models.ReservaBungalow.RegistrarIngresoBungalow(id);
            return View();
        }

        public ActionResult RegistrarSalidaBungalow(short? id)
        {
            if (id.HasValue)
                Models.ReservaBungalow.RegistrarSalidaBungalow(id);
            return View("RegistrarIngresoBungalow");
        }

        public ActionResult IngresarBungalow(short id)
        {
            
            return View("RegistrarIngresoBungalow()");
        }


    //JSONS
        public ActionResult LeerReservasCanchas([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Models.ReservaCancha> listaReservas = Models.ReservaCancha.SeleccionarTodo();
            return Json(listaReservas.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult LeerReservasBungalows([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Models.ReservaBungalow> listaReservas = Models.ReservaBungalow.SeleccionarIngreso();
            return Json(listaReservas.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}
