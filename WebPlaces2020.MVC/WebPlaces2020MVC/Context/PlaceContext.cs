using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPlaces2020.CLI.Models;

namespace WebPlaces2020.CLI.Context
{
    public class PlaceContext : DbContext
    {

        public PlaceContext(DbContextOptions<PlaceContext> options): base(options)
        {
            
        }

        public DbSet<Place> PlaceItems { get; set; }
    }
}
