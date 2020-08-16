using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPlaces2020.CLI.Models;
using Microsoft.EntityFrameworkCore;

namespace WebPlaces2020.CLI.Context
{
    public class CompteContext : DbContext
    {

        public CompteContext(DbContextOptions<CompteContext> options) : base(options)
        {

        }

        public DbSet<Compte> AccountItems { get; set; }

    }
}
