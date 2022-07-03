using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using Domain.DTO;
using Domain.Models;

namespace Application.Interfaces
{
	public interface ITransactionsRepository
	{
		Task<PagedData<Transaction>> GetCustomerTransactionsAsync(PaginationFilters filters, Guid accountId);
		Task DeleteCustomerTransaction(Guid transactionId);
	}
}
