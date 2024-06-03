using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class initss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VaccinationCenters_AspNetUsers_AppUserId",
                table: "VaccinationCenters");

            migrationBuilder.DropIndex(
                name: "IX_VaccinationCenters_AppUserId",
                table: "VaccinationCenters");

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

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "VaccinationCenters",
                newName: "ManagerId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1b17c48b-8414-441e-8507-8e5ac0647681", null, "VaccinationCenter", "VaccinationCenter" },
                    { "3a060a94-095a-4e83-87a6-e65252be8d3d", null, "Admin", "ADMIN" },
                    { "96bddb58-8aae-4b4f-ac4e-8d7800a02237", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationCenters_ManagerId",
                table: "VaccinationCenters",
                column: "ManagerId",
                unique: true,
                filter: "[ManagerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccinationCenters_AspNetUsers_ManagerId",
                table: "VaccinationCenters",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VaccinationCenters_AspNetUsers_ManagerId",
                table: "VaccinationCenters");

            migrationBuilder.DropIndex(
                name: "IX_VaccinationCenters_ManagerId",
                table: "VaccinationCenters");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1b17c48b-8414-441e-8507-8e5ac0647681");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3a060a94-095a-4e83-87a6-e65252be8d3d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "96bddb58-8aae-4b4f-ac4e-8d7800a02237");

            migrationBuilder.RenameColumn(
                name: "ManagerId",
                table: "VaccinationCenters",
                newName: "AppUserId");

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

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationCenters_AppUserId",
                table: "VaccinationCenters",
                column: "AppUserId",
                unique: true,
                filter: "[AppUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccinationCenters_AspNetUsers_AppUserId",
                table: "VaccinationCenters",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
