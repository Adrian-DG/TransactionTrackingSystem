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
	public class TransactionsRepository : ITransactionsRepository
	{
		private MainContext _context;
		private DbSet<Transaction> _transactions;
		public TransactionsRepository(MainContext context)
		{
			_context = context;
			_transactions = _context.Transactions;
		}

		public async Task DeleteCustomerTransaction(Guid transactionId)
		{
			var transaction = await _transactions.FindAsync(transactionId);
			_transactions.Remove(transaction);
		}

		public async Task<PagedData<Transaction>> GetCustomerTransactionsAsync(PaginationFilters filters, Guid accountId)
		{
			var results = await _transactions
							.Skip<Transaction>((filters.Page - 1) * filters.Size)
							.Take<Transaction>(filters.Size)
							.Where<Transaction>(x => x.AccountId == accountId && x.Concept.Contains(filters.SearchTerm) && x.Status == filters.Status)
							.ToListAsync<Transaction>();

			return new PagedData<Transaction>
			{
				Page = filters.Page,
				Size = filters.Size,
				Items = results,
				TotalCount = results.Count
			};
		}
	}
}
