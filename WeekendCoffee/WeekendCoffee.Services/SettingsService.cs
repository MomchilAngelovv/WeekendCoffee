namespace WeekendCoffee.Services
{
	using Microsoft.EntityFrameworkCore;

	using WeekendCoffee.Data;
	using WeekendCoffee.Common;

	public interface ISettingsService
	{
		Task<List<Setting>> GetMeetingTimeAsync();
		Task<Setting> InsertOneAsync(string key, string value);
	}

	public class SettingsService : ISettingsService
	{
		private readonly WeekendCoffeeDbContext db;

		public SettingsService(
			WeekendCoffeeDbContext db)
		{
			this.db = db;
		}

		public async Task<List<Setting>> GetMeetingTimeAsync()
		{
			return await this.db.Settings
				.Where(s => s.Key == GlobalConstants.HoursSettingKey || s.Key == GlobalConstants.MinutesSettingKey)
				.ToListAsync();
		}

		public async Task<Setting> InsertOneAsync(string key, string value)
		{
			var setting = new Setting
			{
				Key = key,
				Value = value
			};

			await this.db.Settings.AddAsync(setting);
			await this.db.SaveChangesAsync();

			return setting;
		}
	}
}
