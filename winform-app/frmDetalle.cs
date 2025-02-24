using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;

namespace winform_app
{
    public partial class frmDetalle : Form
    {
        private Helper help;
        public frmDetalle()
        {
            InitializeComponent();
        }

        public frmDetalle(Articulo seleccionado)
        {
            InitializeComponent();
            help = new Helper();    
            txtCodDetalle.Text = seleccionado.Codigo;
            txtDescDetalle.Text = seleccionado.Descripcion;
            txtNomDetalle.Text = seleccionado.Nombre;
            txtMarca.Text = seleccionado.Marca.ToString();
            txtCategoria.Text = seleccionado.Categoria.ToString();
            txtPrecio.Text = seleccionado.Precio.ToString("C", new CultureInfo("es-AR"));
            help.cargarImagen(seleccionado.UrlImagen,pbImagen);

        }

      
    }
}
