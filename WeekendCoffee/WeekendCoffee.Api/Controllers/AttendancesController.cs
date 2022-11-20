namespace WeekendCoffee.Api.Controllers
{
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Mvc;

	using WeekendCoffee.Common;
	using WeekendCoffee.Services;
	using WeekendCoffee.Api.Models.Requests;
	using WeekendCoffee.Api.Models.Responses;

	public class AttendancesController : BaseController
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
			var currentMeeting = await meetingsService.GetCurrentAsync();
			if (currentMeeting is null)
			{
				return this.ErrorResponse(GlobalErrorMessages.CannotFindMeeting);
			}

			var member = await this.membersService.GetOneAsync(request.MemberId);
			if (member is null)
			{
				return this.ErrorResponse(GlobalErrorMessages.CannotFindMember);
			}

			var attendanceInfo = await this.attendancesService.GetOneAsync(currentMeeting.Id, member.Id);
			if (attendanceInfo is not null)
			{
				return this.ErrorResponse(GlobalErrorMessages.MemberAlreadySignedForMeeting);
			}

			var attendance = await this.attendancesService.SignMemberForMeetingAsync(currentMeeting.Id, member.Id, request.Comment);

			var responseData = new SignUpForMeetingResponse
			{
				Id = attendance.Id,
			};

			return this.SuccessResponse(responseData);
		}
	}
}
