namespace WeekendCoffee.Api.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	public class WelcomeController : BaseController
	{
		public WelcomeController(
			)
		{

		}

		[HttpGet]
		public IActionResult Welcome()
		{
			var responseData = "Welcome";
			return this.SuccessResponse(responseData);
		}
	}
}
