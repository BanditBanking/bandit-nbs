using Bandit.NBS.NpgsqlRepository.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace Bandit.NBS.NpgsqlRepository
{
    public class NpgsqlDbContext : DbContext
    {
        public NpgsqlDbContext(DbContextOptions<NpgsqlDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>().HasKey(t => t.Id);
        }

        public DbSet<Transaction> Transactions { get; set; } = null!;
    }
}
