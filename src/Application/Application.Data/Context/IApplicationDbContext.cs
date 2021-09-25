using Application.Domain.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Application.Data.Context
{
    public interface IApplicationDbContext
    {
        DbSet<T> Set<T>() where T : Entity;

        int SaveChanges();

        Task<int> SaveChangesAsync();

        void Dispose();
    }
}
