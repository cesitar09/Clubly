using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Datos;
using Negocio.Util;
using System.Data.Objects.DataClasses;
using System.Data;

namespace Negocio
{
    public class ReservaCancha
    {

//CONEXION BASE DE DATOS
        public static Entities context()
        {
            return Datos.Context.context();
        }


//QUERY PARA INSERTAR
        public static int Insertar(Datos.ReservaCancha reservaCancha)
        {
            try
            {
                context().ReservaCancha.AddObject(reservaCancha);
                context().SaveChanges();
                return 1;
            }
            catch (Exception ex){
                return 0;
            }
        }


        public static int Modificar(Datos.ReservaCancha reserva)
        {
            try
            {
                context().ReservaCancha.ApplyCurrentValues(reserva);
                context().SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
      
        }

//QUERYS DE BUSCAR
        public static IEnumerable<Datos.ReservaCancha> SeleccionarTodo()
        {
            return context().ReservaCancha.Where(reserva => reserva.estado != ListaEstados.ESTADO_ELIMINADO);
        }

        public static Datos.ReservaCancha BuscarId(short idCancha)
        {
            return context().ReservaCancha.FirstOrDefault(p => p.id == idCancha);
        }

        //Buscar todas las canchas en las que un socio a reservado
        public static IEnumerable<Datos.ReservaCancha> BuscarCanchaIdFamilia(short idFamilia)
        {
            IEnumerable<Datos.ReservaCancha> listaSocioXCancha = context().ReservaCancha.Where(p => (p.estado != 0) && (p.Familia.id == idFamilia));
            return listaSocioXCancha;
        }


//QUERY DE ELIMINAR
        //Metodos para eliminar una Reserva
        public static void Eliminar(short id)
        {
            //using (Entities tempContext = new Entities())
            //
            Datos.ReservaCancha reservaEliminar = ReservaCancha.BuscarId(id);
            reservaEliminar.estado = 2;

            context().SaveChanges();
            //}
        }        


    }
}
