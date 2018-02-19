using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NugetForAspMvc.Models
{
    public class DataContext : DbContext
    {
        public DataContext()
                : base("Name=DefaultConnection")
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}