using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Proyecto_Microservicios_Asp_Net.Migrations
{
    /// <inheritdoc />
    public partial class agregue_los_modelos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logins_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Logins_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "Id", "descripcion" },
                values: new object[,]
                {
                    { 1, "Activo" },
                    { 2, "Inactivo" },
                    { 3, "Bloqueado" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "descripcion" },
                values: new object[,]
                {
                    { 1, "Administrador" },
                    { 2, "Cliente" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "apellido", "direccion", "email", "nombre", "telefono" },
                values: new object[,]
                {
                    { 1, "Isaza", "Calle 123", "admin@example.com", "Jorge", "123456789" },
                    { 2, "Pérez", "Avenida 456", "juan@example.com", "Juan", "987654321" }
                });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "Id", "EstadoId", "Nickname", "Password", "UsuarioId" },
                values: new object[,]
                {
                    { 1, 1, "Admin123", "Admin@123", 1 },
                    { 2, 1, "User456", "User@456", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logins_EstadoId",
                table: "Logins",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_UsuarioId",
                table: "Logins",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
