using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using System.Collections;
namespace Negocio
{
    public class Usuario
    {

       

        public static void insertar(Datos.Usuario usuario)
        {
            //try
            //{
                Datos.Context.context().Usuario.AddObject(usuario);
                Datos.Context.context().SaveChanges();
                Negocio.Util.logito.ElLogeador("Se ingreso el usuario", usuario.nomUsuario);
            //}catch(Exception ex)
            //{
            //    return ex;
            //}
            //return null;
        }

        public static IEnumerable<Datos.Usuario> seleccionarTodo()
        {
            List<Datos.Usuario> listaUsuarios = Datos.Context.context().Usuario.ToList();  
            return listaUsuarios;
        }

        public static Datos.Usuario buscarId(short id)
        {
            Datos.Usuario usuario = Datos.Context.context().Usuario.Single(p => p.id == id);
            return usuario;
        }

        public static Datos.Usuario buscarFamilia(short id)
        {
            Datos.Usuario usuario = Datos.Context.context().Usuario.Single(p => p.Familia.id == id);
            return usuario;
        }

        public static Exception modificar(Datos.Usuario usuario)
        {
            try
            {
                var auxUsuario = Datos.Context.context().Usuario.Single(p => p.id == usuario.id);
                auxUsuario.contrasena = usuario.contrasena;
                //auxUsuario.Empleado = usuario.Empleado;
                auxUsuario.estado = usuario.estado;
                //auxUsuario.Familia = usuario.Familia;
                auxUsuario.nomUsuario = usuario.nomUsuario;
                auxUsuario.Perfil = Negocio.Perfil.seleccionarTodo().Single(p => p.id == usuario.Perfil.id);
                //auxUsuario.Permiso = usuario.Permiso;
                Datos.Context.context().SaveChanges();
                Negocio.Util.logito.ElLogeador("Se modifico el usuario", usuario.nomUsuario);
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public static int esValido(string nombre , String contrasena)
        {
                if (Datos.Context.context().Usuario.Any(p => p.nomUsuario == nombre && p.contrasena == contrasena))
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
        }

        public static short obtenerid(string nombre, string contrasena) {
            Datos.Usuario usuario = Context.context().Usuario.Single(p => p.nomUsuario == nombre && p.contrasena == contrasena);
                return usuario.id;
        }
    }
}
