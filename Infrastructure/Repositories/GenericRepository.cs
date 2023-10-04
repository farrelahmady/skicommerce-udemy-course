using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly StoreContext context;

		public GenericRepository(StoreContext context)
		{
			this.context = context;
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await this.context.Set<T>().FindAsync(id);
		}

		public async Task<IReadOnlyList<T>> ListAllAsync()
		{
			return await this.context.Set<T>().ToListAsync();
		}
	}
}