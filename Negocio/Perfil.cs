using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using System.Collections;
using System.Data.Objects.DataClasses;
using System.Data.Objects;

namespace Negocio
{
    public class Perfil
    {
        

        public static Exception insertar(Datos.Perfil perfil)
        {
            try
            {
                Datos.Context.context().Perfil.AddObject(perfil);
                Datos.Context.context().SaveChanges();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static IEnumerable<Datos.Perfil> seleccionarTodo()
        {
            try
            {
                return Datos.Context.context().Perfil;
            }
            catch
            {
                return null;
            }
        }

        public static void Modificar(Datos.Perfil nuevoPerfil)
        {
            try
            {
                Datos.Context.context().SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Datos.Perfil BuscarId(int idPerfil)
        {
            return Context.context().Perfil.SingleOrDefault(p => p.id == idPerfil);
        }
    }
}
