using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Newtonsoft.Json;
using Kendo.Mvc.UI;


namespace Web.Controllers
{
    public class GestionarReservaController : Controller
    {

 //************************************RESERVA CANCHA*********************************************************************************//
       
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GestionarReservarCancha(Models.ReservaCancha reserva)
        {
            return View((IView)null);
        }

        public bool BuscarDisponibilidad(Models.ReservaCancha reserva)
        {
            //----Excepcion
            IEnumerable<Models.ReservaCancha> ListaReserva = Models.ReservaCancha.SeleccionarTodo();
            bool disponibilidad = true;

            if (ListaReserva != null) //Para que la Lista no este Vacio
            {
                foreach (var resev in ListaReserva)
                {
                    if (resev.cancha != null && reserva.cancha != null)
                    {
                        if (resev.cancha.id == reserva.cancha.id)
                        {
                            if (resev.horaInicio.Date == reserva.horaInicio.Date)
                            {
                                if ((resev.horaInicio>reserva.horaInicio && resev.horaInicio < reserva.horaFin) ||
                                    (resev.horaInicio==reserva.horaInicio && resev.horaInicio==reserva.horaFin) ||
                                    (resev.horaInicio<reserva.horaInicio && resev.horaFin>reserva.horaInicio ))
                                {
                                    disponibilidad = false;
                                }

                            }

                        }   //*Primero Valida Si la hora que eligo engloba otro hora
                    }       //*Segundo  Valida si la hora que eligo esta entre otra hora
                }           //*Tercero valida si la hora de incio esta antes de de la hora y la hora fin esta entre las horas de otra fecha
                            //*Cuarto valida si la hora de inicio esta entre la hora de incio de otra y la hora fin y la hora fin es mayor
            }               //*Quinto que no sea igual a las horas    
            return disponibilidad;
        }

        public bool ValidarHoras(Models.ReservaCancha reserva)
        {
            if (reserva.horaFin.Hour > reserva.horaInicio.Hour)
            {
                if (reserva.horaFin.Hour - reserva.horaInicio.Hour <= Models.Parametros.SeleccionarParametros().tiempo) return true;
                else ViewData["message"] = "ML";
            }
            else
                if (reserva.horaFin.Hour == reserva.horaInicio.Hour)
                {
                    if (reserva.horaFin.Minute >= reserva.horaInicio.Minute)
                        if (reserva.horaFin.Hour - reserva.horaInicio.Hour <= Models.Parametros.SeleccionarParametros().tiempo) return true;
                        else ViewData["message"] = "ML";
                }


            return false;
        }

        public bool VecesMaxima(Models.ReservaCancha reserva)
        {
            int contador = 0;
            IEnumerable<Models.ReservaCancha> ListaReserva = Models.ReservaCancha.SeleccionarTodo();
            bool aceptable = false;
            foreach (var resev in ListaReserva)
            {
                if (resev.fechaInicio.Date == reserva.fechaInicio.Date && resev.idFamilia == reserva.idFamilia)
                {
                    contador = contador + 1;
                }
            }

            if (contador <= 2)
                aceptable = true;

            return aceptable;
        }

        public ActionResult IngresarReservaCancha(Models.ReservaCancha reserva)
        {
            int val;
            bool disponibilidad = false;


            if (ValidarHoras(reserva))
            {
                if (VecesMaxima(reserva))
                {
                    //Asigno horas para el model0
                    DateTime fechaInicial = new DateTime();
                    DateTime fechaFinal = new DateTime();

                    fechaInicial = reserva.fechaInicio.Add(reserva.horaInicio.TimeOfDay);
                    fechaFinal = reserva.fechaInicio.Add(reserva.horaFin.TimeOfDay);
                    reserva.horaInicio = fechaInicial;
                    reserva.horaFin = fechaFinal;

                    reserva.actividad = null;

                    //Si selecciona Cancha
                    if (reserva.idCancha != 0)
                    {   //-----excepcion
                        reserva.cancha = Models.Cancha.buscarId(reserva.idCancha);
                        if (BuscarDisponibilidad(reserva))
                        {   //-----Excepcion
                            reserva.familia = Models.Familia.buscarId(reserva.idFamilia);
                            //-----Excepcion
                            val = Models.ReservaCancha.Insertar(reserva);
                            if (val == 0) ViewData["message"] = "F";
                            if (val == 1) ViewData["message"] = "R";
                            return View("GestionarReservarCancha", reserva);

                        }
                        else
                        {
                            ViewData["message"] = "ND";
                            return View("GestionarReservarCancha", reserva);
                        }
                    }
                    else //Sino selecciona Cancha
                    {   //------Excepcion
                        IEnumerable<Models.Cancha> ListaCancha = Models.Cancha.BuscarSedeTipo(reserva.idSede, reserva.idTipoCancha);

                        foreach (var cancha in ListaCancha)
                        {
                            reserva.cancha = cancha;
                            if (BuscarDisponibilidad(reserva))
                            {
                                disponibilidad = true;
                                break;
                            }
                        }

                        if (disponibilidad)
                        {
                            if (reserva.id == 0)
                                //----Excepcion
                                reserva.familia = Models.Familia.buscarId(10001);

                            //---Excepcion
                            reserva.idCancha = reserva.cancha.id;
                            val = Models.ReservaCancha.Insertar(reserva);
                            if (val == 0) ViewData["message"] = "F";
                            if (val == 1) ViewData["message"] = "R";
                            return View("GestionarReservarCancha", reserva);

                        }
                        else
                        {
                            ViewData["message"] = "NDT";
                            return View("GestionarReservarCancha", reserva);
                        }

                    }
                }
                else ViewData["message"] = "CVM";
            }
            else
            {
                ViewData["message"] = "HNV";
            }


            return View("GestionarReservarCancha", reserva);
        }

