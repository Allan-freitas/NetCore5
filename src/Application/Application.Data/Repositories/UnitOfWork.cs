using Application.Data.Context;
using Application.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private IRepository<User>? _userRepository;

        public UnitOfWork(IApplicationDbContext capRoverDbContext)
        {
            _applicationDbContext = capRoverDbContext;
        }

        public IRepository<User> UserRepository => _userRepository ??= _userRepository = new EfRepository<User>(_applicationDbContext);

        public void Commit()
        {
            _applicationDbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }

        public void RejectChanges()
        {
            throw new NotImplementedException();
        }
    }
}
