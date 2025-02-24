using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D; 
using negocio;
using dominio;
using System.Globalization;

namespace winform_app
{
    public partial class frmArticulos : Form
    {
        private List<Articulo> articulos;
        private Articulo seleccionado;
        private Helper help;
        public frmArticulos()
        {
            InitializeComponent(); 
           // this.Paint += new PaintEventHandler(FormArticulos_Paint);
        }

        /*private void FormArticulos_Paint(object sender, PaintEventArgs e)
        {
            // Definir colores del degradado
            Color colorInicio = Color.FromArgb(30, 144, 255); // Azul moderno
            Color colorFin = Color.FromArgb(0, 0, 128); // Azul oscuro

            // Crear el objeto para el degradado
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle, colorInicio, colorFin, LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }*/

        private void frmArticulos_Load(object sender, EventArgs e)
        {
            cargar();

        }

        private void cargar()
        {
            ArticuloNegocio articulosRepo = new ArticuloNegocio();
            help = new Helper();
            try
            {
                articulos = articulosRepo.listarArticulos();
                dgvArticulos.DataSource = articulos;
                help.ocultarColumnas(dgvArticulos);
                help.cargarImagen(articulos[0].UrlImagen, picBoxImagen);
                cboCampo.Items.Clear();
                cboCriterio.Items.Clear();
                cboCampo.Items.Add("Marca");
                cboCampo.Items.Add("Precio");
                txtPrecio.Clear();
                lblPrecio.Visible = false;
                txtPrecio.Visible = false;
                // help.ajustarAlturaDGV(dgvArticulos);


            }
            catch (Exception)
            {

                throw;
            }

        }

      

        private void dgvArticulos_Click(object sender, EventArgs e)
        {
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            help.cargarImagen(seleccionado.UrlImagen, picBoxImagen); 
        }



        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregar alta = new frmAgregar();
            alta.ShowDialog();
            cargar();// vuelvo a cargar para que se muestre el articulo agregado
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            seleccionado = new Articulo();
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            frmAgregar modificar = new frmAgregar(seleccionado);
            modificar.ShowDialog();
            cargar();
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            frmDetalle detalle = new frmDetalle(seleccionado);
            detalle.ShowDialog();
            cargar();
        }


       

        private void dgvArticulos_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvArticulos.Columns[e.ColumnIndex].Name == "Precio")
            {
                // Formatear el valor de la celda como moneda en ARS
                if (e.Value != null)
                {
                    e.Value = Convert.ToDecimal(e.Value).ToString("C", new CultureInfo("es-AR"));
                    e.FormattingApplied = true;  // Marca como formateado
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Esta seguro que quiere eliminar este disco?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    negocio.eliminar(seleccionado);

                    cargar();
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = txtFiltro.Text;

            if (filtro != "")
            {
                listaFiltrada = articulos.FindAll(a => a.Nombre.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                listaFiltrada = articulos;
            }
            dgvArticulos.DataSource = null; 
            dgvArticulos.DataSource = listaFiltrada;
            help.ocultarColumnas(dgvArticulos);
            
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cboCampo.SelectedItem.ToString();
            MarcaNegocio negocio = new MarcaNegocio();
            if (opcion == "Marca")
            {
                cboCriterio.Items.Clear();
                List<Marca> marcas = negocio.listarMarcas();
                foreach (var m in marcas)
                {
                    cboCriterio.Items.Add(m);
                    lblPrecio.Visible = false;
                    txtPrecio.Visible=false;
                }
                
            }
            else
            {
                lblPrecio.Visible = true;
                txtPrecio.Visible = true;
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("Menor a");
                
            }

        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if (validarFiltro())
                    return;
                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtPrecio.Text;

                dgvArticulos.DataSource = negocio.filtrar(campo,criterio,filtro);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private bool validarFiltro()
        {
            if (cboCampo.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, seleccione el campo para filtrar.");
                return true;
            }
            if (cboCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, seleccione el criterio para filtrar.");
                return true;
            }
            if (cboCampo.SelectedItem.ToString() == "Precio")
            {
                if (string.IsNullOrEmpty(txtPrecio.Text))
                {
                    MessageBox.Show("Debes cargar un precio");
                    return true;
                }
                if (!(help.validarPrecio(txtPrecio.Text)))
                {
                    MessageBox.Show("El precio no es válido. Debe ser un número con hasta dos decimales. ");
                    return true;
                }

            }

            return false;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cargar();
            
        }
    }
}
