using Application.Domain.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Application.Data.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public new DbSet<T> Set<T>() where T : Entity
        {
            return base.Set<T>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseNpgsql("Host=omv.serveblog.net;Port=15432;Pooling=true;Database=Jogatina;User Id=devboost;Password=21293811;");
    }
}
