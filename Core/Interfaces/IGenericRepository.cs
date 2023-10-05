using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
	public interface IGenericRepository<Type> where Type : BaseEntity
	{
		Task<Type> GetByIdAsync(int id);
		Task<IReadOnlyList<Type>> ListAllAsync();
		Task<Type> GetEntityWithSpec(ISpecification<Type> spec);
		Task<IReadOnlyList<Type>> ListAsync(ISpecification<Type> spec);
		Task<int> CountAsync(ISpecification<Type> spec);
	}
}