using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restorann.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorann.Data.DAL
{
    public class AppDbContext : IdentityDbContext 
    {
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<AppUser> Users { get; set; } 
        public AppDbContext(DbContextOptions options) : base(options) 
        {

        }
    }
}
