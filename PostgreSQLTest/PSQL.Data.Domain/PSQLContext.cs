
using System;

using Microsoft.EntityFrameworkCore;

using PSQL.Data.Domain.Models;

namespace PSQL.Data.Domain
{
    public class PSQLContext: DbContext
    {
        public PSQLContext() : base() { }
        public PSQLContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //var connectionString = "Server=127.0.0.1; port=5432; user id = testuser; password = 12345; database= Chat; pooling = true;"; // test db with 225M entries
                var connectionString = "Server=127.0.0.1; port=5432; user id = testuser; password = 12345; database= ChatTest2; pooling = true;"; // test db with 100K entries
                optionsBuilder.UseNpgsql(connectionString, opt => {
                    opt.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
                    opt.UseFuzzyStringMatch();
                    opt.UseTrigrams();
                    });
                //optionsBuilder.UseNpgsql(connectionString);s
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasPostgresExtension("fuzzystrmatch");
            modelBuilder.HasPostgresExtension("pg_trgm");

            modelBuilder.Entity<Message>()
                .HasGeneratedTsVectorColumn(
                    p => p.SearchVector,
                    "english",  // Text search config
                    p => new { p.Text })  // Included properties
                .HasIndex(p => p.SearchVector)
                .HasMethod("GIN"); // Index method on the search vector (GIN or GIST)

            modelBuilder.Entity<Message>()
                .HasIndex(p => p.Text)
                .HasMethod("GIN")
                .HasOperators("gin_trgm_ops");
        }
    }

}
