namespace WeekendCoffee.Api.Controllers
{
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Mvc;

	using WeekendCoffee.Common;
	using WeekendCoffee.Services;
	using WeekendCoffee.Api.Models;
	using WeekendCoffee.Api.Models.Requests;
	using WeekendCoffee.Api.Models.Responses;

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
		public async Task<IActionResult> SignMemberForMeeting(SignMemberForMeetingRequest request)
		{
			var response = new ControllerResponse<SignUpForMeetingResponse>();

			var currentMeeting = await meetingsService.GetCurrentAsync();
			if (currentMeeting is null)
			{
				response.Status = GlobalConstants.Error;
				response.ErrorMessage = GlobalErrorMessages.CannotFindMeeting;
				return this.Ok(response);
			}

			var member = await this.membersService.GetOneAsync(request.MemberId);
			if (member is null)
			{
				response.Status = GlobalConstants.Error;
				response.ErrorMessage = GlobalErrorMessages.CannotFindMember;
				return this.Ok(response);
			}

			var attendanceInfo = await this.attendancesService.GetOneAsync(currentMeeting.Id, member.Id);
			if (attendanceInfo is not null)
			{
				response.Status = GlobalConstants.Error;
				response.ErrorMessage = GlobalErrorMessages.MemberAlreadySignedForMeeting;
				return this.Ok(response);
			}

			var attendance = await this.attendancesService.SignMemberForMeetingAsync(currentMeeting.Id, member.Id, request.Comment);

			response.Status = GlobalConstants.Success;
			response.ErrorMessage = GlobalConstants.NotAvailable;
			response.Data = new SignUpForMeetingResponse
			{
				AttendanceId = attendance.Id,
			};

			return this.Ok(response);
		}
	}
}
