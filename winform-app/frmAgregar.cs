using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using negocio;
using dominio;
using System.ComponentModel.Design;
using System.IO;
using System.Configuration;
using System.Globalization;

namespace winform_app
{
    public partial class frmAgregar : Form
    {

        private Articulo articulo = null;
        private OpenFileDialog archivo = null;
        private Helper help;

        public frmAgregar()
        {
            InitializeComponent();
        }

        public frmAgregar(Articulo modificar)
        {
            InitializeComponent();
            this.articulo = modificar;
            Text = "Modificar Artículo";
        }

        private void frmAgregar_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categoria = new CategoriaNegocio();
            MarcaNegocio marca = new MarcaNegocio();
            help = new Helper();
            try
            {
                cboCategoria.DataSource = categoria.listarCategorias();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";


                cboMarca.DataSource = marca.listarMarcas();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";

                if (articulo != null) // si esta modificando 
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtNombre.Text = articulo.Nombre;
                    txtPrecio.Text = articulo.Precio.ToString();
                    txtImagen.Text = articulo.UrlImagen;
                    help.cargarImagen(txtImagen.Text, pbImagenAgregar);
                    cboCategoria.SelectedValue = articulo.Categoria.Id;
                    cboMarca.SelectedValue = articulo.Marca.Id;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



        private void btnAceptar_Click(object sender, EventArgs e)
        {


            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {

                if (articulo == null)
                    articulo = new Articulo();
                articulo.Descripcion = txtDescripcion.Text;

                if (!help.validarPrecio(txtPrecio.Text))
                {
                    MessageBox.Show("El precio no es válido. Debe ser un número con hasta dos decimales.");
                    return;
                }

                articulo.Precio = Convert.ToDecimal(txtPrecio.Text);
                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.UrlImagen = txtImagen.Text;
                articulo.Categoria = (Categoria)cboCategoria.SelectedItem;
                articulo.Marca = (Marca)cboMarca.SelectedItem;


                if (articulo.Id != 0)
                {

                    negocio.modificar(articulo);
                    MessageBox.Show("Modificado exitosamente");
                }
                else
                {
                    negocio.agregar(articulo);
                    MessageBox.Show("Agregado exitosamente");

                }
                //Guardo imagen si la levantó localmente:
                if (archivo != null && !(txtImagen.Text.ToUpper().Contains("HTTP")))
                    File.Copy(archivo.FileName, ConfigurationManager.AppSettings["images-folder"] + archivo.SafeFileName);
                this.Close();


            }
            catch (Exception)
            {

                throw;
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregarImg_Click(object sender, EventArgs e)
        {
           
            archivo = new OpenFileDialog();
            archivo.Filter = "Archivos de imagen (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"; // Filtro de imágenes

          
            if (archivo.ShowDialog() == DialogResult.OK)
            {
             
                txtImagen.Text = archivo.FileName;
                help.cargarImagen(archivo.FileName, pbImagenAgregar);
            }
        }


        private void txtImagen_Leave(object sender, EventArgs e)
        {
            help.cargarImagen(txtImagen.Text, pbImagenAgregar);
        }








    }
}
