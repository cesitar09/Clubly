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
using System.IO;
using System.Data.SqlClient;
using Negocio.Util;
using System.ComponentModel.DataAnnotations;
using Web.Util;
using System.Net;

namespace Web.Controllers
{
    [HandleError]
    public class LogisticaController : Controller
    {
        //
        // GET: /Logistica/

        //..............AMBIENTE.................//

        public ActionResult getAmbiente([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Datos.Ambiente> ListaAmbientes = Negocio.Ambiente.seleccionarTodo();
            DataSourceResult result = Models.Ambiente.ConvertirLista(ListaAmbientes).ToDataSourceResult(request);
            //DataSourceResult result = Models.Ambiente.SeleccionarTodo().ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult Leer_EstadosAmbiente()
        {
            IEnumerable<Models.Ambiente.Estado_Ambiente> Listaestados = Models.Ambiente.listestadoamb.ToList();
            return Json(Listaestados, JsonRequestBehavior.AllowGet);    
        }

        public ActionResult eliminarAmbiente([DataSourceRequest] DataSourceRequest request, Web.Models.Ambiente amb)
        {


            if (Models.Ambiente.HayReserva(amb) == false) {
                if (amb != null)
                {
                    Models.Ambiente.eliminarAmbiente(amb.id);
                }
            }
            else ViewData["message"] = "ELIMINA";
            
            return View("MantenerAmbiente", amb);
               
        }
        
        public ActionResult modificarAmbiente(Web.Models.Ambiente amb)
        {
            amb = Ambiente.SeleccionarporId(amb.id);

            ViewData["sedeSeleccionarTodo"] = Sede.SeleccionarTodo();
            ViewData["listaEstadosAmbiente"] = Ambiente.listestadoamb;
            ViewData["ambienteSeleccionarTodo"] = Ambiente.SeleccionarTodo();
            return View("MantenerAmbiente", amb);

        }

        [HttpPost]
        public ActionResult agregarAmbiente(Web.Models.Ambiente amb)
        {
            try
            {
                if (amb.id == 0)
                {
                    Ambiente.insertarAmbiente(amb);
                }
                else
                {
                    Ambiente.modificarAmbiente(amb);
                }
            }
            catch (SqlException)
            {
                ViewData["message"] = TransactionMessages.SQL_EXCEPTION_MESSAGE;
            }
            catch (EntityException)
            {
                ViewData["message"] = TransactionMessages.ENTITY_EXCEPTION_MESSAGE;
            }
            ViewData["sedeSeleccionarTodo"] = Sede.SeleccionarTodo();
            ViewData["listaEstadosAmbiente"] = Ambiente.listestadoamb;
            ViewData["ambienteSeleccionarTodo"] = Ambiente.SeleccionarTodo();
            return View("MantenerAmbiente", amb);
        }

        public ActionResult Leer_Sedes()
        {
            IEnumerable<Models.Sede> ListaSedes = Models.Sede.SeleccionarTodo();

            return Json(ListaSedes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FilterMenuCustomization_Nombre2()
        {
            return Json(Negocio.Ambiente.seleccionarTodo().Select(e => e.nombre).Distinct(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult FilterMenuCustomization_Area()
        {
            return Json(Negocio.Ambiente.seleccionarTodo().Select(e => e.area).Distinct(), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult MantenerAmbiente(Web.Models.Ambiente amb)
        {
            ViewData["sedeSeleccionarTodo"] = Sede.SeleccionarTodo();
            ViewData["listaEstadosAmbiente"] = Ambiente.listestadoamb;
            ViewData["ambienteSeleccionarTodo"] = Ambiente.SeleccionarTodo();
            return View((IView)null);
        }


        //Validar Ambiente para no Eliminarlos

       


        
        //..............SEDE.................//

        [HttpPost]
        public ActionResult Create(Sede configItem)
        {
            if (ModelState.IsValid)
            {
                // do something.
            }
            return View("Index", configItem);
        }

        public ActionResult getSede([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Models.Sede> ListaSedes = Models.Sede.SeleccionarTodo();
            DataSourceResult result = ListaSedes.ToDataSourceResult(request);
            return Json(result);
        }


        public ActionResult eliminarSede ([DataSourceRequest] DataSourceRequest request, Web.Models.Sede sede)
        {
            if (Models.Sede.HayAmbBung(sede) == false)
            {
                if (sede != null)
                {
                    Sede.eliminarSede(sede);
                }
            }
            else ViewData["message"] = "ELIMINA";
            return View("MantenerSede", sede);
        }

        public ActionResult habilitarSede([DataSourceRequest] DataSourceRequest request, Web.Models.Sede sede)
        {
            if (sede != null)
            {
                Sede.habilitarSede(sede);
            }

            return Json(ModelState.ToDataSourceResult());
        }


        public ActionResult modificarSede(Web.Models.Sede sede)
        {
            sede = Sede.SeleccionarporId(sede.id);

            return View("MantenerSede", sede);
        }

        [HttpPost]
        public ActionResult agregarSede(Web.Models.Sede sede)
        {
            if (sede != null)
            {
                    if (sede.id == 0)
                    {
                        sede.estado = ListaEstados.ESTADO_ACTIVO;
                        try
                        {
                            int nuevoId=Sede.insertarSede(sede);
                            ViewData["message"] = "E";
                        }catch(Exception){
                            ViewData["message"] = "F";
                        }
                    }
                    else
                    {
                        try
                        {
                            Sede.insertarSede(sede);
                            ViewData["message"] = "E";
                        }
                        catch (Exception)
                        {
                            ViewData["message"] = "F";
                        }
                    }
            }
            return View("MantenerSede", sede);           
        }



        //public ActionResult createSede([DataSourceRequest] DataSourceRequest request)
        //{

           // return Json(result);
       // }


        public ActionResult SubirImagenSede(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                // extract only the fielname
                var fileExtension = Path.GetExtension(file.FileName);
                var fileName =fileExtension;
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/Content/img"), fileName);
                file.SaveAs(path);
            }
            // redirect back to the index action to show the form once again
            return View();
        }
     


        public ActionResult FilterMenuCustomization_Nombre()
        {
            return Json(Negocio.Sede.seleccionarTodo().Select(e => e.nombre).Distinct(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult FilterMenuCustomization_Descripcion()
        {
            return Json(Negocio.Sede.seleccionarTodo().Select(e => e.descripcion).Distinct(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult FilterMenuCustomization_Direccion()
        {
            return Json(Negocio.Sede.seleccionarTodo().Select(e => e.direccion).Distinct(), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult MantenerSede(Web.Models.Sede sede)
        {
            ViewData["message"] = null;
            return View((IView) null);
        }

        public ActionResult Sede2()
        {
            return View(Models.Sede.SeleccionarTodo());
        }

        //Concesionario
        
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult MantenerConcesionario(Web.Models.Concesionario concesionario)
        {
            try
            {
                ViewData["sedeSeleccionarTodo"] = Sede.SeleccionarTodo();
                return View((IView)null);
            }
            catch (SqlException)
            {
                ViewData["message"] = TransactionMessages.SQL_EXCEPTION_MESSAGE;
                ViewData["sedeSeleccionarTodo"] = null;
                return View((IView)null);
            }
            catch (EntityException)
            {
                ViewData["message"] = TransactionMessages.ENTITY_EXCEPTION_MESSAGE;
                ViewData["sedeSeleccionarTodo"] = null;
                return View((IView)null);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InsertarConcesionario(Web.Models.Concesionario concesionario)
        {
            try
            {
                if (concesionario.sedesAux != null)
                {
                    List<Sede> lista = new List<Sede>(concesionario.sedesAux.Count());
                    foreach (short codigo in concesionario.sedesAux)
                    {
                        lista.Add(Sede.buscarId(codigo));
                    }
                    concesionario.sedes = lista.AsEnumerable();
                }

                if (concesionario.id > 0)
                {
                    Concesionario.modificar(concesionario);
                    ViewData["message"] = TransactionMessages.OK_CHANGE_DATA_MESSAGE;
                }
                else
                {
                    concesionario.estado = ListaEstados.ESTADO_ACTIVO;
                    Concesionario.insertar(concesionario);
                    ViewData["message"] = TransactionMessages.OK_ADD_DATA_MESSAGE;
                }
                ViewData["enlistarSedes"] = Sede.Enlistar(concesionario.sedes);
                ViewData["sedeSeleccionarTodo"] = Sede.SeleccionarTodo();
                return View("MantenerConcesionario", concesionario);
            }
            catch (SqlException)
            {
                ViewData["message"] = TransactionMessages.SQL_EXCEPTION_MESSAGE;
                ViewData["sedeSeleccionarTodo"] = null;
                return View("MantenerConcesionario", (IView)null);
            }
            catch (EntityException)
            {
                ViewData["message"] = TransactionMessages.ENTITY_EXCEPTION_MESSAGE;
                ViewData["sedeSeleccionarTodo"] = null;
                return View("MantenerConcesionario", (IView)null);
            }

        }

        public ActionResult EditarConcesionario(Web.Models.Concesionario concesionario)
        {
            try
            {
                concesionario = Concesionario.buscarId(concesionario.id);
                ViewData["enlistarSedes"] = Sede.Enlistar(concesionario.sedes);
                ViewData["sedeSeleccionarTodo"] = Sede.SeleccionarTodo();
                return View("MantenerConcesionario", concesionario);
            }
            catch(SqlException)
            {
                ViewData["message"] = TransactionMessages.SQL_EXCEPTION_MESSAGE;
                ViewData["sedeSeleccionarTodo"] = null;
                return View("MantenerConcesionario", (IView)null);
            }
            catch (EntityException)
            {
                ViewData["message"] = TransactionMessages.ENTITY_EXCEPTION_MESSAGE;
                ViewData["sedeSeleccionarTodo"] = null;
                return View("MantenerConcesionario", (IView)null);
            }
            catch (InvalidOperationException)
            {
                ViewData["message"] = TransactionMessages.SINGLE_NOT_FOUND_MESSAGE;
                ViewData["sedeSeleccionarTodo"] = null;
                return View("MantenerConcesionario", (IView)null);
            }
        }

        public ActionResult LeerConcesionario([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                IEnumerable<Models.Concesionario> lista = Concesionario.SeleccionarTodo();
                DataSourceResult result = lista.ToDataSourceResult(request);
                return Json(result);
            }
            catch (EntityException)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("");
            }
        }

        public ActionResult EliminarConcesionario(Web.Models.Concesionario concesionario)
        {
            try
            {
                Concesionario.eliminar(concesionario.id);
                ViewData["sedeSeleccionarTodo"] = Sede.SeleccionarTodo();
                ViewData["concesionarioSeleccionarTodo"] = Concesionario.SeleccionarTodo();
                return View("MantenerConcesionario", concesionario);
            }
            catch (SqlException)
            {
                ViewData["message"] = TransactionMessages.SQL_EXCEPTION_MESSAGE;
                ViewData["sedeSeleccionarTodo"] = null;
                return View("MantenerConcesionario", (IView)null);
            }
            catch (EntityException)
            {
                ViewData["message"] = TransactionMessages.ENTITY_EXCEPTION_MESSAGE;
                ViewData["sedeSeleccionarTodo"] = null;
                return View("MantenerConcesionario", (IView)null);
            }
            catch (InvalidOperationException)
            {
                ViewData["message"] = TransactionMessages.SINGLE_NOT_FOUND_MESSAGE;
                ViewData["sedeSeleccionarTodo"] = null;
                return View("MantenerConcesionario", (IView)null);
            }
        }

        //CONTROLADOR DE PROVEEDOR//****************************************

        
        public ActionResult leerProveedor([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                IEnumerable<Models.Proveedor> Lista = Models.Proveedor.SeleccionarTodo();
                DataSourceResult result = Lista.ToDataSourceResult(request);
                return Json(result);
            }
            catch (Exception)
            {
                return Json(null);
            }
        }

        public ActionResult modificarProveedor(Web.Models.Proveedor proveedor)
        {
            proveedor = Proveedor.buscarId(proveedor.id);
            return View("MantenerProveedor", proveedor);
        }
        
        [HttpPost]
        public ActionResult insertarProveedor(Web.Models.Proveedor proveedor)
        {

                if (proveedor != null)
                {
                    if (proveedor.nombre != null && proveedor.ruc != null && proveedor.direccion != null)
                    {               
                        if (proveedor.id == 0)
                        {
                            proveedor.estado = 1;
                            if (Proveedor.insertar(proveedor) == 1)
                            {
                                ViewData["message"] = "E";
                            }
                            else { ViewData["message"] = "F"; }
                        }
                        else
                        {
                            if(Proveedor.modificar(proveedor) == 1)
                            {
                                ViewData["message"] = "E";
                            }
                            else { ViewData["message"] = "F"; }
                        }                                             
                    }
                }
                return View("MantenerProveedor", proveedor);
        }


        public ActionResult eliminarProveedor(Web.Models.Proveedor proveedor)
        {
            if (proveedor != null)
            {
                Proveedor.eliminar(proveedor);
            }
            return View("MantenerProveedor", proveedor);
        }
        public ActionResult cancelarProveedor()
        {
            return RedirectToAction("MantenerProveedor","Logistica");        
        }

        //******************* FILTROS PROVEEDOR

        public ActionResult FilterMenuCustomization_NombreProveedor()
        {
            return Json(Negocio.Proveedor.seleccionarTodo().Select(e => e.nombre).Distinct(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult FilterMenuCustomization_RucProveedor()
        {
            return Json(Negocio.Proveedor.seleccionarTodo().Select(e => e.ruc).Distinct(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult FilterMenuCustomization_DireccionProveedor()
        {
            return Json(Negocio.Proveedor.seleccionarTodo().Select(e => e.direccion).Distinct(), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult MantenerProveedor()
        {
            //Datos.Proveedor proveedor = new Datos.Proveedor();
            //Models.Proveedor Proveedor = new Models.Proveedor(proveedor);
            //return View(Proveedor);
            ViewData["message"] = null;
            return View((IView)null);
        }
        //******************************************************************BUNGALOWS

        public ActionResult SubirImagenBugalow(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                // extract only the fielname
                var fileName = Path.GetFileName(file.FileName);
                //var fileName = id.ToString() + fileExtension;
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/Content/img"), fileName);
                file.SaveAs(path);
            }
            // redirect back to the index action to show the form once again
            return View(); 
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult MantenerBungalow(Web.Models.Bungalow bungalow)
        {

            ViewData["message"] = null;
            return View((IView)null);
        }

        public ActionResult InsertarBungalow(Models.Bungalow bungalow)
        {
            if (bungalow != null)
            {
                if (bungalow.id == 0)
                {
                    if (Models.Bungalow.ExisteNumero(bungalow)==false){
                        //bungalow.estado = ListaEstados.ESTADO_ACTIVO;
                        if (Bungalow.insertarBungalow(bungalow) == 1)
                        {
                            ViewData["message"] = "E";
                         }
                        else  ViewData["message"] = "F";
                    }
                   else ViewData["message"] = "NUM";
                }
                else
                {
                    if (Bungalow.modificarBungalow(bungalow) == 1)
                    {
                        ViewData["message"] = "E";
                    }
                    else { ViewData["message"] = "F"; }
                }
            }



            return View("MantenerBungalow", bungalow);
        }

        public ActionResult LeerBungalows([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Models.Bungalow> listaBungalows = Models.Bungalow.SeleccionarTodo();
            try
            {
                DataSourceResult result = listaBungalows.ToDataSourceResult(request);
                return Json(result);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
            return null;
        }

        public ActionResult EditarBungalow(Web.Models.Bungalow bung)
        {
            bung = Bungalow.SeleccionarporId(bung.id);

            return View("MantenerBungalow", bung);
        }


        public ActionResult EliminarBungalow([DataSourceRequest] DataSourceRequest request, Web.Models.Bungalow bungalow)
        {
            if (Models.Bungalow.HayReserva(bungalow) == false)
            {
                if (bungalow != null)
                {
                    Bungalow.eliminarBungalow(bungalow);
                }
            }
            else ViewData["message"] = "ELIMINA";
            return View("MantenerBungalow", bungalow);
        } 

        public ActionResult Leer_Estados() 
        {
            IEnumerable<Models.Bungalow.Estado_Bungalow> Listaestados = Models.Bungalow.listestadobung.ToList();
            return Json(Listaestados, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult habilitarBungalow([DataSourceRequest] DataSourceRequest request, Web.Models.Bungalow bungalow)
        //{
        //    if (bungalow != null)
        //    {
        //        Bungalow.habilitarBungalow(bungalow);
        //    }

        //    return Json(ModelState.ToDataSourceResult());
        //}

        //******************************************************************TIPO BUNGALOWS

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult MantenerTipoBungalow(Web.Models.TipoBungalow tipoB)
        {

            ViewData["message"] = null;
            return View((IView)null);
        }


        public ActionResult InsertarTipoBungalow(Models.TipoBungalow tipoB)
        {
            if (tipoB != null)
            {
                if (tipoB.id == 0)
                {
                    tipoB.estado = ListaEstados.ESTADO_ACTIVO;
                    if (TipoBungalow.insertarTipoBungalow(tipoB) == 1)
                    {
                        ViewData["message"] = "E";
                    }
                    else { ViewData["message"] = "F"; }
                }
                else
                {
                    if (TipoBungalow.modificarTipoBungalow(tipoB) == 1)
                    {
                        ViewData["message"] = "E";
                    }
                    else { ViewData["message"] = "F"; }
                }
            }
            return View("MantenerTipoBungalow", tipoB);
        }


        public ActionResult LeerTipoBungalows([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Models.TipoBungalow> listaTipoBungalows = Models.TipoBungalow.SeleccionarTodo();
            try
            {
                DataSourceResult result = listaTipoBungalows.ToDataSourceResult(request);
                return Json(result);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
            return null;
        }

        public ActionResult EditarTipoBungalow(Web.Models.TipoBungalow tipoB)
        {
            tipoB = TipoBungalow.SeleccionarporId(tipoB.id);

            return View("MantenerTipoBungalow", tipoB);
        }


        public ActionResult EliminarTipoBungalow([DataSourceRequest] DataSourceRequest request, Web.Models.TipoBungalow tipoB)
        {
            if (Models.TipoBungalow.HayBungalow(tipoB) == false)
            {
                if (tipoB != null)
                {
                    TipoBungalow.eliminarTipoBungalow(tipoB);
                }
                
            }
            else ViewData["message"] = "eliminaTipo";
            return View("MantenerTipoBungalow", tipoB);
        }

        /***********************************************************/
   }
}

