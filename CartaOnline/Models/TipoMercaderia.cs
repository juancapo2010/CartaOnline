using System.Collections.Generic;


namespace CartaOnline.Models
{
    public class TipoMercaderia
    {
        public int TipoMercaderiaId { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Mercaderia> Mercederias { get; set; }



    }
}
