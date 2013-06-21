using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using System.Collections;
namespace Negocio
{
    public class Asistencia
    {
        public static Entities context()
        {
            return Context.context();
        }

        public static void insertar(Datos.Asistencia asistencia)
        {
            using (Entities tempContext = new Entities())
            {
                Datos.Asistencia eliminado = null;
                eliminado = tempContext.Asistencia.SingleOrDefault(e => (e.Empleado.Persona.id == asistencia.Empleado.Persona.id));
                if (eliminado == null)
                {
                    tempContext.Asistencia.AddObject(asistencia);
                    tempContext.SaveChanges();
                }
                else 
                {
                    eliminado.estado = 1;
                    tempContext.SaveChanges();
                }
            }
        }

        public static void modificar(Datos.Asistencia asistencia)
        {
            context().Asistencia.ApplyCurrentValues(asistencia);
            context().SaveChanges();

        }

        public static IEnumerable<Datos.Asistencia> SeleccionarAsistencia()
        {
            return context().Asistencia.Where(asistencia => asistencia.estado != 0);
        }

        public static Datos.Asistencia buscarId(short id)
        {
            return context().Asistencia.Single(p => p.id == id);
        }



        public static int Insertar(short idEmpleado,string nombre)
        {
            try
            {
                if (context().Empleado.SingleOrDefault(empleado => empleado.id == idEmpleado && nombre.CompareTo(empleado.Persona.nombre)==0) != null)
                {
                    DateTime fecha = DateTime.Today;
                    TimeSpan hora = DateTime.Now.TimeOfDay;
                    IEnumerable<Datos.Asistencia> listaAsistencia = Negocio.Asistencia.SeleccionarAsistencia();
                    //////////
                        //DateTime fecha2 = DateTime.Now;
                        //string lines = "\n Se paso asistencia      "+ nombre +"       "+ fecha2.ToString();   
                        //System.IO.StreamWriter file = new System.IO.StreamWriter("C:/Users/James/Desktop/LOOOG.txt",true);
                        //file.WriteLine(lines);

                        //file.Close();
                     /////////

                    Datos.Asistencia asist = listaAsistencia.SingleOrDefault(a => ((a.fecha.Day == fecha.Day) && (a.fecha.Month == fecha.Month) && (a.fecha.Year == fecha.Year) && (a.Empleado.id == idEmpleado)));
                    if (asist == null)
                    {
                        asist = new Datos.Asistencia();
                        asist.horaEntrada = hora;
                        asist.fecha = fecha;
                        asist.estado = 1;
                        asist.Empleado = Negocio.Empleado.buscarId(idEmpleado);
                        context().Asistencia.AddObject(asist);
                        context().SaveChanges();
                        
                        return 1; 
                    }
                    else
                    {
                        Datos.Asistencia asistencia = listaAsistencia.SingleOrDefault(a => ((a.fecha.Day == fecha.Day) && (a.fecha.Month == fecha.Month) && (a.fecha.Year == fecha.Year) && (a.Empleado.id == idEmpleado) && (a.horaSalida == null)));
                        if (asistencia != null)
                        {
                            asistencia.horaSalida = hora;
                            context().SaveChanges();
                            return 2;
                        }
                    }
                    return 4;  
                }
            }
            catch (Exception) 
            { 
                return 3;
            }
            return 0;
        }
    }
}
