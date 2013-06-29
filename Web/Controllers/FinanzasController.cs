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
    public class FinanzasController : Controller
    {
        //
        // GET: /Finanzas/

        public ActionResult ConsultarHistorialPagosParaEmpleado(Models.Pago pago)
        {
            return View(pago);
        }

        //PRIMERA VISTA
        // Esto es para la tabla kendo, que  va a mostrar todos los pagos 
        public ActionResult LeerPagosDisponibles([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Models.Pago> ListaPagos = Models.Pago.SeleccionarTodo();
            try
            {
                DataSourceResult result = ListaPagos.ToDataSourceResult(request);
                return Json(result);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
            return null;
        }

        //public ActionResult GenerarComprobanteDePago(String pagos)
        //{
        //    ComprobanteDePago comprobanteDePago = new ComprobanteDePago();
        //    //string aux = strid.Trim(new Char[] { '\\', '\"' });
        //    //comprobanteDePago.id = short.Parse(aux != "" ? aux : "0");
        //    List<Pago> datos = new List<Pago>();
        //    comprobanteDePago.listaPagos = datos;
        //    if (pagos != "[]")
        //    {
        //        List<String> listaPagos = pagos.Split(',').ToList();
        //        foreach (var pago in listaPagos)
        //        {
        //            short id = short.Parse(pago.Trim(new Char[] { '[', ']' }));
        //            Pago pagoSeleccionado = Pago.Convertir(Negocio.Pago.BuscarId(id));
        //            //pagoSeleccionado = Negocio.Pago.
        //            datos.Add(pagoSeleccionado);
        //        }
               
        //    }

        //    //String javascript = " window.location.href= '{0}';";
        //    //return JavaScript(String.Format(javascript,returnUrl));
        //    //return View("MostrarComprobanteDePago", comprobanteDePago);
        //    return Json(comprobanteDePago.id, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GenerarComprobanteDePago(String pagos)
        {
            //ComprobanteDePago comprobanteDePago = new ComprobanteDePago();
            List<short> idPagos = new List<short>();
            if (pagos != "[]")
            {
                List<String> listaPagos = pagos.Split(',').ToList();
                foreach (var pago in listaPagos)
                {
                    short id = short.Parse(pago.Trim(new Char[] { '[', ']' }));
                    idPagos.Add(id);
                }
            }
            if (idPagos.Count() != 0)
            {
                Models.ComprobanteDePago comprobanteDePago = ComprobanteDePago.Insertar(idPagos);

                return Json(comprobanteDePago.id, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Error: Debe seleccionar algún pago");
            }
        }

        public ActionResult MostrarComprobanteDePago(short id)
        {

            return View("MostrarComprobanteDePago", ComprobanteDePago.BuscarId(id));
        }

    }
}
