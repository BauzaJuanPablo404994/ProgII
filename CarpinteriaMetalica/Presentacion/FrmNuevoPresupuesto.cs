using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using CarpinteriaMetalica.Entidades;

namespace CarpinteriaMetalica.Presentacion
{
    public partial class frmNuevoPresupuesto : Form
    {
        Presupuesto nuevo;
        public frmNuevoPresupuesto()
        {
            InitializeComponent();
            nuevo = new Presupuesto();
        }

        private void FrmNuevoPresupuesto_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Today.ToShortDateString();
            txtCliente.Text = "Consumidor Final";
            txtDescuento.Text = "0";
            txtCantidad.Text = "1";
            ProximoPresupuesto();
            CargarProductos();
        }

        private void CargarProductos()
        {
            SqlConnection cnn = new SqlConnection();
            cnn.ConnectionString = @"Data Source=172.16.10.196;Initial Catalog=Carpinteria_2023;User ID=alumno1w1;password=alumno1w1";
            cnn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_CONSULTAR_PRODUCTOS";

            DataTable tabla = new DataTable();
            tabla.Load(cmd.ExecuteReader());

            cnn.Close(); ;

            cboProducto.DataSource = tabla;
            cboProducto.ValueMember = tabla.Columns[0].ColumnName;
            cboProducto.DisplayMember = tabla.Columns[1].ColumnName;
        }

        private void ProximoPresupuesto()
        {
            SqlConnection cnn = new SqlConnection();
            cnn.ConnectionString = @"Data Source=172.16.10.196;Initial Catalog=Carpinteria_2023;User ID=alumno1w1;password=alumno1w1";
            cnn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_PROXIMO_ID";

            SqlParameter parametro = new SqlParameter(); //Creamos un parametro para recibir el id
            parametro.ParameterName = "@next";
            parametro.SqlDbType = SqlDbType.Int;
            parametro.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(parametro);
            cmd.ExecuteNonQuery();

            cnn.Close();

            lblPresupuestoNro.Text = lblPresupuestoNro.Text + " " + parametro.Value.ToString();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //Validación
            if (cboProducto.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un producto..."
                    , "Control"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtCantidad.Text) || !int.TryParse(txtCantidad.Text, out _))
            {
                MessageBox.Show("Debe ingresar una cantidad valida..."
                    , "Control"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Exclamation);
                return;
            }
            foreach (DataGridViewRow fila in dgvDetalles.Rows)
            {
                if (fila.Cells["ColProducto"].Value.ToString().Equals(cboProducto.Text))
                {
                    MessageBox.Show("Este producto ya está presupuestado..."
                    , "Control"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Exclamation);
                    return;
                }
            }

            //Vamos a transformar los items en objetos-----
            //Creamos un item tipo DataRowView (la fila seleccionada del cboBox)
            DataRowView item = (DataRowView)cboProducto.SelectedItem;

            //Extraemos las variables de cada columna del DataRowView para desps hacer un objeto tipo Producto
            int nro = Convert.ToInt32(item.Row.ItemArray[0]);
            string nom = item.Row.ItemArray[1].ToString();
            double pre = Convert.ToDouble(item.Row.ItemArray[2]);

            //Creamos el objeto tipo Producto
            Producto p = new Producto(nro, nom, pre);

            //Tomamos el numero escrito en el txt y parceamos para luego crear un objeto tipo DetallePresupuesto
            int cant = Convert.ToInt32(txtCantidad.Text);

            //Creamos DetallePresupuesto
            DetallePresupuesto detalle = new DetallePresupuesto(p, cant);

            //Ejecutamos el metodo de Presupuesto para agregar al detalle el detalle creado
            nuevo.AgregarDetalle(detalle);

            //Se agrega de forma ordenada columna por columna cada dato del detalle creado
            dgvDetalles.Rows.Add(detalle.Producto.ProductoNro,
                                 detalle.Producto.Nombre,
                                 detalle.Producto.Precio,
                                 detalle.Cantidad,
                                 "Quitar");//Este dato es el string que va a figurar en la última columna

            CalcularTotales();
        }

        private void CalcularTotales()
        {
            double total = nuevo.CalcularTotal();
            txtSubTotal.Text = total.ToString();
            double dto = total * Convert.ToDouble(txtDescuento.Text) / 100;
            txtTotal.Text = (total - dto).ToString();
        }

        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalles.CurrentCell.ColumnIndex == 4) // Consulta si el click realizado es sobre la columna de indice = 4
            {
                if (dgvDetalles.CurrentRow.Index != -1) // Chequeamos que no estemos clickeando los títulos
                {
                    nuevo.QuitarDetalle(dgvDetalles.CurrentRow.Index);
                    dgvDetalles.Rows.RemoveAt(dgvDetalles.CurrentRow.Index);
                }
                CalcularTotales();
            }

            //if (e.ColumnIndex == 4) // Consulta si el click realizado es sobre la columna de indice = 4
            //{
            //    nuevo.QuitarDetalle(e.RowIndex);
            //    dgvDetalles.Rows.RemoveAt(e.RowIndex);

            //    CalcularTotales();
            //}

        }
    }
}
