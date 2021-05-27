using System;
using System.Collections.Generic;

namespace CartaOnline.DTO
{
    public class ResponseGetAllComandaDto
    {
        public int ComandaId { get; set; }
        public int PrecioTotal { get; set; }
        public DateTime Fecha { get; set; }
        public int FormaEntregaId { get; set; }
        public ResponseGetFormaEntregaByComanda FormaEntrega { get; set; }
        public List<ResponseGetMercaderiaByComanda> Mercaderia { get; set; }
    }
    public class ResponseGetMercaderiaByComanda
    {
        public string MercaderiaId { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public string Ingredientes { get; set; }
        public string Preparacion { get; set; }
        public string Imagen { get; set; }

        public int TipoMercaderiaId { get; set; }
    }
    public class ResponseGetFormaEntregaByComanda
    {
        public int FormaEntregaId { get; set; }
        public string Descripcion { get; set; }

    }
}
