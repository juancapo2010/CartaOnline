using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CartaOnline.Migrations
{
    public partial class iniciar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormasEntregas",
                columns: table => new
                {
                    FormaEntregaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormasEntregas", x => x.FormaEntregaId);
                });

            migrationBuilder.CreateTable(
                name: "TipoMercaderia",
                columns: table => new
                {
                    TipoMercaderiaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMercaderia", x => x.TipoMercaderiaId);
                });

            migrationBuilder.CreateTable(
                name: "Comanda",
                columns: table => new
                {
                    ComandaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrecioTotal = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FormaEntregaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comanda", x => x.ComandaId);
                    table.ForeignKey(
                        name: "FK_Comanda_FormasEntregas_FormaEntregaId",
                        column: x => x.FormaEntregaId,
                        principalTable: "FormasEntregas",
                        principalColumn: "FormaEntregaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mercaderias",
                columns: table => new
                {
                    MercaderiaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Precio = table.Column<int>(type: "int", nullable: false),
                    Ingredientes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Preparacion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoMercaderiaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mercaderias", x => x.MercaderiaId);
                    table.ForeignKey(
                        name: "FK_Mercaderias_TipoMercaderia_TipoMercaderiaId",
                        column: x => x.TipoMercaderiaId,
                        principalTable: "TipoMercaderia",
                        principalColumn: "TipoMercaderiaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComandaMercaderias",
                columns: table => new
                {
                    ComandaId = table.Column<int>(type: "int", nullable: false),
                    MercaderiaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComandaMercaderias", x => new { x.ComandaId, x.MercaderiaId });
                    table.ForeignKey(
                        name: "FK_ComandaMercaderias_Comanda_ComandaId",
                        column: x => x.ComandaId,
                        principalTable: "Comanda",
                        principalColumn: "ComandaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComandaMercaderias_Mercaderias_MercaderiaId",
                        column: x => x.MercaderiaId,
                        principalTable: "Mercaderias",
                        principalColumn: "MercaderiaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FormasEntregas",
                columns: new[] { "FormaEntregaId", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Salon" },
                    { 2, "Delivery" },
                    { 3, "Pedidos Ya" }
                });

            migrationBuilder.InsertData(
                table: "TipoMercaderia",
                columns: new[] { "TipoMercaderiaId", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Entrada" },
                    { 2, "Minutas" },
                    { 3, "Pastas" },
                    { 4, "Parrilla" },
                    { 5, "Pizzas" },
                    { 6, "Sandwich" },
                    { 7, "Ensaladas" },
                    { 8, "Bebidas" },
                    { 9, "Cerveza Artesanal" },
                    { 10, "Postres" }
                });

            migrationBuilder.InsertData(
                table: "Mercaderias",
                columns: new[] { "MercaderiaId", "Imagen", "Ingredientes", "Nombre", "Precio", "Preparacion", "TipoMercaderiaId" },
                values: new object[,]
                {
                    { 6, "image/milanesa.jpg", "Carne vacuna al plato", "Milanesa con fritas", 350, "frita", 2 },
                    { 7, "image/milanesa.jpg", "Carne vacuna y pan", "Milanesa de pollo con fritas", 350, "frita", 2 },
                    { 5, "image/pasta.jpg", "harina y sal", "Tallarines", 300, "hervida", 3 },
                    { 1, "image/milanesa.jpg", "Carne vacuna y pan", "Milanesa de Carne", 350, "frita", 6 },
                    { 4, "image/milanesa.jpg", "Carne pollo y pan", "Milanesa de Pollo", 350, "frita", 6 },
                    { 2, "image/coca.jpg", "no se sabe", "Coca Cola", 100, "fria", 8 },
                    { 3, "image/helado.jpg", "leche", "Helado de Chocolate", 150, "fria", 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comanda_FormaEntregaId",
                table: "Comanda",
                column: "FormaEntregaId");

            migrationBuilder.CreateIndex(
                name: "IX_ComandaMercaderias_MercaderiaId",
                table: "ComandaMercaderias",
                column: "MercaderiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mercaderias_TipoMercaderiaId",
                table: "Mercaderias",
                column: "TipoMercaderiaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComandaMercaderias");

            migrationBuilder.DropTable(
                name: "Comanda");

            migrationBuilder.DropTable(
                name: "Mercaderias");

            migrationBuilder.DropTable(
                name: "FormasEntregas");

            migrationBuilder.DropTable(
                name: "TipoMercaderia");
        }
    }
}
