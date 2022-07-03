using Application.Interfaces;
using Infrastructure.Repositories;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private MainContext _context;
        public UnitOfWork(MainContext context)
        {
            _context = context;
            AuthRepository = new AuthRepository(_context);
        }

        public IAuthRespository AuthRepository { get; }
        public IGenericRepository<T> Repository<T> () where T : class 
        {
            return (IGenericRepository<T>) new GenericRepository<T>(_context);
        }

    }
}