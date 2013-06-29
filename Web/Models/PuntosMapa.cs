using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class PuntosMapa
    {
        public const short LIBRE = 1;
        public const short OCUPADOPARCIAL = 2;
        public const short OCUPADOTOTAL = 3;

        public short numeroBungalow { get; set; }
        public short posX { get; set; }
        public short posY { get; set; }
        public short estado { get; set; }

        public PuntosMapa(Datos.Bungalow bungalow, DateTime fechaInicial, DateTime fechaFinal)
        {
            numeroBungalow = bungalow.numero;
            //posX = bungalow.posX;
            //posY = bungalow.posY;
            //estado = Negocio.Bungalow.Disponibilidad(bungalow.id,fechaInicial,fechaFinal);
        }

        public static IEnumerable<PuntosMapa> ConsultarDisponibilidad(short idSede, DateTime fechaInicial, DateTime fechaFinal)
        {
            return Negocio.Sede.buscarId(idSede).Bungalow.Select(b => new PuntosMapa(b,fechaInicial,fechaFinal));
        }
    }
}