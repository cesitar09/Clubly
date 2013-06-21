using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Web.Models
{
    public class Asistencia
    {
        //[ScaffoldColumn(false)]
        public short id { get; set; }

        [JsonProperty("fecha")]
        public DateTime fecha { get; set; }

        [JsonProperty("horaEntrada")]
        public TimeSpan horaEntrada { get; set; }

        [JsonProperty("Monto")]
        public TimeSpan? horaSalida { get; set; }


        [JsonProperty("Estado")]        
        public short estado { get; set; }

        public Empleado empleado {get; set;}

        public Asistencia()
        {
        }

        public Asistencia(Datos.Asistencia asistencia)
        {
            id = asistencia.id;
            fecha = asistencia.fecha;
            horaEntrada = asistencia.horaEntrada;
            horaSalida = asistencia.horaSalida;
            estado = asistencia.estado;
            empleado = Empleado.Convertir(asistencia.Empleado); 
        }

        public static Asistencia Convertir(Datos.Asistencia asistencia)
        {
            return new Asistencia(asistencia);
        }

        public static IEnumerable<Models.Asistencia> ConvertirLista(IEnumerable<Datos.Asistencia> listaAsistencia)
        {
            //var northwind = new NorthwindDataContext();
            return listaAsistencia.Select(asistencia => new Models.Asistencia(asistencia));
            //return listaConceptos.Select(concepto => Convertir(concepto)).AsQueryable();
           
        }

        public static Datos.Asistencia ConvertirInverso(Models.Asistencia mAsistencia)
        {
            Datos.Asistencia dAsistencia = new Datos.Asistencia();

            dAsistencia.id = mAsistencia.id;
            dAsistencia.fecha = mAsistencia.fecha;
            dAsistencia.horaEntrada = mAsistencia.horaEntrada;
            dAsistencia.horaSalida = mAsistencia.horaSalida;
            dAsistencia.estado = mAsistencia.estado;
            dAsistencia.Empleado = Models.Empleado.Invertir(mAsistencia.empleado); 
            return dAsistencia;
        }

        public static IEnumerable<Models.Asistencia> SeleccionarTodo()
        {
            return ConvertirLista(Negocio.Asistencia.SeleccionarAsistencia());
        }

        public static Models.Asistencia SeleccionarporId(short id)
        {
            return Convertir(Negocio.Asistencia.buscarId(id));
        }

        public static int InsertarAsistencia(Models.Empleado empleado)
        {
            return Negocio.Asistencia.Insertar(empleado.persona.id,empleado.persona.nombre);  
        }

        public static void modificarAsistencia(Models.Asistencia asistencia)
        {
            Negocio.Asistencia.modificar(ConvertirInverso(asistencia));
        }

    }
}