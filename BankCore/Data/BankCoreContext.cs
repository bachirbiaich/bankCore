using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BankCore.Models;

namespace BankCore.Models
{
    public class BankCoreContext : DbContext
    {
        public BankCoreContext (DbContextOptions<BankCoreContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(x => x.email).IsUnique();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Compte> Comptes { get; set; }
        public DbSet<Mouvement> Mouvements { get; set; }
        public DbSet<Virement> Virements { get; set; }

    }
}
