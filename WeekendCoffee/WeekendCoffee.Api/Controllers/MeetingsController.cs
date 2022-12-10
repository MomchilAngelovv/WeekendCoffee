namespace WeekendCoffee.Api.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	using WeekendCoffee.Common;
	using WeekendCoffee.Services;

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

			var responseData = new 
			{
				upcomingMeeting.Label,
				Members = currentMeeting.Attendances.Select(a => a.Member.NickName).ToList()
			};

			return this.SuccessResponse(responseData);
		}
	}
}
