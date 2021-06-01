using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartaOnline.DTO
{
    public class ResponseGetMercaderiaById
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public int Tipo { get; set; }
        public int Precio { get; set; }
        public string Ingredientes { get; set; }
        public string Preparacion { get; set; }
        public string Imagen { get; set; }
    }
}
