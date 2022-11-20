namespace WeekendCoffee.Services
{
	using System.Globalization;

	using Microsoft.EntityFrameworkCore;
	using WeekendCoffee.Common;
	using WeekendCoffee.Data;

	public interface IMeetingsService
	{
		string GenerateLabelAsync(bool upcoming);
		Task<Meeting> GetCurrentAsync();
		Task<Meeting> GetUpcomingAsync();
		//Task<Meeting> GetByLabelAsync(string label);
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

		public async Task<Meeting> GetByLabelAsync(string label)
		{
			return await this.db.Meetings
				.Include(m => m.Attendances)
					.ThenInclude(a => a.Member)
				.SingleOrDefaultAsync(m => m.Label == label);
		}

		public async Task<Meeting> GetUpcomingAsync()
		{
			var upcomingMeeting = await this.db.Meetings
				.Where(m => DateTime.UtcNow < m.OccursOn)
				.OrderByDescending(m => m.OccursOn)
				.FirstOrDefaultAsync();

			return upcomingMeeting;
		}

		public string GenerateLabelAsync(bool upcoming)
		{
			var currentSaturdayDate = DateTime.UtcNow;
			while (currentSaturdayDate.DayOfWeek != DayOfWeek.Saturday)
			{
				currentSaturdayDate = currentSaturdayDate.AddDays(1);
			}

			if (upcoming)
			{
				currentSaturdayDate = currentSaturdayDate.AddDays(7);
			}

			var label = $"{currentSaturdayDate.Hour}:{currentSaturdayDate.Minute}_{currentSaturdayDate.DayOfWeek}_{currentSaturdayDate.Day}_{currentSaturdayDate.ToString("MMMM", CultureInfo.InvariantCulture)}_{currentSaturdayDate.Year}";
			return label;
		}
	}
}	
