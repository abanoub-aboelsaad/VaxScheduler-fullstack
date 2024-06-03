using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class initssssssssssss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "ManagerId",
                table: "VaccinationCenters",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1ccb1271-cf2f-4031-a2db-e8911fe5488b", null, "VaccinationCenter", "VaccinationCenter" },
                    { "a5a1a07b-990e-4ac8-91c2-73a244acae3d", null, "Admin", "ADMIN" },
                    { "c1449e92-4144-44af-921f-5ec8a1639662", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationCenters_ManagerId",
                table: "VaccinationCenters",
                column: "ManagerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VaccinationCenters_AspNetUsers_ManagerId",
                table: "VaccinationCenters",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
                keyValue: "1ccb1271-cf2f-4031-a2db-e8911fe5488b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a5a1a07b-990e-4ac8-91c2-73a244acae3d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1449e92-4144-44af-921f-5ec8a1639662");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "ManagerId",
                table: "VaccinationCenters",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
    }
}
