using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CartaOnline.Models
{
    public class Mercaderia
    {

        [Required]
        public int MercaderiaId { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        public int Precio { get; set; }
        [Required]
        [StringLength(250)]
        public string Ingredientes { get; set; }
        [Required]
        [StringLength(250)]
        public string Preparacion { get; set; }
        public string Imagen { get; set; }

        public int TipoMercaderiaId { get; set; }
        public TipoMercaderia TipoMercaderia { get; set; }
        public virtual ICollection<ComandaMercaderia> ComandaMercaderia { get; set; }

    }
}
