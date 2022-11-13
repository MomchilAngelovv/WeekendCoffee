using Microsoft.AspNetCore.Mvc;
using WeekendCoffee.Api.Models;
using WeekendCoffee.Services;

namespace WeekendCoffee.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AttendancesController : ControllerBase
	{
		private readonly IAttendancesService attendancesService;
		private readonly IMeetingsService meetingsService;
		private readonly IMembersService membersService;

		public AttendancesController(
			IAttendancesService attendancesService,
			IMeetingsService meetingsService,
			IMembersService membersService)
		{
			this.attendancesService = attendancesService;
			this.meetingsService = meetingsService;
			this.membersService = membersService;
		}

		[HttpPost]
		public async Task<IActionResult> SignUpForMeeting(SignUpForMeetingRequestModel requestModel)
		{
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

			var member = await this.membersService.GetOneAsync(requestModel.MemberId);
			if (member is null)
			{

			}

			var attendanceInfo = this.attendancesService.GetAttendanceInfoAsync(currentMeeting, member);
			if (attendanceInfo is not null)
			{

			}

			var attendance = await this.attendancesService.SignUpForMeetingAsync("Coming", "", currentMeeting, member);
			return this.Ok(attendance);
		}
	}
}
