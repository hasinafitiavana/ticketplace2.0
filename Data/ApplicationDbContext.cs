using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ticketplace.Models;
using TicketPlace2._0.Models;

namespace ticketplace.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UtilisateurModel> Utilisateurs { get; set; }
        public DbSet<TypePlaceModel> TypePlaces { get; set; }
        public DbSet<EspaceModel> Espaces { get; set; }
        public DbSet<EvenementModel> Evenements { get; set; }
        public DbSet<EvenementTypePlaceModel> EvenementTypePlaces { get; set; }
        public DbSet<PlaceVendueModel> PlaceVendues { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
         public override int SaveChanges()
        {
            ConvertDatesToUtc();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ConvertDatesToUtc();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ConvertDatesToUtc()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                var properties = entry.Properties
                    .Where(p => p.Metadata.ClrType == typeof(DateTime) || p.Metadata.ClrType == typeof(DateTime?));

                foreach (var property in properties)
                {
                    if (property.CurrentValue is DateTime dateTime && dateTime.Kind == DateTimeKind.Unspecified)
                    {
                        property.CurrentValue = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
                    }
                }
            }
        }
    }
}