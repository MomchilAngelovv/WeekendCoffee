namespace WeekendCoffee.Data
{
	public class Attendance
	{
		public Attendance()
		{
			this.Id = Guid.NewGuid().ToString("N");
		}

		public string Id { get; set; }
		public string State { get; set; }
		public string Reason { get; set; }
		public DateTime SignedOn { get; set; }

		public string MeetingId { get; set; }
		public int MemberId { get; set; }

		public virtual Meeting Meeting { get; set; }
		public virtual Member Member { get; set; }
	}
}
