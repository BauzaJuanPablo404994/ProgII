using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionQuiosco.Products
{
    internal class Cliente
    {
        public int NroCliente { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public int Documento { get; set; }

        public Cliente(int nroCliente, string apellido, string nombre, int documento)
        {
            NroCliente = nroCliente;
            Apellido = apellido;
            Nombre = nombre;
            Documento = documento;          
        }

        public override string ToString()
        {
            return Apellido + " " + Nombre;
        }
    }
}
