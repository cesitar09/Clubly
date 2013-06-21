using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Newtonsoft.Json;
using Web.Models;
using System.Data;
using System.Data.SqlClient;
using Negocio.Util;

namespace Web.Controllers
{
    public class VentaController : Controller
    {
        //
        // GET: /Venta/

        //..............Punto de Venta.................//

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult MantenerPuntoDeVenta(Web.Models.PuntoDeVenta puntoDeVenta)
        {
            return View((IView)null);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InsertarPuntoDeventa(Web.Models.PuntoDeVenta puntoDeVenta)
        {
            try
            {
                puntoDeVenta.sede = Sede.buscarId(puntoDeVenta.codSede);
                if (puntoDeVenta.id > 0)
                {
                    PuntoDeVenta.modificar(puntoDeVenta);
                    return View("MantenerPuntoDeVenta");
                }
                else
                {
                    puntoDeVenta.estado = ListaEstados.ESTADO_ACTIVO;
                    PuntoDeVenta.insertar(puntoDeVenta);
                    return View("MantenerPuntoDeVenta");
                }
            }
            catch (ConstraintException)
            {
                return View("MantenerPuntoDeVenta", puntoDeVenta);
            }
        }

        public ActionResult EditarPuntoDeVenta(Web.Models.PuntoDeVenta puntoDeVenta)
        {
            puntoDeVenta = PuntoDeVenta.buscarId(puntoDeVenta.id);
            return View("MantenerPuntoDeVenta", puntoDeVenta);
        }

        public ActionResult LeerPuntoDeVenta([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Models.PuntoDeVenta> ListaPuntoDeVenta = Models.PuntoDeVenta.SeleccionarTodo();
            DataSourceResult result = ListaPuntoDeVenta.ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult EliminarPuntoDeVenta(Web.Models.PuntoDeVenta puntoDeVenta)
        {
            PuntoDeVenta.eliminar(puntoDeVenta);
            return View("MantenerPuntoDeVenta", puntoDeVenta);
        }

        public ActionResult FilterMenuCustomization_Nombre()
        {
            return Json(Models.PuntoDeVenta.SeleccionarTodo().Select(e => e.nombre).Distinct(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult FilterMenuCustomization_Sede()
        {
            return Json(Models.PuntoDeVenta.SeleccionarTodo().Select(e => e.sede.nombre).Distinct(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult FilterMenuCustomization_Estado()
        {
            return Json(Models.PuntoDeVenta.SeleccionarTodo().Select(e => e.estado).Distinct(), JsonRequestBehavior.AllowGet);
        }

       

    }
}
