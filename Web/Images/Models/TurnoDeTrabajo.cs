using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class TurnoDeTrabajo
    {
        //atributos
        public short id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public short estado { get; set; }
        public IEnumerable<Models.Empleado> empleado { get; set; }
        public IEnumerable<Models.EmpleadoXTurno> empleadoxturno { get; set; }

        //metodos para convertir
        public TurnoDeTrabajo(Datos.TurnoDeTrabajo turnodetrabajo)
        {
            id = turnodetrabajo.id;
            nombre = turnodetrabajo.nombre;
            descripcion = turnodetrabajo.descripcion;
            estado = turnodetrabajo.estado;
            empleado = Models.Empleado.ConvertirLista(turnodetrabajo.Empleado.ToList());
            empleadoxturno = Models.EmpleadoXTurno.ConvertirLista(turnodetrabajo.EmpleadoXTurno.ToList());
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
    }
}