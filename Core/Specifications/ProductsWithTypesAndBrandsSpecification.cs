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
		public ProductWithTypesAndBrandsSpecification(string sort)
		{
			AddInclude(p => p.ProductType);
			AddInclude(p => p.ProductBrand);

			if (!string.IsNullOrEmpty(sort))
			{
				switch (sort)
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