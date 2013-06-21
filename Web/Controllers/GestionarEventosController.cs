using System;
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
using System.ComponentModel.DataAnnotations;

namespace Web.Controllers
{
    public class GestionarEventosController : Controller
    {
        
        //CONTROLADOR DE EVENTOS//
        

        //LEER EVENTOS
        public ActionResult leerEventosNoCorp([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Datos.Evento> Lista = Negocio.Evento.seleccionarTodo();
            DataSourceResult result = Models.Evento.ConvertirLista(Lista).ToDataSourceResult(request);
            return Json(result);
        }
        
        public ActionResult leerEventosCorp([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Datos.EventoCorporativo> Lista = Negocio.EventoCorp.seleccionarEventoCorp();
            DataSourceResult result = Models.EventoCorporativo.ConvertirListaCorp(Lista).ToDataSourceResult(request);
            return Json(result);
        }

        //MODIFICAR 
        public ActionResult modificarEventoNoCorp(Web.Models.Evento evento)
        {
            evento = Evento.buscarId(evento.id);
            return View("MantenerEventos", evento);
        }

        public ActionResult modificarEventoCorp(Web.Models.EventoCorporativo eventoCorp)
        {
            eventoCorp = EventoCorporativo.buscarIdCorp(eventoCorp.id);
            return View("MantenerEventosCorp", eventoCorp);
        }

        //INSERTAR
        [HttpPost]
        public ActionResult insertarEventoNoCorp(Web.Models.Evento evento)
        {

            if (evento != null)
            {
                    if (evento.id == 0)
                    {
                        evento.estado = ListaEstados.ESTADO_ACTIVO;
                        if (Evento.insertarEventREs(evento) == 1)
                        {
                            ViewData["message"] = "E";
                        }
                        else { ViewData["message"] = "F"; }
                    }
                    else
                    {
                        if (Evento.modificar(evento) == 1)
                        {
                            ViewData["message"] = "E";
                        }
                        else { ViewData["message"] = "F"; }
                    }                
            }
            return View("MantenerEventos", evento);
        }

        [HttpPost]
        public ActionResult insertarEventoCorp(Web.Models.EventoCorporativo eventoCorp)
        {

            if (eventoCorp != null)
            {
                if (eventoCorp.id == 0)
                {
                    eventoCorp.estado = ListaEstados.ESTADO_ACTIVO;
                    if (EventoCorporativo.insertarCorpRes(eventoCorp) == 1)
                    {
                        ViewData["message"] = "E";
                    }
                    else { ViewData["message"] = "F"; }
                }
                else
                {
                    if (EventoCorporativo.modificar(eventoCorp) == 1)
                    {
                        ViewData["message"] = "E";
                    }
                    else { ViewData["message"] = "F"; }
                }
            }
            return View("MantenerEventosCorp", eventoCorp);
        }

        //ELIMINAR
        public ActionResult eliminarEventoNoCorp(Web.Models.Evento evento)
        {
            if (evento != null)
            {
                Evento.eliminar(evento);
            }
            return View("MantenerEventos", evento);
        }

        public ActionResult eliminarEventoCorp(Web.Models.EventoCorporativo eventoCorp)
        {
            if (eventoCorp != null)
            {
                EventoCorporativo.eliminar(eventoCorp);
            }
            return View("MantenerEventosCorp", eventoCorp);
        }
        //CANCELAR
        public ActionResult cancelarEventoNoCorp()
        {
            return RedirectToAction("MantenerEventos", "GestionarEventos");
        }

        public ActionResult cancelarEventoCorp()
        {
            return RedirectToAction("MantenerEventosCorp", "GestionarEventos");
        }

        //******************* CONTROLADOR DE VISTA PRINCIPAL DE EVENTOS

        //VENTANA DE EVENTOS NO CORPORATIVOS
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult MantenerEventos()
        {
            ViewData["message"] = null;
            return View((IView)null);
        }
        //VENTANA DE EVENTOS CORPORATIVOS
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult MantenerEventosCorp()
        {
            ViewData["message"] = null;
            return View((IView)null);
        }
        //******************* 
    }
}