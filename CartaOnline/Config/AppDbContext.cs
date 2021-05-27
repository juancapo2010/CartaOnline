using Microsoft.EntityFrameworkCore;
using CartaOnline.Models;

namespace CartaOnline.Config
{
    public class AppDbContext : DbContext
    {
        public DbSet<Mercaderia> Mercaderias { get; set; }
        public DbSet<Comanda> Comandas { get; set; }
        public DbSet<FormaEntrega> FormasEntregas { get; set; }
        public DbSet<TipoMercaderia> TipoMercaderia { get; set; }
        public DbSet<ComandaMercaderia> ComandaMercaderias { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var t1 = new TipoMercaderia() {TipoMercaderiaId = 1,Descripcion = "Entrada" };
            var t2 = new TipoMercaderia() { TipoMercaderiaId = 2, Descripcion = "Minutas" };
            var t3 = new TipoMercaderia() { TipoMercaderiaId = 3, Descripcion = "Pastas" };
            var t4 = new TipoMercaderia() { TipoMercaderiaId = 4, Descripcion = "Parrilla" };
            var t5 = new TipoMercaderia() { TipoMercaderiaId = 5, Descripcion = "Pizzas" };
            var t6 = new TipoMercaderia() { TipoMercaderiaId = 6, Descripcion = "Sandwich" };
            var t7 = new TipoMercaderia() { TipoMercaderiaId = 7, Descripcion = "Ensaladas" };
            var t8 = new TipoMercaderia() { TipoMercaderiaId = 8, Descripcion = "Bebidas" };
            var t9 = new TipoMercaderia() { TipoMercaderiaId = 9, Descripcion = "Cerveza Artesanal" };
            var t10 = new TipoMercaderia() { TipoMercaderiaId = 10, Descripcion = "Postres" };
            modelBuilder.Entity<TipoMercaderia>().HasData(new TipoMercaderia[] { t1, t2, t3, t4, t5, t6, t7, t8, t9, t10 });
            var f1 = new FormaEntrega() { FormaEntregaId = 1, Descripcion = "Salon" };
            var f2 = new FormaEntrega() { FormaEntregaId = 2, Descripcion = "Delivery" };
            var f3 = new FormaEntrega() { FormaEntregaId = 3, Descripcion = "Pedidos Ya" };
            modelBuilder.Entity<FormaEntrega>().HasData(new FormaEntrega[] { f1, f2, f3 });
            modelBuilder.Entity<ComandaMercaderia>().HasKey(x => new { x.ComandaId, x.MercaderiaId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
