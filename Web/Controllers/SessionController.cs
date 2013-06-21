using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class SessionController : Controller
    {
        //
        // GET: /Session/
        [AllowAnonymous]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            if (Session != null)
            {
                Session["IdUsuario"] = null;
                Session["PerfilUsuario"] = null;
            }
            ViewData["message"] = null;
            return View();
        }
        
        [HttpPost]
        public ActionResult login(Models.Login login)
        {
            int valido = Models.Login.esValido(login.usuario, login.contrasena);
            if (valido == 1)
            {
                login.perfil = Web.Models.Usuario.obtenerPerfil(login.usuario);
                Session["IdUsuario"] = login.usuario;
                Session["PerfilUsuario"] = login.perfil;
                if (login.perfil == 12)
                {//socio
                    return RedirectToAction("MantenerEmpleado", "RRHH");//falta poner que vaya a la vista pagina personal
                }
                else
                {
                    return RedirectToAction("Home", "Home");//si es empleado tendrai q ser la pagina principal
                }
            }
            else
            {
                if (valido == 2)
                {
                    ViewData["message"] = "F";
                    return View("Index");
                }
                else
                {
                    
                        ViewData["message"] = "FF";
                        return View("Index");
                    
                }
            }
        }

        [HttpGet]
        public ActionResult logof() { 
            //algo con lo de sesion
            Session["IdUsuario"] = null;
            Session["PerfilUsuario"] = null;
            return View("Index");
        }

    }
}
