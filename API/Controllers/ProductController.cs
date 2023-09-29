using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
				public String GetProducts() {
					return "api/product : This will be list of Products";
				}

				[HttpGet("{id}")]
				public String GetProduct(int id){
					return "api/product/" + id + " : This will be product with id " + id;
				}
    }
}