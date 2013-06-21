using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Negocio.Util;
using System.Data.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;


namespace Web.Models
{
    public class InvitadoXFamilia
    {
        //private Invitado invitado { get; set; }   //Falta hacer el model de Invitado
        public Familia familia { get; set; }
        public DateTime horaIngreso { get; set; }
        public Pago pago { get; set; }
        public short estado { get; set; }
        public Invitado invitado { get; set; }
        public InvitadoXFamilia()
        {
        }


        public static void insertar(InvitadoXFamilia invitadoxFamilia)
        {
            Negocio.InvitadoXFamilia.insertar(Invertir(invitadoxFamilia));
        }

        public InvitadoXFamilia(Datos.InvitadoXFamilia invitadoFamilia)
        {
            invitado = Invitado.Convertir(invitadoFamilia.Invitado);  //Falta hacer el model de Invitado
            familia = Familia.Convertir(invitadoFamilia.Familia);
            horaIngreso = invitadoFamilia.horaIngreso.Value;
        }

        public static Datos.InvitadoXFamilia Invertir(InvitadoXFamilia invxfam)
        {
            Datos.InvitadoXFamilia inv = new Datos.InvitadoXFamilia();
            inv.Familia = Familia.Invertir(invxfam.familia);
            inv.fechaIngreso = invxfam.horaIngreso;
            inv.idFamilia = invxfam.familia.id;
            inv.Invitado = Invitado.Invertir(invxfam.invitado);
            inv.idInvitado = inv.Invitado.id;
            return inv;

        }

        public static InvitadoXFamilia Convertir(Datos.InvitadoXFamilia invitadoFamilia)
        {
            return new InvitadoXFamilia(invitadoFamilia);
        }

        public static IEnumerable<InvitadoXFamilia> ConvertirLista(IEnumerable<Datos.InvitadoXFamilia> invitados)
        {
            return invitados.Select(invi => Convertir(invi));
        }

        public static IEnumerable<Models.InvitadoXFamilia> SeleccionarTodo()
        {
            return ConvertirLista(Negocio.InvitadoXFamilia.seleccionarTodo());
        }
    }
}