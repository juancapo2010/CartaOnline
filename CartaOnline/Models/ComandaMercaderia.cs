
namespace CartaOnline.Models
{
    public class ComandaMercaderia
    {
     
        public int ComandaId { get; set; }
        public Comanda Comanda { get; set; }

        public int MercaderiaId { get; set; }
        public Mercaderia Mercaderia { get; set; }

    }
}
