using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace API.Controllers
{
	// ?? This class is used for testing error handling
	public class BuggyController : BaseApiController
	{
		private readonly StoreContext context;

		public BuggyController(StoreContext context)
		{
			this.context = context;
		}

		[HttpGet("notfound")]
		public ActionResult GetNotFound()
		{
			var thing = this.context.Products.Find(42);

			if (thing == null)
			{
				return NotFound(new ApiResponse(404));
			}
			return Ok();
		}

		[HttpGet("servererror")]
		public ActionResult GetServerError()
		{
			var thing = this.context.Products.Find(42);

			var thingToReturn = thing.ToString();

			return Ok();
		}

		[HttpGet("badrequest")]
		public ActionResult GetBadRequest()
		{
			return BadRequest(new ApiResponse(400));
		}

		[HttpGet("badrequest/{id}")]
		public ActionResult GetBadRequest(int id)
		{
			return Ok();
		}
	}
}