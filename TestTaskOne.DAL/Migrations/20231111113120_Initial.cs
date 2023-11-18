using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TestTaskOne.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nomenclatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nomenclatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Waybills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseCost = table.Column<double>(type: "float", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Waybills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Components_Nomenclatures_Id",
                        column: x => x.Id,
                        principalTable: "Nomenclatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materials_Nomenclatures_Id",
                        column: x => x.Id,
                        principalTable: "Nomenclatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NomenclaturesLinks",
                columns: table => new
                {
                    NomenclatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NomenclaturesLinks", x => new { x.ProductId, x.NomenclatureId });
                    table.ForeignKey(
                        name: "FK_NomenclaturesLinks_Nomenclatures_NomenclatureId",
                        column: x => x.NomenclatureId,
                        principalTable: "Nomenclatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NomenclaturesLinks_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WaybillItems",
                columns: table => new
                {
                    WaybillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomenclatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaybillItems", x => new { x.WaybillId, x.NomenclatureId });
                    table.ForeignKey(
                        name: "FK_WaybillItems_Nomenclatures_NomenclatureId",
                        column: x => x.NomenclatureId,
                        principalTable: "Nomenclatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WaybillItems_Waybills_WaybillId",
                        column: x => x.WaybillId,
                        principalTable: "Waybills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Nomenclatures",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("0fda203e-0a1b-491e-9e48-6ddf5d15fa52"), "Резина" },
                    { new Guid("547b7d31-5587-458c-94bf-c9c784bbd624"), "Болт" },
                    { new Guid("703d47c5-2e2f-4142-b35e-3b302a11e63c"), "Аллюминий" },
                    { new Guid("a435b233-ff88-4b59-ae57-403f2e9dea94"), "КПП" },
                    { new Guid("a9b4f7ba-cbdf-428d-98c0-42feea0fff0d"), "Стекло" },
                    { new Guid("be65e6a5-6eec-4202-af34-9bfffac3ec69"), "Плата управления" },
                    { new Guid("f6a58813-a64e-4c43-8cdb-176ee1f02047"), "Шестеренка" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("d754f72b-b788-4d0c-867f-ce3b09c36aed"), "Персональный компьютер" },
                    { new Guid("da03a140-ce82-4ed7-b999-5cb11144fffc"), "Подъемный кран" },
                    { new Guid("ffd226dc-ceb5-4138-9cc5-c886a687dc80"), "Легковой автомобиль" }
                });

            migrationBuilder.InsertData(
                table: "Waybills",
                columns: new[] { "Id", "PurchaseCost", "PurchaseDate" },
                values: new object[,]
                {
                    { new Guid("1421b940-02d3-4939-aa49-416c8b7e0b87"), 9992.0, new DateTime(2022, 7, 12, 13, 29, 17, 0, DateTimeKind.Unspecified) },
                    { new Guid("5f9332fc-4529-480c-8a5f-ea76ed37cd02"), 5888.0, new DateTime(2022, 10, 23, 12, 50, 43, 0, DateTimeKind.Unspecified) },
                    { new Guid("67c0fb2d-44b7-4cb0-9c80-13a3d8dc9b2d"), 1601.0, new DateTime(2021, 7, 4, 14, 40, 30, 0, DateTimeKind.Unspecified) },
                    { new Guid("84d41619-6286-4778-be86-f8741277688f"), 9296.0, new DateTime(2021, 11, 8, 7, 24, 21, 0, DateTimeKind.Unspecified) },
                    { new Guid("8ba6d9e5-502f-4542-9893-a184d1c9e42e"), 4625.0, new DateTime(2023, 4, 13, 6, 43, 59, 0, DateTimeKind.Unspecified) },
                    { new Guid("9b1cd368-02ba-4516-b61c-6af91952e569"), 8396.0, new DateTime(2023, 6, 15, 4, 9, 30, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Components",
                column: "Id",
                values: new object[]
                {
                    new Guid("547b7d31-5587-458c-94bf-c9c784bbd624"),
                    new Guid("a435b233-ff88-4b59-ae57-403f2e9dea94"),
                    new Guid("be65e6a5-6eec-4202-af34-9bfffac3ec69"),
                    new Guid("f6a58813-a64e-4c43-8cdb-176ee1f02047")
                });

            migrationBuilder.InsertData(
                table: "Materials",
                column: "Id",
                values: new object[]
                {
                    new Guid("0fda203e-0a1b-491e-9e48-6ddf5d15fa52"),
                    new Guid("703d47c5-2e2f-4142-b35e-3b302a11e63c"),
                    new Guid("a9b4f7ba-cbdf-428d-98c0-42feea0fff0d")
                });

            migrationBuilder.InsertData(
                table: "NomenclaturesLinks",
                columns: new[] { "NomenclatureId", "ProductId" },
                values: new object[,]
                {
                    { new Guid("547b7d31-5587-458c-94bf-c9c784bbd624"), new Guid("d754f72b-b788-4d0c-867f-ce3b09c36aed") },
                    { new Guid("703d47c5-2e2f-4142-b35e-3b302a11e63c"), new Guid("d754f72b-b788-4d0c-867f-ce3b09c36aed") },
                    { new Guid("be65e6a5-6eec-4202-af34-9bfffac3ec69"), new Guid("d754f72b-b788-4d0c-867f-ce3b09c36aed") },
                    { new Guid("0fda203e-0a1b-491e-9e48-6ddf5d15fa52"), new Guid("da03a140-ce82-4ed7-b999-5cb11144fffc") },
                    { new Guid("547b7d31-5587-458c-94bf-c9c784bbd624"), new Guid("da03a140-ce82-4ed7-b999-5cb11144fffc") },
                    { new Guid("703d47c5-2e2f-4142-b35e-3b302a11e63c"), new Guid("da03a140-ce82-4ed7-b999-5cb11144fffc") },
                    { new Guid("a435b233-ff88-4b59-ae57-403f2e9dea94"), new Guid("da03a140-ce82-4ed7-b999-5cb11144fffc") },
                    { new Guid("a9b4f7ba-cbdf-428d-98c0-42feea0fff0d"), new Guid("da03a140-ce82-4ed7-b999-5cb11144fffc") },
                    { new Guid("be65e6a5-6eec-4202-af34-9bfffac3ec69"), new Guid("da03a140-ce82-4ed7-b999-5cb11144fffc") },
                    { new Guid("f6a58813-a64e-4c43-8cdb-176ee1f02047"), new Guid("da03a140-ce82-4ed7-b999-5cb11144fffc") },
                    { new Guid("0fda203e-0a1b-491e-9e48-6ddf5d15fa52"), new Guid("ffd226dc-ceb5-4138-9cc5-c886a687dc80") },
                    { new Guid("547b7d31-5587-458c-94bf-c9c784bbd624"), new Guid("ffd226dc-ceb5-4138-9cc5-c886a687dc80") },
                    { new Guid("703d47c5-2e2f-4142-b35e-3b302a11e63c"), new Guid("ffd226dc-ceb5-4138-9cc5-c886a687dc80") },
                    { new Guid("a435b233-ff88-4b59-ae57-403f2e9dea94"), new Guid("ffd226dc-ceb5-4138-9cc5-c886a687dc80") },
                    { new Guid("f6a58813-a64e-4c43-8cdb-176ee1f02047"), new Guid("ffd226dc-ceb5-4138-9cc5-c886a687dc80") }
                });

            migrationBuilder.InsertData(
                table: "WaybillItems",
                columns: new[] { "NomenclatureId", "WaybillId" },
                values: new object[,]
                {
                    { new Guid("0fda203e-0a1b-491e-9e48-6ddf5d15fa52"), new Guid("1421b940-02d3-4939-aa49-416c8b7e0b87") },
                    { new Guid("547b7d31-5587-458c-94bf-c9c784bbd624"), new Guid("1421b940-02d3-4939-aa49-416c8b7e0b87") },
                    { new Guid("703d47c5-2e2f-4142-b35e-3b302a11e63c"), new Guid("1421b940-02d3-4939-aa49-416c8b7e0b87") },
                    { new Guid("a435b233-ff88-4b59-ae57-403f2e9dea94"), new Guid("1421b940-02d3-4939-aa49-416c8b7e0b87") },
                    { new Guid("a9b4f7ba-cbdf-428d-98c0-42feea0fff0d"), new Guid("1421b940-02d3-4939-aa49-416c8b7e0b87") },
                    { new Guid("be65e6a5-6eec-4202-af34-9bfffac3ec69"), new Guid("1421b940-02d3-4939-aa49-416c8b7e0b87") },
                    { new Guid("f6a58813-a64e-4c43-8cdb-176ee1f02047"), new Guid("1421b940-02d3-4939-aa49-416c8b7e0b87") },
                    { new Guid("0fda203e-0a1b-491e-9e48-6ddf5d15fa52"), new Guid("5f9332fc-4529-480c-8a5f-ea76ed37cd02") },
                    { new Guid("547b7d31-5587-458c-94bf-c9c784bbd624"), new Guid("5f9332fc-4529-480c-8a5f-ea76ed37cd02") },
                    { new Guid("703d47c5-2e2f-4142-b35e-3b302a11e63c"), new Guid("5f9332fc-4529-480c-8a5f-ea76ed37cd02") },
                    { new Guid("a435b233-ff88-4b59-ae57-403f2e9dea94"), new Guid("5f9332fc-4529-480c-8a5f-ea76ed37cd02") },
                    { new Guid("a9b4f7ba-cbdf-428d-98c0-42feea0fff0d"), new Guid("5f9332fc-4529-480c-8a5f-ea76ed37cd02") },
                    { new Guid("be65e6a5-6eec-4202-af34-9bfffac3ec69"), new Guid("5f9332fc-4529-480c-8a5f-ea76ed37cd02") },
                    { new Guid("f6a58813-a64e-4c43-8cdb-176ee1f02047"), new Guid("5f9332fc-4529-480c-8a5f-ea76ed37cd02") },
                    { new Guid("0fda203e-0a1b-491e-9e48-6ddf5d15fa52"), new Guid("67c0fb2d-44b7-4cb0-9c80-13a3d8dc9b2d") },
                    { new Guid("547b7d31-5587-458c-94bf-c9c784bbd624"), new Guid("67c0fb2d-44b7-4cb0-9c80-13a3d8dc9b2d") },
                    { new Guid("703d47c5-2e2f-4142-b35e-3b302a11e63c"), new Guid("67c0fb2d-44b7-4cb0-9c80-13a3d8dc9b2d") },
                    { new Guid("a435b233-ff88-4b59-ae57-403f2e9dea94"), new Guid("67c0fb2d-44b7-4cb0-9c80-13a3d8dc9b2d") },
                    { new Guid("a9b4f7ba-cbdf-428d-98c0-42feea0fff0d"), new Guid("67c0fb2d-44b7-4cb0-9c80-13a3d8dc9b2d") },
                    { new Guid("be65e6a5-6eec-4202-af34-9bfffac3ec69"), new Guid("67c0fb2d-44b7-4cb0-9c80-13a3d8dc9b2d") },
                    { new Guid("f6a58813-a64e-4c43-8cdb-176ee1f02047"), new Guid("67c0fb2d-44b7-4cb0-9c80-13a3d8dc9b2d") },
                    { new Guid("0fda203e-0a1b-491e-9e48-6ddf5d15fa52"), new Guid("84d41619-6286-4778-be86-f8741277688f") },
                    { new Guid("547b7d31-5587-458c-94bf-c9c784bbd624"), new Guid("84d41619-6286-4778-be86-f8741277688f") },
                    { new Guid("703d47c5-2e2f-4142-b35e-3b302a11e63c"), new Guid("84d41619-6286-4778-be86-f8741277688f") },
                    { new Guid("a435b233-ff88-4b59-ae57-403f2e9dea94"), new Guid("84d41619-6286-4778-be86-f8741277688f") },
                    { new Guid("a9b4f7ba-cbdf-428d-98c0-42feea0fff0d"), new Guid("84d41619-6286-4778-be86-f8741277688f") },
                    { new Guid("be65e6a5-6eec-4202-af34-9bfffac3ec69"), new Guid("84d41619-6286-4778-be86-f8741277688f") },
                    { new Guid("f6a58813-a64e-4c43-8cdb-176ee1f02047"), new Guid("84d41619-6286-4778-be86-f8741277688f") },
                    { new Guid("0fda203e-0a1b-491e-9e48-6ddf5d15fa52"), new Guid("8ba6d9e5-502f-4542-9893-a184d1c9e42e") },
                    { new Guid("547b7d31-5587-458c-94bf-c9c784bbd624"), new Guid("8ba6d9e5-502f-4542-9893-a184d1c9e42e") },
                    { new Guid("703d47c5-2e2f-4142-b35e-3b302a11e63c"), new Guid("8ba6d9e5-502f-4542-9893-a184d1c9e42e") },
                    { new Guid("a435b233-ff88-4b59-ae57-403f2e9dea94"), new Guid("8ba6d9e5-502f-4542-9893-a184d1c9e42e") },
                    { new Guid("a9b4f7ba-cbdf-428d-98c0-42feea0fff0d"), new Guid("8ba6d9e5-502f-4542-9893-a184d1c9e42e") },
                    { new Guid("be65e6a5-6eec-4202-af34-9bfffac3ec69"), new Guid("8ba6d9e5-502f-4542-9893-a184d1c9e42e") },
                    { new Guid("f6a58813-a64e-4c43-8cdb-176ee1f02047"), new Guid("8ba6d9e5-502f-4542-9893-a184d1c9e42e") },
                    { new Guid("0fda203e-0a1b-491e-9e48-6ddf5d15fa52"), new Guid("9b1cd368-02ba-4516-b61c-6af91952e569") },
                    { new Guid("547b7d31-5587-458c-94bf-c9c784bbd624"), new Guid("9b1cd368-02ba-4516-b61c-6af91952e569") },
                    { new Guid("703d47c5-2e2f-4142-b35e-3b302a11e63c"), new Guid("9b1cd368-02ba-4516-b61c-6af91952e569") },
                    { new Guid("a435b233-ff88-4b59-ae57-403f2e9dea94"), new Guid("9b1cd368-02ba-4516-b61c-6af91952e569") },
                    { new Guid("a9b4f7ba-cbdf-428d-98c0-42feea0fff0d"), new Guid("9b1cd368-02ba-4516-b61c-6af91952e569") },
                    { new Guid("be65e6a5-6eec-4202-af34-9bfffac3ec69"), new Guid("9b1cd368-02ba-4516-b61c-6af91952e569") },
                    { new Guid("f6a58813-a64e-4c43-8cdb-176ee1f02047"), new Guid("9b1cd368-02ba-4516-b61c-6af91952e569") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_NomenclaturesLinks_NomenclatureId",
                table: "NomenclaturesLinks",
                column: "NomenclatureId");

            migrationBuilder.CreateIndex(
                name: "IX_WaybillItems_NomenclatureId",
                table: "WaybillItems",
                column: "NomenclatureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "NomenclaturesLinks");

            migrationBuilder.DropTable(
                name: "WaybillItems");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Nomenclatures");

            migrationBuilder.DropTable(
                name: "Waybills");
        }
    }
}
