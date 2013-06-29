using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;

namespace Negocio
{
    public class Parametros
    {
        public static Datos.Parametros SeleccionarParametros()
        {
            return Context.context().Parametros.First(p=>p.estado!=0 && p.fechaFinal==null);
        }
    }
}
