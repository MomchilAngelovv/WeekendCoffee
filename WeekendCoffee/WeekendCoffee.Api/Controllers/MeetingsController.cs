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

			var upcomingMeeting = await meetingsService.GetUpcomingAsync();
			if (upcomingMeeting is null)
			{
				var upcomingMeetingDate = DateTime.UtcNow.AddDays(7);
				while (upcomingMeetingDate.DayOfWeek != DayOfWeek.Saturday)
				{
					upcomingMeetingDate = upcomingMeetingDate.AddDays(1);
				}

				upcomingMeeting = await this.meetingsService.InsertOneAsync(upcomingMeetingDate);

				if (upcomingMeeting is null)
				{
					return this.ErrorResponse(GlobalErrorMessages.CannotCreateMeeting);
				}
			}

			var responseData = new MeetingInformationResponse
			{
				Label = upcomingMeeting.Label,
			};

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
