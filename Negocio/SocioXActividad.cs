using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using System.Data.Objects;
using System.Data;
using System.Diagnostics;

namespace Negocio
{
    public class SocioXActividad
    {

    //CONEXION BASE DE DATOS
        public static Entities context()
        {
            return Datos.Context.context();
        }

    //QUERY PARA INSERTAR
        public static int Insertar(Datos.SocioXActividad socioXActividad)
        {
            bool actividadActualizada = false;
            Datos.Actividad actividad=null;

            using (Entities tempContext = new Entities())
            {
                //Busca cupo en la actividad
                try
                {
                    actividad = Negocio.Actividad.BuscarId(socioXActividad.idActividad);
                    Datos.SocioXActividad eliminado = tempContext.SocioXActividad.SingleOrDefault(e =>
                        (e.idActividad == socioXActividad.idActividad) &&
                        (e.idSocio == socioXActividad.idSocio));
                    //Paso 1: comprueba que no se encuentre inscrito
                    if (eliminado == null || eliminado.estado == 0)
                    {
                        //Paso 2: trata de reservar cupo modificando la cantidad de vacantesDisponibles
                        //Si ha habido otra transaccion que obtuvo un cupo, vuelve a intentarlo
                        while (actividad.vacantesDisponibles > 0 && actividadActualizada == false)
                        {
                            try
                            {
                                actividad.vacantesDisponibles--;
                                context().SaveChanges();
                                actividadActualizada = true;    //ejecuta esto si no hubo problemas al obtener un cupo
                            }
                            catch (OptimisticConcurrencyException)    //ocurre una excepcion si otro usuario ha modificado el valor de vacantesDisponibles al mismo tiempo
                            {
                                Debug.WriteLine("Error al separar cupo para la actividad "+actividad.nombre);
                                context().Refresh(RefreshMode.StoreWins, actividad);
                            }
                        }
                        //Paso 3: Si obtuvo cupo correctamente inserta en la tabla socioXActividad
                        if (actividadActualizada)
                        {
                            if (eliminado == null)
                            {   //si no esta en la bd
                                tempContext.SocioXActividad.AddObject(socioXActividad);
                                tempContext.SaveChanges();
                            }
                            else
                            {   //Si se encuentra en la bd como inactivo
                                eliminado.estado = 1;
                                tempContext.SaveChanges();
                            }
                            return 1;
                        }
                        return 0;
                    }
                    return 0;
                }
                catch
                {
                    throw;
                }
            }
        }

    //QUERYS DE BUSCAR
        public static IEnumerable<Datos.SocioXActividad> SeleccionarTodo()
        {
            //using (Entities context = new Entities())
            //{
                IEnumerable<Datos.SocioXActividad> listaSocioXActividad = context().SocioXActividad.Where
                    (p => p.estado != 0 && p.Socio.Persona.estado!=0 && p.Actividad.estado!=0);
                return listaSocioXActividad;
            //}
        }
        public static IEnumerable<Datos.SocioXActividad> BuscarIdActividad(short idActividad)
        {
            IEnumerable<Datos.SocioXActividad> listaSocioXActividad = context().SocioXActividad.Where(p => 
                ( (p.estado != 0) && (p.idActividad==idActividad) ));
            return listaSocioXActividad;
        }

        //Buscar todas las activdades en las que un socio se ha incrito
        public static IEnumerable<Datos.SocioXActividad> BuscarActividadIdFamilia(short idFamilia)
        {
            IEnumerable<Datos.SocioXActividad> listaSocioXActividad = context().SocioXActividad
                .Where(p => (p.estado != 0) && (p.Socio.Familia.id == idFamilia));
            return listaSocioXActividad;
        }

        //Busca a una actividad especifica para un socio especifico
        public static IEnumerable<Datos.SocioXActividad> BuscarIdActividadIdFamilia(int idActividad, int idFamilia)
        {
            //using (Entities context = new Entities())
            //{
                IEnumerable<Datos.SocioXActividad> listaSocioXActividad = context().SocioXActividad
                    .Where(p => (p.estado != 0) && (p.idActividad == idActividad) && (p.Socio.Familia.id == idFamilia));
                return listaSocioXActividad;
            //}
        }

    //QUERY DE ELIMINAR
        //Metodos para eliminar una Inscripcion
        public static void Eliminar(Datos.SocioXActividad socioXActividad)
        {
            Eliminar(socioXActividad.idSocio, socioXActividad.idActividad);
        }

        public static void Eliminar(short idSocio, short idActividad)
        {
            bool actividadActualizada = false;
            Datos.Actividad actividad = null;

            using (Entities tempContext = new Entities())
            {
                //Busca cupo en la actividad
                try
                {
                    actividad = Negocio.Actividad.BuscarId(idActividad);
                    while (actividadActualizada == false)
                    {
                        try
                        {
                            actividad.vacantesDisponibles++;
                            context().SaveChanges();
                            actividadActualizada = true;
                        }
                        catch (OptimisticConcurrencyException)    //ocurre una excepcion si otro usuario ha modificado el valor de vacantesDisponibles
                        {
                            Debug.WriteLine("error de concurrencia");
                            context().Refresh(RefreshMode.StoreWins, actividad);
                        }
                    }
                }
                catch
                {
                    //entra aqui si hubo una excepcion en el buscarid
                }
                //Inserta si alcanzo cupo
                if (actividadActualizada)
                {
                    try
                    {
                        Datos.SocioXActividad encontrado = tempContext.SocioXActividad.FirstOrDefault(e =>
                            (e.idActividad == idActividad) && (e.idSocio == idSocio));
                        if (encontrado != null)
                        {
                            encontrado.estado = 0;
                            tempContext.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }
    
        public static void EliminarTodo(short idSocio,short idActividad)
        {
            //using (Entities tempContext = new Entities())
            {
                IEnumerable<Datos.SocioXActividad> encontrado = BuscarIdActividadIdFamilia(idActividad, idSocio);

                if (encontrado != null)
                {
                    foreach (var socioxAct in encontrado)
                    {
                        using (Entities tempContext = new Entities())
                        {
                            Datos.SocioXActividad eliminado = null;
                            eliminado = tempContext.SocioXActividad.SingleOrDefault(e =>
                            (e.idActividad == socioxAct.idActividad) &&
                            (e.idSocio == socioxAct.idSocio));
                            if (eliminado != null)
                            {
                                eliminado.estado = 0;
                                tempContext.SaveChanges();
                            }                       
                        }                    
                    }                    
                }
            }
        }
        
    }
}
