using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductController : ControllerBase
	{
		private readonly IGenericRepository<Product> productRepo;
		private readonly IGenericRepository<ProductBrand> productBrandRepo;
		private readonly IGenericRepository<ProductType> productTypeRepo;

		public ProductController(IGenericRepository<Product> productRepo, IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTypeRepo)
		{
			this.productRepo = productRepo;
			this.productBrandRepo = productBrandRepo;
			this.productTypeRepo = productTypeRepo;
		}

		[HttpGet]
		public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
		{
			var spec = new ProductWithTypesAndBrandsSpecification();

			var products = await this.productRepo.ListAsync(spec);

			return products.Select(product => new ProductToReturnDto(product)).ToList();
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
		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{
			var spec = new ProductWithTypesAndBrandsSpecification(id);

			var product = await this.productRepo.GetEntityWithSpec(spec);
			return new ProductToReturnDto(product);
		}
	}
}