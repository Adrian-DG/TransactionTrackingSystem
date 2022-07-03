using Domain.DTO;

namespace Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<PagedData<T>> GetAllAsync(PaginationFilters filters);
        Task<T> GetByIdAsync(object id);
        Task InsertAsync(T entity);
        void UpdateAsync(T entity);
        Task DeleteAsync(object id);
    }
}