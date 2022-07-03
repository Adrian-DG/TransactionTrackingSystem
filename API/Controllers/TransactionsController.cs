using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class TransactionsController : GenericController<Transaction>
	{
		public TransactionsController(IUnitOfWork<Transaction> uow, ISpecification<Transaction> specification) : base(uow, specification)
		{
			_predicate = x => x.Concept.Contains(_searchTerm) && x.Status == _status;
		}
	}
}
