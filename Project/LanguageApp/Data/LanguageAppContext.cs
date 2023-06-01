using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LanguageApp.Models;

namespace LanguageApp.Data
{
    public class LanguageAppContext : DbContext
    {
        public LanguageAppContext (DbContextOptions<LanguageAppContext> options)
            : base(options)
        {
        }

        public DbSet<LanguageApp.Models.Quiz> Quiz { get; set; } = default!;

        public DbSet<LanguageApp.Models.Word> Word { get; set; } = default!;

        public DbSet<LanguageApp.Models.Statistics> Statistics { get; set; } = default!;

        public DbSet<LanguageApp.Models.User> User { get; set; } = default!;

        public DbSet<LanguageApp.Models.Question> Question { get; set; } = default!;
    }
}
