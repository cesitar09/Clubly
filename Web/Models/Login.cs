using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
using System.Data.Objects.DataClasses;

namespace Web.Models
{
    public class Login
    {
        [DisplayName("Usuario")]
        public short usuario { get; set; }
        [DisplayName("Contraseña")]
        public String contrasena { get; set; }
        [DisplayName("Nombre de usuario")]
        public String nombreusuario { get; set; }
        public short perfil { get; set; }
        //public string perfil { get; set; }

        public  Login(){}

        public  Login(short us, String cont, short perf, string nomb) {
            usuario = us;
            contrasena = cont;
            perfil = perf;
            nombreusuario = nomb;
        }
        public static int esValido(string nomb, String cont) {
            return Negocio.Usuario.esValido(nomb , cont);        
        }

        public static   short obtenerid(string nomb, string  contrasena){
            return Negocio.Usuario.obtenerid(nomb, contrasena);
        }
    }

  

}