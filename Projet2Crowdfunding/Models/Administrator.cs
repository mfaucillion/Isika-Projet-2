using System;
using System.ComponentModel.DataAnnotations;

namespace Projet2Crowdfunding.Models
{
    public enum AdministratorType { employee, volunteer}

    public class Administrator 
    {
        public int Id { get; set; }     

        [MaxLength(45)]
        [Required]
        [Display(Name = "Nom")]
        public string LastName { get; set; }

        [MaxLength(45)]
        [Required]
        [Display(Name = "Prenom")]
        public string FirstName { get; set; }


        [MaxLength(15)]
        [Required]
        [Display(Name = "Numéro de téléphone")]
        public string PhoneNumber { get; set; }

        [Required]
        public AdministratorType? Type { get; set; }

        [Required]
        public int? AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
