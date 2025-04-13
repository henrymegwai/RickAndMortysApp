using Microsoft.EntityFrameworkCore;
using RickAndMorty.Data.Entities;
using RickAndMorty.Persistence.Configurations;

namespace RickAndMorty.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public virtual DbSet<Character> Characters { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new CharacterConfiguration());
        }
    }
}