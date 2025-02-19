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
            try
            {
                articulos = articulosRepo.listarArticulos();
                dgvArticulos.DataSource = articulos;
                ocultarColumnas();
                cargarImagen(articulos[0].UrlImagen);
                ajustarAlturaDGV();

            }
            catch (Exception)
            {

                throw;
            }

        }

        private void cargarImagen(string img)
        {
            try
            {
                // MessageBox.Show("Cargando imagen desde: " + img);  // Verificar URL
                picBoxImagen.Load(img);
            }
            catch (Exception)
            {
                //MessageBox.Show("Error al cargar imagen: "); // Para ver el mensaje de error
                picBoxImagen.Load("https://img.freepik.com/vector-premium/fondo-patron-transparente-cuadrado-vacio-ilustracion-vectorial_522680-518.jpg");
            }
        }

        private void ocultarColumnas()
        {
            dgvArticulos.Columns["UrlImagen"].Visible = false;
            dgvArticulos.Columns["Id"].Visible = false;
        }

        private void dgvArticulos_Click(object sender, EventArgs e)
        {
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.UrlImagen);
        }


        private void ajustarAlturaDGV()
        {
            int maxFilasVisibles = 4; // Máximo de filas visibles sin scroll
            int filas = dgvArticulos.Rows.Count;
            int alturaFila = dgvArticulos.RowTemplate.Height;
            int alturaEncabezado = dgvArticulos.ColumnHeadersHeight;

            // Si hay más de 4 filas, solo mostrar 4 filas y activar el scroll
            if (filas > maxFilasVisibles)
            {
                filas = maxFilasVisibles;
                dgvArticulos.ScrollBars = ScrollBars.Vertical;
            }
            else
            {
                dgvArticulos.ScrollBars = ScrollBars.None;
            }

            dgvArticulos.Height = (filas * alturaFila) + alturaEncabezado + 2;
        }





    }
}
