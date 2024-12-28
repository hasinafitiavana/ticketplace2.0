using System;
using System.ComponentModel.DataAnnotations;

namespace TicketPlace2._0.Models
{
    public class TypePlaceModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; } = string.Empty;
    }
}