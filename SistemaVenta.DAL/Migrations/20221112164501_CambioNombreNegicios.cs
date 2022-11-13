using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVenta.DAL.Migrations
{
    public partial class CambioNombreNegicios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UrlNegocio",
                table: "Negocios",
                newName: "UrlLogo");

            migrationBuilder.RenameColumn(
                name: "NombreDocumento",
                table: "Negocios",
                newName: "NumeroDocumento");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UrlLogo",
                table: "Negocios",
                newName: "UrlNegocio");

            migrationBuilder.RenameColumn(
                name: "NumeroDocumento",
                table: "Negocios",
                newName: "NombreDocumento");
        }
    }
}
