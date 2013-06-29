using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ReporteFinanzas
    {

        public string nombre { get; set; }

        public double total { get; set; }

        public List<Models.ConceptoDePago> listaconceptos { get; set; }

        


        public ReporteFinanzas(DateTime fechaini, DateTime fechafin)
        {
            nombre="Reporte de Finanzas Clubly";

            double totaltemp=0;
            IEnumerable<Datos.ConceptoDePago> listemp;
            IEnumerable<Datos.Pago> listapagos;
            listemp = Negocio.ConceptoDePago.SeleccionarTodoTiposDePago();
            Models.ConceptoDePago modelsconcepto;
            listaconceptos = new List<ConceptoDePago>();
            foreach (Datos.ConceptoDePago concepto in listemp) {
                
                modelsconcepto = Models.ConceptoDePago.Convertir(concepto);
                listaconceptos.Add(modelsconcepto);
                listapagos = concepto.Pago.Where(p=>p.fechaRegistro.Date >= fechaini.Date && p.fechaRegistro.Date<= fechafin.Date && p.estado==Negocio.Pago.CANCELADO);
                modelsconcepto.monto = 0;
                foreach (Datos.Pago pago in listapagos) {
                    modelsconcepto.monto += pago.monto;
                    totaltemp += pago.monto;
                }
                
            }
            total = Math.Round(totaltemp,2);
            foreach(Models.ConceptoDePago concepto in listaconceptos){
                concepto.porcentaje = concepto.monto.Value*100/ total;
                concepto.porcentaje = Math.Round(concepto.porcentaje,2);
                concepto.monto = Math.Round(concepto.monto.Value, 2);
            }

        }
    }


}