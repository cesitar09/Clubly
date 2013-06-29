using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using Web.Util;
using Negocio.Util;

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
                Session["Nombre"]= null;
                Session["PerfilUsuario"] = null;
            }
            ViewData["message"] = null;
            return View();
        }
        
        [HttpPost]
        public ActionResult login(Models.Login login)
        {
            try
            {
                int valido = Models.Login.esValido(login.nombreusuario, login.contrasena);
                if (valido == 1)
                {
                    login.usuario = Web.Models.Login.obtenerid(login.nombreusuario, login.contrasena);
                    login.perfil = Web.Models.Usuario.obtenerPerfil(login.usuario);
                    Session["IdUsuario"] = login.usuario;
                    Session["Nombre"] = login.nombreusuario;
                    Session["PerfilUsuario"] = login.perfil;
                    logito.ElLogeador("Inicio sesion el usuario", login.nombreusuario);
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
                    ViewData["message"] = "F";
                    return View("Index");
                }
            }
            catch (SqlException ex)
            {
                ViewData["message"] = TransactionMessages.SQL_EXCEPTION_MESSAGE;
                logito.ElLogeador(TransactionMessages.SQL_EXCEPTION_MESSAGE, ex.GetType().ToString());
                return View("Index");

            }
            catch (InvalidOperationException ex)
            {
                ViewData["message"] = TransactionMessages.SINGLE_NOT_FOUND_MESSAGE;
                logito.ElLogeador(TransactionMessages.SINGLE_NOT_FOUND_MESSAGE, ex.GetType().ToString());
                return View("Index");
            }
            catch (EntityException ex) {
                ViewData["message"] = TransactionMessages.ENTITY_EXCEPTION_MESSAGE;
                logito.ElLogeador(TransactionMessages.ENTITY_EXCEPTION_MESSAGE, ex.GetType().ToString());
                return View("Index");
            }
        }

        [HttpGet]
        public ActionResult logof() { 
            //algo con lo de sesion
            Session["IdUsuario"] = null;
            Session["PerfilUsuario"] = null;
            Session["Nombre"] = null;
            return View("Index");
        }

    }
}
