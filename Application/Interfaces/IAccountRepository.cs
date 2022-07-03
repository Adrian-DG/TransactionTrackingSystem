using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.DTO;
using Domain.Models;

namespace Application.Interfaces
{
	public interface IAccountRepository
	{
		Task<PagedData<Account>> GetCustomerAccountsAsync(PaginationFilters filters, Guid userId);
		Task DeleteCustomerAccountAsync(Guid accountId);
	}
}
