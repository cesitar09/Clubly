﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;

namespace Negocio
{
    class ReservaBungalowSorteo
    {
        public const short PENDIENTE = 1;

        public static Entities context()
        {
            return Datos.Context.context();
        }

        public static IEnumerable<Datos.ReservaBungalowSorteo> seleccionarPendientes()
        {
            return context().ReservaBungalowSorteo.Where(s => s.estado == PENDIENTE);
        }
       
      
    }
}
