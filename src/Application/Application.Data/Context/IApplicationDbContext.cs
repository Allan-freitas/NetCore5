using Application.Domain.Core.Domain;
using Microsoft.EntityFrameworkCore;

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
