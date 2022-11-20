namespace WeekendCoffee.Api.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	using WeekendCoffee.Services;
	using WeekendCoffee.Api.Models.Requests;

	[ApiController]
	[Route("[controller]")]
	public class SettingsController : ControllerBase
	{
		private readonly ISettingsService settingsService;

		public SettingsController(
			ISettingsService settingsService)
		{
			this.settingsService = settingsService;
		}

		[HttpPost]
		public async Task<IActionResult> InsertSetting(InsertSettingRequestModel requestModel)
		{
			var setting = await this.settingsService.InsertOneAsync(requestModel.Key, requestModel.Value);
			return this.Ok(setting);
		}
	}
}
