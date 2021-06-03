using Microsoft.EntityFrameworkCore;
using CartaOnline.Models;

namespace CartaOnline.Config
{
    public class AppDbContext : DbContext
    {
        public DbSet<Mercaderia> Mercaderias { get; set; }
        public DbSet<Comanda> Comanda { get; set; }
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
            var m1 = new Mercaderia() { MercaderiaId = 1, Nombre = "Milanesa de Carne", Precio = 350, Ingredientes = "Carne vacuna y pan", Preparacion = "frita", Imagen = "image/milanesa.jpg", TipoMercaderiaId = 6 };
            var m2 = new Mercaderia() { MercaderiaId = 2, Nombre = "Coca Cola", Precio = 100, Ingredientes = "no se sabe", Preparacion = "fria", Imagen = "image/coca.jpg", TipoMercaderiaId = 8 };
            var m3 = new Mercaderia() { MercaderiaId = 3, Nombre = "Helado de Chocolate", Precio = 150, Ingredientes = "leche", Preparacion = "fria", Imagen = "image/helado.jpg", TipoMercaderiaId = 10 };
            var m4 = new Mercaderia() { MercaderiaId = 4, Nombre = "Milanesa de Pollo", Precio = 350, Ingredientes = "Carne pollo y pan", Preparacion = "frita", Imagen = "image/milanesa.jpg", TipoMercaderiaId = 6 };
            var m5 = new Mercaderia() { MercaderiaId = 5, Nombre = "Tallarines", Precio = 300, Ingredientes = "harina y sal", Preparacion = "hervida", Imagen = "image/pasta.jpg", TipoMercaderiaId = 3 };
            var m6 = new Mercaderia() { MercaderiaId = 6, Nombre = "Milanesa con fritas", Precio = 350, Ingredientes = "Carne vacuna al plato", Preparacion = "frita", Imagen = "image/milanesa.jpg", TipoMercaderiaId = 2 };
            var m7 = new Mercaderia() { MercaderiaId = 7, Nombre = "Milanesa de pollo con fritas", Precio = 350, Ingredientes = "Carne vacuna y pan", Preparacion = "frita", Imagen = "image/milanesa.jpg", TipoMercaderiaId = 2 };
            modelBuilder.Entity<Mercaderia>().HasData(new Mercaderia[] { m1,m2,m3,m4,m5,m6,m7});
            base.OnModelCreating(modelBuilder);
        }
    }
}
