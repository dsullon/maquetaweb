namespace Veterinaria.Models
{
    public class Servicio
    {
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string PathImagen { get; set; }
        public string Activo { get; set; }
    }
}
