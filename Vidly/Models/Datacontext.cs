using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Vidly.Models
{
    public class Datacontext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Movie> Movie { get; set; }

        public DbSet<MemberShipType> MemberShipTypes { get; set; }

        public DbSet<Genre> Genres { get; set; }
    }
}