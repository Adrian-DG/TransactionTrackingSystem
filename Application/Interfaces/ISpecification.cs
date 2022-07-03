using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Domain.Abstractions;

namespace Application.Interfaces
{
	public interface ISpecification<T> where T : ModelMetadata 
	{
		Expression<Func<T, bool>> GetFilterPredicate(Expression<Func<T, bool>> predicate);
	}
}
