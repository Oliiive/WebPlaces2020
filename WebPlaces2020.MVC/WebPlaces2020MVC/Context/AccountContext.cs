using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPlaces2020.Client.Models;
using Microsoft.EntityFrameworkCore;

namespace WebPlaces2020.Client.Context
{
    public class AccountContext : DbContext
    {

        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {

        }

        public DbSet<Account> AccountItems { get; set; }

    }
}
