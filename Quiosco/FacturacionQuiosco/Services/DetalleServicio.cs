using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacturacionQuiosco.Data;
using FacturacionQuiosco.Products;

namespace FacturacionQuiosco.Services
{
    internal class DetalleServicio
    {
        private AccesoDatos aDatos;
        private DetalleFactura detalle;

        public DetalleServicio()
        {
            aDatos = new AccesoDatos();
        }
        

    }
}
