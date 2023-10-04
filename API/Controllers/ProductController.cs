using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductController : ControllerBase
	{
		private readonly IProductRepository repo;
		public ProductController(IProductRepository repo)
		{
			this.repo = repo;
		}

		[HttpGet]
		public async Task<ActionResult<List<Product>>> GetProducts()
		{
			return Ok(await this.repo.GetProductsAsync());
		}

		[HttpGet("brand")]
		public async Task<ActionResult<List<Product>>> GetProductBrands()
		{
			return Ok(await this.repo.GetProductBrandsAsync());
		}

		[HttpGet("type")]
		public async Task<ActionResult<List<Product>>> GetProductTypes()
		{
			return Ok(await this.repo.GetProductTypesAsync());
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			return await this.repo.GetProductByIdAsync(id);
		}
	}
}