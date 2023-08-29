using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacturacionQuiosco.Data;
using FacturacionQuiosco.Products;
using System.Data;

namespace FacturacionQuiosco.Services
{
    internal class ArticuloServicios
    {
        private AccesoDatos aDatos;
        private Articulo articulo;
        public ArticuloServicios()
        {
            aDatos = new AccesoDatos();     
        }

        public DataTable CargarArticulos()
        {
            DataTable tabla = new DataTable();
            tabla = aDatos.ConsultarDB("SP_CONSULTAR_ARTICULOS");
            return tabla;
        }
        

    }
}
