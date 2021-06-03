
using System.ComponentModel.DataAnnotations;

namespace CartaOnline.Models
{
    public class ComandaMercaderia
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int ComandaId { get; set; }
        public Comanda Comanda { get; set; }
        [Required]
        public int MercaderiaId { get; set; }
        public Mercaderia Mercaderia { get; set; }

    }
}
