using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class AccountsController : GenericController<Account>
	{
		public AccountsController(IUnitOfWork<Account> uow) : base(uow)
		{
		}
	}
}
