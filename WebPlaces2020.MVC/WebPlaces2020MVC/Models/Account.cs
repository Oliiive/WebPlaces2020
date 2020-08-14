using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPlaces2020.Client.Models
{
    public class Account
    {
        public long Id { get; set; }

        public string LastName_ACC { get; set; } // Max 50 az10 - required

        public string FirstName_ACC { get; set; } // Max 50 az10 - required

        public string Email_ACC { get; set; } //Max 255 idem  - valide unique required

        public string Phone_ACC { get; set; } // Max 25 et mobile - Validé par LibPhoneNumber

        public string Gender_ACC { get; set; }  // Male Female Nonbinary Required

        public DateTime BirthDate_ACC { get; set; } // Valide dd/mm/yyyy required - DatePicker

        public bool Pro_ACC { get; set; } // required

    }
}