        public ActionResult LeerReservasCanchas([DataSourceRequest] DataSourceRequest request)
        {  // try
           // {
            IEnumerable<Models.ReservaCancha> listaReservas = Models.ReservaCancha.SeleccionarTodo();
            return Json(listaReservas.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
           // }
           // catch (Exception e)
           // {
           // Console.WriteLine("Excepcion en GestionarReservaController\n", e);
           // DataSourceResult result = new DataSourceResult();
           // result.Errors = "error";
           // return Json(result);
           // }
        }

        //public ActionResult LimpiarPagina() {
          //  Models.ReservaCancha reserva = new Models.ReservaCancha();
          //  return GestionarReservarCancha(reserva);

        //}
        public ActionResult EditarReservaCancha(Web.Models.ReservaCancha reserva)
        {       
                Models.ReservaCancha rese = Models.ReservaCancha.BuscarId(reserva.id);
                if (rese.estado == "Cancelada")
                    ViewData["message"] = "EC";
                else if (rese.estado == "Vencida")
                    ViewData["message"] = "EV";
                else
                {
                    return View("GestionarReservarCancha", rese);
                }
                return View("GestionarReservarCancha");

        }

        public ActionResult CancelarReservaCancha(short id)
        {
            Models.ReservaCancha reserva = Models.ReservaCancha.BuscarId(id);

            if (reserva.estado == "Cancelada")
                ViewData["message"] = "CC";
             else if (reserva.estado == "Vencida")
                ViewData["message"] = "CV";
            else
                Models.ReservaCancha.Eliminar(id);
 
            return View("GestionarReservarCancha");

        }

        public ActionResult LeerSedes()
        {
            IEnumerable<Models.Sede> ListaSedes = Models.Sede.SeleccionarTodo();

            return Json(ListaSedes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LeerTipoCancha(string idSede)
        {
            short idsede = Convert.ToInt16(idSede);
            IEnumerable<Models.TipoCancha> ListaSedes = Models.TipoCancha.BuscarPorSede(idsede);
            return Json(ListaSedes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LeerNumeroCancha(string idTipo, string idSede)
        {
            short idtipo = Convert.ToInt16(idTipo);
            short idsede = Convert.ToInt16(idSede);
            IEnumerable<Models.Cancha> ListaSedes = Models.Cancha.BuscarSedeTipo(idsede,idtipo);// buscarTipo(idtipo);
            return Json(ListaSedes, JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult LeerIdFamilia()
        {
            IEnumerable<short> ListaId = Models.Familia.SeleccionarTodo().Select(p => p.id);
            return Json(ListaId, JsonRequestBehavior.AllowGet);        
        }

        public ActionResult BuscarId(String id)
        {
            bool valido = false;
            IEnumerable<Models.Familia> ListaFamilia=Models.Familia.SeleccionarTodo();
            foreach(Models.Familia familia in ListaFamilia){

                string palabra = "\"" + Convert.ToString(familia.id) + "\"";
                if (palabra.Equals(id)) {          
                    valido = true;
                    break;
                }
            }
            if (valido)
            {
                short sid = short.Parse(id.Trim(new Char[] { '\\', '\"' }));
                IEnumerable<Models.Socio> socio = Models.Socio.SeleccionarTodo().Where(p => p.familia.id == sid);
                Models.Persona persona = socio.Single(p => p.estado).persona;
                return Json(persona);
            }
            else
            {
                ViewData["message"] = "EN";
                return Json(null);
            }

        }



//************************************RESERVA INGRESO DE BUNGALOW*********************************************************************************//
       
        public ActionResult RegistrarIngresoBungalow(short? id)
        {
            if (id.HasValue)
                Models.ReservaBungalow.RegistrarIngresoBungalow(id);
            return View();
        }

        public ActionResult RegistrarSalidaBungalow(short? id)
        {
            if (id.HasValue)
                Models.ReservaBungalow.RegistrarSalidaBungalow(id);
            return View("RegistrarIngresoBungalow");
        }

        public ActionResult IngresarBungalow(short id)
        {
            
            return View("RegistrarIngresoBungalow()");
        }


    //JSONS
       // public ActionResult LeerReservasBungalow([DataSourceRequest] DataSourceRequest request)
       // {
       //     IEnumerable<Models.ReservaCancha> listaReservas = Models.ReservaCancha.SeleccionarTodo();
       //     return Json(listaReservas.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
       // }
        public ActionResult LeerReservasBungalows([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Models.ReservaBungalow> listaReservas = Models.ReservaBungalow.SeleccionarIngreso();
            return Json(listaReservas.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}
