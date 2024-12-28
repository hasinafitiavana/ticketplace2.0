using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketPlace2._0.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NombreDePlaces",
                table: "PlaceVendues",
                newName: "NumeroDePlace");

            migrationBuilder.AddColumn<string>(
                name: "TypeReservation",
                table: "PlaceVendues",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeReservation",
                table: "PlaceVendues");

            migrationBuilder.RenameColumn(
                name: "NumeroDePlace",
                table: "PlaceVendues",
                newName: "NombreDePlaces");
        }
    }
}
