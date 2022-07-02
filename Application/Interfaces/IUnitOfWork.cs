namespace Application.Interfaces
{
    public interface IUnitOfWork
    {
        IAuthRespository AuthRepository { get; set; }
        IGenericRepository<T> Repository<T>() where T : class;
    }
}