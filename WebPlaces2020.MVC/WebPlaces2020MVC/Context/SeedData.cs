using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPlaces2020.Client.Models;
using WebPlaces2020.Client.Context;
using Microsoft.EntityFrameworkCore;

namespace WebPlaces2020.Client.Context
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PlaceContext (
                serviceProvider.GetRequiredService<DbContextOptions<PlaceContext>>()))
            {
                //Look for any places
                if (context.PlaceItems.Any())
                { return; // DB Seeded
                }

                context.PlaceItems.AddRange(
                    new Place
                    {

                    },
                    new Place
                    {

                    },
                    new Place
                    {

                    }




                );
                context.SaveChanges();


            }

        }


    }
}
