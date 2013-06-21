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
    public class AdministracionController : Controller
    {
        //
        // GET: /Administracion/

        public ActionResult getConceptoPago([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Models.ConceptoDePago> ListaConceptoDePago = Models.ConceptoDePago.SeleccionarTodo();
            DataSourceResult result = ListaConceptoDePago.ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult EditarInscripcion(Web.Models.ConceptoDePago concepto)
        {
            concepto = ConceptoDePago.SeleccionarporId(concepto.id);
            return View("TiposPago", concepto);
        }

        public ActionResult FilterMenuCustomization_Nombre()
        {
            return Json(Negocio.ConceptoDePago.SeleccionarTodoTiposDePago().Select(e => e.nombre).Distinct(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult FilterMenuCustomization_Monto()
        {
            return Json(Negocio.ConceptoDePago.SeleccionarTodoTiposDePago().Select(e => e.monto).Distinct(), JsonRequestBehavior.AllowGet);
        } 

        //public ActionResult Remote_Data()
        //{
        //    return View("AjaxBinding");
        //}

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult TiposPago(Web.Models.ConceptoDePago conceptoDePago)
        {
            return View(conceptoDePago);
        }

        public ActionResult getActividad([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Models.Actividad> ListaActividad = Models.Actividad.SeleccionarTodo();
            DataSourceResult result = ListaActividad.ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult MantenerActividad() 
        {
            Datos.Actividad actividad = new Datos.Actividad();
            Models.Actividad Actividad = new Models.Actividad(actividad);
            return View(Actividad);
        }

        public ActionResult FilterActividad_Nombre()
        {
            return Json(Negocio.Actividad.SeleccionarTodo().Select(e => e.nombre).Distinct(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult FilterActividad_Precio()
        {
            return Json(Negocio.Actividad.SeleccionarTodo().Select(e => e.precio).Distinct(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CancelarTipoDePago()
        {
            return RedirectToAction("TiposPago", "Administracion");
        }

        [HttpPost]
        public ActionResult AñadirTipoDePago(Web.Models.ConceptoDePago concepto) 
        {
            if (concepto != null) 
            {
                    if (concepto.nombre != null && concepto.descripcion != null)
                    {
                        if (concepto.id == 0)
                        {
                            //concepto.estado = 1;
                            //ConceptoDePago.insertarConceptoDePago(concepto);
                        }
                        else
                        {
                            ConceptoDePago.modificarConceptoDePago(concepto);
                        }
                    }   
            }
            return View("TiposPago",concepto);
        }


        /////////////Mantenimiento Productos//////////////////////////
        public ActionResult getProducto([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Models.Producto> ListaProducto = Models.Producto.SeleccionarTodo();
            DataSourceResult result = ListaProducto.ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult EditarProducto(Web.Models.Producto producto)
        {
            producto = Producto.SeleccionarporId(producto.id);
            return View("MantenimientoProducto", producto);
        }

        public ActionResult FilterMenuCustomization_NombreProd()
        {
            return Json(Negocio.Producto.SeleccionarTodoProducto().Select(e => e.nombre).Distinct(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult FilterMenuCustomization_PrecUnitarioProd()
        {
            return Json(Negocio.Producto.SeleccionarTodoProducto().Select(e => e.precioUnitario).Distinct(), JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult MantenimientoProducto(Web.Models.Producto producto)
        {
            ViewData["message"] = null;
            return View((IView)null);
        }

        public ActionResult CancelarProducto()
        {
            return RedirectToAction("MantenimientoProducto", "Administracion");
        }

        [HttpPost]
        public ActionResult AñadirProducto(Web.Models.Producto producto)
        {
            if (producto != null)
            { 
                    if (producto.nombre != null && producto.descripcion != null)
                    {
                        if (producto.id == 0)
                        {
                            producto.estado = 1;
                            if (Producto.insertarProducto(producto) == 1)
                            {
                                ViewData["message"] = "E";
                            }
                            else { ViewData["message"] = "F"; }
                        }
                        else
                        {
                            if (Producto.modificarProducto(producto) == 1)
                            {
                                ViewData["message"] = "E";
                            }
                            else { ViewData["message"] = "F"; }
                        }
                    } 
            }
            return View("MantenimientoProducto", producto);
        }
         
        [HttpPost]
        public ActionResult EliminarProducto(Web.Models.Producto producto) 
        {
            if (producto != null)
            {
                Producto.eliminar(producto);  
            }
            return View("MantenimientoProducto", producto);
        }


        // Mantener Servicios
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult MantenerServicio(Web.Models.Servicio servicio)
        {

            ViewData["message"] = null;
            return View((IView)null);
        }

        /*[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InsertarServicio(Web.Models.Servicio servicio)
        {
            try
            {
                if (servicio != null)
                {
                    if (servicio.id == 0)
                    {
                        servicio.estado = 1;
                        if (Servicio.insertar(servicio) == 1)
                        {
                            ViewData["message"] = "E";
                        }
                        else { ViewData["message"] = "F"; }
                    }
                    else
                    {
                        if (Servicio.modificar(servicio) == 1)
                        {
                            ViewData["message"] = "E";
                        }
                        else { ViewData["message"] = "F"; }
                    }
                }
                return View("MantenerServicio", servicio);
            }
            catch (ConstraintException)
            {
                return View("MantenerServicio", servicio);
            }
            catch (UpdateException)
            {
                return View("MantenerServicio", servicio);
            }
            catch (SqlException)
            {
                return View("MantenerServicio", servicio);
            }
            catch (ValidationException)
            {
                return View("MantenerServicio", servicio);
            }
        }

        public ActionResult EditarServicio(Web.Models.Servicio servicio)
        {
            servicio = Servicio.buscarId(servicio.id);
            return View("MantenerServicio", servicio);
        }

        public ActionResult LeerServicio([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Models.Servicio> lista = Servicio.SeleccionarTodo();
            DataSourceResult result = lista.ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult EliminarServicio(Web.Models.Servicio servicio)
        {
            Servicio.eliminar(servicio);
            return View("MantenerServicio", servicio);
        }

        public IEnumerable<String> EnlistarSedes()
        {
            return Servicio.listaSedes();
        }*/


        // Mantener Promociones
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
         public ActionResult MantenerPromociones(Web.Models.Promocion promocion)
         {

             ViewData["message"] = null;
             return View((IView)null);
         }

         /*[AcceptVerbs(HttpVerbs.Post)]
         public ActionResult InsertarPromocion(Web.Models.Promocion promocion)
         {
             try
             {
                 if (promocion != null)
                 {
                     if (promocion.id == 0)
                     {
                         promocion.estado = 1;
                         if (Promocion.insertar(promocion) == 1)
                         {
                             ViewData["message"] = "E";
                         }
                         else { ViewData["message"] = "F"; }
                     }
                     
                 }
                 return View("MantenerPromocion", promocion);
             }
             catch (ConstraintException)
             {
                 return View("MantenerPromocion", promocion);
             }
             catch (UpdateException)
             {
                 return View("MantenerPromocion", promocion);
             }
             catch (SqlException)
             {
                 return View("MantenerPromocion", promocion);
             }
             catch (ValidationException)
             {
                 return View("MantenerPromocion", promocion);
             }
         }

         public ActionResult LeerPromocion([DataSourceRequest] DataSourceRequest request)
         {
             IEnumerable<Models.Promocion> lista = Promocion.SeleccionarTodo();
             DataSourceResult result = lista.ToDataSourceResult(request);
             return Json(result);
         }

         public ActionResult EliminarPromocion(Web.Models.Promocion promocion)
         {
             Promocion.eliminar(promocion);
             return View("MantenerPromocion", promocion);
         }

         public IEnumerable<String> EnlistarFamilias()
         {
             return Promocion.listaFamilias();
         }*/


        /****************TEMPORADA ALTA*******/

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult MantenerTemporadaAlta(Web.Models.TemporadaAlta tempA)
        {
            ViewData["message"] = null;
            return View((IView)null);
        }

        public ActionResult getTemporadaAlta([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Datos.TemporadaAlta> ListaTempA = Negocio.TemporadaAlta.seleccionarTodo();
            DataSourceResult result = Models.TemporadaAlta.ConvertirLista(ListaTempA).ToDataSourceResult(request);
          
            return Json(result);
        }

        public ActionResult eliminarTemporadaAlta([DataSourceRequest] DataSourceRequest request, Web.Models.TemporadaAlta tempA)
        {
            if (tempA != null)
            {
                TemporadaAlta.eliminarTemporadaAlta(tempA);
            }
            return Json(ModelState.ToDataSourceResult());
        }


        public ActionResult modificarTemporadaAlta(Web.Models.TemporadaAlta tempA)
        {
            tempA = TemporadaAlta.SeleccionarporId(tempA.id);

            return View("MantenerTemporadaAlta", tempA);

        }

        [HttpPost]
        public ActionResult agregarTemporadaAlta(Web.Models.TemporadaAlta tempA)
        {
            if (tempA != null)
            {
                if (tempA.id == 0)
                {
                    tempA.estado = ListaEstados.ESTADO_ACTIVO;
                    if (TemporadaAlta.insertarTemporadaAlta(tempA) == 1)
                    {
                        ViewData["message"] = "E";
                    }
                    else { ViewData["message"] = "F"; }
                }
                else
                {
                    if (TemporadaAlta.modificarTemporadaAlta(tempA) == 1)
                    {
                        ViewData["message"] = "E";
                    }
                    else { ViewData["message"] = "F"; }
                }
            }

            return View("MantenerTemporadaAlta", tempA);
        }

    }

}

