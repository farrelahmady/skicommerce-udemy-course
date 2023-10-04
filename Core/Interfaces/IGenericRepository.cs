using Core.Entities;

namespace Core.Interfaces
{
	public interface IGenericRepository<Type> where Type : BaseEntity
	{
		Task<Type> GetByIdAsync(int id);
		Task<IReadOnlyList<Type>> ListAllAsync();
	}
}