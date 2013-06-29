using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
using System.Data.Objects.DataClasses;

namespace Web.Models
{
    public class TurnoDeTrabajo
    {
        //atributos
        public short id { get; set; }
        [DisplayName("*Turno de trabajo")]
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public short estado { get; set; }
   
        //metodos para convertir
        public TurnoDeTrabajo() { 
        
        }
        public TurnoDeTrabajo(Datos.TurnoDeTrabajo turnodetrabajo)
        {
            id = turnodetrabajo.id;
            nombre = turnodetrabajo.nombre;
            descripcion = turnodetrabajo.descripcion;
            estado = turnodetrabajo.estado;
            
        }
        public static Models.TurnoDeTrabajo Convertir(Datos.TurnoDeTrabajo turnoDeTrabajo)
        {
            return new TurnoDeTrabajo(turnoDeTrabajo);
        }
        public static IEnumerable<Models.TurnoDeTrabajo> ConvertirLista(IEnumerable<Datos.TurnoDeTrabajo> turnodetrabajos)
        {
            return turnodetrabajos.Select(turnodetrabajo => new Models.TurnoDeTrabajo(turnodetrabajo));
        }

        //metodos para invertir
        public static Datos.TurnoDeTrabajo Invertir(Models.TurnoDeTrabajo turno)//id,nombre,desc,estado
        {
            Datos.TurnoDeTrabajo dataTurno = new Datos.TurnoDeTrabajo();
            dataTurno.id = turno.id;
            dataTurno.nombre = turno.nombre;
            dataTurno.descripcion = turno.descripcion;
            dataTurno.estado = turno.estado;
            return dataTurno;
        }

        public static Models.TurnoDeTrabajo buscarId(short id) {
            return Models.TurnoDeTrabajo.Convertir(Negocio.TurnoDeTrabajo.buscarId(id));
        
        }

        public static IEnumerable<TurnoDeTrabajo> seleccionarTodo() {
            return ConvertirLista(Negocio.TurnoDeTrabajo.seleccionarTodo());
        }

        public static short primerId() {
            //return Negocio.TurnoDeTrabajo.primerId();
            return 0;
        }
    }
}