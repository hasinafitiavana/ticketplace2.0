using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketPlace2._0.Models
{
    public class EspaceModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nom { get; set; } = string.Empty;

        [StringLength(255)]
        public string Adresse { get; set; } = string.Empty;

        [StringLength(255)]
        public string Ville { get; set; } = string.Empty;

        [StringLength(255)]
        public string CodePostal { get; set; } = string.Empty;

        [Required]
        public int Capacite { get; set; }

        public DateTime OnCreate { get; set; } = DateTime.UtcNow;

        public DateTime OnUpdate { get; set; } = DateTime.UtcNow;
    }
}