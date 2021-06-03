using System.Collections.Generic;


namespace CartaOnline.DTO
{
    public class ComandaDto
    {
        public List<int> Mercaderias { get; set; }
        public int FormaEntrega { get; set; }

    }
    public class ComandaMercaderiaListaDto
    {
        public int Mercaderia { get; set; }
    }

}
