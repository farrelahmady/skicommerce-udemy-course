using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;
using API.Errors;

namespace API.Controllers
{
	public class ProductController : BaseApiController
	{
		private readonly IGenericRepository<Product> productRepo;
		private readonly IGenericRepository<ProductBrand> productBrandRepo;
		private readonly IGenericRepository<ProductType> productTypeRepo;
		private readonly IMapper mapper;

		public ProductController(IGenericRepository<Product> productRepo, IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTypeRepo, IMapper mapper)
		{
			this.productRepo = productRepo;
			this.productBrandRepo = productBrandRepo;
			this.productTypeRepo = productTypeRepo;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts(string sort, int? brandId, int? typeId)
		{
			var spec = new ProductWithTypesAndBrandsSpecification(sort, brandId, typeId);

			var products = await this.productRepo.ListAsync(spec);

			return Ok(this.mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
		}

		[HttpGet("brand")]
		public async Task<ActionResult<List<Product>>> GetProductBrands()
		{
			return Ok(await this.productBrandRepo.ListAllAsync());
		}

		[HttpGet("type")]
		public async Task<ActionResult<List<Product>>> GetProductTypes()
		{
			return Ok(await this.productTypeRepo.ListAllAsync());
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{
			var spec = new ProductWithTypesAndBrandsSpecification(id);

			var product = await this.productRepo.GetEntityWithSpec(spec);

			if (product == null) return NotFound(new ApiResponse(404, "Product not found"));


			return this.mapper.Map<Product, ProductToReturnDto>(product);
		}
	}
}