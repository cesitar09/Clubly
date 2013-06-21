using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Newtonsoft.Json;
using Web.Controllers;
using Web.Models;
using System.Data;
namespace Web.Controllers
{
    public class RRHHController : Controller
    {
        //
        // GET: /Empleado/

        public JsonResult L()
        {
            IEnumerable<Models.TipoEmpleado> Listatipos = Models.TipoEmpleado.seleccionarTodo();
            
            return Json(Listatipos  , JsonRequestBehavior.AllowGet);
        }

        public ActionResult Leer([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                IEnumerable<Models.Empleado> ListaEmpleado = Models.Empleado.seleccionarTodo();
                DataSourceResult result = ListaEmpleado.ToDataSourceResult(request);
                return Json(result);
            }
            catch (EntityException) {
                return View("MantenerEmpleado");
            }
        }    
            

        public ActionResult Leer_Sedes()
        {
            IEnumerable<Models.Sede> ListaSedes = Models.Sede.SeleccionarTodo();
           
            return Json(ListaSedes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Leer_Estados()
        {
            IEnumerable<Models.Persona.Estado_Civil> Listaestados = Models.Persona.listestadocivil.ToList();
            return Json(Listaestados, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Leer_Tipos()
        {
            IEnumerable<Models.TipoEmpleado> Listatipos = Models.TipoEmpleado.seleccionarTodo();
           
            return Json(Listatipos, JsonRequestBehavior.AllowGet);
        } 
        public ActionResult FilterMenuCustomization_Nombre()
        {
            return Json(Models.Empleado.seleccionarTodo().Select(e => e.persona.nombre).Distinct(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult FilterMenuCustomization_Sueldo()
        {
            return Json(Models.Empleado.seleccionarTodo().Select(e => e.sueldo).Distinct(), JsonRequestBehavior.AllowGet);
        }

     
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult MantenerEmpleado( Web.Models.Empleado empleado)
        {
            ViewData["message"] = null;
            return View((IView)null);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Eliminar([DataSourceRequest] DataSourceRequest request, Web.Models.Empleado empleado)
        {
            if (empleado != null)
            {
                if (Empleado.eliminar(empleado) == 1)
                    ViewData["message"] = "E";
                else ViewData["message"] = "F";
            }

            return View("MantenerEmpleado");
        }

         [AcceptVerbs( HttpVerbs.Post)]
        public ActionResult Guardar(Web.Models.Empleado empleado) {
            try
            {
                
                
                    if (empleado.persona == null || empleado.persona.id == 0)
                    {
                        if (Empleado.insertar(empleado) == 1)
                            ViewData["message"] = "E";
                        else ViewData["message"] = "F";
                    }
                    else
                    {
                        Empleado emp = Empleado.buscarId(empleado.persona.id);
                        
                        if(Empleado.modificar(emp, empleado)== 1)
                            ViewData["message"] = "E";
                        else ViewData["message"] = "F";
                    }



                    return View("MantenerEmpleado", empleado);
                
            }
            catch (ConstraintException) {
                return View("MantenerEmpleado", empleado);
            }
        }

       
        public ActionResult Editar(Web.Models.Empleado empleado) {
            if (empleado != null)
            {
                //IEnumerable<Models.Sede> sedes = Models.Sede.SeleccionarTodo();
                //int index;
                //if (empleado.sede != null)
                //{
                //    for (index = 0; index < sedes.Count(); index++)
                //    {
                //        if (sedes.ElementAt(index) == empleado.sede) break;

                //    }
                //    ViewBag.indexSede = index;
                //}
                empleado = Empleado.buscarId(empleado.persona.id);
            }
            return View("MantenerEmpleado", empleado);
        }

        //ASISTENCIA/////////////////////////////////////////////////////

        public ActionResult Asistencia() 
        {
            return View((IView)null);
        }

        [HttpPost]
        public JsonResult Asistencia(string data)
        {
            short numero = Convert.ToInt16(data);
            Empleado emp = Models.Empleado.buscarId(numero);
            return Json(new{ me = emp.persona.nombre });
        }

        [AcceptVerbs( HttpVerbs.Post)]
        public ActionResult AñadirAsistencia(Web.Models.Empleado empleado)
        {
            if (empleado != null)
            {
                int val = Models.Asistencia.InsertarAsistencia(empleado); 
                if ( val == 0)
                {
                    ViewData["message"] = "N";
                }
                else if (val == 1)
                {
                    ViewData["message"] = "EA";
                }
                else if (val == 2)
                {
                    ViewData["message"] = "EM";
                }
                else if (val == 3)
                {
                    ViewData["message"] = "F";
                }
                else if (val == 4) 
                {
                    ViewData["message"] = "YAAM";    
                }
                
            }
            return View("Asistencia", empleado);
        }
    }
}
