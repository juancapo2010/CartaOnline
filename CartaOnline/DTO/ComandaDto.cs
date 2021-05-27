using System.Collections.Generic;


namespace CartaOnline.DTO
{
    public class ComandaDto
    {
        public List<ComandaMercaderiaListaDto> Mercaderia { get; set; }
        public int FormaEntregaId { get; set; }

    }
    public class ComandaMercaderiaListaDto
    {
        public int MercaderiaId { get; set; }
    }

}
