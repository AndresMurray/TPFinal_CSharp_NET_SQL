using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using dominio; 

namespace negocio
{
    public class ArticuloNegocio
    {

        public List <Articulo> listarArticulos()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearConsulta("select a.Codigo as codigoArticulo, a.Descripcion as descripArticulo, a.Id as articuloId, a.ImagenUrl, a.Nombre, a.Precio," +
                                     "c.Id AS idCategoria, c.Descripcion as categoriaDescrip, m.Id as idMarca, m.Descripcion as marcaDescrip " +
                                     "from ARTICULOS a INNER JOIN CATEGORIAS c ON(a.IdCategoria = c.Id) INNER JOIN MARCAS m ON(a.IdMarca = m.Id)");
                datos.ejecutarLectura();
                return armarLista(datos);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }   

        }

        public List<Articulo> armarLista(AccesoDatos datos)
        {
            List<Articulo> articulos = new List<Articulo>();
            try
            {
                while (datos.Lector.Read())
                {
                    Articulo articulo = new Articulo();
                    articulo.Codigo = (string)datos.Lector["codigoArticulo"];
                    articulo.Id = (int)datos.Lector["articuloId"];
                    articulo.Descripcion = (string)datos.Lector["descripArticulo"];
                    articulo.UrlImagen = (string)datos.Lector["ImagenUrl"];
                    articulo.Nombre = (string)datos.Lector["Nombre"];
                    articulo.Precio = (decimal)datos.Lector["Precio"];

                    articulo.Marca= new Marca();
                    articulo.Marca.Id = (int)datos.Lector["idMarca"];
                    articulo.Marca.Descripcion = (string)datos.Lector["marcaDescrip"];

                    articulo.Categoria = new Categoria();
                    articulo.Categoria.Id = (int)datos.Lector["idCategoria"];
                    articulo.Categoria.Descripcion = (string)datos.Lector["categoriaDescrip"];

                    articulos.Add(articulo);
                }
                return articulos;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO ARTICULOS(Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) values(@Cod, @Nom, @Desc, @IdMarca, @IdCategoria, @Img, @Precio)");

                datos.setearParametro("@Cod", nuevo.Codigo);
                datos.setearParametro("@Nom", nuevo.Nombre);
                datos.setearParametro("@Desc", nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.Marca.Id);
                datos.setearParametro("@IdCategoria", nuevo.Categoria.Id);
                datos.setearParametro("@Img", nuevo.UrlImagen);
                datos.setearParametro("@Precio", nuevo.Precio);

                datos.ejecutarAccion();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificar (Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE ARTICULOS set Codigo = @Cod , Nombre = @Nom, Descripcion = @Desc, IdMarca = @IdMarca, IdCategoria = @IdCategoria , ImagenUrl = @Img, Precio = @Precio Where Id = @Id");
                datos.setearParametro("@Id", nuevo.Id);
                datos.setearParametro("@Cod", nuevo.Codigo);
                datos.setearParametro("@Nom", nuevo.Nombre);
                datos.setearParametro("@Desc", nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.Marca.Id);
                datos.setearParametro("@IdCategoria", nuevo.Categoria.Id);
                datos.setearParametro("@Img", nuevo.UrlImagen);
                datos.setearParametro("@Precio", nuevo.Precio);

                datos.ejecutarAccion();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


       

    }
}
