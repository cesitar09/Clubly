using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Kendo.Mvc.UI;
using Web.Controllers;
using Web.Models;
using System.Data.Linq;
using Kendo.Mvc.Extensions;
using System.Web.Script.Serialization;
using System.Net;

namespace Web.Controllers
{
    public class GestionarActividadController : Controller
    {
        //LEER SEDES
        public ActionResult leerSedes()
        {
            IEnumerable<Models.Sede> Lista = Models.Sede.SeleccionarTodo();

            return Json(Lista, JsonRequestBehavior.AllowGet);
        }

        //LEER AMBIENTE
        public ActionResult leerAmbientes(string idSede)
        {
            short idsede = Convert.ToInt16(idSede);
            IEnumerable<Models.Ambiente> listaAmbientes = null;// Models.Ambiente.buscarPorSede(idsede);
            return Json(listaAmbientes, JsonRequestBehavior.AllowGet);
        }

        // MANTENER ACTIVIDAD ************************

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult MantenerActividad()
        {
            ViewData["message"] = null;
            return View((IView)null);
        }

        public ActionResult leerActividad([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                IEnumerable<Models.Actividad> Lista = Models.Actividad.SeleccionarTodo();
                DataSourceResult result = Lista.ToDataSourceResult(request);
                return Json(result);
            }
            catch (Exception)
            {
                return Json(null);
            }
        }

        public ActionResult modificarActividad(Web.Models.Actividad actividad)
        {
            actividad = Actividad.buscarId(actividad.id);
            return View("MantenerActividad", actividad);
        }

        public bool ValidarFechas(Models.Actividad actividad)
        {
            if (actividad.fechaFin > actividad.fechaInicio) return true;
            return false;
        }

        [HttpPost]
        public ActionResult insertarActividad(Web.Models.Actividad actividad)
        {
                
            if (ValidarFechas(actividad))
            {
                if (actividad != null)
                {
                    if (actividad.id == 0)
                    {
                        if (Ambiente.AmbienteReservado(actividad.fechaInicio, actividad.fechaFin, actividad.reserva.ambiente.id) == false)
                        {
                            if (Actividad.insertarAR(actividad) == 1)
                            {
                                string nombre = (string)Session["Nombre"];
                                Negocio.Util.logito.ElLogeador("Insertó Actividad", nombre);
                                ViewData["message"] = "E";
                            }
                            else { ViewData["message"] = "F"; }
                        }
                        else { ViewData["message"] = "Reservado";}
                    }
                    else
                    {
                        //if (Actividad.HayInscritos(actividad) == false)
                        //{
                            if (Actividad.modificarAR(actividad) == 1)
                            {
                                string nombre = (string)Session["Nombre"];
                                Negocio.Util.logito.ElLogeador("Modificó Actividad",nombre);
                                ViewData["message"] = "E";
                            }
                            else { ViewData["message"] = "F"; }

                        //}
                        //else ViewData["message"] = "M";
                    }
                }
            }
            else
            {
                string nombre = (string)Session["Nombre"];
                Negocio.Util.logito.ElLogeador("Fecha no válida para insertar/modificar actividad", nombre);
                ViewData["message"] = "FNV";
            }
            return View("MantenerActividad",actividad);
        }


        public ActionResult eliminarActividad(Web.Models.Actividad actividad)
        {
            if (Actividad.HayInscritos(actividad) == false)
            {
                if (actividad != null)
                {
                    Actividad.eliminar(actividad);
                    string nombre = (string)Session["Nombre"];
                    Negocio.Util.logito.ElLogeador("Eliminó Actividad", nombre);
                }
            }
            else
            {
                string nombre = (string)Session["Nombre"];
                Negocio.Util.logito.ElLogeador("No pudo eliminar actividad", nombre);
                ViewData["message"] = "ELIMINA";
            }

            return View("MantenerActividad");
        }

        public ActionResult cancelarActividad()
        {
            return RedirectToAction("MantenerActividad", "GestionarActividad");
        }

        //******************* FILTROS ACTIVIDAD

