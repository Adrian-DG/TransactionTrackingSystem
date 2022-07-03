using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
	public class PaginationFilters
	{
		public int Page { get; set; }
		public int Size { get; set; }
		public string SearchTerm { get; set; }
		public bool Status { get; set; }
	}
}
