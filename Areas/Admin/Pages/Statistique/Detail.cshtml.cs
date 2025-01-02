using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketPlace2._0.Models;

namespace TicketPlace2._0.Areas.Admin.Pages.Statistique
{
    public class Detail : PageModel
    {
         private readonly ticketplace.Data.ApplicationDbContext _context;

        public Detail(ticketplace.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IList<PlaceVendueModel> PlaceVendueModel { get; set; } = default!;
        public Dictionary<string, int> SalesStatistics { get; set; } = new();
        public Dictionary<string, int> PriceStatistics { get; set; } = new();

        public async Task OnGetAsync(int evenementId)
        {
            // Récupérer les données de ventes pour l'événement spécifié
            PlaceVendueModel = await _context.PlaceVendues
                .Include(p => p.Evenement)
                .Include(p => p.TypePlace)
                .Where(p => p.EvenementId == evenementId && p.TypeReservation == "ACHETER")
                .ToListAsync();
            Console.WriteLine("**************************************** Nombre de ventes récupérées : " + PlaceVendueModel.Count);

            // Calculer les statistiques des ventes
            SalesStatistics = PlaceVendueModel
                .GroupBy(p => p.TypePlace.Type)
                .ToDictionary(g => g.Key, g => g.Count());


            PriceStatistics = PlaceVendueModel
                .GroupBy(p => p.TypePlace.Type)
                .ToDictionary(g => g.Key, g => (int)g.Sum(p => p.Prix));

            // Vérifier les statistiques calculées
            foreach (var stat in SalesStatistics)
            {
                Console.WriteLine($"*****************************Type: {stat.Key}, Count: {stat.Value}");
            }
        }

    }
}