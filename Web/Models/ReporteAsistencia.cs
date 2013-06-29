using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Datos;
using System.Data.Objects;

namespace Web.Models
{
    public class ReporteAsistencia
    {
        public List<Datos.Asistencia> asistenciaF{get; set;}
        public List<Datos.Asistencia> asistenciaP { get; set; }

        public string nombreEmpleado { get; set; }
        public short EstadoEmpleado { get; set; }
        public int cantHorasCumplidas{get; set;}
        public int cantHorasNoCumplidas{get; set;}
        public int cantFaltas{get; set;}
        public int cantAsist{get; set;}
        public int cantFaltaMarcaSalida { get; set; }
        public int cantDias { get; set; }
        public double porcAsisten { get; set; }

        public ReporteAsistencia(string codigo, DateTime fechaInicio, DateTime fechaFin){
            long codaux = Convert.ToInt64(codigo);
            Empleado empleado = Models.Empleado.buscarId(codaux);
            EstadoEmpleado = empleado.persona.estado;
            short cod = Convert.ToInt16(codigo);
            //DateTime fechaI = fechaInicio;
            //DateTime fechaF = fechaFin;
            DateTime fechaI = Convert.ToDateTime(fechaInicio);
            DateTime fechaF = Convert.ToDateTime(fechaFin);
            asistenciaF = Context.context().Asistencia.Where(asis => asis.Empleado.Persona.id == cod && asis.estado != 0 && EntityFunctions.TruncateTime(asis.fecha) >= fechaI.Date && EntityFunctions.TruncateTime(asis.fecha) <= fechaF.Date && asis.horaSalida == null).ToList();
            asistenciaP = Context.context().Asistencia.Where(asis => asis.Empleado.Persona.id == cod && asis.estado != 0 && EntityFunctions.TruncateTime(asis.fecha) == fechaI.Date && EntityFunctions.TruncateTime(asis.fecha)==fechaF.Date && asis.horaSalida != null ).ToList();
            cantFaltaMarcaSalida = asistenciaF.Count();
            cantHorasNoCumplidas = 0;
            cantHorasCumplidas = 0;
            
           
            nombreEmpleado = empleado.persona.nombre + " " + empleado.persona.apPaterno + " " + empleado.persona.apMaterno;
            foreach (var asistencia in asistenciaP)
            {
                if ((Convert.ToInt32(asistencia.horaSalida.Value.ToString("hh")) - Convert.ToInt16(asistencia.horaEntrada.ToString("hh"))) > asistencia.Empleado.TurnoDeTrabajo.numHoras)
                {
                    cantHorasCumplidas++;
                    //nombreEmpleado = asistencia.Empleado.Persona.nombre + " " + asistencia.Empleado.Persona.apPaterno + " " + asistencia.Empleado.Persona.apMaterno;
                }
                else
                {
                    cantHorasNoCumplidas++;
                    //nombreEmpleado = asistencia.Empleado.Persona.nombre + " " + asistencia.Empleado.Persona.apPaterno + " " + asistencia.Empleado.Persona.apMaterno;
                }
            }
            TimeSpan ts = fechaFin - fechaInicio;
            cantDias = ts.Days;
            cantFaltas = cantDias - cantHorasNoCumplidas - cantHorasCumplidas - cantFaltaMarcaSalida;
            cantAsist = asistenciaP.Count() + cantHorasCumplidas + cantHorasNoCumplidas + cantFaltaMarcaSalida;
            cantFaltaMarcaSalida = asistenciaF.Count();
            porcAsisten = Math.Round((double)(cantAsist * 100) / cantDias,2);
            
        }
   }
}