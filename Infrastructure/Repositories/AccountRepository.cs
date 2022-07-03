using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Interfaces;
using Domain.DTO;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
	public class AccountRepository : IAccountRepository
	{
		private MainContext _context;
		private DbSet<Account> _accounts;
		public AccountRepository(MainContext context)
		{
			_context = context;
			_accounts = _context.Accounts;
		}

		public async Task DeleteCustomerAccountAsync(Guid accountId)
		{
			var account = await _accounts.FindAsync(accountId);
			_accounts.Remove(account);
		}

		public async Task<PagedData<Account>> GetCustomerAccountsAsync(PaginationFilters filters, Guid userId)
		{
			var results = await _accounts
							.Skip<Account>((filters.Page - 1) * filters.Size)
							.Take<Account>(filters.Size)
							.Where<Account>(x => x.UserId == userId && x.Status == filters.Status)
							.ToListAsync<Account>();

			return new PagedData<Account>
			{
				Page = filters.Page,
				Size = filters.Size,
				Items = results,
				TotalCount = results.Count
			};
		}
	}
}
