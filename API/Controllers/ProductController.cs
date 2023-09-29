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
				public async Task<ActionResult<List<Product>>> GetProducts() {
					return Ok(await this.repo.GetProductsAsync());
				}

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			return await this.repo.GetProductByIdAsync(id);
		}
	}
}