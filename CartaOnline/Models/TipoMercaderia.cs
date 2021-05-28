using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CartaOnline.Models
{
    public class TipoMercaderia
    {
        [Required]
        public int TipoMercaderiaId { get; set; }
        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        public virtual ICollection<Mercaderia> Mercederias { get; set; }



    }
}
