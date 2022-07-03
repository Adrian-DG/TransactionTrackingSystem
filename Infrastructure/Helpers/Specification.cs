using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Abstractions;
using Application.Interfaces;
using System.Linq.Expressions;

namespace Infrastructure.Helpers
{
	public class Specification<T> : ISpecification<T> where T : ModelMetadata
	{
		public Expression<Func<T, bool>> GetFilterPredicate(Expression<Func<T, bool>> predicate) => predicate;
	}
}
