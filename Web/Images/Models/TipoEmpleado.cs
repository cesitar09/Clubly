using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class TipoEmpleado
    {
        //atributos
        [Required(ErrorMessage="Seleccione una opcion")]
        public short id { get; set; }
        
        [DisplayName("Tipo de empleado")]
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public double sueldo { get; set; }
        public string area { get; set; }
        public string cargo { get; set; }
        public short estado { get; set; }

        public TipoEmpleado()
        { 
        }
        //metodos para convertir
        public TipoEmpleado(Datos.TipoEmpleado tipoempleado)
        {
            id = tipoempleado.id;
            nombre = tipoempleado.nombre;
            descripcion = tipoempleado.descripcion;
            sueldo = tipoempleado.sueldo;
            area = tipoempleado.area;
            cargo = tipoempleado.cargo;
            estado = tipoempleado.estado;
            
        }
        public static Models.TipoEmpleado Convertir(Datos.TipoEmpleado tipoEmpleado)
        {
            return new Models.TipoEmpleado(tipoEmpleado);
        }
        public static IEnumerable<Models.TipoEmpleado> ConvertirLista(IEnumerable<Datos.TipoEmpleado> tipoempleados)
        {
                return tipoempleados.Select(tipoempleado => Convertir(tipoempleado) );
            } 

        //metodos para invertir
        public static Datos.TipoEmpleado Invertir(Models.TipoEmpleado tipoempleado)
        {
            if (tipoempleado==null) return null;
            Datos.TipoEmpleado dtipoempleado = new Datos.TipoEmpleado();
            dtipoempleado.id = tipoempleado.id;
            dtipoempleado.nombre = tipoempleado.nombre;
            dtipoempleado.descripcion = tipoempleado.descripcion;
            dtipoempleado.sueldo = tipoempleado.sueldo;
            dtipoempleado.area = tipoempleado.area;
            dtipoempleado.cargo = tipoempleado.cargo;
            dtipoempleado.estado = tipoempleado.estado;
            return dtipoempleado;
        }


        public static IEnumerable<Web.Models.TipoEmpleado> seleccionarTodo() {
            return ConvertirLista(Negocio.TipoEmpleado.seleccionarTodo());
        }
        public static Web.Models.TipoEmpleado buscarId(short id) {
            return Convertir(Negocio.TipoEmpleado.buscarId(id));
        }
    }

}