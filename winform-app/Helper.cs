using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winform_app
{
    public class Helper
    {

        public void cargarImagen(string img, PictureBox pictureBox)
        {
            try
            {
                // MessageBox.Show("Cargando imagen desde: " + img);  // Verificar URL
                pictureBox.Load(img);
            }
            catch (Exception)
            {
                //MessageBox.Show("Error al cargar imagen: "); // Para ver el mensaje de error
                pictureBox.Load("https://img.freepik.com/vector-premium/fondo-patron-transparente-cuadrado-vacio-ilustracion-vectorial_522680-518.jpg");
            }
        }


        public void ocultarColumnas(DataGridView dgv)
        {
            dgv.Columns["UrlImagen"].Visible = false;
            dgv.Columns["Id"].Visible = false;
            dgv.Columns["Descripcion"].Visible = false;
            dgv.Columns["Categoria"].Visible = false;
            dgv.Columns["Codigo"].Visible = false;
        }


        public void ajustarAlturaDGV(DataGridView dgv)
        {
            int maxFilasVisibles = 4; // Máximo de filas visibles sin scroll
            int filas = dgv.Rows.Count;
            int alturaFila = dgv.RowTemplate.Height;
            int alturaEncabezado = dgv.ColumnHeadersHeight;

            // Si hay más de 4 filas, solo mostrar 4 filas y activar el scroll
            if (filas > maxFilasVisibles)
            {
                filas = maxFilasVisibles;
                dgv.ScrollBars = ScrollBars.Vertical;
            }
            else
            {
                dgv.ScrollBars = ScrollBars.None;
            }

            dgv.Height = (filas * alturaFila) + alturaEncabezado + 2;
        }


        public bool soloNumeros(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)))
                    return false;
            }
            return true;
        }


        public bool validarPrecio(string precio)
        {
            
            string patron = @"^\d+(\,|\.)?\d{0,2}$";

            return Regex.IsMatch(precio, patron);
        }


    }
}
