using Application.Interfaces;
using Domain.DTO;
using Infrastructure.Repositories;
using Domain.Abstractions;

namespace Infrastructure.Data
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : ModelMetadata
    {
        private MainContext _context;
        private ServerResponse serverResponse;
        public UnitOfWork(MainContext context)
        {
            _context = context;
            serverResponse = new ServerResponse();
            AuthRepository = new AuthRepository(_context);
            Repository = new GenericRepository<T>(_context);
            AccountRepository = new AccountRepository(_context);
            TransactionsRepository = new TransactionsRepository(_context);
        }

        public IAuthRespository AuthRepository { get; }

		public IGenericRepository<T> Repository { get; }

		public IAccountRepository AccountRepository { get; }

        public ITransactionsRepository TransactionsRepository { get; }

        public async Task<ServerResponse> CommintChangesAsync()
		{
            return await _context.SaveChangesAsync() > 0 ? serverResponse.GetResponse(true) : serverResponse.GetResponse(false); 
		}

		

    }
}