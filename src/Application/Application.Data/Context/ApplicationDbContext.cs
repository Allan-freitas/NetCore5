using Application.Domain.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading.Tasks;

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
            optionsBuilder.UseNpgsql("");
    }
}
