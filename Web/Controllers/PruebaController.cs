﻿using System;
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
            //try
            //{
            //    Models.Evento Evento=new Models.Evento();
            //    Models.Evento.modificarPrueba();

            //    return View();
            //}
            //catch (OptimisticConcurrencyException )
            //{
            //    ModelState.AddModelError("nombre", "Este campo fue modificado");
            //    ModelState.AddModelError("", "Uno o varios de los valores fue modificado por otro usuario");
            //    return View("error");
            //}

            return View();
        }

    }
}
