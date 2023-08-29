using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarpinteriaMetalica.Presentacion
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNuevoPresupuesto nuevo = new frmNuevoPresupuesto();
            nuevo.ShowDialog(); // .ShowDialog a diferencia de .Show hace que la ventana de Principal no se pueda usar hasta que esta (NuevoPresupuesto) no cierre
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {

        }
    }
}
