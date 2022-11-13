namespace WeekendCoffee.Services
{
	using System.Globalization;

	using Microsoft.EntityFrameworkCore;

	using WeekendCoffee.Data;

	public interface IMeetingsService
	{
		string GenerateLabelAsync(bool upcoming);
		Task<Meeting> GetByLabelAsync(string label);
		Task<Meeting> CreateAsync(DateTime occursOn);
	}

	public class MeetingsService : IMeetingsService
	{
		private readonly WeekendCoffeeDbContext db;

		public MeetingsService(
			WeekendCoffeeDbContext db)
		{
			this.db = db;
		}

		public async Task<Meeting> CreateAsync(DateTime occursOn)
		{
			var label = $"{occursOn.ToString("MMMM", CultureInfo.InvariantCulture)}_{occursOn.Day}_{occursOn.Year}";
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
			return await this.db.Meetings.SingleOrDefaultAsync(l => l.Label == label);
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

			var label = $"{currentSaturdayDate.ToString("MMMM", CultureInfo.InvariantCulture)}_{currentSaturdayDate.Day}_{currentSaturdayDate.Year}";
			return label;
		}
	}
}
