
namespace CartaOnline.DTO
{
    public class ResponseGetMercaderiaTipoDto
    {
        public string Nombre { get; set; }
        public int Tipo { get; set; }
        public int Precio { get; set; }
        public string Ingredientes { get; set; }
        public string Preparacion { get; set; }
        public string Imagen { get; set; }

    }
    public class ResponseGetTipoMercaderiaByMercaderia
    {
        public int Tipo { get; set; }
        public string Descripcion { get; set; }
    }
}
