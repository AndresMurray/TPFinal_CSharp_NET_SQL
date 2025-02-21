using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
   public class CategoriaNegocio
    {

        public List<Categoria> listarCategorias()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Select Id, Descripcion From Categorias");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Categoria c = new Categoria();
                    c.Id = (int)datos.Lector["Id"];
                    c.Descripcion = (string)datos.Lector["Descripcion"];
                    lista.Add(c);
                }
                return lista;



            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
