namespace WeekendCoffee.Data
{
	public interface IEntityMetaData
	{
		public DateTime CreatedOn { get; set; }
		public DateTime? UpdatedOn { get; set; }
		public DateTime? DeletedOn { get; set; }
		public string? MetaData { get; set; }
	}
}
