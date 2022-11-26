namespace WeekendCoffee.Data
{
	public class Attendance : IEntityMetaData
	{
		public Attendance()
		{
			this.Id = Guid.NewGuid().ToString("N");
			this.CreatedOn = DateTime.UtcNow;
		}

		public string Id { get; set; }
		public string? Comment { get; set; }

		public string MeetingId { get; set; }
		public int MemberId { get; set; }

		public virtual Meeting Meeting { get; set; }
		public virtual Member Member { get; set; }

		public DateTime CreatedOn { get; set ; }
		public DateTime? UpdatedOn { get; set; }
		public DateTime? DeletedOn { get; set; }
		public string? MetaData { get; set; }
	}
}
