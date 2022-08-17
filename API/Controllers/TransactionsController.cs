using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class TransactionsController : GenericController<Transaction>
	{
		private IUnitOfWork<Transaction> unitOfWork;
		private ITransactionsRepository transactionsRepository;
		public TransactionsController(IUnitOfWork<Transaction> uow, ISpecification<Transaction> specification) : base(uow, specification)
		{
			_predicate = x => x.Concept.Contains(_searchTerm) && x.Status == _status;

			unitOfWork = uow;
			transactionsRepository = unitOfWork.TransactionsRepository;

		}

		[Authorize(Roles = "Customer")]
		[HttpGet("{accountId}/all")]
		public async Task<IActionResult> GetAccountTransactionsAsync([FromQuery] PaginationFilters filters, [FromRoute] string accountId)
		{
			try
			{
				filters.SearchTerm = (filters.SearchTerm is null) ? "" : filters.SearchTerm;
				var results = await transactionsRepository.GetCustomerTransactionsAsync(filters, Guid.Parse(accountId));
				return new JsonResult(results);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[Authorize(Roles = "Customer")]
		[HttpDelete("{transactionId}")]
		public async Task<IActionResult> DeleteCustomerTransaction([FromRoute] string transactionId)
		{
			try
			{
				await transactionsRepository.DeleteCustomerTransaction(Guid.Parse(transactionId));
				return Ok(await unitOfWork.CommintChangesAsync());
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

	}
}
