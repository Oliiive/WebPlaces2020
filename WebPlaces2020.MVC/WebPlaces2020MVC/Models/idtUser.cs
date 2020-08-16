using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebPlaces2020.CLI.Models
{
    public class IdtUser : IdentityUser<int>   
    {
        public string IdtUFirstName { get; set; }
        public string IdtULastName { get; set; }

        public string IdtUEmail { get; set; }
}
}
