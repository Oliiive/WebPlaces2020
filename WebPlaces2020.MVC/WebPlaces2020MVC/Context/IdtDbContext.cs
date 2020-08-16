using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPlaces2020.CLI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebPlaces2020.CLI.Context
{
    public class IdtDbContext : IdentityDbContext<IdtUser, IdtRole, int>
    {
        public IdtDbContext(DbContextOptions<IdtDbContext> options): base(options)
        {


        }
        
    }
}
