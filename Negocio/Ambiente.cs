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
                ambiente.estado = 1;
                context().Ambiente.AddObject(ambiente);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static IEnumerable<Datos.Ambiente> seleccionarTodo()
        {
            IEnumerable<Datos.Ambiente> listaAmbientes = context().Ambiente.Where(p => p.estado == 1);
            return listaAmbientes;
        }

        public static Datos.Ambiente seleccionarId(short id)
        {
            Datos.Ambiente ambiente = context().Ambiente.Single(p => p.id == id);
            return ambiente;
        }

        public static Datos.Ambiente buscarId(short id)
        {
            return context().Ambiente.Single(p => p.id == id);
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

        public static Exception eliminar(Datos.Ambiente ambiente)
        {
            //Datos.Ambiente amb_eliminado = context().Ambiente.Single(p => p.id == ambiente.id);
            //amb_eliminado.estado = 0;
            //modificar(amb_eliminado);
            try
            {
                ambiente.estado = 0;
                context().Ambiente.ApplyCurrentValues(ambiente);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        //Cholo aqui esta lo que se me ocurre que se deberia hacer. Habria que llenar datos y probarlo
        public static IEnumerable<Datos.Ambiente> SeleccionarDisponibles(DateTime fechaInicial, DateTime fechaFinal)
        {
            IEnumerable<Datos.Ambiente> listaAmbientes = context().Ambiente.Where(a=>a.estado!=0);
            return listaAmbientes.Where(a =>
                a.ReservaAmbiente.Where(r=>
                    ((r.horaInicio <= fechaInicial && fechaInicial < r.horaFin) ||
                    (fechaInicial <= r.horaInicio && r.horaInicio < fechaFinal)
                    )
                ).ToList().Count!=1);
        }
    }
}

//((r.horaInicio.CompareTo(fechaInicial)>=0) && (r.horaInicio.CompareTo(fechaFinal)<=0))  ||
//                     ((r.horaFin.CompareTo(fechaInicial)>=0) && (r.horaFin.CompareTo(fechaFinal)<=0)) 