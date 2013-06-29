using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Usuario
    {
        //auxUsuario.contrasena = usuario.contrasena;
        ////auxUsuario.Empleado = usuario.Empleado;
        //auxUsuario.estado = usuario.estado;
        ////auxUsuario.Familia = usuario.Familia;
        //auxUsuario.nomUsuario = usuario.nomUsuario;
        //auxUsuario.Perfil = Negocio.Perfil.seleccionarTodo().Single(p => p.id == usuario.Perfil.id);
        ////auxUsuario.Permiso = usuario.Permiso;

        //atributos
        [DisplayName("id")]
        public short id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre de usuario")]
        [DisplayName("Nombre de Usuario")]
        [StringLength(50)]
        public String nomUsuario { get; set; }

        [DisplayName("Contrasena")]
        [StringLength(50)]
        [Required(ErrorMessage = "Debe ingresar una contrasena")]
        public String contrasena { get; set; }

        [DisplayName("Estado")]
        public short estado { get; set; }

        [DisplayName("Perfil")]
        public Models.Perfil perfil { get; set; }

        [DisplayName("Perfil")]
        public short idPerfil { get; set; }

        public Usuario() { 
        
        }
        //metodos para convertir
        public Usuario(Datos.Usuario usuario)
        {
            if (usuario != null)
            {
                id = usuario.id;
                nomUsuario = usuario.nomUsuario;
                contrasena = usuario.contrasena;
                perfil = Models.Perfil.buscarId(usuario.Perfil.id);
                estado = usuario.estado;
            }

        }
        public static Usuario Convertir(Datos.Usuario usuario)
        {
            return new Usuario(usuario);
        }

        public static IEnumerable<Usuario> ConvertirLista(IEnumerable<Datos.Usuario> listaUsuarios)
        {
            return  listaUsuarios.Select(usuario => Convertir(usuario));
        }

        //metodos para invertir
        public static Datos.Usuario Invertir(Models.Usuario usuario)
        {
            Datos.Usuario dataUsuario = new Datos.Usuario();
            dataUsuario.id = usuario.id;
            dataUsuario.nomUsuario = usuario.nomUsuario;
            dataUsuario.contrasena = usuario.contrasena;
            dataUsuario.estado = usuario.estado;
            dataUsuario.Perfil = Models.Perfil.Invertir(usuario.perfil);
            return dataUsuario;
        }
        public static IEnumerable<Datos.Usuario> InvertirLista(IEnumerable<Models.Usuario> listaUsuarios)
        {
            return listaUsuarios.Select(usuario => Invertir(usuario));
        }

        public static int esValido(string nomb, string contrasena) {
            return Negocio.Usuario.esValido(nomb, contrasena);
        }

        public static Models.Usuario buscarId(short id) {
            return Models.Usuario.Convertir(Negocio.Usuario.buscarId(id));
        }
        public static Models.Usuario buscarFamilia(short id)
        {
            return Models.Usuario.Convertir(Negocio.Usuario.buscarFamilia(id));
        }
        public static short obtenerPerfil(short id) {
            return Models.Usuario.buscarId(id).perfil.id;
        }

        public static void insertar(Models.Usuario usuario){
            usuario.estado = 1;
            Negocio.Usuario.insertar(Invertir(usuario));
        }
    }
}