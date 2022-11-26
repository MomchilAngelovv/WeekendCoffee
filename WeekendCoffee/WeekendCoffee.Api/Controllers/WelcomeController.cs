namespace WeekendCoffee.Api.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	public class WelcomeController : BaseController
	{
		[HttpGet]
		public IActionResult Welcome()
		{
			var responseData = "Welcome";
			return this.SuccessResponse(responseData);
		}
	}
}
