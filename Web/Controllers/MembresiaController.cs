using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Newtonsoft.Json;
using Web.Controllers;
using Web.Models;
using System.Data;


namespace Web.Controllers
{
    public class MembresiaController : Controller
    {
        //
        // GET: /Membresia/

        //Registrar No titular
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult RegistrarNoTitular(Web.Models.Socio socio)
        {

            ViewBag.indexFamilia = -1;
            return View(socio);
        }

        [HttpPost]
        public ActionResult Modificar(InvitadoXFamilia invxfam)
        {
            invxfam.familia = Familia.buscarId(invxfam.familia.id);
            InvitadoXFamilia.insertar(invxfam);
            return View("RegistrarInvitados");
        }

        public ActionResult LeerId()
        {
            IEnumerable<short> ListaId = Familia.SeleccionarTodo().Select(p => p.id);
            return Json(ListaId, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LeerDNI()
        {
            IEnumerable<int> ListaDNI = Invitado.SeleccionarTodo().Select(p => p.dni);
            return Json(ListaDNI, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Leer_Socio([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                IEnumerable<Models.Socio> ListaSocio = Models.Socio.SeleccionarTodo();
                DataSourceResult result = ListaSocio.ToDataSourceResult(request);
                return Json(result);
            }
            catch (EntityException e)
            {
                return View("RegistrarNoTitular");
            }
        }

        public ActionResult BuscarId(String id)
        {
            short sid = short.Parse(id.Trim(new Char[] { '\\', '\"' }));
            IEnumerable<Socio> socio = Socio.SeleccionarTodo().Where(p => p.familia.id == sid);
            Persona persona = socio.Single(p => p.estado).persona;
            return Json(persona); 

        }
        [HttpPost]
        public ActionResult RegistrarIngresoInvitados(InvitadoXFamilia registro)
        {
            Datos.InvitadoXFamilia invxFam = new Datos.InvitadoXFamilia();
            invxFam.estado = registro.estado;
            invxFam.Familia = Negocio.Familia.buscarId(registro.familia.id);
            invxFam.idFamilia = registro.familia.id;
            invxFam.Invitado = Negocio.Invitado.SeleccionarTodo().Single(p => p.dni == registro.invitado.dni);
            invxFam.idInvitado = Invitado.SeleccionarTodo().Single(p => p.dni == registro.invitado.dni).id;
            invxFam.horaIngreso = DateTime.Now;
            Negocio.InvitadoXFamilia.insertar(invxFam);
            return View("RegistrarIngresoInvitados");
        }

        public ActionResult BuscarInvitado(String dni)
        {
            int sdni = int.Parse(dni.Trim(new Char[] { '\\', '\"' }));
            Invitado invitado = Invitado.SeleccionarTodo().Single(p => p.dni == sdni);
            return Json(invitado);

        }

        [AcceptVerbs(HttpVerbs.Post)]

        public ActionResult Guardar(Web.Models.Socio socio)
        {
            try
            {
                if (socio.persona == null || socio.persona.id == 0)
                {
                    socio.persona.estado = 1;
                    //socio.familia = Familia.buscarId(socio.familia.id);
                    Socio.InsertarNoTitular(socio);
                }

                return View("RegistrarNoTitular", socio);

            }
            catch (ConstraintException e)
            {
                return View("RegistrarNoTitular", socio);
            }
        }
        public ActionResult Leer_Familia()
        {
            IEnumerable<Models.Familia> ListaFamilia = Models.Familia.SeleccionarTodo();

            return Json(ListaFamilia, JsonRequestBehavior.AllowGet);
        }

        //Mantener
        public ActionResult Leer_Socio1([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                IEnumerable<Models.Socio> ListaSocio = Models.Socio.SeleccionarTodo();
                DataSourceResult result = ListaSocio.ToDataSourceResult(request);
                return Json(result);
            }
            catch (EntityException e)
            {
                return View("MantenerSocio");
            }
        }
        public ActionResult Modificar(Web.Models.Socio socio)
        {
            try
            {
                if (socio.persona != null || socio.persona.id != 0)
                {
                    Socio.Modificar(socio);
                }

                return View("MantenerSocio");

            }
            catch (ConstraintException e)
            {
                return View("MantenerSocio", socio);
            }
        }
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult MantenerSocio(Web.Models.Socio socio)
        {

            ViewBag.indexFamilia = -1;
            return View((IView)null);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Eliminar([DataSourceRequest] DataSourceRequest request, Web.Models.Socio socio)
        {
            if (socio != null)
            {
                Socio.Eliminar(socio);
            }

            return View("MantenerSocio");
        }


        //Llena Datos del Socio en el formulario
        public ActionResult Editar(Web.Models.Socio socio)
        {
            if (socio != null)
            {
                socio = Socio.BuscarId(socio.persona.id);
            }
            return View("MantenerSocio", socio);
        }

        public ActionResult EditarSocio(short id)
        {
            Models.Socio socio = null;
            if (id != 0)
            {
                socio = Socio.BuscarId(id);
            }
            return View("MantenerSocio", socio);
        }

        //CONSULTAR INFORMACIÓN DE FAMILIA
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult ConsultarFamilia()
        {
            return View();
        }

        public ActionResult leerFamilias([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Models.Familia> listaFamilias = Models.Familia.SeleccionarTodo();
            DataSourceResult result = listaFamilias.ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult LeerIngresoDia([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Models.InvitadoXFamilia> ListaInvitados = Models.InvitadoXFamilia.SeleccionarTodo().Where(p => p.horaIngreso.Date == DateTime.Now.Date);
            return Json(ListaInvitados.ToDataSourceResult(request));
        }

        public ActionResult LeerSocios(short id, [DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Models.Socio> listaSocios = Socio.BuscarIdFamilia(id);
            return Json (listaSocios.ToDataSourceResult(request));
        }

        //Registrar ingreso de invitados
        public ActionResult RegistrarIngresoInvitados()
        {
            return View();
        }
    }
}
