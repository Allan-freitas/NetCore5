using Application.Domain.Models.Users;
using System.Threading.Tasks;

namespace Application.Data.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get; }

        void Commit();

        Task CommitAsync();

        void RejectChanges();
    }
}
