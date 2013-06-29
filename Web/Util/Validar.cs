using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Util
{
    public class Validar
    {
        public static void ValidarIEnumerable(Object dic)
        {
            ((IEnumerable<Object>)dic).ToList(); //Si falla, throwea la excepción.
        }
    }
}