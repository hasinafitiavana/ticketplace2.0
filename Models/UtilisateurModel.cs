using System;
using System.ComponentModel.DataAnnotations;

namespace ticketplace.Models
{
    public class UtilisateurModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nom { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Prenom { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string MotDePasse { get; set; } = string.Empty;

        [Required]
        public DateTime DateDeNaissance { get; set; }

        public DateTime OnCreate { get; set; } = DateTime.UtcNow;

        public DateTime OnUpdate { get; set; } = DateTime.UtcNow;
    }
}