using Domain.DTO;
using Domain.Abstractions;

namespace Application.Interfaces
{
    public interface IUnitOfWork<T> where T : ModelMetadata
    {
        Task<ServerResponse> CommintChangesAsync();
        IAuthRespository AuthRepository { get; }
        IGenericRepository<T> Repository { get; }
        IAccountRepository AccountRepository { get; }
        ITransactionsRepository TransactionsRepository { get; }
    }
}