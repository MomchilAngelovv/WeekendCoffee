namespace WeekendCoffee.Services
{
	using Microsoft.EntityFrameworkCore;

	using WeekendCoffee.Data;

	public interface IAttendancesService
	{
		Task<Attendance> GetOneAsync(string meetingId, int memberId);
		Task<Attendance> SignMemberForMeetingAsync(string meetingId, int memberId, string comment);
	}

	public class AttendancesService : IAttendancesService
	{
		private readonly WeekendCoffeeDbContext db;

		public AttendancesService(
			WeekendCoffeeDbContext db)
		{
			this.db = db;
		}

		public async Task<Attendance> GetOneAsync(string meetingId, int memberId)
		{
			return await this.db.Attendances
				.SingleOrDefaultAsync(a => a.MeetingId == meetingId && a.MemberId == memberId);
		}
		public async Task<Attendance> SignMemberForMeetingAsync(string meetingId, int memberId, string comment)
		{
			var newAttendance = new Attendance
			{
				Comment = comment,
				MeetingId = meetingId,
				MemberId = memberId
			};

			await this.db.Attendances.AddAsync(newAttendance);
			await this.db.SaveChangesAsync();

			return newAttendance;
		}
	}
}
