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


   
    }
}
