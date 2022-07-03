namespace Application.Interfaces
{
    public interface IUnitOfWork
    {
        IAuthRespository AuthRepository { get; }
        IGenericRepository<T> Repository<T>() where T : class;
    }
}