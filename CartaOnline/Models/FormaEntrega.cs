using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CartaOnline.Models
{
    public class FormaEntrega
    {
        [Required]
        public int FormaEntregaId { get; set; }
        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        public virtual ICollection<Comanda> Comanda { get; set; }
    }
}
