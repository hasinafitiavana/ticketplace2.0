using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TicketPlace2._0.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Espaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nom = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Adresse = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Ville = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CodePostal = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Largeur = table.Column<int>(type: "integer", nullable: false),
                    Longueur = table.Column<int>(type: "integer", nullable: false),
                    Capacite = table.Column<int>(type: "integer", nullable: false),
                    OnCreate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OnUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Espaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypePlaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypePlaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nom = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Prenom = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    MotDePasse = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    EstAdmin = table.Column<bool>(type: "boolean", nullable: false),
                    DateDeNaissance = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OnCreate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OnUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Evenements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EspaceId = table.Column<int>(type: "integer", nullable: false),
                    Nom = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Heure = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Lieu = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    OnCreate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OnUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evenements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evenements_Espaces_EspaceId",
                        column: x => x.EspaceId,
                        principalTable: "Espaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvenementTypePlaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EvenementId = table.Column<int>(type: "integer", nullable: false),
                    TypePlaceId = table.Column<int>(type: "integer", nullable: false),
                    NombreDePlaces = table.Column<int>(type: "integer", nullable: false),
                    Emplacement = table.Column<int>(type: "integer", nullable: false),
                    Prix = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    OnCreate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OnUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvenementTypePlaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvenementTypePlaces_Evenements_EvenementId",
                        column: x => x.EvenementId,
                        principalTable: "Evenements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvenementTypePlaces_TypePlaces_TypePlaceId",
                        column: x => x.TypePlaceId,
                        principalTable: "TypePlaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaceVendues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EvenementId = table.Column<int>(type: "integer", nullable: false),
                    TypePlaceId = table.Column<int>(type: "integer", nullable: false),
                    UtilisateurId = table.Column<int>(type: "integer", nullable: false),
                    NumeroDePlace = table.Column<int>(type: "integer", nullable: false),
                    TypeReservation = table.Column<string>(type: "text", nullable: false),
                    Prix = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    OnCreate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OnUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceVendues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaceVendues_Evenements_EvenementId",
                        column: x => x.EvenementId,
                        principalTable: "Evenements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaceVendues_TypePlaces_TypePlaceId",
                        column: x => x.TypePlaceId,
                        principalTable: "TypePlaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaceVendues_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evenements_EspaceId",
                table: "Evenements",
                column: "EspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_EvenementTypePlaces_EvenementId",
                table: "EvenementTypePlaces",
                column: "EvenementId");

            migrationBuilder.CreateIndex(
                name: "IX_EvenementTypePlaces_TypePlaceId",
                table: "EvenementTypePlaces",
                column: "TypePlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceVendues_EvenementId",
                table: "PlaceVendues",
                column: "EvenementId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceVendues_TypePlaceId",
                table: "PlaceVendues",
                column: "TypePlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceVendues_UtilisateurId",
                table: "PlaceVendues",
                column: "UtilisateurId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvenementTypePlaces");

            migrationBuilder.DropTable(
                name: "PlaceVendues");

            migrationBuilder.DropTable(
                name: "Evenements");

            migrationBuilder.DropTable(
                name: "TypePlaces");

            migrationBuilder.DropTable(
                name: "Utilisateurs");

            migrationBuilder.DropTable(
                name: "Espaces");
        }
    }
}
