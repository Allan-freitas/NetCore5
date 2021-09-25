using Application.Domain.Models.Users;

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
