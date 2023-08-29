using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionQuiosco.Products
{
    internal class FormaPago
    {
        public int CodFormaPago { get; set; }
        public string FormaDePago { get; set; }

        public FormaPago(int codFormaPago, string formaDePago)
        {
            CodFormaPago = codFormaPago;
            FormaDePago = formaDePago;
        }

        public override string ToString()
        {
            return FormaDePago;
        }

    }
}
