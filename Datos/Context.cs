using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos
{
    public static class Context
    {
        private static Entities ctx = null;
        public static Entities context()
        {
            if (ctx == null)
                ctx = new Entities();
            return ctx;
        }
    }
}
