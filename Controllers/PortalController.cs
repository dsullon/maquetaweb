using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Veterinaria.Models;

namespace Veterinaria.Controllers
{
    public class PortalController : Controller
    {
        string cadenaConexion = "Server=localhost;Database=Veterinaria;" +
            "Integrated Security=true; TrustServerCertificate=true";
        public IActionResult Index()
        {
            List<Servicio> listado = new List<Servicio>();
            using(var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("Select * from Servicios", conexion);
                SqlDataReader reader = command.ExecuteReader();
                if(reader != null && reader.HasRows)
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

            return View(listado);
        }
    }
}
