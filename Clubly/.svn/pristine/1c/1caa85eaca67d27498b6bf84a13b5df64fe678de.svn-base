using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Web.Models
{
    public class Invitado
    {
        [DisplayName("id")]
        public short id{get; set;}
        [DisplayName("Nombre")]
        public string nombre { get; set; }
        [DisplayName("DNI")]
        public int dni { get; set; }
        [DisplayName("Apellido Paterno")]
        public string apPaterno { get; set; }
        [DisplayName("Apellido Materno")]
        public string apMaterno { get; set; }
        [DisplayName("Estado")]
        public short estado { get; set; }

        public Invitado()
        {
        }

        public Invitado(Datos.Invitado invitado)
        {
            id = invitado.id;
            nombre = invitado.nombre;
            apPaterno = invitado.apPaterno;
            apMaterno = invitado.apMaterno;
            estado = invitado.estado;
            dni = invitado.dni;
        }

        public static Datos.Invitado Invertir(Invitado inv)
        {
            Datos.Invitado aux = new Datos.Invitado();
            aux.apMaterno = inv.apMaterno;
            aux.apPaterno = inv.apPaterno;
            aux.dni = inv.dni;
            aux.estado = 1;
            return aux;
        }

        public static Invitado Convertir(Datos.Invitado invitado)
        {
            return new Invitado(invitado);
        }

        public static IEnumerable<Invitado> ConvertirLista(IEnumerable<Datos.Invitado> invitados)
        {
            return invitados.Select(invi => Convertir(invi));
        }

        public static IEnumerable<Models.Invitado> SeleccionarTodo()
        {
            return ConvertirLista(Negocio.Invitado.SeleccionarTodo());
        }
    }
}