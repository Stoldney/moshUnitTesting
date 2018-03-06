using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking
{
	public interface IUnitOfWork
	{
		IQueryable<T> Query<T>();
	}

	public class UnitOfWork : IUnitOfWork
	{
		public IQueryable<T> Query<T>() => new List<T>().AsQueryable();
	}
}