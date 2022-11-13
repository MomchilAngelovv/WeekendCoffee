﻿namespace WeekendCoffee.Api.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	using WeekendCoffee.Services;
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

		[HttpPost]
		public async Task<IActionResult> SignUpForMeeting(SignUpForMeetingRequestModel requestModel)
		{
			var response = new SignUpForMeetingResponse();

			var currentMeetingLabel = this.meetingsService.GenerateLabelAsync(false);
			var currentMeeting = await meetingsService.GetByLabelAsync(currentMeetingLabel);
			if (currentMeeting is null)
			{
				response.Status = "Failed";
				response.Message = $"Cannot find meeting with label: {currentMeetingLabel}";
				return this.Ok(response);
			}

			var member = await this.membersService.GetOneAsync(requestModel.MemberId);
			if (member is null)
			{
				response.Status = "Failed";
				response.Message = $"Cannot find member with Id: {requestModel.MemberId}";
				return this.Ok(response);
			}

			var attendanceInfo = await this.attendancesService.GetAttendanceInfoAsync(currentMeeting, member);
			if (attendanceInfo is not null)
			{
				response.Status = "Failed";
				response.Message = $"Member with Id: {requestModel.MemberId} has already signed for meeting: {currentMeeting.Label}";
				return this.Ok(response);
			}

			var attendance = await this.attendancesService.SignUpForMeetingAsync("Coming", "", currentMeeting, member);

			response.Status = "Succeed";
			response.Message = $"Member with Id: {attendance.MemberId} successfully signed for meeting: {currentMeeting.Label}";
			response.MemberId = member.Id;
			response.MeetingLabel = currentMeeting.Label;

			return this.Ok(response);
		}
	}
}
