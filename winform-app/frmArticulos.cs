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
                help.ajustarAlturaDGV(dgvArticulos); 

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
    }
}
