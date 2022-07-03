using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class AccountsController : GenericController<Account>
	{

		private IUnitOfWork<Account> unitOfWork;
		private IAccountRepository accountRepository;
		public AccountsController(IUnitOfWork<Account> uow, ISpecification<Account> specification) : base(uow, specification)
		{
			unitOfWork = uow;
			accountRepository = unitOfWork.AccountRepository;
		}

		[HttpGet("{userId}/all")]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> GetCustomerAccountsAsync([FromQuery] PaginationFilters filters, [FromRoute] string userId)
		{
			try
			{
				var results = await accountRepository.GetCustomerAccountsAsync(filters, Guid.Parse(userId));
				return new JsonResult(results);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpDelete("{accountId}")]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> DeleteCustomerAccountAsync([FromRoute] string accountId)
		{
			try
			{
				await accountRepository.DeleteCustomerAccountAsync(Guid.Parse(accountId));
				return Ok(await unitOfWork.CommintChangesAsync());
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

	}
}
