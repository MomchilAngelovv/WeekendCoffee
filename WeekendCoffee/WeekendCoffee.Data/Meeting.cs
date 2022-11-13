namespace WeekendCoffee.Data
{
	public class Meeting
	{
		public Meeting()
		{
			this.Id = Guid.NewGuid().ToString("N");
		}

		public string Id { get; set; }
		public string Label { get; set; }
		public DateTime OccursOn { get; set; }

		public virtual ICollection<Attendance> Attendances { get; set; }
	}
}