        public ActionResult FilterMenuCustomization_TipoActividad()
        {
            return Json(Negocio.Actividad.SeleccionarTodo().Select(e => e.TipoActividad.nombre).Distinct(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult FilterMenuCustomization_Precio()
        {
            return Json(Negocio.Actividad.SeleccionarTodo().Select(e => e.precio).Distinct(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult FilterMenuCustomization_FechaInicio()
        {
            return Json(Negocio.Actividad.SeleccionarTodo().Select(e => e.fechaInicio).Distinct(), JsonRequestBehavior.AllowGet);
        }


        // INSCRIBIR SOCIO EN ACTIVIDAD **************

        public ActionResult InscribirSocioEnActividad()
        {
            return View();
        }
        public ActionResult AgregarSocioEnActividad(string strIdSocio, string strIdActividad)
        {
            try
            {
                short idSocio = short.Parse(strIdSocio);
                short idActividad = short.Parse(strIdActividad);
                if (Models.SocioXActividad.Insertar(idSocio, idActividad) == 0)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json("Error: no hay vacantes disponibles");
                }
                else
                    return Json("");
            }
            catch (Exception e)
            {
                Console.WriteLine("Excepcion en GestionarActividadController\n", e);
            }
            return null;
        }
        public ActionResult EliminarInscripcion(string strIdSocio, string strIdActividad)
        {
            try
            {
                short idSocio = short.Parse(strIdSocio);
                short idActividad = short.Parse(strIdActividad);
                if ((idSocio != 0) && (idActividad != 0))
                {
                    SocioXActividad.Eliminar(idSocio, idActividad);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Excepcion en GestionarActividadController\n", e);
            }
            return Json("");
        }

        // Esto es para mi tabla kendo, que te va a mostrar todas las actividades para llenar la data
        public ActionResult LeerActividadesDisponible([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                IEnumerable<Models.Actividad> ListaActividades = Models.Actividad.SeleccionarActividadesDisponibles();
                DataSourceResult result = ListaActividades.ToDataSourceResult(request);
                return Json(result);
            }
            catch (Exception e)
            {
                Console.WriteLine("Excepcion en GestionarActividadController\n", e);
                DataSourceResult result = new DataSourceResult();
                result.Errors = "error";
                return Json(result);
            }

        }

        //Esto es para la tabla kendo, que te da todos los socios
        public ActionResult LeerSociosNoInscritos([DataSourceRequest] DataSourceRequest request, Models.Actividad actividad)
        {
            try
            {
                IList<Models.Socio> listaSocios = Models.Socio.SeleccionarTodo().ToList();
                IEnumerable<Models.SocioXActividad> listaInscritos = Models.SocioXActividad.BuscarIdActividad(actividad.id);
                foreach (SocioXActividad inscrito in listaInscritos)
                {
                    listaSocios.Remove(listaSocios.FirstOrDefault(s => s.persona.id == inscrito.idSocio));
                }
                DataSourceResult result = listaSocios.ToDataSourceResult(request);
                return Json(result);
            }
            catch (Exception e)
            {
                Console.WriteLine("Excepcion en GestionarActividadController\n", e);
                DataSourceResult result = new DataSourceResult();
                result.Errors = "error";
                return Json(result);
            }
        }

        // Esto es para mi tabla kendo, que te da toda la data a mostrar socioxActiviad       
        public ActionResult LeerSocioxActividad([DataSourceRequest] DataSourceRequest request, Models.Actividad actividad)
        {
            try
            {
                IEnumerable<Models.SocioXActividad> listaSocioXActividades = Models.SocioXActividad.BuscarIdActividadIdFamilia(actividad.id, 10001);
                DataSourceResult result = listaSocioXActividades.ToDataSourceResult(request);
                return Json(result);
            }
            catch (Exception e)
            {
                Console.WriteLine("Excepcion en GestionarActividadController\n", e);
                DataSourceResult result = new DataSourceResult();
                result.Errors = "error";
                return Json(result);
            }
        }
        public ActionResult LeerTodoSocioxActividad([DataSourceRequest] DataSourceRequest request, Models.Actividad actividad)
        {
            try
            {
                IEnumerable<Models.SocioXActividad> listaSocioXActividades = Models.SocioXActividad.BuscarIdActividad(actividad.id);
                DataSourceResult result = listaSocioXActividades.ToDataSourceResult(request);
                return Json(result);
            }
            catch (Exception e)
            {
                Console.WriteLine("Excepcion en GestionarActividadController\n", e);
                DataSourceResult result = new DataSourceResult();
                result.Errors = "error";
                return Json(result);
            }
        }
        //Metodo en cual llama a la ventana para poder inscribir al socio y a sus familiares
        public ActionResult InscribirASocio(Models.Actividad actividad)
        {
            try
            {
                Models.Actividad actividadXid = Models.Actividad.buscarId(actividad.id);
                return View(actividadXid);
            }
            catch (Exception e)
            {
                Console.WriteLine("Excepcion en GestionarActividadController\n", e);
                DataSourceResult result = new DataSourceResult();
                result.Errors = "error";
                return Json(result);
            }
        }

        //Metodo que llama el Cancelar y cancela todo para mostrar la pagina inicial otra vez
        public ActionResult CancelarInscripcionTotal()
        {
            return RedirectToAction("InscribirSocioEnActividad","GestionarActividad");
        }

    }
}
