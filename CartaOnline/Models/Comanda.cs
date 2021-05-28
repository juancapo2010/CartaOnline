using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CartaOnline.Models
{
    public class Comanda
    {
        [Required]
        public int ComandaId { get; set; }
        [Required]
        public int PrecioTotal { get; set; }
        [Required]
        public DateTime Fecha { get; set; }

        public int FormaEntregaId { get; set; }
        public FormaEntrega FormaEntrega { get; set; }

        public virtual ICollection<ComandaMercaderia> ComandaMercaderia { get; set; }

    }
}

