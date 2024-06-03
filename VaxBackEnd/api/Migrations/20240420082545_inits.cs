using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class inits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ac63942f-f239-460f-a282-a81e6ecc0858");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb226bcb-9c2e-4fae-b6b5-6459e6ec20c5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc6efac4-4cf9-4342-9517-05a4f423e64f");

            migrationBuilder.AddColumn<int>(
                name: "VaccinationCenterId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "98dfc90e-3c4a-4474-b408-7aef3332b9da", null, "User", "USER" },
                    { "d26c59b2-8ab6-4ed8-9c84-80757ad1742f", null, "Admin", "ADMIN" },
                    { "fca4b91d-c4e5-4b5f-8dd6-34d889ad0cbf", null, "VaccinationCenter", "VaccinationCenter" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98dfc90e-3c4a-4474-b408-7aef3332b9da");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d26c59b2-8ab6-4ed8-9c84-80757ad1742f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fca4b91d-c4e5-4b5f-8dd6-34d889ad0cbf");

            migrationBuilder.DropColumn(
                name: "VaccinationCenterId",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ac63942f-f239-460f-a282-a81e6ecc0858", null, "User", "USER" },
                    { "bb226bcb-9c2e-4fae-b6b5-6459e6ec20c5", null, "Admin", "ADMIN" },
                    { "bc6efac4-4cf9-4342-9517-05a4f423e64f", null, "VaccinationCenter", "VaccinationCenter" }
                });
        }
    }
}
