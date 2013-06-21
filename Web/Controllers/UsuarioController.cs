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
using System.Text.RegularExpressions;
using System.Data;
namespace Web.Controllers
{
    public class UsuarioController : Controller
    {
        //
        // GET: /Usuario/

        //public ActionResult Index()
        //{
        //    return View();
        //}
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Usuario()
        {
            return View();
        }

        public ActionResult Leer_Personas([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Models.Persona> ListaPersona = Models.Persona.seleccionarTodo();
            return Json(ListaPersona.ToDataSourceResult(request));
        }

        public ActionResult Leer_Perfil()
        {
            IEnumerable<Models.Perfil> ListaPerfil = Models.Perfil.SeleccionarTodo();

            return Json(ListaPerfil, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Leer_Empleado([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Models.Empleado> ListaEmpleado = Models.Empleado.seleccionarTodo();

            return Json(ListaEmpleado.ToDataSourceResult(request));
        }

        public ActionResult ObtenerPermisosPorPerfil(int id, [DataSourceRequest] DataSourceRequest request)
        {
            var perfil = Negocio.Perfil.seleccionarTodo().Single(p => p.id == id);
            IEnumerable<Datos.Permiso> ListaPermiso = perfil.Permiso.ToList();
            DataSourceResult result = Models.Permiso.ConvertirLista(ListaPermiso).ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult ObtenerPermisos([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Datos.Permiso> ListaPermiso = Negocio.Permiso.seleccionarTodo();
            DataSourceResult result = Models.Permiso.ConvertirLista(ListaPermiso).ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult ObtenerPerfil([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Datos.Perfil> ListaPerfil = Negocio.Perfil.seleccionarTodo();
            DataSourceResult result = Models.Perfil.ConvertirLista(ListaPerfil).ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult ObtenerUsuario([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Datos.Usuario> ListaUsuario = Negocio.Usuario.seleccionarTodo();
            DataSourceResult result = Models.Usuario.ConvertirLista(ListaUsuario).ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult GuardarPerfil(String permisos, string strid, string strnombre, string strdescripcion)
        {
            Perfil perfil = new Perfil();
            string aux = strid.Trim(new Char[] { '\\', '\"' });
            perfil.id = short.Parse(aux != "" ? aux : "0");
            perfil.nombre = strnombre.Trim(new Char[] { '\\', '\"' });
            //perfil.descripcion = strdescripcion.Trim(new Char[] { '\\', '\"' });
            List<short> datos = new List<short>();
            perfil.listaPermiso = datos;
            if(permisos != "[]"){
                List<String> listaPermisos = permisos.Split(',').ToList();
                foreach(var permiso in listaPermisos){
                    datos.Add(short.Parse(permiso.Trim(new Char[] { '[', ']' })));
                }
            }
            try
            {
                if (perfil.id > 0)
                {
                    Web.Models.Perfil.modificar(perfil);
                    return View("Perfil");
                }
                else
                {
                    Web.Models.Perfil.insertar(perfil);
                    return View("Perfil");
                }
            }
            catch (ConstraintException)
            {
                return View("Perfil", perfil);
            }
        }

        
        
        public ActionResult Perfil()
        {
            return View();
        }

        public ActionResult EditarPerfil(Web.Models.Perfil perfil)
        {
            perfil = Web.Models.Perfil.buscarId(perfil.id);
            return View("Perfil", perfil);
        }

        public ActionResult GuardarUsuario(Web.Models.Usuario usuario) {
            Models.Perfil perfil = Models.Perfil.buscarId(usuario.idPerfil);
            usuario.perfil = perfil;
            Models.Usuario.insertar(usuario);    
            return View("Usuario");
        }

    }
}
