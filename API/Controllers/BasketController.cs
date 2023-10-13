using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class BasketController : BaseApiController
	{
		public IBasketRepository BasketRepository { get; }
		public BasketController(IBasketRepository basketRepository)
		{
			this.BasketRepository = basketRepository;
		}

		[HttpGet]
		public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
		{
			var basket = await this.BasketRepository.GetBasketAsync(id);

			return Ok(basket ?? new CustomerBasket(id));
		}

		[HttpPost]
		public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
		{
			var updatedBasket = await this.BasketRepository.UpdateBasketAsync(basket);

			return Ok(updatedBasket);
		}

		[HttpDelete]
		public async Task DeleteBasket(string id)
		{
			await this.BasketRepository.DeleteBasketAsync(id);
		}
	}
}