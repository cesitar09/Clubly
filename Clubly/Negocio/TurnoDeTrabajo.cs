using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;

namespace Negocio
{
    public class TurnoDeTrabajo
    {
        public static Entities context()
        {
            return Context.context();
        }
        public static IEnumerable<Datos.TurnoDeTrabajo> seleccionarTodo()
        {
            return context().TurnoDeTrabajo.Where(persona => persona.estado != 0);
        }

        public static Datos.TurnoDeTrabajo buscarId(short id)
        {
            return context().TurnoDeTrabajo.Single(p => p.id == id);
        }

        public static short primerId() {
            return seleccionarTodo().FirstOrDefault().id;
        
        }
    }
}
