using FacturacionQuiosco.Services;
using FacturacionQuiosco.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Net.Mime.MediaTypeNames;
using System.IO;

namespace FacturacionQuiosco.View
{
    public partial class frmNuevaFactura : Form
    {
        ArticuloServicios aServicios;
        FacturaServicios fServicios;
        Factura factura;
        public frmNuevaFactura()
        {
            aServicios = new ArticuloServicios();
            fServicios = new FacturaServicios();
            factura = new Factura();
            InitializeComponent();
            dgvArticulos.ReadOnly = true;
        }

        private void frmNuevaFactura_Load(object sender, EventArgs e)
        {

            txtFecha.Text = DateTime.Today.ToShortDateString();
            txtApellido.Text = "Consumidor Final";
            CargarCampos();
            CargarComboPrecio();
            CargarNroFactura();
            dgvArticulos.RowHeadersVisible = false; // Ocultamos la columna izquierda de encabezados
        }

        private void CargarCampos()
        {
            txtDescuento.Text = "0";
            txtCantidad.Text = "1";
        }

        private DataTable CargarTablaArticulos()
        {
            DataTable tablaArticulos = new DataTable();
            tablaArticulos = aServicios.CargarArticulos();
            return tablaArticulos;
        }


        private void CargarNroFactura()
        {
            string nroFactura = fServicios.ProximaFactura();
            if (nroFactura == "")
            {
                nroFactura = "1";
            }
            lblNroFactura.Text = lblNroFactura.Text + " " + nroFactura;
        }

        private void CargarComboPrecio()
        {
            DataTable tabla = CargarTablaArticulos();
            cboArticulo.DataSource = tabla;
            cboArticulo.ValueMember = tabla.Columns[0].ColumnName;
            cboArticulo.DisplayMember = tabla.Columns[1].ColumnName;
        }

        private void cboArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable tabla = CargarTablaArticulos();
            txtPrecioUnitario.Text = (tabla.Rows[cboArticulo.SelectedIndex][2]).ToString() + "$";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {


            DataRowView item = (DataRowView)cboArticulo.SelectedItem;
            //Crear articulo--------
            int cod = Convert.ToInt32(item.Row.ItemArray[0]);
            string nom = item.Row.ItemArray[1].ToString();
            double pre = Convert.ToDouble(item.Row.ItemArray[2]);
            bool act = false;
            if (Convert.ToInt32(item.Row.ItemArray[3]) == 1)
            {
                act = true;
            }
            Articulo articulo = new Articulo(cod, nom, pre, act);

            //VALIDACION ID
            if (Validacion(cod) == true) 
            {
                //Crear DetalleFactura
                DetalleFactura detalle;
                int cantidad = Convert.ToInt32(txtCantidad.Text);
                double descuento = Convert.ToDouble(txtDescuento.Text);
                detalle = new DetalleFactura(articulo, cantidad, descuento);
                factura.AgregarDetalle(detalle);

                //Ejecutamos la actualizacion de la lista
                ActualizarDgv(factura);

                //Cargamos nuevamente los campos para que vuelvan al "Default"
                CargarCampos();
            }
        }

        private void ActualizarDgv(Factura factura)
        {
            //Variables creadas para txtSubTotal y txtTotal
            double total = 0;
            double subTotalTxt = 0;

            dgvArticulos.Rows.Clear();//Limpiamos previamente todas las filas del dgv
            foreach (DetalleFactura detalle in factura.Detalles) 
            {
                //Se agrega al dgv cada detalle que se encuentra en factura
                double subTotal = detalle.CalcularSubTotal();
                dgvArticulos.Rows.Add(detalle.Articulo.CodArticulo,
                                      detalle.Articulo.Nombre,
                                      detalle.Articulo.PrecioUnitario,
                                      detalle.Cantidad,
                                      detalle.Descuento + " %",
                                      subTotal,
                                      "Eliminar"
                                      );



                //Se suman los montos totales sin descuento para txtSubtotal y txtxTotal
                subTotalTxt += (detalle.Cantidad * detalle.Articulo.PrecioUnitario);
                total += subTotal;
            }
            txtSubTotal.Text = subTotalTxt.ToString();
            txtTotal.Text = total.ToString();
        }

        private bool Validacion(int cod)
        {
            bool validacion = false;
            while (validacion == false)
            {
                //Validacion Apellido---------
                if (txtApellido.Text == null || txtApellido.Text == "")
                {
                    MessageBox.Show("Debe ingresar un apellido..."
                    , "Control"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Exclamation);
                    return false;
                }
                //Validacion Cantidad--------
                //Cantidad Nula  o No Numerica
                if (string.IsNullOrEmpty(txtCantidad.Text) || !int.TryParse(txtCantidad.Text, out _))
                {
                    MessageBox.Show("Debe ingresar una cantidad valida..."
                        , "Control"
                        , MessageBoxButtons.OK
                        , MessageBoxIcon.Exclamation);
                    return false;
                }
                //Cantidad menor o igual a 0
                if (Convert.ToInt32(txtCantidad.Text) <= 0)
                {
                    MessageBox.Show("Debe ingresar una cantidad mayor a 0..."
                        , "Control"
                        , MessageBoxButtons.OK
                        , MessageBoxIcon.Exclamation);
                    return false;
                }
                //Validacion Descuento-----------
                //Cantidad Nula o No Numerica
                if (string.IsNullOrEmpty(txtDescuento.Text) || !int.TryParse(txtDescuento.Text, out _))
                {
                    MessageBox.Show("Debe ingresar un descuento válido..."
                        , "Control"
                        , MessageBoxButtons.OK
                        , MessageBoxIcon.Exclamation);
                    return false;
                }
                //Cantidad menor a 0
                if (Convert.ToInt32(txtDescuento.Text) < 0)
                {
                    MessageBox.Show("Debe ingresar un descuento igual o mayor a 0..."
                        , "Control"
                        , MessageBoxButtons.OK
                        , MessageBoxIcon.Exclamation);
                    return false;
                }
                if (Convert.ToInt32(txtDescuento.Text) > 100)
                {
                    MessageBox.Show("No puede ingresar un descuento mayor al 100%..."
                        , "Control"
                        , MessageBoxButtons.OK
                        , MessageBoxIcon.Exclamation);
                    return false;
                }

                //Revisar que el Articulo no se haya ingresado antes
                foreach (DataGridViewRow fila in dgvArticulos.Rows)
                {
                    if (Convert.ToInt32(fila.Cells["colCodArticulo"].Value).Equals(cod))
                    {
                        MessageBox.Show("El articulo que intentas cargar ya fue ingresado..."
                            , "Control"
                            , MessageBoxButtons.OK
                            , MessageBoxIcon.Exclamation);
                        return false;
                    }
                }
                validacion = true;
            }
            return validacion;
        }

        private void dgvArticulos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6 && e.RowIndex > -1)
            {
                factura.EliminarDetalle(e.RowIndex);
                ActualizarDgv(factura);
            }
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
