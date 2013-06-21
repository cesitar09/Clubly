﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Newtonsoft.Json;
using Kendo.Mvc.UI;
using Web.Controllers;
using Web.Models;
using System.Data;
using System.Data.SqlClient;
using Negocio.Util;
namespace Web.Controllers
{
    public class SolicitudMembresiaController : Controller
    {
        //
        // GET: /SolicitudMembresia/


        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult VerSolicitudesDeMembresia(Models.SolicitudMembresia solicitudMembresia)
        {
            {
                return View((IView)null);
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InsertarSolicitud(string submit,Web.Models.SolicitudMembresia solicitud)
        {
            try
            {

                if (submit == "Insertar" || submit == "Modificar")
                {
                    if (solicitud.id > 0)
                    {

                        if (SolicitudMembresia.Modificar(solicitud) == 1)
                        {
                            ViewData["message"] = "E";
                        }
                        else { ViewData["message"] = "F"; }
                    }
                    else
                    {
                        if (SolicitudMembresia.Insertar(solicitud) == 1)
                        {
                            ViewData["message"] = "E";
                        }
                        else { ViewData["message"] = "F"; }                        
                    }

                    return View("VerSolicitudesDeMembresia", solicitud);
                }
                else if (submit == "Aceptar")
                {
                    if (SolicitudMembresia.Modificar(solicitud) == 1)
                    {
                        ViewData["message"] = "E";
                    }
                    else { ViewData["message"] = "F"; }

                    SolicitudMembresia.Aceptar(solicitud);
                    return View("VerSolicitudesDeMembresia");
                }
                else if (submit == "rechazar")
                {
                    if (SolicitudMembresia.Modificar(solicitud) == 1)
                    {
                        ViewData["message"] = "E";
                    }
                    else { ViewData["message"] = "F"; }

                    SolicitudMembresia.Rechazar(solicitud);
                    return View("VerSolicitudesDeMembresia");
                }
                else
                {
                    return View("VerSolicitudesDeMembresia", solicitud);
                }
               
            }
            catch (ConstraintException)
            {
                return View("VerSolicitudesDeMembresia", solicitud);
            }
            
        }

        public ActionResult EditarSolicitud(Web.Models.SolicitudMembresia solicitud)
        {
            solicitud = SolicitudMembresia.BuscarId(solicitud.id);
            return View("VerSolicitudesDeMembresia", solicitud);
        }
        public ActionResult AceptarSolicitud(Web.Models.SolicitudMembresia solicitud)
        {

            return View("VerSolicitudesDeMembresia");
        }

        

        public ActionResult getSolicitudesDeMembresia([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Models.SolicitudMembresia> ListaSolicitudesDeMembresia = Models.SolicitudMembresia.SeleccionarTodo();
            try{
            DataSourceResult result = ListaSolicitudesDeMembresia.ToDataSourceResult(request);
            return Json(result);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
            return null;
        }

        public ActionResult EliminarSolicitud(Web.Models.SolicitudMembresia solicitud)
        {
            if(solicitud.estado.Equals("Pendiente")){
            SolicitudMembresia.Eliminar(solicitud);
            return View("VerSolicitudesDeMembresia", solicitud);
            }
            return View("VerSolicitudesDeMembresia");
        }

        public ActionResult FilterMenuCustomization_dni()
        {
            return Json(Models.SolicitudMembresia.SeleccionarTodo().Select(e => e.dni).Distinct(), JsonRequestBehavior.AllowGet);
        }

    }
}
