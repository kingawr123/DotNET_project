using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project_webapplication.Models;

namespace Project_webapplication.Data
{
    public class LoodspotContext : DbContext
    {
        public LoodspotContext (DbContextOptions<LoodspotContext> options)
            : base(options)
        {
        }

        public DbSet<Project_webapplication.Models.Loodspot> Loodspot { get; set; } = default!;
    }
}
