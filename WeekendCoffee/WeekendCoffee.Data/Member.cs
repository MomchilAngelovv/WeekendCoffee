namespace WeekendCoffee.Data
{
	public class Member : IEntityMetaData
	{
		public Member()
		{
			this.CreatedOn = DateTime.UtcNow;
		}

		public int Id { get; set; }
		public string FirstName { get; set; }
		public string? MiddleName { get; set; }
		public string LastName { get; set; }
		public string NickName { get; set; }
		public string? PhoneNumber { get; set; }

		public virtual ICollection<Attendance> Attendances { get; set; }

		public DateTime CreatedOn { get; set; }
		public DateTime? UpdatedOn { get; set; }
		public DateTime? DeletedOn { get; set; }
		public string? MetaData { get; set; }
	}
}
