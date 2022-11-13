namespace WeekendCoffee.Services
{
	using Microsoft.EntityFrameworkCore;

	using WeekendCoffee.Data;

	public interface IAttendancesService
	{
		Task<Attendance> SignUpForMeetingAsync(string state, string reason, Meeting meeting, Member member);
		Task<Attendance> GetAttendanceInfoAsync(Meeting meeting, Member member);
	}

	public class AttendancesService : IAttendancesService
	{
		private readonly WeekendCoffeeDbContext db;

		public AttendancesService(
			WeekendCoffeeDbContext db)
		{
			this.db = db;
		}

		public async Task<Attendance> GetAttendanceInfoAsync(Meeting meeting, Member member)
		{
			return await this.db.Attendances.SingleOrDefaultAsync(a => a.MeetingId == meeting.Id && a.MemberId == member.Id);
		}

		public async Task<Attendance> SignUpForMeetingAsync(string state, string reason, Meeting meeting, Member member)
		{
			var newAttendance = new Attendance
			{
				State = state,
				Reason = reason,
				SignedOn = DateTime.UtcNow,
				Meeting = meeting,
				Member = member
			};

			await this.db.Attendances.AddAsync(newAttendance);
			await this.db.SaveChangesAsync();

			return newAttendance;
		}
	}
}
