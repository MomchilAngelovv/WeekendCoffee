namespace WeekendCoffee.Services
{
	using Microsoft.EntityFrameworkCore;

	using WeekendCoffee.Data;
	using WeekendCoffee.Common;

	public interface ISettingsService
	{
		Task<Setting> GetOneAsync(string key);
		Task<List<Setting>> GetMeetingTimeAsync();
		Task<Setting> InsertOneAsync(string key, string value);
		Task<Setting> UpdateOneAsync(Setting setting, string newValue);
	}

	public class SettingsService : ISettingsService
	{
		private readonly WeekendCoffeeDbContext db;

		public SettingsService(
			WeekendCoffeeDbContext db)
		{
			this.db = db;
		}

		public async Task<Setting> GetOneAsync(string key)
		{
			return await this.db.Settings
				.SingleOrDefaultAsync(s => s.Key == key);
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
		public async Task<Setting> UpdateOneAsync(Setting setting, string newValue)
		{
			setting.Value = newValue;
			setting.UpdatedOn = DateTime.UtcNow;

			await this.db.SaveChangesAsync();

			return setting;
		}
	}
}
