namespace WeekendCoffee.Api.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	using WeekendCoffee.Common;
	using WeekendCoffee.Services;
	using WeekendCoffee.Api.Models.Responses;

	public class MeetingsController : BaseController
	{
		private readonly IMeetingsService meetingsService;

		public MeetingsController(
			IMeetingsService meetingsService)
		{
			this.meetingsService = meetingsService;
		}

		[HttpGet("current")]
		public async Task<IActionResult> GetCurrentMeetingInformation()
		{
			var currentMeeting = await meetingsService.GetOrCreateCurrentAsync();
			if (currentMeeting is null)
			{
				return this.ErrorResponse(GlobalErrorMessages.CannotCreateMeeting);
			}

			var upcomingMeeting = await meetingsService.GetOrCreateUpcomingAsync();
			if (upcomingMeeting is null)
			{
				//TODO Error message I dont like
				return this.ErrorResponse(GlobalErrorMessages.CannotCreateMeeting);
			}

			var responseData = new MeetingInformationResponse
			{
				Label = upcomingMeeting.Label,
			};

			//TODO This IF is not needed. I can remove it when removing the response models
			if (currentMeeting.Attendances is not null)
			{
				responseData.Members = currentMeeting.Attendances
					.Select(a => a.Member.NickName)
					.ToList();
			}

			return this.SuccessResponse(responseData);
		}
	}
}
