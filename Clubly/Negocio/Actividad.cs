    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;

namespace Negocio
{
    public class Actividad
    {
        public static Entities context()
        {
            return Datos.Context.context();
        }

        public static Exception Insertar(Datos.Actividad actividad)
        {
            try
            {
                actividad.estado = 1;
                context().Actividad.AddObject(actividad);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static Exception Modificar(Datos.Actividad actividad)  
        {
            try
            {
                context().Actividad.ApplyCurrentValues(actividad);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static Exception Eliminar(Datos.Actividad actividad)
        {
            try
            {
                actividad.estado = 0;                
                context().Actividad.ApplyCurrentValues(actividad);
                context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }
        
        public static IEnumerable<Datos.Actividad> SeleccionarTodo() { 

            IEnumerable<Datos.Actividad> listaActividad = context().Actividad.Where(p => p.estado != 0);
            return listaActividad;

        }

        public static Datos.Actividad BuscarId(short id)
        {
            return context().Actividad.FirstOrDefault(p => p.id == id);
        }

        public static IEnumerable<Datos.Actividad> BuscarNombre(String nombre)
        {
            return context().Actividad.Where(p => p.nombre == nombre);
        }

        public static IEnumerable<Datos.Actividad> SeleccionarActividadesDisponibles()
        {
            DateTime hoy = DateTime.Now;
            return Context.context().Actividad.Where(a => a.estado != 0 && a.fechaInicio.CompareTo(hoy) > 0);
        }
        public static IEnumerable<Datos.Actividad> SeleccionarActividadesDisponiblesSocio()
        {
            
            Datos.Parametros param = Negocio.Parametros.SeleccionarParametros();
            DateTime hoy = DateTime.Now.AddDays(param.diasLimitePago);
            return Context.context().Actividad.Where(a => a.estado != 0 && a.fechaInicio
                //.AddDays(param.diasLimitePago)
                .CompareTo(hoy) > 0);
        }

        public static bool HayInscritos(short id)
        {
            IEnumerable<Datos.SocioXActividad> listaInscritos = BuscarId(id).SocioXActividad.Where(p => p.estado == 1);
            if(listaInscritos.Count() > 0) return true;
            else
                return false;
        }

        public static IEnumerable<Datos.Actividad> BuscarActividadIdFamilia(short idFamilia)
        {
            List<Datos.Actividad> listaActividad = new List<Datos.Actividad>();
            foreach (Datos.Actividad actividad in Negocio.Actividad.SeleccionarActividadesDisponiblesSocio())
            {
                bool inscrito = false;
                foreach (Datos.SocioXActividad sxa in actividad.SocioXActividad)
                    if (sxa.Socio.Familia.id == idFamilia)
                        inscrito = true;

                if (inscrito)
                    listaActividad.Add(actividad);
            }
            
            //IEnumerable<Datos.Actividad> listaSocioXActividad = Context.context().Actividad
            //    .Where(p => p.SocioXActividad.Where(s=>s.Socio.Familia.id == idFamilia).Count() > 0);
            return listaActividad;
        }

        public static IEnumerable<Datos.Ambiente> listaAmbientesDisponibles { get; set; }

    
    }
}
