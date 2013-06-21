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

namespace Web.Controllers
{
    public class PaginaPersonalController : Controller
    {
        //
        // GET: /PaginaPersonal/

        public ActionResult ConsultarHistorialPagos(Models.Pago pago)
        {
            return View(pago);
        }

        //PRIMERA VISTA
        // Esto es para la tabla kendo, que  va a mostrar todos los pagos 
        public ActionResult LeerPagosDisponibles([DataSourceRequest] DataSourceRequest request)
        {
            //Aca muestra los pagos por codigo de familia pero falta hacer que un usuario 
            //ingrese, jale su codigo de familia y recien muestre sus pagos
            short idUsuario = (short)Session["IdUsuario"];
            Familia familia = Models.Familia.buscarIdUsuario(idUsuario) ;
            IEnumerable<Models.Pago> ListaPagos = Models.Pago.SeleccionarPorFamilia(familia.id);
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

    }
}
