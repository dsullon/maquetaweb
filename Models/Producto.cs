﻿namespace Veterinaria.Models
{
    public class Producto
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string PathImagen { get; set; }
        public decimal Precio { get; set; }
        public int CategoriaID { get; set; }
        public string Activo { get; set; }
    }
}
