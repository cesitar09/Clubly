using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Datos;
using Negocio.Util;
//using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Ambiente
    {
        [ScaffoldColumn(false)]
        [DisplayName("Id")]
        public short id { get; set; }

        
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DisplayName("Nombre")]
        public String nombre { get; set; }

        [DisplayName("Estado")]
        public string estado { get; set; }

        
        [DisplayName("Área")]
        public double? area { get; set; }

        //Un ambiente está asignado a una Sede
        [DisplayName("Sede")]
        public Models.Sede sede { get; set; }

        [DisplayName("Reserva")]
        public Models.ReservaAmbiente reserva { get; set; }

        //Validación de Estados para Habilitado e Inhabilitado

        public static ListaEstados listaEstados = new ListaEstados();
        public static List<Estado_Ambiente> listestadoamb { get; set; }


        public class Estado_Ambiente
        {
            public short id { get; set; }
            public string nombre { get; set; }

            public Estado_Ambiente(short i, string n)
            {
                id = i;
                nombre = n;
            }
        }
        
        static Ambiente() {
            listaEstados = new ListaEstados();
            listaEstados.AgregarEstado(1, "Habilitado");
            listaEstados.AgregarEstado(2, "Inhabilitado");
            Estado_Ambiente estado1 = new Estado_Ambiente(1, "Habilitado");
            Estado_Ambiente estado2 = new Estado_Ambiente(2, "Inhabilitado");
            listestadoamb = new List<Estado_Ambiente>();
            listestadoamb.Add(estado1);
            listestadoamb.Add(estado2);
        }


        public Ambiente() { }

        public Ambiente(Datos.Ambiente amb)
        {
            id = amb.id;
            nombre = amb.nombre;
            estado = listaEstados.TextoEstado(amb.estado);
            area = amb.area;
            sede = Sede.Convertir(amb.Sede);
        }

        public static Ambiente Convertir(Datos.Ambiente ambiente)
        {
            return new Ambiente(ambiente);
        }

        public static IEnumerable<Ambiente> ConvertirLista(IEnumerable<Datos.Ambiente> listaamb){

            return listaamb.Select( amb => Convertir(amb));

        }

        //Busca si tiene Reservas

        public static Datos.Ambiente Invertir(Models.Ambiente mAmbiente)
        {
            Datos.Ambiente dAmbiente;
            if (mAmbiente.id == 0)
                dAmbiente = new Datos.Ambiente();
            else
                dAmbiente = Negocio.Ambiente.buscarId(mAmbiente.id);
            dAmbiente.nombre = mAmbiente.nombre;
            dAmbiente.area = mAmbiente.area;
            dAmbiente.estado = listaEstados.EstadoTexto(mAmbiente.estado);
            dAmbiente.Sede = Negocio.Sede.buscarId(mAmbiente.sede.id);
            return dAmbiente;
        
        }

        public static bool HayReserva(Models.Ambiente ambiente) {            

            return Negocio.Ambiente.HayReserva(ambiente.id);
        }


        //FUNCIÓN QUE RETORNA TRUE SI ES QUE EL AMBIENTE ESTA DISPONIBLE SINO EMITIRA FALSE
        public static bool AmbienteReservado(DateTime ini, DateTime fin, short idAmbiente)
        {
            if (Negocio.Ambiente.AmbienteLibre(ini, fin, idAmbiente) == true)
                return false;
            else
                return true;
        }
        /*********************************************************************************/


        public static IEnumerable<Datos.Ambiente> ConvertirListaInverso(IEnumerable<Models.Ambiente> mAmbiente)
        {
            return mAmbiente.Select(amb => Invertir(amb));
        }

        public static Models.Ambiente buscarId(short id)
        {
            return Convertir(Negocio.Ambiente.buscarId(id));
        }

        public static IEnumerable<Ambiente> buscarPorSede(short idSede)
        {
            IEnumerable<Datos.Ambiente> lista = Negocio.Ambiente.buscarPorSede(idSede);
            return ConvertirLista(lista);
        }
        public static IEnumerable<Ambiente> SeleccionarTodo()
        {
            IEnumerable<Datos.Ambiente> ambiente = Negocio.Ambiente.seleccionarTodo();
            return ConvertirLista(ambiente);
        }

        public static Models.Ambiente SeleccionarporId(short id)
        {
            return Convertir(Negocio.Ambiente.buscarId(id));
        }

        public static IEnumerable<Ambiente> SeleccionarDisponibles(DateTime fechaI, DateTime fechaF)
        { 
            IEnumerable<Datos.Ambiente> lista = Negocio.Ambiente.SeleccionarDisponibles(fechaI,fechaF);
            return ConvertirLista(lista);
        }
 
        public static void modificarAmbiente(Models.Ambiente ambiente)
        {
            Negocio.Ambiente.modificar(Invertir(ambiente));
        }

        public static void insertarAmbiente(Models.Ambiente ambiente)
        {
            Negocio.Ambiente.insertar(Invertir(ambiente));
        }

        public static void eliminarAmbiente(short id)
        {
            Negocio.Ambiente.eliminar(id);
        }
    }
}