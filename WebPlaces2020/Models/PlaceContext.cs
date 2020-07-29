using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace WebPlaces2020.Models
{
    public class PlaceContext : DbContext
    {

        public PlaceContext(DbContextOptions<PlaceContext> options) : base(options)
        {


        }

        public DbSet<Place> Place { get; set; }
    }
}
