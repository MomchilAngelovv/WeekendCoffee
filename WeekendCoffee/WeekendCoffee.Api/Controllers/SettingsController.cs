namespace WeekendCoffee.Api.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	using WeekendCoffee.Common;
	using WeekendCoffee.Services;
	using WeekendCoffee.Api.Models.Requests;

	public class SettingsController : BaseController
	{
		private readonly ISettingsService settingsService;

		public SettingsController(
			ISettingsService settingsService)
		{
			this.settingsService = settingsService;
		}

		[HttpPost]
		public async Task<IActionResult> InsertSetting(InsertSettingRequest request)
		{
			var setting = await this.settingsService.GetOneAsync(request.Key);
			if (setting is not null)
			{
				return this.ErrorResponse(GlobalErrorMessages.SettingWithKeyAlreadyExists);
			}

			var newSetting = await this.settingsService.InsertOneAsync(request.Key, request.Value);

			var responseData = new
			{
				newSetting.Id
			};

			return this.SuccessResponse(responseData);
		}

		[HttpPut("{key}")]
		public async Task<IActionResult> UpdateSetting(string key, UpdateSettingRequest request)
		{
			var setting = await this.settingsService.GetOneAsync(key);
			if (setting is null)
			{
				return this.ErrorResponse(GlobalErrorMessages.SettingDoesNotExists);
			}

			var updatedSetting = await this.settingsService.UpdateOneAsync(setting, request.NewValue);

			var responseData = new
			{
				updatedSetting.Id,
				updatedSetting.Key,
				updatedSetting.Value
			};

			return this.SuccessResponse(responseData);
		}
	}
}
