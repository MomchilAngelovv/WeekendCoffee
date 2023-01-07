namespace WeekendCoffee.Services
{
	using Microsoft.EntityFrameworkCore;

	using WeekendCoffee.Data;
	using WeekendCoffee.Common;
	using WeekendCoffee.Services.FilterModels;

	public interface ISettingsService
	{
		//TODO implement filter classes
		Task<Setting> GetOneAsync(int id);
		Task<Setting> GetOneAsync(SettingsFilter filter);
		Task<List<Setting>> GetManyAsync();
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

		public async Task<Setting> GetOneAsync(int id)
		{
			return await this.db.Settings
				.SingleOrDefaultAsync(s => s.Id == id);
		}
		public async Task<Setting> GetOneAsync(SettingsFilter filter)
		{
			var settings = this.db.Settings.AsQueryable();

			if (filter.Id > 0)
			{
				settings = settings.Where(s => s.Id == filter.Id);
			}

			if (!string.IsNullOrWhiteSpace(filter.KeyEquals))
			{
				settings = settings.Where(s => s.Key == filter.KeyEquals);
			}

			return await settings.SingleOrDefaultAsync();
		}
		public async Task<List<Setting>> GetManyAsync()
		{
			return await this.db.Settings
				.ToListAsync();
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
