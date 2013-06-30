using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;

namespace Negocio
{
    public class Ambiente
    {
        public static Entities context(){
           
        return Context.context();
        }


        public static Exception insertar(Datos.Ambiente ambiente)
        {
            try
            {
                //ambiente.estado = 1;
                context().Ambiente.AddObject(ambiente);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        //falta fecha

        public static bool HayReserva(short id)
        {
            IEnumerable<Datos.ReservaAmbiente> listaReservas = buscarId(id).ReservaAmbiente.Where(p => p.estado == 1);
            if(listaReservas.Count() > 0) return true; 
            else
                return false;
        }

        



        public static IEnumerable<Datos.Ambiente> seleccionarTodo()
        {
            IEnumerable<Datos.Ambiente> listaAmbientes = context().Ambiente.Where(p => p.estado >= 1);
            return listaAmbientes;
        }

        public static Datos.Ambiente seleccionarId(short id)
        {
            Datos.Ambiente ambiente = context().Ambiente.Single(p => p.id == id);
            return ambiente;
        }

        public static Datos.Ambiente buscarId(short id)
        {
            return context().Ambiente.SingleOrDefault(p => p.id == id && p.estado!=0);
        }
        //Ambientes por sede
        public static IEnumerable<Datos.Ambiente> buscarPorSede(short idSede)
        {
            return context().Ambiente.Where(p =>p.Sede.id == idSede );
        }

        public static Exception modificar(Datos.Ambiente ambiente)
        {
            //Datos.Ambiente a = context().Ambiente.Single(p => p.id == ambiente.id);
            //a.nombre = ambiente.nombre;
            //a.estado = ambiente.estado;
            //a.area = ambiente.area;
            //a.Sede = ambiente.Sede;
            //context().Ambiente.ApplyCurrentValues(a);
            //context().SaveChanges();

            try
            {
                context().Ambiente.ApplyCurrentValues(ambiente);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static Exception eliminar(short id)
        {
            //Datos.Ambiente amb_eliminado = context().Ambiente.Single(p => p.id == ambiente.id);
            //amb_eliminado.estado = 0;
            //modificar(amb_eliminado);
            try
            {
                buscarId(id).estado = 0;
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        //MÉTODO QUE RETORNA UNA LISTA DE AMBIENTES DISPONIBLES EN UN RANGO DE FECHAS----------
        public static IEnumerable<Datos.Ambiente> SeleccionarDisponibles(DateTime fechaInicial, DateTime fechaFinal)
        {
            IEnumerable<Datos.Ambiente> listaAmbientes = context().Ambiente.Where(a=>a.estado!=0);
            return listaAmbientes.Where(a =>
                a.ReservaAmbiente.Where(r=>
                    ((r.horaInicio <= fechaInicial && fechaInicial < r.horaFin) ||
                    (fechaInicial <= r.horaInicio && r.horaInicio < fechaFinal)) && r.estado != 0
                    
                ).ToList().Count!=0);
        }

        //MÉTODO QUE VERIFICA SI EL AMBIENTE INGRESADO EN EL RANGO DADO ESTA DISPONIBLE O NO---------
        public static bool AmbienteLibre(DateTime i, DateTime f, short idAmb)
        {
            IEnumerable<Datos.Ambiente> listaAmbientesDispobibles = Negocio.Ambiente.SeleccionarDisponibles(i, f);
            foreach (Datos.Ambiente amb in listaAmbientesDispobibles)
            {
                if (amb.id == idAmb)
                    return false;
            }
            return true;
        }

    }
}