namespace WeekendCoffee.Services
{
	using System.Globalization;

	using WeekendCoffee.Data;
	using WeekendCoffee.Common;
	using Microsoft.EntityFrameworkCore;

	public interface IMeetingsService
	{
		Task<Meeting> GetCurrentAsync();
		Task<Meeting> GetUpcomingAsync();
		Task<Meeting> InsertOneAsync(DateTime occursOnDate);
	}

	public class MeetingsService : IMeetingsService
	{
		private readonly WeekendCoffeeDbContext db;
		private readonly ISettingsService settingsService;

		public MeetingsService(
			WeekendCoffeeDbContext db,
			ISettingsService settingsService)
		{
			this.db = db;
			this.settingsService = settingsService;
		}

		public async Task<Meeting> InsertOneAsync(DateTime occursOnDate)
		{
			var meetingSettings = await settingsService.GetMeetingTimeAsync();

			if (meetingSettings is null || meetingSettings.Count != 2)
			{
				return null;
			}

			var hoursValue = int.Parse(meetingSettings.First(m => m.Key == GlobalConstants.HoursSettingKey).Value);
			var minutesValue = int.Parse(meetingSettings.First(m => m.Key == GlobalConstants.MinutesSettingKey).Value);

			var occursOn = new DateTime(occursOnDate.Year, occursOnDate.Month, occursOnDate.Day, hoursValue, minutesValue, 0);
			var label = $"{occursOn.Hour}:{occursOn.Minute}_{occursOn.DayOfWeek}_{occursOn.Day}_{occursOn.ToString("MMMM", CultureInfo.InvariantCulture)}_{occursOn.Year}";

			var newMeeting = new Meeting
			{
				Label = label,
				OccursOn = occursOn,
			};

			await this.db.Meetings.AddAsync(newMeeting);
			await this.db.SaveChangesAsync();

			return newMeeting;
		}

		public async Task<Meeting> GetCurrentAsync()
		{
			var currentMeeting = await this.db.Meetings
				.Where(m => DateTime.UtcNow >= m.OccursOn)
				.OrderByDescending(m => m.OccursOn)
				.FirstOrDefaultAsync();

			return currentMeeting;
		}

		public async Task<Meeting> GetUpcomingAsync()
		{
			var upcomingMeeting = await this.db.Meetings
				.Where(m => DateTime.UtcNow >= m.OccursOn)
				.OrderByDescending(m => m.OccursOn)
				.Skip(1)
				.FirstOrDefaultAsync();

			return upcomingMeeting;
		}
	}
}	
