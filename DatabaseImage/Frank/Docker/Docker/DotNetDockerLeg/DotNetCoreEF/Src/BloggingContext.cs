using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EFCoreConsole
{
    public class BloggingContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlite("Data Source=blogging.db");
            optionsBuilder.UseSqlServer("Server=db;Database=Blogging;User Id=SA;Password=Your_password123;");

        }
    }
}
