using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tourist.Domain.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class InitialDomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TourRoutes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourRoutes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attractions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TourRouteId = table.Column<int>(type: "int", nullable: false),
                    TicketPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attractions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attractions_TourRoutes_TourRouteId",
                        column: x => x.TourRouteId,
                        principalTable: "TourRoutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TourRoutes",
                columns: new[] { "Id", "Description", "Image", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "Путешествие по средневековым замкам страны", null, "Замки Беларуси", "zamki" },
                    { 2, "Столица Беларуси и её достопримечательности", null, "Минск и окрестности", "minsk" },
                    { 3, "Древнейший лес Европы и национальный парк", null, "Беловежская пуща", "belovezhskaya-pushcha" },
                    { 4, "Мемориальный комплекс и крепость-герой", null, "Брестская крепость", "brest" },
                    { 5, "Уникальный край болот, лесов и древних городов", null, "Полесье", "polesye" }
                });

            migrationBuilder.InsertData(
                table: "Attractions",
                columns: new[] { "Id", "Description", "Image", "Name", "TicketPrice", "TourRouteId" },
                values: new object[,]
                {
                    { 1, "Замково-парковый комплекс XVI века, объект ЮНЕСКО", null, "Мирский замок", 15m, 1 },
                    { 2, "Дворцово-парковый комплекс рода Радзивиллов", null, "Несвижский замок", 15m, 1 },
                    { 3, "Средневековый замок XIV века", null, "Лидский замок", 7m, 1 },
                    { 4, "Неоготический дворец XIX века", null, "Коссовский замок", 5m, 1 },
                    { 5, "Исторический центр Минска с Ратушей", null, "Верхний город", 0m, 2 },
                    { 6, "Исторический район Минска", null, "Троицкое предместье", 0m, 2 },
                    { 7, "Мемориальный комплекс воинам-интернационалистам", null, "Остров слёз", 0m, 2 },
                    { 8, "Древнейший лес Европы, объект ЮНЕСКО", null, "Беловежская пуща", 25m, 3 },
                    { 9, "Интерактивный музей о флоре и фауне", null, "Музей природы", 10m, 3 },
                    { 10, "Резиденция белорусского Деда Мороза", null, "Поместье Деда Мороза", 12m, 3 },
                    { 11, "Мемориальный комплекс", null, "Брестская крепость-герой", 0m, 4 },
                    { 12, "Музей с экспонатами об обороне 1941 года", null, "Музей обороны Брестской крепости", 8m, 4 },
                    { 13, "Один из старейших городов Беларуси", null, "Пинск", 5m, 5 },
                    { 14, "Древнейший город Беларуси X века", null, "Туров", 3m, 5 },
                    { 15, "Уникальный заповедник с болотами Полесья", null, "Национальный парк Припятский", 10m, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attractions_TourRouteId",
                table: "Attractions",
                column: "TourRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_TourRoutes_NormalizedName",
                table: "TourRoutes",
                column: "NormalizedName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attractions");

            migrationBuilder.DropTable(
                name: "TourRoutes");
        }
    }
}
