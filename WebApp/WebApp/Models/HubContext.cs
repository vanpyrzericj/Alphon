using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class HubContext : DbContext
    {
        //TODO: Update this to your local or production server
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseMySql(@"Server=albatross.jm-corp.net;database=buhavore_buh;uid=buhavore_buh;pwd=GlCKGv_Pve3[;");

        //public HubContext(DbContextOptions<HubContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
    }
}