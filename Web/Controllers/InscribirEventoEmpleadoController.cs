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

namespace Web.Controllers
{
    public class InscribirEventoEmpleadoController : Controller
    {
        //
        // GET: /InscribirEventoEmpleado/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Inscribir()
        {
            return View();
        }

        //metodo de kendo para leer la data
        public ActionResult leerEventos([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Models.Evento> listaEventos = Models.Evento.seleccionarEventosDisponibles();
            DataSourceResult result = listaEventos.ToDataSourceResult(request);
            return Json(result);
        }

    }
}
