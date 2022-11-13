namespace WeekendCoffee.Api.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	using WeekendCoffee.Services;
	using WeekendCoffee.Api.Models.Responses;

	[ApiController]
	[Route("[controller]")]
	public class MeetingsController : ControllerBase
	{
		private readonly IMeetingsService meetingsService;

		public MeetingsController(
			IMeetingsService meetingsService)
		{
			this.meetingsService = meetingsService;
		}

		[HttpGet]
		public async Task<IActionResult> GetCurrentMeetingInformation()
		{
			var response = new GetCurrentMeetingInformationResponse();
			var currentMeetingLabel = this.meetingsService.GenerateLabelAsync(false);
			var currentMeeting = await meetingsService.GetByLabelAsync(currentMeetingLabel);
			if (currentMeeting is null)
			{
				var currentSaturdayDate = DateTime.UtcNow;
				while (currentSaturdayDate.DayOfWeek != DayOfWeek.Saturday)
				{
					currentSaturdayDate = currentSaturdayDate.AddDays(1);
				}

				currentMeeting = await this.meetingsService.CreateAsync(currentSaturdayDate);
			}

			var upcomingMeetingLabel = this.meetingsService.GenerateLabelAsync(true);
			var upcomingMeeting = await meetingsService.GetByLabelAsync(upcomingMeetingLabel);
			if (upcomingMeeting is null)
			{
				var upcomingMeetingDate = DateTime.UtcNow.AddDays(7);
				while (upcomingMeetingDate.DayOfWeek != DayOfWeek.Saturday)
				{
					upcomingMeetingDate = upcomingMeetingDate.AddDays(1);
				}

				await this.meetingsService.CreateAsync(upcomingMeetingDate);
			}

			response.Label = currentMeeting.Label;

			if (currentMeeting.Attendances is not null)
			{
				response.Members = currentMeeting.Attendances.Select(a => a.Member.NickName).ToList();
			}

			return this.Ok(response);
		}
	}
}
