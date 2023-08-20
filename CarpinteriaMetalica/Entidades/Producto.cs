using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpinteriaMetalica.Entidades
{
    internal class Producto
    {
        public int ProductoNro { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public bool Activo { get; set; }

        public Producto(int productoNumero, string nombre, double precio)
        {
            ProductoNro = productoNumero;
            Nombre = nombre;
            Precio = precio;
            Activo = true;
        }

        public override string ToString()
        {
            return Nombre;
        }


    }
    
}
