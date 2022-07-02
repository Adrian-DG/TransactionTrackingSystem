using Application.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private MainContext _context;
        public GenericRepository(MainContext context)
        {
            _context = context;
        }
    }
}