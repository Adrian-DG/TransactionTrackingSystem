using Application.Interfaces;
using Domain.DTO;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private MainContext _context;
		private DbSet<T> _repo;
		public GenericRepository(MainContext mainContext)
		{
			_context = mainContext;
			_repo = _context.Set<T>();
		}

		public async Task DeleteAsync(object id)
		{
			var entity = await GetByIdAsync(id);
			_repo.Remove(entity);
		}

		public async Task<PagedData<T>> GetAllAsync(PaginationFilters filters, Expression<Func<T, bool>> predicateWhere)
		{
			var results = await _repo
							.Skip<T>((filters.Page - 1) * filters.Size)
							.Take<T>(filters.Size)
							.Where<T>(predicateWhere)					
							.ToListAsync<T>();

			return new PagedData<T>
			{
				Page = filters.Page,
				Size = filters.Size,
				Items = results,
				TotalCount = results.Count
			};
		}				

		public async Task<T> GetByIdAsync(object id)
		{
			return await _repo.FindAsync(id);
		}

		public async Task InsertAsync(T entity)
		{
			await _repo.AddAsync(entity);
		}

		public void UpdateAsync(T entity)
		{
			_context.Attach<T>(entity);
			_context.Entry<T>(entity).State = EntityState.Modified;
		}
	}
}