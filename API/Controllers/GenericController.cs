using Domain.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Authorize]
	public class GenericController<T> : BaseController where T : ModelMetadata
	{
		private IUnitOfWork<T> _uow;
		private IGenericRepository<T> _repository;
		public GenericController(IUnitOfWork<T> uow)
		{
			_uow = uow;
			_repository = _uow.Repository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllAsync([FromQuery] PaginationFilters filters)
		{
			try
			{
				PagedData<T> result = await _repository.GetAllAsync(filters);
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
				_repository.UpdateAsync(model);
				return Ok(await _uow.CommintChangesAsync());
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpDelete("{id}")]
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
