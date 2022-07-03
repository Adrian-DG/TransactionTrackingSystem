using Domain.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

using Domain.Enums;

namespace API.Controllers
{
	[Authorize]
	public class GenericController<T> : BaseController where T : ModelMetadata
	{
		private IUnitOfWork<T> _uow;
		private IGenericRepository<T> _repository;

		protected ISpecification<T> _spec;
		protected Expression<Func<T, bool>> _predicate;
		protected string _searchTerm;
		protected bool _status;
		public GenericController(IUnitOfWork<T> uow, ISpecification<T> specification)
		{
			_uow = uow;
			_repository = _uow.Repository;
			_spec = specification;

			_searchTerm = "";
			_status = true;
			_predicate = _spec.GetFilterPredicate(x => x.Status == _status); // Default predicate
		}

		[HttpGet]
		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> GetAllAsync([FromQuery] PaginationFilters filters)
		{
			try
			{
				_searchTerm = (filters.SearchTerm is null) ? "" : filters.SearchTerm;
				_status = filters.Status;

				PagedData<T> result = await _repository.GetAllAsync(filters, _predicate);
				return new JsonResult(result);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetByIdAsync([FromRoute] object id)
		{
			try
			{
				var result = await _repository.GetByIdAsync(id);
				return new JsonResult(result);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpPost]
		public async Task<IActionResult> InsertAsync([FromBody] T model)
		{
			try
			{
				model.Created = DateTime.Now;
				model.Modified = DateTime.Now;
				model.Status = true;
				
				await _repository.InsertAsync(model);
				return Ok(await _uow.CommintChangesAsync());
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpPut]
		public async Task<IActionResult> UpdateAsync([FromBody] T model)
		{
			try
			{
				model.Modified = DateTime.Now;

				_repository.UpdateAsync(model);
				return Ok(await _uow.CommintChangesAsync());
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> DeleteAsync([FromRoute] object id)
		{
			try
			{
				await _repository.DeleteAsync(id);
				return Ok(await _uow.CommintChangesAsync());
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

	}
}
