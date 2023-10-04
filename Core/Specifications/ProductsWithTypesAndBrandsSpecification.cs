using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
	public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>
	{
		public ProductWithTypesAndBrandsSpecification()
		{
			AddInclude(p => p.ProductType);
			AddInclude(p => p.ProductBrand);
		}
	}
}