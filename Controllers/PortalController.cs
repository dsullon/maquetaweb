using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Veterinaria.Models;

namespace Veterinaria.Controllers
{
    public class PortalController : Controller
    {
        private readonly IConfiguration _config;
        public PortalController(IConfiguration config)
        {
            _config = config;
        }

        private IEnumerable<Servicio> listadoServicios(){
            string cadenaConexion = _config["ConnectionStrings:local"];
            List<Servicio> listado = new List<Servicio>();
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("Select * from Servicios", conexion);
                SqlDataReader reader = command.ExecuteReader();
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        listado.Add(new Servicio
                        {
                            ID = reader.GetInt32(0),
                            Titulo = reader.GetString(1),
                            Descripcion = reader.GetString(2),
                            PathImagen = reader.GetString(3),
                            Precio = reader.GetDecimal(4),
                            Activo = reader.GetString(5)
                        });
                    }
                }
            }
            return listado;
        }

        private IEnumerable<Producto> listadoProductos()
        {
            string cadenaConexion = _config["ConnectionStrings:local"];
            List<Producto> listado = new List<Producto>();
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("ListarProductos", conexion);
                SqlDataReader reader = command.ExecuteReader();
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        listado.Add(new Producto
                        {
                            ID = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Descripcion = reader.GetString(2),
                            PathImagen = reader.GetString(3),
                            Precio = reader.GetDecimal(4),
                            CategoriaID = reader.GetInt32(5),
                            Activo = reader.GetString(6)
                        });
                    }
                }
            }
            return listado;
        }

        private IEnumerable<Categoria> listadoCategorias()
        {
            string cadenaConexion = _config["ConnectionStrings:local"];
            List<Categoria> listado = new List<Categoria>();
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("Select * from CategoriaProductos", conexion);
                SqlDataReader reader = command.ExecuteReader();
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        listado.Add(new Categoria
                        {
                            ID = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Activo = reader.GetString(2)
                        });
                    }
                }
            }
            return listado;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Servicio> servicios = listadoServicios();
            IEnumerable<Producto> productos = listadoProductos();
            var viewModel = new PortalIndexViewModel
            {
                Servicios = servicios,
                Productos = productos.Take(5)
            };

            return View(await Task.Run(() => viewModel));
        }
        
        [HttpGet("Productos")]
        public async Task<IActionResult> Productos(int pag=1, int categoria=0)
        {
            pag -= 1;

            IEnumerable<Producto> productos = listadoProductos();
            if(categoria>0)
                productos = productos.Where(x => x.CategoriaID == categoria);

            int totalRegistros = productos.Count();
            int registrosPorPagina = 6;
            int numPaginas = totalRegistros / registrosPorPagina;
            int residuo = totalRegistros % registrosPorPagina;
            if (residuo > 0)
                numPaginas += 1;

            ViewBag.numeroPaginas = numPaginas;
            ViewBag.paginaActual = pag;
            ViewBag.categorias = listadoCategorias();
            ViewBag.categoria = categoria;

            return View(await Task.Run(()=> 
                                        productos.Skip(pag*registrosPorPagina).
                                        Take(registrosPorPagina)));
        }
    }
}
