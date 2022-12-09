namespace WeekendCoffee.Services
{
	using System.Globalization;

	using Microsoft.EntityFrameworkCore;

	using WeekendCoffee.Data;
	using WeekendCoffee.Common;

	public interface IMeetingsService
	{
		Task<Meeting> GetOrCreateCurrentAsync();
		Task<Meeting> GetOrCreateUpcomingAsync();

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

		public async Task<Meeting> GetOrCreateCurrentAsync()
		{
			var currentSaturdayDate = DateTime.UtcNow;
			while (currentSaturdayDate.DayOfWeek != DayOfWeek.Saturday)
			{
				currentSaturdayDate = currentSaturdayDate.AddDays(1);
			}

			var currentMeeting = await this.db.Meetings
				.Include(m => m.Attendances)
					.ThenInclude(a => a.Member)
				.FirstOrDefaultAsync(m => m.OccursOn.Date == currentSaturdayDate.Date);

			if (currentMeeting is null)
			{
				currentMeeting = await this.InsertOneAsync(currentSaturdayDate);
			}

			return currentMeeting;
		}

		public async Task<Meeting> GetOrCreateUpcomingAsync()
		{
			var currentMeeting = await this.GetOrCreateCurrentAsync();
			var upcomingMeetingDate = currentMeeting.OccursOn.Date.AddDays(7);

			var upcomingMeeting = await this.db.Meetings
				.Where(m => m.OccursOn.Date == upcomingMeetingDate)
				.FirstOrDefaultAsync();

			if (upcomingMeeting is null)
			{
				upcomingMeeting = await this.InsertOneAsync(upcomingMeetingDate);
			}

			return upcomingMeeting;
		}
	}
}	
