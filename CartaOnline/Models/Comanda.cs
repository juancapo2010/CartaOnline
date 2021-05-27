using System;
using System.Collections.Generic;


namespace CartaOnline.Models
{
    public class Comanda
    {
        public int ComandaId { get; set; }
        public int PrecioTotal { get; set; }
        public DateTime Fecha { get; set; }

        public int FormaEntregaId { get; set; }
        public FormaEntrega FormaEntrega { get; set; }

        public virtual ICollection<ComandaMercaderia> ComandaMercaderia { get; set; }

    }
}

