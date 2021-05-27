using System;
using System.Collections.Generic;

namespace CartaOnline.DTO
{
    public class ResponseGetComandaById
    {
        public int ComandaId { get; set; }
        public int PrecioTotal { get; set; }
        public DateTime Fecha { get; set; }
        public int FormaEntregaId { get; set; }
        public ResponseGetComandaByIdFormaEntrega FormaEntrega { get; set; }
        public List<ResponseGetComandaByIdMercaderia> Mercaderia { get; set; }
    }
    public class ResponseGetComandaByIdMercaderia
    {
        public string MercaderiaId { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public string Ingredientes { get; set; }
        public string Preparacion { get; set; }
        public string Imagen { get; set; }

        public int TipoMercaderiaId { get; set; }
    }
    public class ResponseGetComandaByIdFormaEntrega
    {
        public int FormaEntregaId { get; set; }
        public string Descripcion { get; set; }

    }
}
