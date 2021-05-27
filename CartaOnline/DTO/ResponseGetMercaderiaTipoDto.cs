
namespace CartaOnline.DTO
{
    public class ResponseGetMercaderiaTipoDto
    {
        public string Nombre { get; set; }
        public int TipoMercaderiaId { get; set; }
        public int Precio { get; set; }
        public string Ingredientes { get; set; }
        public string Preparacion { get; set; }
        public string Imagen { get; set; }

    }
    public class ResponseGetTipoMercaderiaByMercaderia
    {
        public int TipoMercaderiaId { get; set; }
        public string Descripcion { get; set; }
    }
}
