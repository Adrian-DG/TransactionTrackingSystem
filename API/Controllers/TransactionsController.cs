using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class TransactionsController : GenericController<Transaction>
	{
		public TransactionsController(IUnitOfWork<Transaction> uow) : base(uow)
		{
		}
	}
}
