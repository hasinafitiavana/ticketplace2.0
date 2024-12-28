using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketPlace2._0.Models
{
    public class EvenementModel
    {
        public int Id { get; set; }

        [Required]
        public int EspaceId { get; set; }

        [ForeignKey("EspaceId")]
        public EspaceModel? Espace { get; set; }

        [Required]
        [StringLength(255)]
        public string Nom { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan Heure { get; set; }

        [Required]
        [StringLength(255)]
        public string Lieu { get; set; } = string.Empty;

        public DateTime OnCreate { get; set; }

        public DateTime OnUpdate { get; set; }
    }
}