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
using System.IO;

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

        public bool ValidarFechas(Models.Evento evento)
        {
            if (evento.fechaFin > evento.fechaInicio) return true;
            return false;
        }

        //INSERTAR
        [HttpPost]
        public ActionResult insertarEventoNoCorp(Web.Models.Evento evento)
        {
            if (ValidarFechas(evento))
            {
                if (evento != null)
                {                    
                    if (evento.id == 0)
                    {
                        if (Ambiente.AmbienteReservado(evento.fechaInicio, evento.fechaFin, evento.reserva.ambiente.id) == false)
                        {
                            if (Evento.insertarEventRes(evento) == 1)
                            {
                                string nombre = (string)Session["Nombre"];
                                Negocio.Util.logito.ElLogeador("Insertó evento", nombre);
                                ViewData["message"] = "E";
                            }
                            else
                            {  
                                string nombre = (string)Session["Nombre"];
                                Negocio.Util.logito.ElLogeador("No pudo insertar evento", nombre);
                                ViewData["message"] = "F"; 
                            }
                        }
                        else
                        {
                            string nombre = (string)Session["Nombre"];
                            Negocio.Util.logito.ElLogeador("No pudo insertar evento - Ambiente reservado", nombre); 
                            ViewData["message"] = "Reservado";
                        }
                    }
                    else
                    {
                        if (Evento.modificarEventRes(evento) == 1)
                        {
                            string nombre = (string)Session["Nombre"];
                            Negocio.Util.logito.ElLogeador("Modificó evento", nombre);
                            ViewData["message"] = "E";
                        }
                        else
                        {
                            string nombre = (string)Session["Nombre"];
                            Negocio.Util.logito.ElLogeador("No pudo modificar evento", nombre); 
                            ViewData["message"] = "F";
                        }
                    }
                }
            }
            else
            {
                string nombre = (string)Session["Nombre"];
                Negocio.Util.logito.ElLogeador("No pudo insertar/modificar evento - fecha no válida", nombre);
                ViewData["message"] = "FNV";
            }
            return View("MantenerEventos", evento);
        }


        [HttpPost]
        public ActionResult insertarEventoCorp(Web.Models.EventoCorporativo eventoCorp, HttpPostedFileBase file)
        {
            if (ValidarFechas(eventoCorp))
            {
                if (eventoCorp != null)
                {                    
                    if (eventoCorp.id == 0)
                    {
                        if (Ambiente.AmbienteReservado(eventoCorp.fechaInicio, eventoCorp.fechaFin, eventoCorp.reserva.ambiente.id) == false)
                        {
                            if (EventoCorporativo.insertarCorp(eventoCorp) == 1)
                            {   //Agregar archivo de lista de participantes                            
                                if (file != null && file.ContentLength > 0)
                                {
                                    var fileExtension = Path.GetExtension(file.FileName);
                                    var path = Path.Combine(Server.MapPath("~/Content/ListaParticipantesEvento/"),"ListaParticipantes-"+eventoCorp.razonSocial+fileExtension);
                                    file.SaveAs(path);
                                } 
                                string nombre = (string)Session["Nombre"];
                                Negocio.Util.logito.ElLogeador("Insertó evento corporativo", nombre);
                                ViewData["message"] = "E";
                            }
                            else
                            {
                                string nombre = (string)Session["Nombre"];
                                Negocio.Util.logito.ElLogeador("No pudo insertar evento corporativo", nombre); 
                                ViewData["message"] = "F";
                            }
                        }
                        else
                        {
                            string nombre = (string)Session["Nombre"];
                            Negocio.Util.logito.ElLogeador("No pudo insertar evento corporativo - Ambiente reservado", nombre);
                            ViewData["message"] = "Reservado";}
                    }
                    else
                    {
                        if (EventoCorporativo.modificarCorp(eventoCorp) == 1)
                        {
                            string nombre = (string)Session["Nombre"];
                            Negocio.Util.logito.ElLogeador("Modificó evento", nombre);
                            ViewData["message"] = "E";
                        }
                        else
                        {
                            string nombre = (string)Session["Nombre"];
                            Negocio.Util.logito.ElLogeador("No pudo modificar evento", nombre); 
                            ViewData["message"] = "F";
                        }
                    }
                }
            }
            else
            {
                string nombre = (string)Session["Nombre"];
                Negocio.Util.logito.ElLogeador("No pudo insertar/modificar evento - Fecha no válida", nombre);
                ViewData["message"] = "FNV";
            }
            return View("MantenerEventosCorp", eventoCorp);
        }

        //ELIMINAR
        public ActionResult eliminarEventoNoCorp(Web.Models.Evento evento)
        {
            if (evento != null)
            {
                string nombre = (string)Session["Nombre"];
                Negocio.Util.logito.ElLogeador("Eliminó evento", nombre);
                Evento.eliminar(evento);
            }
            return View("MantenerEventos", evento);
        }

        public ActionResult eliminarEventoCorp(Web.Models.EventoCorporativo eventoCorp)
        {
            if (eventoCorp != null)
            {
                string nombre = (string)Session["Nombre"];
                Negocio.Util.logito.ElLogeador("Eliminó evento corporativo", nombre);
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
        //*******************CONTROLADOR PARA INSERTAR UNA LISTA 
        public ActionResult BajarArchivoEvento(Web.Models.EventoCorporativo eventoCorp)
        {
            Models.EventoCorporativo eve = EventoCorporativo.buscarIdCorp(eventoCorp.id);
            Stream download = null;
            string path = "~/Content/ListaParticipantesEvento/ListaParticipantes-" + eve.razonSocial + ".txt";
            
            try
            {
                download = new FileStream(Server.MapPath(path),FileMode.Open,FileAccess.Read);

                Response.ContentType = "text/plain";
                string nombDescarga = "attachment; filename=ListaParticipantes-"+eve.razonSocial+".txt";
                Response.AppendHeader("Content-Disposition", nombDescarga);

                // Write the file to the Response
                const int bufferLength = 10000;
                byte[] buffer = new Byte[bufferLength];
                int length = 0;

                do
                {
                    if (Response.IsClientConnected)
                    {
                        length = download.Read(buffer, 0, bufferLength);
                        Response.OutputStream.Write(buffer, 0, length);
                        buffer = new Byte[bufferLength];
                    }
                    else
                    {
                        length = -1;
                    }
                }
                while (length > 0);
                Response.Flush();
                Response.End();                
                download.Close();
            }
            catch (Exception)
            {
                ViewData["message"] = "NoFile";               
                return View("MantenerEventosCorp",eve);
            }
            return View();
        }
    }
}