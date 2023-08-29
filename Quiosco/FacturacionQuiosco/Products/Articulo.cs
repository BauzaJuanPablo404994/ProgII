using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionQuiosco.Products
{
    internal class Articulo
    {
        public int CodArticulo { get; set; }
        public string Nombre { get; set; }
        public double PrecioUnitario { get; set; }
        public bool Activo { get; set; }

        public Articulo(int codArticulo, string nombre, double precio, bool activo)
        {
            CodArticulo = codArticulo;
            Nombre = nombre;
            PrecioUnitario = precio;
            Activo = activo;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
