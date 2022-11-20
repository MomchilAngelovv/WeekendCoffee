namespace WeekendCoffee.Api.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	using WeekendCoffee.Common;
	using WeekendCoffee.Api.Models;

	[ApiController]
	[Route("[controller]")]
	public class BaseController : ControllerBase
	{
		protected IActionResult SuccessResponse<T>(T data)
		{
			var response = new ControllerResponse<T>
			{
				Status = GlobalConstants.Success,
				ErrorMessage = GlobalConstants.NotAvailable,
				Data = data
			};

			return this.Ok(response);
		}

		protected IActionResult ErrorResponse(string errorMessage)
		{
			var response = new ControllerResponse<object>
			{
				Status = GlobalConstants.Error,
				ErrorMessage = errorMessage,
			};

			return this.Ok(response);
		}
	}
}
