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

namespace Web.Controllers
{
    public class RealizarSorteoController : Controller
    {
        //
        // GET: /RealizarSorteo/

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult VerSorteo()
        {
           IEnumerable<Models.Sorteo> sorteosPendientes = Models.Sorteo.ObtenerProximos();
            return View("VerSorteo",sorteosPendientes);
        }

        public ActionResult Sortear()
        {
            Models.Sorteo.RealizarSorteo();
            return View("VerSorteo");
        }

        public ActionResult getSorteos([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Models.Sorteo> ListaSorteos = Models.Sorteo.ObtenerProximos();
            try
            {
                DataSourceResult result = ListaSorteos.ToDataSourceResult(request);
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
