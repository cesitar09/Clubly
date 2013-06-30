using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;

namespace Negocio.Util
{
    /*
     * Esta clase se encargará de manejar una correlación entre códigos de estado y el texto que deben mostrar.
     */
    public class ListaEstados
    {
        public const short ESTADO_ELIMINADO = 0;
        public const short ESTADO_ACTIVO = 1;

        private Dictionary<short, String> estados = new Dictionary<short, String>();

        public ListaEstados(){
            estados.Add(ESTADO_ELIMINADO, "Eliminado");
            estados.Add(ESTADO_ACTIVO, "Activo");
        }

        public void AgregarEstado(short codigo, String texto)
        {
            if (codigo <= 0)
                throw new ClublyStateException("Se agregó un código de estado erróneo.");
            else
            {
                if (!estados.ContainsKey(codigo))
                    estados.Add(codigo, texto);
                else
                    estados[codigo] = texto;
            }
        }

        public String TextoEstado(short codigo)
        {
            return estados[codigo];
        }

        public short EstadoTexto(String texto)
        {
            foreach (KeyValuePair<Int16, String> entry in estados)
            {
                if (entry.Value.Equals(texto))
                    return entry.Key;
            }
            return 0;
        }
    }
}