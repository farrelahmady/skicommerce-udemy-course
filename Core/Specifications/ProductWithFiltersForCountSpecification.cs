using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
	public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
	{
		public ProductWithFiltersForCountSpecification(ProductSpecParams param)
			: base(p =>
					(string.IsNullOrEmpty(param.Search) || p.Name.ToLower().Contains(param.Search)) &&
					(!param.BrandId.HasValue || p.ProductBrandId == param.BrandId) &&
					(!param.TypeId.HasValue || p.ProductTypeId == param.TypeId)
			)
		{
		}
	}
}