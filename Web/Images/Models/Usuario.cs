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
        //atributos
        [DisplayName("id")]
        public short id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre de usuario")]
        [DisplayName("Nombre de Usuario")]
        [StringLength(50)]
        public String nomUsuario { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre")]
        [DisplayName("Nombre del Usuario")]
        [StringLength(150)]
        public String nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar una asignación")]
        [DisplayName("Asignación")]
        [StringLength(50)]
        public short idAsignacion { get; set; }

        [Required(ErrorMessage = "Debe ingresar un tipo")]
        [DisplayName("tipo")]
        public bool tipo { get; set; }

        [DisplayName("Contraseña")]
        [StringLength(50)]
        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        public String contrasena { get; set; }

        //metodos para convertir
        public Usuario(Datos.Usuario usuario)
        {
            id = usuario.id;
            nomUsuario = usuario.nomUsuario;
            contrasena = usuario.contrasena;
            if (usuario.Familia.Count() != 0)
            {
                short idPersona = usuario.Familia.First().Socio.First().Persona.id;
                idAsignacion = usuario.Familia.First().id;
                tipo = false;
                nombre = Models.Persona.buscarID(idPersona).nombre + " " + Models.Persona.buscarID(idPersona).apPaterno + " " + Models.Persona.buscarID(idPersona).apMaterno;
            }
            else
            {
                short idPersona = usuario.Empleado.First().Persona.id;
                idAsignacion = usuario.Empleado.First().id;
                tipo = true;
                nombre = Models.Persona.buscarID(idPersona).nombre + " " + Models.Persona.buscarID(idPersona).apPaterno + " " + Models.Persona.buscarID(idPersona).apMaterno;
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
            if (usuario.tipo){
                dataUsuario.Empleado.Clear();
                dataUsuario.Empleado.Add(Models.Empleado.Invertir(Models.Empleado.buscarId(usuario.idAsignacion)));
            }
            else{
                dataUsuario.Familia.Clear();
                dataUsuario.Familia.Add(Models.Familia.Invertir(Models.Familia.SeleccionarTodo().FirstOrDefault(p => p.id == usuario.idAsignacion)));
            }
            return dataUsuario;
        }
        public static IEnumerable<Datos.Usuario> InvertirLista(IEnumerable<Models.Usuario> listaUsuarios)
        {
            return listaUsuarios.Select(usuario => Invertir(usuario));
        }
    }
}