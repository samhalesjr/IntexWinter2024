using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntexWinter2024.Migrations
{
    /// <inheritdoc />
    public partial class AddedProdRecTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductBasedProductRecommendations",
                columns: table => new
                {
                    ProductBasedProductRecommendationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    RecommendationOneProductId = table.Column<int>(type: "int", nullable: false),
                    RecommendationTwoProductId = table.Column<int>(type: "int", nullable: false),
                    RecommendationThreeProductId = table.Column<int>(type: "int", nullable: false),
                    RecommendationFourProductId = table.Column<int>(type: "int", nullable: false),
                    RecommendationFiveProductId = table.Column<int>(type: "int", nullable: false),
                    RecommendationSixProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBasedProductRecommendations", x => x.ProductBasedProductRecommendationId);
                    table.ForeignKey(
                        name: "FK_ProductBasedProductRecommendations_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductBasedProductRecommendations_Products_RecommendationFiveProductId",
                        column: x => x.RecommendationFiveProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductBasedProductRecommendations_Products_RecommendationFourProductId",
                        column: x => x.RecommendationFourProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductBasedProductRecommendations_Products_RecommendationOneProductId",
                        column: x => x.RecommendationOneProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductBasedProductRecommendations_Products_RecommendationSixProductId",
                        column: x => x.RecommendationSixProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductBasedProductRecommendations_Products_RecommendationThreeProductId",
                        column: x => x.RecommendationThreeProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductBasedProductRecommendations_Products_RecommendationTwoProductId",
                        column: x => x.RecommendationTwoProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductBasedProductRecommendations_ProductId",
                table: "ProductBasedProductRecommendations",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBasedProductRecommendations_RecommendationFiveProductId",
                table: "ProductBasedProductRecommendations",
                column: "RecommendationFiveProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBasedProductRecommendations_RecommendationFourProductId",
                table: "ProductBasedProductRecommendations",
                column: "RecommendationFourProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBasedProductRecommendations_RecommendationOneProductId",
                table: "ProductBasedProductRecommendations",
                column: "RecommendationOneProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBasedProductRecommendations_RecommendationSixProductId",
                table: "ProductBasedProductRecommendations",
                column: "RecommendationSixProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBasedProductRecommendations_RecommendationThreeProductId",
                table: "ProductBasedProductRecommendations",
                column: "RecommendationThreeProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBasedProductRecommendations_RecommendationTwoProductId",
                table: "ProductBasedProductRecommendations",
                column: "RecommendationTwoProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductBasedProductRecommendations");
        }
    }
}
