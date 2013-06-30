using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;

namespace Negocio
{
    public class TipoEmpleado
    {
        public static Entities context()
        {
            return Context.context();
        }
        public static IEnumerable<Datos.TipoEmpleado> seleccionarTodo()
        {
            return context().TipoEmpleado.Where(persona => persona.estado != 0);
        }

        public static Datos.TipoEmpleado buscarId(short id)
        {
            return context().TipoEmpleado.Single(p => p.id == id);
        }

        public static short primerId() {
            return seleccionarTodo().FirstOrDefault().id;
        }
    }
}
