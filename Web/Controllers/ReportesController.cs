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
using System.IO;
using System.Data.SqlClient;
using Negocio.Util;
using System.ComponentModel.DataAnnotations;
using Web.Util;

namespace Web.Controllers
{
    [HandleError]
    public class ReportesController : Controller
    {
        //
        // GET: /Reportes/

        //.............Reporte de Finanzas.........//

        public ActionResult ParametrosFinanzas([DataSourceRequest] DataSourceRequest request)
        {

            return View();
        }

        public ActionResult ReporteFinanzas([DataSourceRequest] DataSourceRequest request, DateTime fechaINI, DateTime fechaFIN)
        {
            Models.ReporteFinanzas reporte = new Models.ReporteFinanzas(fechaINI, fechaFIN);
            return View(reporte);
        }


        //.............Reporte de Asistencia.........//

        //public ActionResult Asistencia() {
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult ReporteAsistencia(string codigo, string fechaInicio, string fechaFin)
        //{
        //    Models.ReporteAsistencia reporte = new Models.ReporteAsistencia(codigo,fechaInicio,fechaFin);
        //    return View();
        //}

        public ActionResult ParametrosAsistencia([DataSourceRequest] DataSourceRequest request)
        {

            return View();
        }

        public ActionResult ReporteAsistencias([DataSourceRequest] DataSourceRequest request, DateTime fechaINI, DateTime fechaFIN, String codigo)
        {
            Models.ReporteAsistencia reporte = new Models.ReporteAsistencia(codigo,fechaINI, fechaFIN);
            if (reporte.EstadoEmpleado == 0)
            {
                ViewData["ErrorEstado"] = "Estado0";
                return View("ParametrosAsistencia");
            }
            else
            {
                return View(reporte);
            }
            
        }
    }
}
