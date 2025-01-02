using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketPlace2._0.service
{
    public class CronJobService : BackgroundService
    {
        
        private readonly ILogger<CronJobService> _logger;
        private readonly ReservationService _reservationService;
        private Timer _timer;

        public CronJobService(ILogger<CronJobService> logger, ReservationService reservationService)
        {
            _logger = logger;
            _reservationService = reservationService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("CronJobService is starting.");

            // Configurer le minuteur pour s'exécuter toutes les 5 minutes
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(60));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("*******************************************CronJobService is working at: {time}", DateTimeOffset.Now);
            // Ajoutez ici le code que vous souhaitez exécuter régulièrement.
            // Par exemple, vous pouvez appeler une méthode pour effectuer une certaine action.
            PerformAction();
        }

        private async void PerformAction()
        {
            // Ajoutez ici votre logique de tâche
            _logger.LogInformation("PerformAction: Action has been performed at: {time}", DateTimeOffset.Now);
            bool result = await _reservationService.DeletePlaceVendueAsync();
            if (result)
            {
                _logger.LogInformation("Places vendues supprimées avec succès.");
            }
            else
            {
                _logger.LogInformation("Aucune place vendue à supprimer.");
            }
            // Exemple d'action : Vous pouvez ajouter des tâches spécifiques ici
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("CronJobService is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _timer?.Dispose();
            base.Dispose();
        }
    }
}