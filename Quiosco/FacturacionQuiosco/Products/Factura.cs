using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacturacionQuiosco.Products;

namespace FacturacionQuiosco.Products
{
    internal class Factura
    {
        public int NroFactura { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public FormaPago FormaPago { get; set; }
        public double Descuento { get; set; }
        public List<DetalleFactura> Detalles { get; set; }


        public Factura()
        {
            Detalles = new List<DetalleFactura>();
        }

        public void AgregarDetalle(DetalleFactura detalle)
        {
            
            Detalles.Add(detalle);
        }

        public void EliminarDetalle(int fila)
        {
            Detalles.RemoveAt(fila);
        }

        public double CalcularTotal()
        {
            double total = 0;
            foreach (DetalleFactura d in Detalles)
            {
                total += d.CalcularSubTotal();
            }

            return total;
        }
    }
}
