﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ticketplace.Data;

#nullable disable

namespace TicketPlace2._0.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TicketPlace2._0.Models.EspaceModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("Capacite")
                        .HasColumnType("integer");

                    b.Property<string>("CodePostal")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("Largeur")
                        .HasColumnType("integer");

                    b.Property<int>("Longueur")
                        .HasColumnType("integer");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("OnCreate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("OnUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Ville")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("Espaces");
                });

            modelBuilder.Entity("TicketPlace2._0.Models.EvenementModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("EspaceId")
                        .HasColumnType("integer");

                    b.Property<TimeSpan>("Heure")
                        .HasColumnType("interval");

                    b.Property<string>("ImagePath")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Lieu")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("OnCreate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("OnUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("EspaceId");

                    b.ToTable("Evenements");
                });

            modelBuilder.Entity("TicketPlace2._0.Models.EvenementTypePlaceModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Emplacements")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("EvenementId")
                        .HasColumnType("integer");

                    b.Property<int>("NombreDePlaces")
                        .HasColumnType("integer");

                    b.Property<DateTime>("OnCreate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("OnUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Prix")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int>("TypePlaceId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EvenementId");

                    b.HasIndex("TypePlaceId");

                    b.ToTable("EvenementTypePlaces");
                });

            modelBuilder.Entity("TicketPlace2._0.Models.PlaceVendueModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("EvenementId")
                        .HasColumnType("integer");

                    b.Property<int>("NumeroDePlace")
                        .HasColumnType("integer");

                    b.Property<DateTime>("OnCreate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("OnUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Prix")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int>("TypePlaceId")
                        .HasColumnType("integer");

                    b.Property<string>("TypeReservation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UtilisateurId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EvenementId");

                    b.HasIndex("TypePlaceId");

                    b.HasIndex("UtilisateurId");

                    b.ToTable("PlaceVendues");
                });

            modelBuilder.Entity("TicketPlace2._0.Models.TypePlaceModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("TypePlaces");
                });

            modelBuilder.Entity("ticketplace.Models.UtilisateurModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateDeNaissance")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<bool>("EstAdmin")
                        .HasColumnType("boolean");

                    b.Property<string>("MotDePasse")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("OnCreate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("OnUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("Utilisateurs");
                });

            modelBuilder.Entity("TicketPlace2._0.Models.EvenementModel", b =>
                {
                    b.HasOne("TicketPlace2._0.Models.EspaceModel", "Espace")
                        .WithMany()
                        .HasForeignKey("EspaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Espace");
                });

            modelBuilder.Entity("TicketPlace2._0.Models.EvenementTypePlaceModel", b =>
                {
                    b.HasOne("TicketPlace2._0.Models.EvenementModel", "Evenement")
                        .WithMany()
                        .HasForeignKey("EvenementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketPlace2._0.Models.TypePlaceModel", "TypePlace")
                        .WithMany()
                        .HasForeignKey("TypePlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Evenement");

                    b.Navigation("TypePlace");
                });

            modelBuilder.Entity("TicketPlace2._0.Models.PlaceVendueModel", b =>
                {
                    b.HasOne("TicketPlace2._0.Models.EvenementModel", "Evenement")
                        .WithMany()
                        .HasForeignKey("EvenementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketPlace2._0.Models.TypePlaceModel", "TypePlace")
                        .WithMany()
                        .HasForeignKey("TypePlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ticketplace.Models.UtilisateurModel", "Utilisateur")
                        .WithMany()
                        .HasForeignKey("UtilisateurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Evenement");

                    b.Navigation("TypePlace");

                    b.Navigation("Utilisateur");
                });
#pragma warning restore 612, 618
        }
    }
}
