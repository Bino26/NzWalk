using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NzWalkAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("01618b85-7a00-4858-a941-43b5c87ee013"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("0ab70e6e-6d60-4d06-ba10-66b192099c06"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("1bf705a8-ac88-421d-98f7-364c906df014"));

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSizeInBytes = table.Column<long>(type: "bigint", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0f6f2ac1-a7ce-4607-8442-043fb2fc47e3"), "Medium" },
                    { new Guid("3fc0d0c4-fd86-4dc5-88e8-7d116e73e7af"), "Easy" },
                    { new Guid("f1f692ea-627f-49d2-b5f9-37fe8b03ca32"), "Hard" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("0f6f2ac1-a7ce-4607-8442-043fb2fc47e3"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("3fc0d0c4-fd86-4dc5-88e8-7d116e73e7af"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("f1f692ea-627f-49d2-b5f9-37fe8b03ca32"));

            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("01618b85-7a00-4858-a941-43b5c87ee013"), "Hard" },
                    { new Guid("0ab70e6e-6d60-4d06-ba10-66b192099c06"), "Easy" },
                    { new Guid("1bf705a8-ac88-421d-98f7-364c906df014"), "Medium" }
                });
        }
    }
}
