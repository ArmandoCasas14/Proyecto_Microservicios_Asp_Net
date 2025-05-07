using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Microservicios_Asp_Net.Migrations
{
    /// <inheritdoc />
    public partial class añadi_rolId_llave_foranea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RolId",
                table: "Logins",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Logins",
                keyColumn: "Id",
                keyValue: 1,
                column: "RolId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Logins",
                keyColumn: "Id",
                keyValue: 2,
                column: "RolId",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Logins_RolId",
                table: "Logins",
                column: "RolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logins_Roles_RolId",
                table: "Logins",
                column: "RolId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logins_Roles_RolId",
                table: "Logins");

            migrationBuilder.DropIndex(
                name: "IX_Logins_RolId",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "RolId",
                table: "Logins");
        }
    }
}
