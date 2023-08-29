using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacturacionQuiosco.Products;

namespace FacturacionQuiosco.Products
{
    internal class DetalleFactura
    {
        public Articulo Articulo { get; set; }
        public int Cantidad { get; set; }
        public double Descuento { get; set; }

        public DetalleFactura(Articulo articulo, int cantidad, double descuento)
        {
            Articulo = articulo;
            Cantidad = cantidad;
            Descuento = descuento;
        }

        public double CalcularSubTotal()
        {
            double monto = (Articulo.PrecioUnitario * Cantidad);
            double subTotal = monto - (monto * Descuento / 100);
            return subTotal;
        }
    }
}
