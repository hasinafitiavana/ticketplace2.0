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
            migrationBuilder.RenameColumn(
                name: "Emplacement",
                table: "EvenementTypePlaces",
                newName: "Emplacements");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Emplacements",
                table: "EvenementTypePlaces",
                newName: "Emplacement");
        }
    }
}
