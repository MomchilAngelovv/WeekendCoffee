namespace WeekendCoffee.Data
{
	public class Setting : IEntityMetaData
	{
		public Setting()
		{
			this.CreatedOn = DateTime.UtcNow;
		}

		public int Id { get; set; }
		public string Key { get; set; }
		public string Value { get; set; }

		public DateTime CreatedOn { get; set; }
		public DateTime? UpdatedOn { get; set; }
		public DateTime? DeletedOn { get; set; }
		public string? MetaData { get; set; }
	}
}
