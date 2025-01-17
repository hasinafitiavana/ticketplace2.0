using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TicketPlace2._0.Models
{
    public class EvenementTypePlaceModel
    {
        public int Id { get; set; }

        [Required]
        public int EvenementId { get; set; }

        [ForeignKey("EvenementId")]
        public EvenementModel? Evenement { get; set; }

        [Required]
        public int TypePlaceId { get; set; }

        [ForeignKey("TypePlaceId")]
        public TypePlaceModel? TypePlace { get; set; }

        [Required]
        public int NombreDePlaces { get; set; }

        [Required]
        public string Emplacements { get; set; } = string.Empty; //atao json ito du genre {VIP: [1, 2, 3]}

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Prix { get; set; }

        public DateTime OnCreate { get; set; } = DateTime.UtcNow;

        public DateTime OnUpdate { get; set; } = DateTime.UtcNow;
    }
}