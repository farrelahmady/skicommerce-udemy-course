using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		public Task<T> GetByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyList<T>> ListAllAsync()
		{
			throw new NotImplementedException();
		}
	}
}