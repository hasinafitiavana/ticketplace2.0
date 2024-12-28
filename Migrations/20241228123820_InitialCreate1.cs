using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketPlace2._0.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Largeur",
                table: "Espaces",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Longueur",
                table: "Espaces",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Largeur",
                table: "Espaces");

            migrationBuilder.DropColumn(
                name: "Longueur",
                table: "Espaces");
        }
    }
}
