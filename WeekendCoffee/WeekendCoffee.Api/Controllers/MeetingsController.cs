namespace WeekendCoffee.Api.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	using WeekendCoffee.Services;
	using WeekendCoffee.Api.Models.Responses;
	using WeekendCoffee.Api.Models;
	using WeekendCoffee.Common;

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

		[HttpGet("current")]
		public async Task<IActionResult> GetCurrentMeetingInformation()
		{
			var response = new ControllerResponse<MeetingInformationResponse>();

			//var currentMeetingLabel = this.meetingsService.GenerateLabelAsync(false);
			var currentMeeting = await meetingsService.GetCurrentAsync();
			if (currentMeeting is null)
			{
				var currentSaturdayDate = DateTime.UtcNow;
				while (currentSaturdayDate.DayOfWeek != DayOfWeek.Saturday)
				{
					currentSaturdayDate = currentSaturdayDate.AddDays(1);
				}

				currentMeeting = await this.meetingsService.InsertOneAsync(currentSaturdayDate);

				if (currentMeeting is null)
				{
					response.Status = GlobalConstants.Error;
					response.ErrorMessage = GlobalErrorMessages.CannotCreateMeeting;
					return this.Ok(response);
				}
			}

			//var upcomingMeetingLabel = this.meetingsService.GenerateLabelAsync(true);
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
					response.Status = GlobalConstants.Error;
					response.ErrorMessage = GlobalErrorMessages.CannotCreateMeeting;
					return this.Ok(response);
				}
			}

			//TODO Fix warnings
			response.Status = GlobalConstants.Success;
			response.ErrorMessage = GlobalConstants.NotAvailable;
			response.Data = new MeetingInformationResponse
			{
				Label = upcomingMeeting.Label,
			};

			if (currentMeeting.Attendances is not null)
			{
				response.Data.Members = currentMeeting.Attendances
					.Select(a => a.Member.NickName)
					.ToList();
			}

			return this.Ok(response);
		}
	}
}
