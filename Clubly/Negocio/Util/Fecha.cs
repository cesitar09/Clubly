using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Negocio.Util
{
    public class Fecha
    {
        public static int CompararFechas(DateTime fecha1, DateTime fecha2)  
        {
            // menor a 0: fecha1 menor que fecha2
            // igual a 0: fecha1 igual a fecha2
            // mayor a 0: fecha1 mayor a fecha2
            int dif=fecha1.Year-fecha2.Year;
            if (dif!= 0)
                return dif;
            else
            {
                //anhos iguales
                dif = fecha1.Month - fecha2.Month;
                if (dif != 0)
                    return dif;
                else
                {
                    //meses iguales
                    return fecha1.Day - fecha2.Day;
                }
            }
        }
    }
}
