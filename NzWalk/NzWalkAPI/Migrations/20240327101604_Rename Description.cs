using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NzWalkAPI.Migrations
{
    /// <inheritdoc />
    public partial class RenameDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("07a2269a-c340-4613-aa94-b6b2345a52d1"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("1dfd7167-06ea-432e-b9a6-2fd3cd04942a"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("4077f7aa-7f29-4192-a859-da01a7e8d4cb"));

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Regions",
                newName: "Description");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Regions",
                newName: "Code");

            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("07a2269a-c340-4613-aa94-b6b2345a52d1"), "Hard" },
                    { new Guid("1dfd7167-06ea-432e-b9a6-2fd3cd04942a"), "Medium" },
                    { new Guid("4077f7aa-7f29-4192-a859-da01a7e8d4cb"), "Easy" }
                });
        }
    }
}
