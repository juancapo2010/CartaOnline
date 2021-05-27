using System.Collections.Generic;

namespace CartaOnline.Models
{
    public class FormaEntrega
    {
        public int FormaEntregaId { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Comanda> Comanda { get; set; }
    }
}
