namespace WeekendCoffee.Data
{
	public class Meeting : IEntityMetaData
	{
		public Meeting()
		{
			this.Id = Guid.NewGuid().ToString("N");
			this.CreatedOn = DateTime.UtcNow;
		}

		public string Id { get; set; }
		public string Label { get; set; }
		public DateTime OccursOn { get; set; }

		public virtual ICollection<Attendance> Attendances { get; set; } = new HashSet<Attendance>();

		public DateTime CreatedOn { get; set; }
		public DateTime? UpdatedOn { get; set; }
		public DateTime? DeletedOn { get; set; }
		public string? MetaData { get; set; }
	}
}
