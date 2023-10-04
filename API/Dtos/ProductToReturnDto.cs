using Core.Entities;

namespace API.Dtos
{
	public class ProductToReturnDto
	{
		public ProductToReturnDto(Product product)
		{
			this.Id = product.Id;
			this.Name = product.Name;
			this.ProductType = product.ProductType.Name;
			this.ProductBrand = product.ProductBrand.Name;
			this.Price = product.Price;
			this.PictureUrl = product.PictureUrl;
			this.Description = product.Description;
		}

		public int Id { get; }
		public string Name { get; }
		public string ProductType { get; }
		public string ProductBrand { get; }
		public decimal Price { get; }
		public string Description { get; }
		public string PictureUrl { get; }
	}
}