using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio; 

namespace negocio
{
    public class MarcaNegocio
    {
        public List<Marca> listarMarcas()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Select Id, Descripcion From Marcas");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Marca m = new Marca ();
                    m.Id = (int)datos.Lector["Id"];
                    m.Descripcion = (string)datos.Lector["Descripcion"];
                    lista.Add(m);
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
