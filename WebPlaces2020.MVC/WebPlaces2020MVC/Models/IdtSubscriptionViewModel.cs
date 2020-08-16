using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebPlaces2020.CLI.Models
{
    public class IdtSubscriptionViewModel
    {
        [Required]
        [Display(Name = "Adresse email")]
        [EmailAddress]
        public string Email { get; set;  }
        [Required]
        [Display(Name = "Prénom")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Nom de famille")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }




    }
}
