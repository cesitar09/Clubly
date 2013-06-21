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
        public short perfil { get; set; }
        //public string perfil { get; set; }

        public  Login(){}

        public  Login(short us, String cont, short perf) {
            usuario = us;
            contrasena = cont;
            perfil = perf;
        }
        public static int esValido(short us, String cont) {
            return Models.Usuario.esValido(us, cont);        
        }
    }

  

}