using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly StoreContext context;

		public ProductRepository(StoreContext context)
		{
			this.context = context;
		}

		public async Task<IReadOnlyList<Product>> GetProductsAsync()
		{
			return await this.context.Products.ToListAsync();
		}

		public async Task<Product> GetProductByIdAsync(int id)
		{
			return await this.context.Products.FindAsync(id);
		}

		public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
		{
			return await this.context.ProductBrands.ToListAsync();
		}

		public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
		{
			return await this.context.ProductTypes.ToListAsync();
		}
	}
}