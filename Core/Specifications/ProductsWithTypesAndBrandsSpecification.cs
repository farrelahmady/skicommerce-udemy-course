using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
	public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>
	{
		public ProductWithTypesAndBrandsSpecification(ProductSpecParams param)
			: base(p =>
					(!string.IsNullOrEmpty(param.Search) || p.Name.ToLower().Contains(param.Search)) &&
					(!param.BrandId.HasValue || p.ProductBrandId == param.BrandId) &&
					(!param.TypeId.HasValue || p.ProductTypeId == param.TypeId)
			)
		{
			AddInclude(p => p.ProductType);
			AddInclude(p => p.ProductBrand);
			AddOrderBy(p => p.Name);
			ApplyPaging(param.PageSize * (param.PageIndex - 1), param.PageSize);


			if (!string.IsNullOrEmpty(param.Sort))
			{
				switch (param.Sort)
				{
					case "priceAsc":
						AddOrderBy(p => p.Price);
						break;

					case "priceDesc":
						AddOrderByDescending(p => p.Price);
						break;

					default:
						AddOrderBy(p => p.Name);
						break;
				}
			}
		}

		public ProductWithTypesAndBrandsSpecification(int id) : base(p => p.Id == id)
		{
			AddInclude(p => p.ProductType);
			AddInclude(p => p.ProductBrand);
		}
	}
}