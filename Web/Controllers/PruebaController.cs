using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity.Infrastructure;

namespace Web.Controllers
{
    public class PruebaController : Controller
    {
        //
        // GET: /Prueba/

        public ActionResult Index()
        {
            Models.ReservaBungalow reserva = new Models.ReservaBungalow();
            reserva.fechaInicio = DateTime.Now.AddDays(5);
            reserva.fechaFin = DateTime.Now.AddDays(6);
            reserva.bungalow = new Models.Bungalow();
            reserva.bungalow.id = 18;
            Models.ReservaBungalow.AgregarReservaBungalow(reserva, 38);
            return View();
        }

    }
}
