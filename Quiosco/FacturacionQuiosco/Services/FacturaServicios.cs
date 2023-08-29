using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacturacionQuiosco.Data;
using System.Data;
using FacturacionQuiosco.Products;

namespace FacturacionQuiosco.Services
{
    internal class FacturaServicios
    {
        private AccesoDatos aDatos;
        private Factura factura;

        public FacturaServicios()
        {
            aDatos = new AccesoDatos();           
        }

        public string ProximaFactura()
        {
            string proxima = aDatos.ProximoId("SP_PROXIMO_ID");
            return proxima;
        }

        
    }
}
